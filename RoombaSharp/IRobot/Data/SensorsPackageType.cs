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

namespace RoombaSharp.iRobot.Data
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
    public enum SensorsPackageCode : byte
    {
        B26_0 = 0,
        B10_1 = 1,
        B6_2  = 2,
        B10_3 = 3,
    }
}
