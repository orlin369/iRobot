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

namespace RoombaSharp.Video
{
    /// <summary>
    /// Structure to Store Information about Video Devices
    /// </summary>
    public class VideoDevice
    {

        #region Variables

        /// <summary>
        /// Name of device.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Device index.
        /// </summary>
        public int Index { get; private set; }

        /// <summary>
        /// Moniker string.
        /// </summary>
        public string MonikerString { get; private set; }

        #endregion

        #region Construcotr

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="index">Device index.</param>
        /// <param name="name">Name</param>
        /// <param name="monikerString">Moniker string.</param>
        public VideoDevice(int index, string name, string monikerString)
        {
            this.Index = index;
            this.Name = name;
            this.MonikerString = monikerString;
        }

        #endregion

        #region ToString()

        /// <summary>
        /// Represent the Device as a String.
        /// </summary>
        /// <returns>
        /// The string representation of this device.
        /// </returns>
        public override string ToString()
        {
            return String.Format("[{0} {1}:{2}]", this.Index, this.Name, this.MonikerString);
        }

        #endregion

    }
}
