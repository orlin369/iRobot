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

namespace iRobot
{

    /// <summary>
    /// This API class is implemented by this documentation:
    /// http://www.ecsl.cs.sunysb.edu/mint/Roomba_SCI_Spec_Manual.pdf
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
        /// Starts the SCI.The Start command must be sent before any
        /// other SCI commands.This command puts the SCI in passive
        /// mode.
        /// </summary>
        public void Start()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.START });
        }

        /// <summary>
        /// Sets the baud rate in bits per second(bps) at which SCI
        /// commands and data are sent according to the baud code sent 
        /// in the data byte. The default baud rate at power up is 57600 
        /// bps. (See Serial Port Settings, above.) Once the baud rate is 
        /// changed, it will persist until Roomba is power cycled by removing
        /// the battery(or until the battery voltage falls below the minimum
        /// required for processor operation). You must wait 100ms after
        /// sending this command before sending additional commands
        /// at the new baud rate.The SCI must be in passive, safe, or full
        /// mode to accept this command.This command puts the SCI in 
        /// passive mode.
        /// </summary>
        public void Baud(BaudRates baudRate)
        {

            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.BAUD, (byte)baudRate });
        }
        
        /// <summary>
        /// Enables user control of Roomba.This command must be sent
        /// after the start command and before any control commands are
        /// sent to the SCI.The SCI must be in passive mode to accept this 
        /// command.This command puts the SCI in safe mode
        /// </summary>
        public void Control()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.CONTROL });
        }

        /// <summary>
        /// Starts the SCI. The Start command must be sent before any 
        /// other SCI commands.This command puts the SCI in passive
        /// mode.
        /// </summary>
        /// <param name="spot">Spot</param>
        /// <param name="clean">Clean</param>
        /// <param name="max">Max</param>
        /// <param name="color">0 = green, 255 = red. Intermediate values are intermediate colors (orange, yellow, etc).</param>
        /// <param name="intensity">0 = off, 255 = full intensity. Intermediate values are intermediate intensities.</param>
        public void LEDs(bool spot, bool clean, bool max, bool dirtDetect, byte color, byte intensity)
        {
            byte leds = Convert(new bool[] { false, false, false, false, spot, clean, max, dirtDetect });
            byte[] command = { (byte)RoombaOpcodes.LEDS,  leds, color, intensity };
            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// Schedule the robot.
        /// </summary>
        /// <param name="scheduleData">Schedule data.</param>
        public void Schedule(ScheduleData scheduleData)
        {
            if (scheduleData == null) return;
            if (!scheduleData.IsValid()) return;

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

            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// This command sets Roomba’s clock.
        /// </summary>
        /// <param name="dateTime">Date time object.</param>
        public void SetDayTime(DateTime dateTime)
        {
            if (dateTime == null) return;

            this.SetDayTime(dateTime.DayOfWeek, dateTime.Hour, dateTime.Minute);
        }

        /// <summary>
        /// This command sets Roomba’s clock.
        /// </summary>
        /// <param name="day">Day</param>
        /// <param name="hour">Hour [00-23]</param>
        /// <param name="minute">Minute [00-95]</param>
        public void SetDayTime(DayOfWeek day, int hour, int minute)
        {
            if (hour > 23 || minute > 59) return;

            byte[] command = { (byte)RoombaOpcodes.SET_DAY_TIME, (byte)day, (byte)hour, (byte)minute };
            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// This command puts the SCI in safe mode.The SCI must be in 
        /// full mode to accept this command.
        /// </summary>
        public void Safe()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.SAFE });
        }

        /// <summary>
        /// Enables unrestricted control of Roomba through the SCI and
        /// turns off the safety features.The SCI must be in safe mode to
        /// accept this command. This command puts the SCI in full mode.
        /// </summary>
        public void Full()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.FULL });
        }

        /// <summary>
        /// Puts Roomba to sleep, the same as a normal “power” button
        /// press.The Device Detect line must be held low for 500 ms to
        /// wake up Roomba from sleep.The SCI must be in safe or full
        /// mode to accept this command.This command puts the SCI in 
        /// passive mode.
        /// </summary>
        public void Power()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.POWER });
        }

        /// <summary>
        /// Starts a spot cleaning cycle, the same as a normal “spot” 
        /// button press.The SCI must be in safe or full mode to accept this
        /// command.This command puts the SCI in passive mode
        /// </summary>
        public void Spot()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.SPOT });
        }

        /// <summary>
        /// Starts a normal cleaning cycle, the same as a normal “clean” 
        /// button press.The SCI must be in safe or full mode to accept this
        /// command.This command puts the SCI in passive mode
        /// </summary>
        public void Clean()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.CLEAN });
        }

        /// <summary>
        /// Starts a maximum time cleaning cycle, the same as a normal 
        /// “max” button press.The SCI must be in safe or full mode to
        /// accept this command.This command puts the SCI in passive
        /// mode.
        /// </summary>
        public void Max()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.MAX });
        }

        /// <summary>
        /// Controls Roomba’s cleaning motors.The state of each motor is 
        /// specified by one bit in the data byte. The SCI must be in safe
        /// or full mode to accept this command.This command does not
        /// change the mode.
        /// </summary>
        /// <param name="mainBrush">Main Brush</param>
        /// <param name="vacuumSide">Vacuum Side</param>
        /// <param name="sideBrush">Brush</param>
        public void Motors(bool mainBrush, bool vacuum, bool sideBrush)
        {
            byte motors = Convert(new bool[] { false, false, false, false, false, mainBrush, vacuum, sideBrush });
            byte[] command = { (byte)RoombaOpcodes.MOTORS, motors };

            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// Controls Roomba’s drive wheels.The command takes four data
        /// bytes, which are interpreted as two 16 bit signed values using 
        /// twos-complement.The first two bytes specify the average velocity
        /// of the drive wheels in millimeters per second(mm/s), with the
        /// high byte sent first.The next two bytes specify the radius, in 
        /// millimeters, at which Roomba should turn.The longer radii make
        /// Roomba drive straighter; shorter radii make it turn more.A Drive
        /// command with a positive velocity and a positive radius will make
        /// Roomba drive forward while turning toward the left. A negative
        /// radius will make it turn toward the right. Special cases for the
        /// radius make Roomba turn in place or drive straight, as specified
        /// below. The SCI must be in safe or full mode to accept this
        /// command.This command does change the mode.
        /// </summary>
        /// <param name="velocity">Velocity (-500 – 500 mm/s)</param>
        /// <param name="radius">Radius (-2000 – 2000 mm)</param>
        public void Drive(short velocity, short radius)
        {
            // Convert values to bytes.
            byte[] bVelocity = BitConverter.GetBytes(velocity);
            byte[] bRadius = BitConverter.GetBytes(radius);

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.DRIVE, bVelocity[1], bVelocity[0], bRadius[1], bRadius[0] };

            // Send command package.
            this.commandQueue.PutToQue(command);
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
        public void DirectDrive(short leftWheel, short rightWheel)
        {
            // Convert values to bytes.
            byte[] bLeftWheel = BitConverter.GetBytes(leftWheel);
            byte[] bRightWheel = BitConverter.GetBytes(rightWheel);

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.DRIVE_DIRECT, bRightWheel[1], bRightWheel[0], bLeftWheel[1], bLeftWheel[0] };

            // Send command package.
            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        ///This command lets you control the raw forward and backward motion of Roomba’s drive wheels
        ///independently.It takes four data bytes, which are interpreted as two 16-bit signed values using two’s
        ///complement.The first two bytes specify the PWM of the right wheel, with the high byte sent first.The
        ///next two bytes specify the PWM of the left wheel, in the same format.A positive PWM makes that wheel
        ///drive forward, while a negative PWM makes it drive backward.
        /// </summary>
        /// <param name="leftPWM">Left wheel PWM (-255 – 255)</param>
        /// <param name="rightPWM">Right wheel PWM (-255 – 255)</param>
        public void DrivePWM(short leftPWM, short rightPWM)
        {
            // Convert values to bytes.
            byte[] bLeftWheel = BitConverter.GetBytes(leftPWM);
            byte[] bRightWheel = BitConverter.GetBytes(rightPWM);

            // Build command package.
            byte[] command = { (byte)RoombaOpcodes.DRIVE_DIRECT, bRightWheel[1], bRightWheel[0], bLeftWheel[1], bLeftWheel[0] };

            // Send command package.
            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// Specifies a song to the SCI to be played later.Each song is 
        /// associated with a song number which the Play command uses
        /// to select the song to play. Users can specify up to 16 songs
        /// with up to 16 notes per song.Each note is specified by a note
        /// number using MIDI note definitions and a duration specified
        /// in fractions of a second.The number of data bytes varies
        /// depending on the length of the song specified.A one note song 
        /// is specified by four data bytes.For each additional note, two data
        /// bytes must be added. The SCI must be in passive, safe, or full
        /// mode to accept this command.This command does not change
        /// the mode
        /// </summary>
        /// <param name="song"></param>
        public void Song(byte songNumber, byte[] song)
        {
            if (song.Length > 255) return;

            // Command
            byte[] command = new byte[1 + 1 + 1 + song.Length];

            // Build command package.
            Buffer.BlockCopy(new byte[] { (byte)RoombaOpcodes.SONG }, 0, command, 0, 1);
            Buffer.BlockCopy(new byte[] { songNumber               }, 0, command, 1, 1);
            Buffer.BlockCopy(new byte[] { (byte)(song.Length / 2)  }, 0, command, 2, 1);
            Buffer.BlockCopy(song,                                    0, command, 3, song.Length);

            // Send command package.
            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// Plays one of 16 songs, as specified by an earlier Song
        /// command.If the requested song has not been specified yet,
        /// the Play command does nothing.The SCI must be in safe or full
        /// mode to accept this command.This command does not change
        /// the mode.
        /// </summary>
        /// <param name="songNumber">Song Numbe</param>
        public void Play(byte songNumber)
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.PLAY, songNumber });
        }

        /// <summary>
        /// Requests the SCI to send a packet of sensor data bytes. The 
        /// user can select one of four different sensor packets.The sensor
        /// data packets are explained in more detail in the next section.
        /// The SCI must be in passive, safe, or full mode to accept this
        /// command.This command does not change the mode
        /// </summary>
        /// <param name="packageCode">Packet Code</param>
        public void Sensors(SensorPacketsIDs packageCode)
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.SENSORS, (byte)packageCode });
        }

        /// <summary>
        /// Turns on force-seeking-dock mode, which causes the robot 
        /// to immediately attempt to dock during its cleaning cycle if it
        /// encounters the docking beams from the Home Base. (Note, 
        /// however, that if the robot was not active in a clean, spot or max
        /// cycle it will not attempt to execute the docking.) Normally the
        /// robot attempts to dock only if the cleaning cycle has completed
        /// or the battery is nearing depletion.This command can be sent
        /// anytime, but the mode will be cancelled if the robot turns off,
        /// begins charging, or is commanded into SCI safe or full modes
        /// </summary>
        public void ForceSeekingDock()
        {
            this.commandQueue.PutToQue(new byte[] { (byte)RoombaOpcodes.DOCK });
        }

        /// <summary>
        /// This command lets you ask for a list of sensor packets. The result is returned once, as in the Sensors
        /// command. The robot returns the packets in the order you specify.
        /// </summary>
        /// <param name="package">Packages IDs</param>
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
            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// Show HEX digits on the Roomba's screen.
        /// </summary>
        /// <param name="d3">Digit 3</param>
        /// <param name="d2">Digit 2</param>
        /// <param name="d1">Digit 1</param>
        /// <param name="d0">Digit 0</param>
        public void DigitLEDsRaw(int d3, int d2, int d1, int d0)
        {
            byte[] command = new byte[]
            {
                (byte)RoombaOpcodes.DIGIT_LEDs_RAW,
                SevenSegment(d3),
                SevenSegment(d2),
                SevenSegment(d1),
                SevenSegment(d0)
            };

            this.commandQueue.PutToQue(command);
        }

        /// <summary>
        /// Shutdown the LED display.
        /// </summary>
        public void DigitLEDsRawOff()
        {
            byte[] command = new byte[]
            {
                (byte)RoombaOpcodes.DIGIT_LEDs_RAW, 0, 0, 0, 0
            };

            this.commandQueue.PutToQue(command);
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
