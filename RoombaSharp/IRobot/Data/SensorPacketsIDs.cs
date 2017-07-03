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

namespace iRobot.Data
{

    /// <summary>
    /// iRobot® Roomba® Serial Command Interface(SCI) Specification
    /// www.irobot.com
    /// Roomba SCI Sensor Packets
    /// The robot will send back one of four different sensor data
    /// packets in response to a Sensor command, depending on the
    /// value of the packet code data byte. The data bytes are specified
    /// below in the order in which they will be sent.A packet code value
    /// of 0 sends all of the data bytes. A value of 1 through 3 sends a
    /// subset of the sensor data.Some of the sensor data values are
    /// 16 bit values. These values are sent as two bytes, high
    /// byte first.
    /// </summary>
    public enum SensorPacketsIDs : byte
    {

        /// <summary>
        /// Address: 7 - 26
        /// Size: 26
        /// </summary>
        Group0 = 0,

        /// <summary>
        /// Address: 7 - 16
        /// Size: 10
        /// </summary>
        Group1 = 1,

        /// <summary>
        /// Address: 17 - 20
        /// Size: 6
        /// </summary>
        Group2 = 2,

        /// <summary>
        /// Address: 21 - 26
        /// Size: 10
        /// </summary>
        Group3 = 3,

        /// <summary>
        /// Address: 27 - 34
        /// Size: 14
        /// </summary>
        Group4 = 4,

        /// <summary>
        /// Address: 35 - 42
        /// Size: 12
        /// </summary>
        Group5 = 5,

        /// <summary>
        /// Address: 7 - 42
        /// Size: 52
        /// </summary>
        Group6 = 6,

        BumpsAndWheelDrops = 7,
        Wall = 8,
        CliffLeft = 9,
        CliffFrontLeft = 10,
        CliffFrontRight = 11,
        CliffRight = 12,
        VirtualWall = 13,
        WheelOvercurrents = 14,
        DirtDetect = 15,
        UnusedByte = 16,
        InfraredCharacterOmni = 17,
        InfraredCharacterLeft = 52,
        InfraredCharacterRight = 53,
        Distance = 19,
        Angle = 20,
        ChargingState = 21,
        Voltage = 22,
        Current = 23,
        Temperature = 24,
        BatteryCharge = 25,
        BatteryCapacity = 26,
        WallSignal = 27,
        CliffLeftSignal = 28,
        CliffFrontLeftSignal = 29,
        CliffFrontRightSignal = 30,
        CliffRightSignal = 31,
        ChargingSourcesAvailable = 34,
        OIMode = 35,
        SongNumber = 36,
        SongPlaying = 37,
        NumberofStreamPackets = 38,
        RequestedVelocity = 39,
        RequestedRadius = 40,
        RequestedRightVelocity = 41,
        RequestedLeftVelocity = 42,
        RightEncoderCounts = 43,
        LeftEncoderCounts = 44,
        LightBumper = 45,
        LightBumpLeftSignal = 46,
        LightBumpFrontLeftSignal = 47,
        LightBumpCenterLeftSignal = 48,
        LightBumpCenterRightSignal = 49,
        LightBumpFrontRightSignal = 50,
        LightBumpRightSignal = 51,
        LeftMotorCurrent = 54,
        RightMotorCurrent = 55,
        MainBrushMotorCurrent = 56,
        SideBrushMotorCurrent = 57,
        Stasis = 58,
    }
}
