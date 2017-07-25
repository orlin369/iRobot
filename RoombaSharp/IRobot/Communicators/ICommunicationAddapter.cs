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

using iRobot.Events;

namespace iRobot.Communicators
{
    public interface ICommunicationAddapter
    {

        #region Events

        /// <summary>
        /// Received command message.
        /// </summary>
        event EventHandler<BytesEventArgs> OnMesage;

        /// <summary>
        /// On connect event.
        /// </summary>
        event EventHandler<EventArgs> OnConnect;

        /// <summary>
        /// On disconnect event.
        /// </summary>
        event EventHandler<EventArgs> OnDisconnect;

        #endregion

        #region Properties

        /// <summary>
        /// If the board is correctly connected.
        /// </summary>
        bool IsConnected { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect to the serial port.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnect
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Write command.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// Knock-Knock - function.
        /// </summary>
        void KnockKnock();

        #endregion

    }
}
