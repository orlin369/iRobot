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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using iRobotRemoteControl.Adapters;
using iRobotRemoteControl.Events;

namespace iRobotRemoteControl.Connectors
{
    public class DataConnector
    {

        #region Variables

        /// <summary>
        /// Connection adapter.
        /// </summary>
        private Adapter adapter;

        #endregion

        #region Events

        /// <summary>
        /// On message event.
        /// </summary>
        public event EventHandler<StringEventArgs> OnMessage;

        #endregion

        #region Properties

        /// <summary>
        /// Is connected flag.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (this.adapter == null) return false;

                return this.adapter.IsConnected;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="adapter">Data adapter.</param>
        public DataConnector(Adapter adapter)
        {
            this.adapter = adapter;
            this.adapter.OnMessage += Adapter_OnMessage;
        }

        #endregion

        #region Private Methods

        private void Adapter_OnMessage(object sender, Events.StringEventArgs e)
        {
            this.OnMessage?.Invoke(this, e);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect
        /// </summary>
        public void Connect()
        {
            if (adapter == null) return;

            this.adapter.Connect();
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (adapter == null) return;

            this.adapter.Disconnect();
        }

        /// <summary>
        /// Send text data.
        /// </summary>
        /// <param name="data"></param>
        public void SendData(string data)
        {
            adapter.SendRequest(data);
        }

        /// <summary>
        /// Send image.
        /// </summary>
        /// <param name="image">Image</param>
        public void SendImage(Bitmap image)
        {
            if (this.adapter == null) return;

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                this.adapter.SendImageBytes(ms.ToArray());
            }
        }

        #endregion

    }
}
