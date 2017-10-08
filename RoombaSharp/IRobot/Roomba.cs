/*
 MIT License

Copyright (c) [2016] [Orlin Dimitrov]

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial SerialPortions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;

using iRobot.Data;
using iRobot.Events;
using iRobot.Communicators;
using iRobot.Queues;
using System.Collections.Generic;

namespace iRobot
{

    /// <summary>
    /// This API class is implemented by this documentation:
    /// http://irobot.lv/uploaded_files/File/iRobot_Roomba_500_Open_Interface_Spec.pdf
    /// </summary>
    public class Roomba
    {

        #region Variables

        /// <summary>
        /// Communicator
        /// </summary>
        private ICommunicationAddapter communicator;

        /// <summary>
        /// Command queue.
        /// </summary>
        private CommandQueue commandQueue = new CommandQueue();
        
        #endregion

        #region Properties

        /// <summary>
        /// Is connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (this.communicator == null) return false;

                return this.communicator.IsConnected;
            }
        }
        
        /// <summary>
        /// Update time.
        /// </summary>
        public int UpdateTime
        {
            get
            {
                return this.commandQueue.QueueDelay;
            }

            set
            {
                this.commandQueue.QueueDelay = value;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Received command message.
        /// </summary>
        public event EventHandler<BytesEventArgs> OnMesage;

        /// <summary>
        /// On connect event.
        /// </summary>
        public event EventHandler<EventArgs> OnConnect;

        /// <summary>
        /// On disconnect event.
        /// </summary>
        public event EventHandler<EventArgs> OnDisconnect;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="portName">Serial port name.</param>
        public Roomba (ICommunicationAddapter communicator)
        {
            this.communicator = communicator;

            this.UpdateTime = 100;
            this.commandQueue.QueueHandler = commandQueue_QueueHandler;
        }

        #endregion

        #region API

        /// <summary>
        /// Connect to the robot.
        /// </summary>
        public void Connect()
        {
            if (this.communicator == null) return;
            this.communicator.OnMesage += Communicator_OnMesage;
            this.communicator.OnConnect += Communicator_OnConnect;
            this.communicator.OnDisconnect += Communicator_OnDisconnect;
            this.communicator.Connect();

            this.commandQueue.Start();
        }

        /// <summary>
        /// Disconnect from the robot.
        /// </summary>
        public void Disconnect()
        {
            this.commandQueue.Stop();

            if (this.communicator == null || !communicator.IsConnected) return;
            this.communicator.Disconnect();
            this.communicator.OnMesage -= Communicator_OnMesage;
            this.communicator.OnConnect -= Communicator_OnConnect;
            this.communicator.OnDisconnect -= Communicator_OnDisconnect;
        }

        /// <summary>
        /// Send raw command.
        /// </summary>
        /// <param name="command"></param>
        public void Command(byte[] command)
        {
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command starts the OI.You must always send the Start command before sending any other commands to the OI.
        /// • Serial sequence: [128]. 
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: Passive.Roomba beeps once to acknowledge it is starting from “off” mode.    
        /// </summary>
        /// <see cref="RoombaOpcodes.START"/> 
        public void Start()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.START });
        }

        /// <summary>
        /// This command sets the baud rate in bits per second(bps)
        /// at which OI commands and data are sent according to the baud code sent 
        /// in the data byte. The default baud rate at power up is 115200 bps,
        /// but the starting baud rate can be changed to 19200 by holding down
        /// the Clean button while powering on
        /// Roomba until you hear a sequence of descending tones.
        /// Once the baud rate is changed, it persists until
        /// Roomba is power cycled by pressing the power button or removing the battery, or when the battery
        /// voltage falls below the minimum required for processor operation.You must wait 100ms after sending
        /// this command before sending additional commands at the new baud rate.
        /// • Serial sequence: [129][Baud Code]
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: No Change 
        /// • Baud data byte 1: Baud Code (0 - 11)
        /// /// </summary>
        /// <see cref="RoombaOpcodes.BAUD"/>
        public void Baud(BaudRates baudRate)
        {

            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.BAUD, (byte)baudRate });
        }
        
        /// <summary>
        /// Enables user control of Roomba.This command must be sent
        /// after the start command and before any control commands are
        /// sent to the SCI.The SCI must be in passive mode to accept this 
        /// command.This command puts the SCI in safe mode
        /// </summary>
        /// <see cref="RoombaOpcodes.CONTROL"/>
        public void Control()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.CONTROL });
        }

        /// <summary>
        /// This command puts the OI into Safe mode, enabling user control of Roomba.
        /// It turns off all LEDs.The OI
        /// can be in Passive, Safe, or Full mode to accept this command.
        /// If a safety condition occurs (see above) Roomba reverts automatically to Passive mode. 
        /// • Serial sequence: [131]
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: Safe
        /// </summary>
        /// <see cref="RoombaOpcodes.SAFE"/>
        public void Safe()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.SAFE });
        }

        /// <summary>
        /// This command gives you complete control over Roomba by putting the OI into Full mode,
        /// and turning off the cliff, wheel-drop and internal charger safety feat ures.That is,
        /// in Full mode, Roomba executes any command that you send it,
        /// even if the internal charger is plugged in, or command triggers a cliff or wheel drop condition.
        /// • Serial sequence: [132] 
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: Full 
        /// </summary>
        /// <see cref="RoombaOpcodes.FULL"/>
        public void Full()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.FULL });
        }

        /// <summary>
        /// This command starts the default cleaning mode.
        /// • Serial sequence: [135]
        /// • Available in modes: Passive, Safe, or Full  
        /// • Changes mode to: Passive
        /// </summary>
        /// <see cref="RoombaOpcodes.CLEAN"/>
        public void Clean()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.CLEAN });
        }

        /// <summary>
        /// This command starts the Max cleaning mode.
        /// • Serial sequence: [136] 
        /// • Available in modes: Passive, Safe, or Full  
        /// • Changes mode to: Passive
        /// </summary>
        /// <see cref="RoombaOpcodes.MAX"/>
        public void Max()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.MAX });
        }

        /// <summary>
        /// This command starts the Spot cleaning mode.
        /// • Serial sequence: [134] 
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: Passive
        /// </summary>
        /// <see cref="RoombaOpcodes.SPOT"/>
        public void Spot()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.SPOT });
        }

        /// <summary>
        /// This command sends Roomba to the dock.
        /// • Serial sequence: [143] 
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: Passive
        /// </summary>
        /// <see cref="RoombaOpcodes.DOCK"/>
        public void SeekDock()
        {
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.DOCK });
        }

        /// <summary>
        /// This command sends Roomba a new schedule.To disable scheduled cleaning, send all 0s.
        /// • Serial sequence: [167][Days][Sun Hour][Sun Minute][Mon Hour][Mon Minute][Tue Hour][Tue Minute][Wed Hour][Wed Minute][Thu Hour][Thu Minute][Fri Hour][Fri Minute][Sat Hour][Sat Minute] 
        /// • Available in modes: Passive, Safe, or Full.
        /// • If Roomba’s schedule or clock button is pressed, this command will be ignored. 
        /// • Changes mode to: No change 
        /// • Times are sent in 24 hour format. Hour (0-23) Minute(0-59)
        /// </summary>
        /// <param name="scheduleData">Schedule data.</param>
        /// <see cref="RoombaOpcodes.SCHEDULE"/>
        /// <seealso cref="ScheduleData"/>
        public void Schedule(ScheduleData scheduleData)
        {
            if (scheduleData == null) return;
            if (!scheduleData.IsValid()) return;

            // Build command package.
            byte[] command =
            {
                (byte)RoombaOpcodes.SCHEDULE,
                (byte)scheduleData.Days,
                (byte)scheduleData.Sunday.Hour,
                (byte)scheduleData.Sunday.Minute,
                (byte)scheduleData.Monday.Hour,
                (byte)scheduleData.Monday.Minute,
                (byte)scheduleData.Tuesday.Hour,
                (byte)scheduleData.Tuesday.Minute,
                (byte)scheduleData.Wednesday.Hour,
                (byte)scheduleData.Wednesday.Minute,
                (byte)scheduleData.Thursday.Hour,
                (byte)scheduleData.Thursday.Minute,
                (byte)scheduleData.Friday.Hour,
                (byte)scheduleData.Friday.Minute,
                (byte)scheduleData.Saturday.Hour,
                (byte)scheduleData.Saturday.Minute,
            };

            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command sets Roomba’s clock.
        /// • Serial sequence: [168][Day][Hour][Minute] 
        /// • Available in modes: Passive, Safe, or Full.
        /// • If Roomba’s schedule or clock button is pressed, this command will be ignored. 
        /// • Changes mode to: No change 
        /// • Time is sent in 24 hour format. Hour (0-23) Minute(0-59)
        /// </summary>
        /// <param name="dateTime">Date time object.</param>
        /// <see cref="RoombaOpcodes.SET_DAY_TIME"/>
        /// <seealso cref="DateTime"/>
        public void SetDayTime(DateTime dateTime)
        {
            if (dateTime == null) return;

            this.SetDayTime(dateTime.DayOfWeek, dateTime.Hour, dateTime.Minute);
        }

        /// <summary>
        /// This command sets Roomba’s clock.
        /// • Serial sequence: [168][Day][Hour][Minute] 
        /// • Available in modes: Passive, Safe, or Full.
        /// • If Roomba’s schedule or clock button is pressed, this command will be ignored. 
        /// • Changes mode to: No change 
        /// • Time is sent in 24 hour format. Hour (0-23) Minute(0-59)
        /// </summary>
        /// <param name="day">Day</param>
        /// <param name="hour">Hour [00-23]</param>
        /// <param name="minute">Minute [00-95]</param>
        /// <see cref="RoombaOpcodes.SET_DAY_TIME"/>
        /// <see cref="DayOfWeek"/>
        public void SetDayTime(DayOfWeek day, int hour, int minute)
        {
            if (hour < 0 || hour > 23) return;
            if (minute < 0 || minute > 59) return;

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.SET_DAY_TIME, (byte)day, (byte)hour, (byte)minute };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command powers down Roomba.
        /// The OI can be in Passive, Safe, or Full mode to accept this command.
        /// • Serial sequence: [133]
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: Passive
        /// </summary>
        /// <see cref="RoombaOpcodes.POWER"/>
        public void Power()
        {
            // Send command package.
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.POWER });
        }

        /// <summary>
        /// his command controls Roomba’s drive wheels.It takes four data bytes, interpreted as two 16-bit signed
        /// values using two’s complement.The first two bytes specify the average
        /// velocity of the drive wheels in millimeters per second (mm/s), with the high byte being sent first.The next two bytes specify the radius 
        /// in millimeters at which Roomba will turn.The longer radii make Roomba drive straighter, while the
        /// shorter radii make Roomba turn more.The radius is measured from the center of the turning circle to the
        /// center of Roomba.  A Drive command with a positive velocity and a positive radius makes Roomba drive
        /// forward while turning toward the left.  A negative radius makes Roomba turn toward the right.  Special
        /// cases for the radius make Roomba turn in place or drive straight, as specified below.  A negative velocity makes Roomba drive backward.
        /// NOTE: Internal and environmental restrictions may prevent Roomba from accurately carrying out some drive commands.For example, it may not
        /// be possible for Roomba to drive at full speed in an arc with a large radius of curvature. 
        /// • Serial sequence: [137] [Velocity high byte] [Velocity low byte] [Radius high byte] [Radius low byte]
        /// • Available in modes: Safe or Full
        /// • Changes mode to: No Change 
        /// • Velocity (-500 – 500 mm/s)
        /// • Radius(-2000 – 2000 mm)
        /// Special cases:
        /// Straight = 32768 or 32767 = hex 8000 or 7FFF
        /// Turn in place clockwise = -1
        /// Turn in place counter-clockwise = 1 
        /// </summary>
        /// <param name="velocity">Velocity (-500 – 500 mm/s)</param>
        /// <param name="radius">Radius (-2000 – 2000 mm)</param>
        /// <see cref="RoombaOpcodes.DRIVE"/>
        public void Drive(short velocity, short radius)
        {
            // Convert values to bytes.
            byte[] bVelocity = BitConverter.GetBytes(velocity);
            byte[] bRadius = BitConverter.GetBytes(radius);

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.DRIVE, bVelocity[1], bVelocity[0], bRadius[1], bRadius[0] };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        ///This command lets you control the forward and backward motion of Roomba’s drive wheels
        ///independently.It takes four data bytes, which are interpreted as two 16-bit signed values using two’s
        ///complement.The first two bytes specify the velocity of the right wheel in millimeters per second (mm/s),
        ///with the high byte sent first.The next two bytes specify the velocity of the left wheel, in the same
        ///format.A positive velocity makes that wheel drive forward, while a negative velocity makes it drive
        ///backward.
        /// </summary>
        /// <param name="leftWheel">Left wheel velocity (-500 – 500 mm/s)</param>
        /// <param name="rightWheel">Right wheel velocity (-500 – 500 mm/s)</param>
        /// <see cref="RoombaOpcodes.DRIVE_DIRECT"/>
        public void DirectDrive(short leftWheel, short rightWheel)
        {
            // Convert values to bytes.
            byte[] bLeftWheel = BitConverter.GetBytes(leftWheel);
            byte[] bRightWheel = BitConverter.GetBytes(rightWheel);

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.DRIVE_DIRECT, bRightWheel[1], bRightWheel[0], bLeftWheel[1], bLeftWheel[0] };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command lets you control the raw forward and backward motion of Roomba’s drive wheels
        /// independently.It takes four data bytes, which are interpreted as two 16-bit signed values using two’s complement.
        /// The first two bytes specify the PWM of the right wheel, with the high byte sent first.The
        /// next two bytes specify the PWM of the left wheel, in the same format.A positive PWM makes that wheel
        /// drive forward, while a negative PWM makes it drive backward.
        /// • Serial sequence: [146] [Right PWM high byte] [Rigхt PWM low byte] [Left PWM high byte] [Left PWM low byte]
        /// • Available in modes: Safe or Full
        /// • Changes mode to: No Change 
        /// • Right wheel PWM (-255 – 255)
        /// • Left wheel PWM(-255 – 255)
        /// </summary>
        /// <param name="leftPWM">Left wheel PWM (-255 – 255)</param>
        /// <param name="rightPWM">Right wheel PWM (-255 – 255)</param>
        /// <see cref="RoombaOpcodes.DRIVE_PWN"/>
        public void DrivePWM(short leftPWM, short rightPWM)
        {
            // Convert values to bytes.
            byte[] bLeftWheel = BitConverter.GetBytes(leftPWM);
            byte[] bRightWheel = BitConverter.GetBytes(rightPWM);

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.DRIVE_DIRECT, bRightWheel[1], bRightWheel[0], bLeftWheel[1], bLeftWheel[0] };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command lets you control the forward and backward motion of Roomba’s main brush, side brush, and vacuum independently.
        /// Motor velocity cannot be controlled with this command, all motors will run at maximum speed when enabled.
        /// The main brush and side brush can be run in either direction. The vacuum only runs forward.
        /// Serial sequence: [138] [Motors]
        /// • Available in modes: Safe or Full
        /// • Changes mode to: No Change 
        /// • Bits 0-2: 0 = off, 1 = on at 100% pwm duty cycle
        /// • Bits 3 & 4: 0 = motor’s default direction, 1 = motor’s opposite direction.
        /// Default direction for the side brush is counterclockwise.Default direction for the main brush/flapper is inward.
        /// </summary>
        /// <param name="mainBrush">Main Brush</param>
        /// <param name="vacuumSide">Vacuum Side</param>
        /// <param name="sideBrush">Brush</param>
        /// <see cref="RoombaOpcodes.MOTORS"/>
        public void Motors(bool mainBrush, bool vacuum, bool sideBrush)
        {
            byte motors = Convert(new bool[] { false, false, false, false, false, mainBrush, vacuum, sideBrush });

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.MOTORS, motors };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        ///This command lets you control the speed of Roomba’s main brush, side brush, and vacuum
        ///independently.With each data byte, you specify the duty cycle for the low side driver(max 128). For
        ///example, if you want to control a motor with 25% of battery voltage, choose a duty cycle of 128 * 25%  32.
        ///The main brush and side brush can be run in either direction.The vacuum only runs forward.
        ///Positive speeds turn the motor in its default (cleaning) direction.Default direction for the side brush is
        ///counterclockwise.Default direction for the main brush/flapper is inward.
        ///Serial sequence: [144] [Main Brush PWM] [Side Brush PWM] [Vacuum PWM] 
        /// • Available in modes: Safe or Full 
        /// • Changes mode to: No Change 
        /// • Main Brush and Side Brush duty cycle(-127 – 127)
        /// • Vacuum duty cycle(0 – 127)
        /// </summary>
        /// <param name="mainBrushPWM"></param>
        /// <param name="sideBrushPWM"></param>
        /// <param name="vacuumPWM"></param>
        /// <see cref="RoombaOpcodes.PWM_MOTORS"/>
        public void PWMMotors(byte mainBrushPWM, byte sideBrushPWM, byte vacuumPWM)
        {
            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.PWM_MOTORS, mainBrushPWM, sideBrushPWM, vacuumPWM };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command controls the LEDs common to all models of Roomba 500.
        /// The Clean/Power LED is specified by two data bytes:
        /// one for the color and the other for the intensity.
        /// • Serial sequence: [139] [LED Bits] [Clean/Power Color] [Clean/Power Intensity]
        /// • Available in modes: Safe or Full
        /// • Changes mode to: No Change 
        /// • LED Bits (0 – 255) .
        /// </summary>
        /// <param name="spot">Spot</param>
        /// <param name="clean">Clean</param>
        /// <param name="max">Max</param>
        /// <param name="color">0 = green, 255 = red. Intermediate values are intermediate colors (orange, yellow, etc).</param>
        /// <param name="intensity">0 = off, 255 = full intensity. Intermediate values are intermediate intensities.</param>
        /// <see cref="RoombaOpcodes.LEDS"/>
        public void LEDs(bool spot, bool clean, bool max, bool dirtDetect, byte color, byte intensity)
        {
            byte leds = Convert(new bool[] { false, false, false, false, spot, clean, max, dirtDetect });
            
            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.LEDS,  leds, color, intensity };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command controls the state of the scheduling LEDs present on the Roomba 560 and 570.  
        /// • Serial sequence: [162][Weekday LED Bits][Scheduling LED Bits]
        /// • Available in modes: Safe or Full 
        /// • Changes mode to: No Change 
        /// • Weekday LED Bits(0 – 255)
        /// • Scheduling LED Bits(0 – 255)
        /// • All use red LEDs: 0 = off, 1 = on
        /// </summary>
        /// <param name="weekdayLEDBits">Weekday LEDs bits.</param>
        /// <param name="schedulingLEDBits">Scheduling LEDs bits.</param>
        /// <see cref="RoombaOpcodes.SCHEDULING_LEDS"/>
        public void SchedulingLEDs(byte weekdayLEDBits, byte schedulingLEDBits)
        {
            // Build command package.
            byte[] command = new byte[] { (byte)RoombaOpcodes.SCHEDULING_LEDS, weekdayLEDBits, schedulingLEDBits };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command controls the four 7 segment displays on the Roomba 560 and 570. 
        /// • Serial sequence: [163][Digit 3 Bits][Digit 2 Bits][Digit 1 Bits][Digit 0 Bits] 
        /// • Available in modes: Safe or Full 
        /// • Changes mode to: No Change 
        /// • Digit N Bits(0 – 255)
        /// • All use red LEDs: 0 = off, 1 = on.Digits are ordered from left to right on the robot 3,2,1,0. 
        /// </summary>
        /// <param name="d3">Digit 3</param>
        /// <param name="d2">Digit 2</param>
        /// <param name="d1">Digit 1</param>
        /// <param name="d0">Digit 0</param>
        /// <see cref="RoombaOpcodes.DIGIT_LEDs_RAW"/>
        public void DigitLEDsRaw(int d3, int d2, int d1, int d0)
        {
            // Build command package.
            byte[] command = new byte[]
            {
                (byte)RoombaOpcodes.DIGIT_LEDs_RAW,
                SevenSegment(d3),
                SevenSegment(d2),
                SevenSegment(d1),
                SevenSegment(d0)
            };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// Shutdown the LED display.
        /// </summary>
        /// <see cref="Roomba.DigitLEDsRaw(int, int, int, int)"/>
        public void DigitLEDsRawOff()
        {
            // Build command package.
            byte[] command = new byte[] { (byte)RoombaOpcodes.DIGIT_LEDs_RAW, 0, 0, 0, 0 };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command controls the four 7 segment displays on the Roomba 560 and 570 using ASCII character
        /// codes.Because a 7 segment display is not sufficient to display alphabetic characters properly, all
        /// characters are an approximation, and not all ASCII codes are implemented.
        /// • Serial sequence: [164] [Digit 3 ASCII] [Digit 2 ASCII] [Digit 1 ASCII] [Digit 0 ASCII]
        /// • Available in modes: Safe or Full 
        /// • Changes mode to: No Change 
        /// • Digit N ASCII(32 – 126)
        /// • All use red LEDs.Digits are ordered from left to right on the robot 3,2,1,0. 
        /// </summary>
        /// <param name="digit3"></param>
        /// <param name="digit2"></param>
        /// <param name="digit1"></param>
        /// <param name="digit0"></param>
        /// <see cref="RoombaOpcodes.DIGIT_LEDs_ASCII"/>
        public void DigitLEDsASCII(byte digit3, byte digit2, byte digit1, byte digit0)
        {
            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.DIGIT_LEDs_ASCII, digit3, digit2, digit1, digit0 };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command lets you push Roomba’s buttons. The buttons will automatically release after 1/6th of a second. 
        /// • Serial sequence: [165] [Buttons]
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: No Change 
        /// • Buttons (0-255) 1 = Push Button, 0 = Release Button
        /// </summary>
        /// <param name="clock"></param>
        /// <param name="chedule"></param>
        /// <param name="maxday"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="dock"></param>
        /// <param name="spot"></param>
        /// <param name="clean"></param>
        /// <see cref="RoombaOpcodes.BUTTONS"/>
        public void Buttons(bool clock, bool chedule, bool maxday, bool hour, bool minute, bool dock, bool spot, bool clean)
        {
            byte buttons = Convert(new bool[] { clock, chedule, maxday, hour, minute, dock, spot, clean });
            
            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.BUTTONS, buttons, };

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command lets you specify up to four songs to the OI that you can play at a later time.
        /// Each song is associated with a song number.The Play command uses the song number to identify your song selection.
        /// Each song can contain up to sixteen notes. Each note is associated with a note number that uses MIDI note definitions and a duration that is specified in fractions of a second.
        /// The number of data bytes varies, depending on the length of the song specified.
        /// A one note song is specified by four data bytes.For each additional note within a song, add two data bytes.   
        /// • Serial sequence: [140] [Song Number] [Song Length] [Note Number 1] [Note Duration 1] [Note Number 2] [Note Duration 2], etc.
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: No Change 
        /// • Song Number (0 – 4) The song number associated with the specific song.If you send a second Song command, using the same song number, the old song is overwritten.
        /// • Song Length(1 – 16) The length of the song, according to thenumber of musical notes within the song.
        /// • Song data bytes 3, 5, 7, etc.:  Note Number (31 – 127) The pitch of the musical note Roomba will play, according to the MIDI note numbering scheme.The lowest musical note that Roomba will play is Note #31. Roomba considers all musical notes outside the range of 31 – 127 as rest notes, and will make no sound during the duration of those notes.
        /// • Song data bytes 4, 6, 8, etc.:  Note Duration (0 – 255) The duration of a musical note, in increments of 1/64th of a second.Example: a half-second long musical note has a duration value of 32.         
        /// </summary>
        /// <param name="song">The song</param>
        /// <param name="autoPlay">Auto play the song.</param>
        /// <see cref="RoombaOpcodes.SONG"/>
        /// <seealso cref="Data.Song"/>
        /// <seealso cref="Roomba.Play(byte)"/>
        public void Song(Song song, bool autoPlay = false)
        {
            // command package.
            List<byte> command = new List<byte>();

            // Build command package.
            command.Add((byte)RoombaOpcodes.SONG);
            command.Add(song.Number);
            command.Add((byte)(song.Notes.Count));
            foreach(Note note in song.Notes)
            {
                command.Add((byte)note.Tone);
                command.Add(note.Duration);
            }

            // Send command package.
            this.commandQueue.PutToQueue(command.ToArray());

            // Auto play.
            if(autoPlay)
            {
                this.Play(song.Number);
            }
        }

        /// <summary>
        /// This command lets you select a song to play from the songs added to Roomba using the Song command.
        /// You must add one or more songs to Roomba using the Song command in order for the Play command to work.
        /// • Serial sequence: [141] [Song Number] 
        /// • Available in modes: Safe or Full 
        /// • Changes mode to: No Change
        /// • Song Number(0 – 4)
        /// The number of the song Roomba is to play.
        /// </summary>
        /// <param name="songNumber">Song Numbe</param>
        /// <see cref="RoombaOpcodes.PLAY"/>
        public void Play(byte songNumber)
        {
            // Send command package.
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.PLAY, songNumber });
        }

        /// <summary>
        /// This command requests the OI to send a packet of sensor data bytes.
        /// There are 58 different sensor data packets.
        /// Each provides a value of a specific sensor or group of sensors.
        /// For more information on sensor pack ets, refer to the next section,
        /// “Roomba Open Interface Sensors Packets”. 
        /// • Serial sequence: [142] [Packet ID]
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: No Change 
        /// • Packet ID: Identifies which of the 58 sensor data packets should be sent back by the OI.
        /// A value of 100 indicates a packet with all of the sensor data.
        /// Values of 0 through 6 and 101 through 107 indicate specific subgroups of the sensor data.
        /// </summary>
        /// <param name="packageCode">Packet Code</param>
        /// <see cref="RoombaOpcodes.SENSORS"/>
        /// <seealso cref="SensorPacketsIDs"/>
        public void Sensors(SensorPacketsIDs packageCode)
        {
            // Send command package.
            this.commandQueue.PutToQueue(new byte[] { (byte)RoombaOpcodes.SENSORS, (byte)packageCode });
        }

        /// <summary>
        /// This command lets you ask for a list of sensor packets.
        /// The result is returned once, as in the Sensors command.
        /// The robot returns the packets in the order you specify.  
        /// • Serial sequence: [149][Number of Packets][Packet ID 1][Packet ID 2]...[Packet ID N]
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes modes to: No Change
        /// </summary>
        /// <param name="package">Packages IDs</param>
        /// <see cref="RoombaOpcodes.QUERY_LIST"/>
        public void QueryList(byte[] packagesIDs)
        {
            if (packagesIDs.Length > 255) return;

            // Command
            byte[] command = new byte[1 + 1 + packagesIDs.Length];

            // Build command package.
            Buffer.BlockCopy(new byte[] { (byte)RoombaOpcodes.QUERY_LIST }, 0, command, 0,                  1);
            Buffer.BlockCopy(new byte[] { (byte)(packagesIDs.Length)     }, 0, command, 1,                  1);
            Buffer.BlockCopy(packagesIDs                                  , 0, command, 2, packagesIDs.Length);

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        /// <summary>
        /// This command starts a stream of data packets.
        /// The list of packets requested is sent every 15 ms, which is the rate Roomba uses to update data.
        /// This method of requesting sensor data is best if you are controlling Roomba over a wireless network
        /// (which has poor real-time characteristics) with software running on a desktop computer.
        /// • Serial sequence:  [148] [Number of packets] [Packet ID 1] [Packet ID 2] [Packet ID 3] etc.
        /// • Available in modes: Passive, Safe, or Full 
        /// • Changes mode to: No Change
        /// The format of the data returned is: [19][N-bytes][Packet ID 1][Packet 1 data...][Packet ID 2][Packet 2 data...][Checksum]
        /// N-bytes is the number of bytes between the n-bytes byte and the checksum. The checksum is a 1-byte value.
        /// It is the 8-bit complement of all of the bytes between the header and the checksum.That is, if you add all of the bytes after the checksum, and the checksum, the low byte of the result will be 0. 
        /// </summary>
        /// <param name="package">Packages IDs</param>
        /// <see cref="RoombaOpcodes.STREAM"/>
        public void Stream(byte[] packagesIDs)
        {
            if (packagesIDs.Length > 255) return;

            // Command
            byte[] command = new byte[1 + 1 + packagesIDs.Length];

            // Build command package.
            Buffer.BlockCopy(new byte[] { (byte)RoombaOpcodes.STREAM }, 0, command, 0, 1);
            Buffer.BlockCopy(new byte[] { (byte)(packagesIDs.Length) }, 0, command, 1, 1);
            Buffer.BlockCopy(packagesIDs, 0, command, 2, packagesIDs.Length);

            // Send command package.
            this.commandQueue.PutToQueue(command);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Bits to byte.
        /// </summary>
        /// <param name="bits">Array of bits.</param>
        /// <returns>Byte</returns>
        private static byte Convert(bool[] bits)
        {
            byte result = 0;

            for(int index = 0; index < bits.Length; index++)
            {
                if(bits[index])
                {
                    result |= (byte)(1 << (7 - index));
                }
            }

            return result;
        }

        /// <summary>
        /// Seven segment encoder.
        /// </summary>
        /// <param name="number">Number [0 - 15]</param>
        /// <returns>7 segment data.</returns>
        private static byte SevenSegment(int number)
        {
            if (number < 0) number = 0;
            if (number > 15) number = 15;

            byte[] segmentData = new byte[16] { 0x3F/*0*/, 0x06/*1*/, 0x5B/*2*/, 0x4F/*3*/, 0xE6/*4*/, 0x6D/*5*/, 0x7D/*6*/, 0x07/*7*/, 0xFF/*8*/, 0xEF/*9*/, 0x77/*A*/, 0x7C/*b*/, 0x58/*c*/, 0x5E/*d*/, 0x79/*E*/, 0x71/*F*/, };

            return segmentData[number];
        }

        #endregion

        #region Command Queue

        /// <summary>
        /// Command handler.
        /// </summary>
        /// <param name="data"></param>
        private void commandQueue_QueueHandler(object data)
        {
            byte[] command = (byte[])data;

            this.communicator.Write(command, 0, command.Length);
        }

        #endregion

        #region Communicator

        /// <summary>
        /// On message handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Communicator_OnMesage(object sender, BytesEventArgs e)
        {
            this.OnMesage?.Invoke(this, e);
        }

        /// <summary>
        /// On disconnect handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Communicator_OnDisconnect(object sender, EventArgs e)
        {
            this.OnDisconnect?.Invoke(this, e);
        }

        /// <summary>
        /// On connect handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Communicator_OnConnect(object sender, EventArgs e)
        {
            this.OnConnect?.Invoke(this, e);
        }

        #endregion

    }
}
