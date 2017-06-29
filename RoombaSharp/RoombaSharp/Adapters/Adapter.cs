﻿using RoombaSharp.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoombaSharp.Adapters
{
    public abstract class Adapter : IDisposable
    {

        #region Properties

        /// <summary>
        /// If the board is correctly connected.
        /// </summary>
        public abstract bool IsConnected { get; protected set; }

        /// <summary>
        /// Maximum timeout.
        /// </summary>
        public abstract int MaxTimeout { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// Received command message.
        /// </summary>
        public abstract event EventHandler<StringEventArgs> OnMessage;

        #endregion

        /// <summary>
        /// Connect
        /// </summary>
        public abstract void Connect();
        
        /// <summary>
        /// Disconnect
        /// </summary>
        public abstract void Disconnect();

        /// <summary>
        /// Reset the Robot.
        /// </summary>
        public abstract void Reset();

        /// <summary>
        /// Send request to the device.
        /// </summary>
        /// <param name="command"></param>
        public abstract void SendRequest(string command);

        /// <summary>
        /// Send image bytes.
        /// </summary>
        /// <param name="data"></param>
        public abstract void SendImageBytes(byte[] data);

        /// <summary>
        /// Dispose
        /// </summary>
        public abstract void Dispose();

    }
}
