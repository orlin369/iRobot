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
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;

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

        public void PlayNotes()
        {
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.START }, 0, 1);
            this.SerialPort.BaseStream.Flush();
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.CONTROL }, 0, 1);
            this.SerialPort.BaseStream.Flush();
            System.Threading.Thread.Sleep(20);
            for (int i = 31; i <= 127; i++)
            {
                PlayNote(i);
                System.Threading.Thread.Sleep(200);
            }
        }

        public void PlayNote(int note)
        {
            if (!SerialPort.IsOpen) return;
            byte[] command = { (byte)RoombaOpCode.SONG, 3, 1, (byte)note, (byte)10, (byte)RoombaOpCode.PLAY, 3 };
            this.SerialPort.Write(command, 0, command.Length);
        }

        public void Drive(int velocity, int radius)
        {
            //https://msdn.microsoft.com/en-us/library/atf689tw(v=vs.110).aspx
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.START }, 0, 1);
            this.SerialPort.BaseStream.Flush();
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.CONTROL }, 0, 1);
            this.SerialPort.BaseStream.Flush();
            Thread.Sleep(20);
            byte[] command = { (byte)RoombaOpCode.DRIVE, (byte)velocity, (byte)(velocity & 0xff), (byte)radius, (byte)(radius & 0xff) };
            this.SerialPort.Write(command, 0, command.Length);
        }

        public void LEDs(bool spot, bool clean, bool max)
        {
            if (!SerialPort.IsOpen) return;
            byte leds = Convert(new bool[] { false, false, false, false, spot, clean, max, false });
            byte[] command = { (byte)RoombaOpCode.LEDS,  leds};
            this.SerialPort.Write(command, 0, command.Length);
        }

        public void Safe()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.SAFE }, 0, 1);
        }
        
        public void Full()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.FULL }, 0, 1);
        }
        
        public void Power()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.POWER }, 0, 1);
        }

        public void Spot()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.SPOT }, 0, 1);
        }

        public void Clean()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.CLEAN }, 0, 1);
        }

        public void Max()
        {
            if (!SerialPort.IsOpen) return;
            this.SerialPort.Write(new byte[] { (byte)RoombaOpCode.MAX }, 0, 1);
        }
        
        private static byte Convert(bool[] bits)
        {
            byte data = 0;

            for (int bitIndex = 0; bitIndex < bits.Length; bitIndex++)
            {
                data |= (byte)((bits[bitIndex] ? 1 : 0) << 7 - bitIndex);
            }

            return data;
        } 
    }
}
