using Newtonsoft.Json;
using RoombaSharp.Adapters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoombaSharp.Connectors
{
    class DataConnector
    {
        #region Variables

        /// <summary>
        /// Connection adapter.
        /// </summary>
        private Adapter adapter;

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
