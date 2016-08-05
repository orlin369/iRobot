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
using System.IO.Ports;
using System.Threading;

using RoombaSharp.iRobot.Events;

namespace RoombaSharp.iRobot.RoombaSharp
{
    public class Communicator : IDisposable
    {

        #region Variables

        /// <summary>
        /// Comunication port.
        /// </summary>
        protected SerialPort SerialPort;

        /// <summary>
        /// Comunication lock object.
        /// </summary>
        private Object requestLock = new Object();

        /// <summary>
        /// When is connected to the robot.
        /// </summary>
        private bool isConnected = false;

        /// <summary>
        /// Serial port name.
        /// </summary>
        private string portName = String.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// If the board is correctly connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }
        
        /// <summary>
        /// Robot serial port name.
        /// </summary>
        public string PortName
        {
            get
            {
                return this.portName;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Recieved command message.
        /// </summary>
        public event EventHandler<MessageString> OnMesage;

        public event EventHandler<EventArgs> OnConnect;

        public event EventHandler<EventArgs> OnDisconnect;


        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Comunication port.</param>
        public Communicator(string portName)
        {
            // Save the port name.
            this.portName = portName;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Communicator()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Add resources for disposing.
            }

            this.Disconnect();
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Data recievce handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Wait ...
            Thread.Sleep(550);

            if (sender != null)
            {
                // Make serial port to get data from.
                SerialPort serialPort = (SerialPort)sender;

                try
                {
                    string inData = serialPort.ReadExisting();

                    if (this.OnMesage != null)
                    {
                        this.OnMesage(this, new MessageString(inData));
                    }

                    // Discart the duffer.
                    serialPort.DiscardInBuffer();
                }
                catch
                { }
            }
        }

        /// <summary>
        /// Send request to the device.
        /// </summary>
        /// <param name="command"></param>
        protected void SendRequest(string command)
        {
            lock (this.requestLock)
            {
                try
                {
                    if (this.isConnected)
                    {
                        this.SerialPort.Write(command);
                    }
                }
                catch
                {
                    this.isConnected = false;
                    // Reconnect.
                    this.Connect();
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connetc to the serial port.
        /// </summary>
        public void Connect()
        {
            try
            {
                if (!this.isConnected)
                {
                    this.SerialPort = new SerialPort(this.portName);
                    this.SerialPort.BaudRate = 115200;
                    this.SerialPort.DataBits = 8;
                    this.SerialPort.StopBits = StopBits.One;
                    this.SerialPort.Parity = Parity.None;
                    this.SerialPort.DataReceived += new SerialDataReceivedEventHandler(this.DataReceivedHandler);
                    this.SerialPort.Open();

                    this.isConnected = true;

                    this.OnConnect?.Invoke(this, new MessageString(""));
                }
            }
            catch
            {
                this.isConnected = false;
            }
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (this.isConnected)
            {
                this.SerialPort.Close();
                this.isConnected = false;
                this.OnDisconnect?.Invoke(this, null);
            }
        }

        void SendRawRequest(string command)
        {
            lock (this.requestLock)
            {
                try
                {
                    if (this.isConnected)
                    {
                        this.SerialPort.Write(command);

                        if (this.OnMesage != null)
                        {
                            this.OnMesage(this, null);
                        }

                    }
                }
                catch
                {
                    this.isConnected = false;
                    // Reconnect.
                    this.Connect();
                }
            }
        }

        #endregion

    }
}
