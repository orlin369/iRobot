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
using System.IO.Ports;

using RoombaSharp.iRobot.Data;

namespace RoombaSharp.iRobot.RoombaSharp
{
    public class Roomba : Communicator
    {

        #region Construcotr

        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="portName">Serial port name.</param>
        public Roomba (string portName) : base(portName)
        {

        }

        #endregion

        #region API

        /// <summary>
        /// Starts the SCI.The Start command must be sent before any
        /// other SCI commands.This command puts the SCI in passive
        /// mode.
        /// </summary>
        public void Start()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.START }, 0, 1);
            this.SerialPort.BaseStream.Flush();
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
        public void Baud(BoudRates baudRate)
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.BAUD, (byte)baudRate }, 0, 2);
            this.SerialPort.BaseStream.Flush();
        }
        
        /// <summary>
        /// Enables user control of Roomba.This command must be sent
        /// after the start command and before any control commands are
        /// sent to the SCI.The SCI must be in passive mode to accept this 
        /// command.This command puts the SCI in safe mode
        /// </summary>
        public void Control()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.CONTROL }, 0, 1);
            this.SerialPort.BaseStream.Flush();
        }
        
        /// <summary>
        /// Starts the SCI. The Start command must be sent before any 
        /// other SCI commands.This command puts the SCI in passive
        /// mode.
        /// </summary>
        /// <param name="spot">Spot</param>
        /// <param name="clean">Clean</param>
        /// <param name="max">Max</param>
        public void LEDs(bool spot, bool clean, bool max)
        {
            if (!SerialPort.IsOpen) return;
            byte leds = Convert(new bool[] { false, false, false, false, spot, clean, max, false });
            byte[] command = { (byte)RoombaOpCode.LEDS,  leds};
            this.SerialPort.Write(command, 0, command.Length);
        }
        
        /// <summary>
        /// This command puts the SCI in safe mode.The SCI must be in 
        /// full mode to accept this command.
        /// </summary>
        public void Safe()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.SAFE }, 0, 1);
        }

        /// <summary>
        /// Enables unrestricted control of Roomba through the SCI and
        /// turns off the safety features.The SCI must be in safe mode to
        /// accept this command. This command puts the SCI in full mode.
        /// </summary>
        public void Full()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.FULL }, 0, 1);
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
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.POWER }, 0, 1);
        }

        /// <summary>
        /// Starts a spot cleaning cycle, the same as a normal “spot” 
        /// button press.The SCI must be in safe or full mode to accept this
        /// command.This command puts the SCI in passive mode
        /// </summary>
        public void Spot()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.SPOT }, 0, 1);
        }

        /// <summary>
        /// Starts a normal cleaning cycle, the same as a normal “clean” 
        /// button press.The SCI must be in safe or full mode to accept this
        /// command.This command puts the SCI in passive mode
        /// </summary>
        public void Clean()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.CLEAN }, 0, 1);
        }

        /// <summary>
        /// Starts a maximum time cleaning cycle, the same as a normal 
        /// “max” button press.The SCI must be in safe or full mode to
        /// accept this command.This command puts the SCI in passive
        /// mode.
        /// </summary>
        public void Max()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.MAX }, 0, 1);
        }

        /// <summary>
        /// Controls Roomba’s cleaning motors.The state of each motor is 
        /// specified by one bit in the data byte. The SCI must be in safe
        /// or full mode to accept this command.This command does not
        /// change the mode.
        /// </summary>
        /// <param name="mainBrush">Main Brush</param>
        /// <param name="vacuumSide">Vacuum Side</param>
        /// <param name="brush">Brush</param>
        public void Motors(bool mainBrush, bool vacuumSide, bool brush)
        {
            if (!SerialPort.IsOpen) return;
            byte motors = Convert(new bool[] { false, false, false, false, false, mainBrush, vacuumSide, brush });
            byte[] command = { (byte)RoombaOpCode.MOTORS, motors };
            this.SerialPort.Write(command, 0, command.Length);
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
        /// <param name="velocity"></param>
        /// <param name="radius"></param>
        public void Drive(int velocity, int radius)
        {
            if (!SerialPort.IsOpen) return;

            // Convert values to bytes.
            byte[] bVelocity = BitConverter.GetBytes(velocity);
            byte[] bRadius = BitConverter.GetBytes(radius);

            // Build command package.
            byte[] command = { (byte)RoombaOpCode.DRIVE, bVelocity[1], bVelocity[0], bRadius[1], bRadius[0] };

            // Send command package.
            this.SerialPort.Write(command, 0, command.Length);
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
        public void Song(byte[] song)
        {
            //TODO: Test
            if (!SerialPort.IsOpen) return;
            if (song.Length > 255) return;

            // Command
            byte[] command = new byte[1 + 1 + song.Length];

            // Build command package.
            System.Buffer.BlockCopy(new byte[] { (byte)RoombaOpCode.SONG }, 0, command, 0, 1);
            System.Buffer.BlockCopy(new byte[] { (byte)song.Length },       0, command, 1, 1);
            System.Buffer.BlockCopy(song,                                   0, command, 2, song.Length);

            // Send command package.
            this.SerialPort.Write(command, 0, command.Length);
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
            if (!SerialPort.IsOpen) return;
            byte[] command = { (byte)RoombaOpCode.PLAY, songNumber };
            this.SerialPort.Write(command, 0, command.Length);
        }

        /// <summary>
        /// Requests the SCI to send a packet of sensor data bytes. The 
        /// user can select one of four different sensor packets.The sensor
        /// data packets are explained in more detail in the next section.
        /// The SCI must be in passive, safe, or full mode to accept this
        /// command.This command does not change the mode
        /// </summary>
        /// <param name="packageCode">Packet Code</param>
        public void Sensors(SensorsPackageCode packageCode)
        {
            if (!SerialPort.IsOpen) return;
            byte[] command = { (byte)RoombaOpCode.SENSORS, (byte)packageCode };
            this.SerialPort.Write(command, 0, command.Length);
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
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.DOCK }, 0, 1);
        }

        #endregion

        #region Private Methods

        private static byte Convert(bool[] bits)
        {
            byte data = 0;

            for (int bitIndex = bits.Length - 1; bitIndex >= 0; bitIndex--)
            {
                data |= (byte)((bits[bitIndex] ? 1 : 0) << bitIndex);
            }

            return data;
        }

        #endregion

    }
}
