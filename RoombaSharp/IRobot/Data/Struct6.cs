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

using System.Runtime.InteropServices;

namespace IRobot.Data
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Struct7
    {
        [FieldOffset(0)]
        public byte BumpersAndWheelDrops;
        [FieldOffset(1)]
        public byte Wall;
        [FieldOffset(2)]
        public byte CliffLeft;
        [FieldOffset(3)]
        public byte CliffFrontLeft;
        [FieldOffset(4)]
        public byte CliffFrontRight;
        [FieldOffset(5)]
        public byte VirtualWall;
        [FieldOffset(6)]
        public byte OverCurrents;
        [FieldOffset(7)]
        public byte US1;
        [FieldOffset(8)]
        public byte US2;
        [FieldOffset(9)]
        public byte IRByte;
        [FieldOffset(10)]
        public byte Buttons;
        [FieldOffset(11)]
        public short Distance;
        [FieldOffset(13)]
        public short Angle;
        [FieldOffset(15)]
        public byte ChargeState;
        [FieldOffset(16)]
        public ushort Voltage;
        [FieldOffset(18)]
        public ushort Current;
        [FieldOffset(20)]
        public byte BatteryTemperature;
        [FieldOffset(21)]
        public ushort BatteryCharge;
        [FieldOffset(23)]
        public ushort BatteryCapacity;
        [FieldOffset(25)]
        public ushort WallSignal;
        [FieldOffset(27)]
        public ushort CliffLeftSignal;
        [FieldOffset(29)]
        public ushort CliffFrontLeftSignal;
        [FieldOffset(31)]
        public ushort CliffFrontRightSignal;
        [FieldOffset(33)]
        public ushort CliffRightSignal;
        [FieldOffset(34)]
        public byte UserDigitalInputs;
        [FieldOffset(35)]
        public ushort UserAnalogInput;
        [FieldOffset(37)]
        public byte ChargingSourceAvailable;
        [FieldOffset(38)]
        public byte OIMode;
        [FieldOffset(39)]
        public byte SongNumber;
        [FieldOffset(40)]
        public byte SongPlaing;
        [FieldOffset(41)]
        public byte NumberOfStreamPackets;
        [FieldOffset(42)]
        public byte Velocity;
        [FieldOffset(44)]
        public byte Radius;
        [FieldOffset(46)]
        public byte RightVelocity;
        [FieldOffset(48)]
        public byte LeftVelocity;

    }
}
