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
    class QueueDataUnit
    {

        #region Properties

        /// <summary>
        /// Data
        /// </summary>
        public byte[] Buffer { get; private set; }

        /// <summary>
        /// Size
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Offset index.
        /// </summary>
        public int Offset { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="buffer">Data</param>
        /// <param name="count">Size</param>
        /// <param name="offset">Offset index</param>
        public QueueDataUnit(byte[] buffer, int count, int offset)
        {
            this.Buffer = buffer;
            this.Count = count;
            this.Offset = offset;
        }

        #endregion

    }
}
