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
using System.Runtime.InteropServices;

namespace iRobot.Data
{
    // TODO: See what is going on.
    // Tis structure is potential bug.
    // Solution: https://msdn.microsoft.com/en-us/library/ms182285.aspx
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct Struct6
    {

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(0)]
        public byte BumpersAndWheelDrops;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(1)]
        public byte Wall;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(2)]
        public byte CliffLeft;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(3)]
        public byte CliffFrontLeft;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(4)]
        public byte CliffFrontRight;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(5)]
        public byte CliffRight;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(6)]
        public byte VirtualWall;
        
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(7)]
        public byte OverCurrents;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(8)]
        public byte DirtDetect;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(9)]
        public byte US1;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(10)]
        public byte IRByte;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(11)]
        public byte Buttons;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(12)]
        public short Angle;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(14)]
        public short Distance;
        
        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(16)]
        public ChargingState ChargeState;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(17)]
        public UInt16 Voltage;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(19)]
        public ushort Current;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(21)]
        public byte BatteryTemperature;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(22)]
        public ushort BatteryCharge;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(24)]
        public ushort BatteryCapacity;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(26)]
        public ushort WallSignal;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(28)]
        public ushort CliffLeftSignal;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(30)]
        public ushort CliffFrontLeftSignal;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(32)]
        public ushort CliffFrontRightSignal;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(34)]
        public ushort CliffRightSignal;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(36)]
        public byte UserDigitalInputs;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(37)]
        public ushort UserAnalogInput;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(39)]
        public byte ChargingSourceAvailable;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(40)]
        public OIMode OIMode;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(41)]
        public byte SongNumber;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(42)]
        public byte SongPlaing;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(43)]
        public byte NumberOfStreamPackets;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(44)]
        public short Velocity;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(46)]
        public short Radius;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(48)]
        public short RightVelocity;

        /// <summary>
        /// 
        /// </summary>
        [FieldOffset(50)]
        public short LeftVelocity;

    }
}
