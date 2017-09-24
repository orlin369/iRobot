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
copies or substantial portions of the Software.

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
    /// Roomba operation codes.
    /// </summary>
    public enum RoombaOpcodes : byte
    {
        INVALID = 0,
        START = 128,            //0x80
        BAUD = 129,             //0x81
        CONTROL = 130,          //0x82
        SAFE = 131,             //0x83
        FULL = 132,             //0x84
        POWER = 133,            //0x85
        SPOT = 134,             //0x86
        CLEAN = 135,            //0x87
        MAX = 136,              //0x88
        DRIVE = 137,            //0x89
        MOTORS = 138,           //0x8A
        LEDS = 139,             //0x8B
        SONG = 140,             //0x8C
        PLAY = 141,             //0x8D
        SENSORS = 142,          //0x8E
        DOCK = 143,             //0x8F
        PWM_MOTORS = 144,       //0x90
        DRIVE_DIRECT = 145,     //0x91
        DRIVE_PWN = 146,        //0x92
        DIGITAL_OUTPUT = 147,   //0x93
        QUERY_LIST = 149,       //0x95
        SCHEDULING_LEDS = 162,  //0xA2
        DIGIT_LEDs_RAW = 163,   //0xA3
        DIGIT_LEDs_ASCII = 164, //0xA4
        BUTTONS = 165,          //0xA5
        SCHEDULE = 167,         //0xA7
        SET_DAY_TIME = 168,     //0xA8
    }
}
