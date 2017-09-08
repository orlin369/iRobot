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

namespace iRobot.Data
{
    [Serializable]
    public class ScheduleData
    {

        /// <summary>
        /// Days
        /// </summary>
        public DayOfWeek Days { get; set; }

        public RoombaDateTime Sunday { get; set; }

        public RoombaDateTime Monday { get; set; }

        public RoombaDateTime Tuesday { get; set; }

        public RoombaDateTime Wednesday { get; set; }

        public RoombaDateTime Thursday { get; set; }

        public RoombaDateTime Friday { get; set; }

        public RoombaDateTime Saturday { get; set; }

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ScheduleData()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validate data content.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool isValid =
                   (this.Sunday != null)
                && (this.Monday != null)
                && (this.Thursday != null)
                && (this.Wednesday != null)
                && (this.Thursday != null)
                && (this.Friday != null)
                && (this.Saturday != null);

            return isValid;
        }

        #endregion

    }
}
