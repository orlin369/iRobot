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

using iRobot.Events;
using iRobot.Queues;
using iRobot.Data;

namespace iRobot.Communicators
{
    public class SerialCommunicator : ICommunicationAddapter, IDisposable
    {

        #region Variables

        /// <summary>
        /// Communication port.
        /// </summary>
        protected SerialPort SerialPort;

        /// <summary>
        /// Communication lock object.
        /// </summary>
        private Object requestLock = new Object();

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
                if (this.SerialPort == null) return false;

                return this.SerialPort.IsOpen;
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

        public bool Reconnect
        {
            get; set;
        }

        #endregion

        #region Events

        /// <summary>
        /// Received command message.
        /// </summary>
        public event EventHandler<BytesEventArgs> OnMesage;

        /// <summary>
        /// On connect event.
        /// </summary>
        public event EventHandler<EventArgs> OnConnect;

        /// <summary>
        /// On disconnect event.
        /// </summary>
        public event EventHandler<EventArgs> OnDisconnect;
        
        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Communication port.</param>
        public SerialCommunicator(string portName)
        {
            // Save the port name.
            this.portName = portName;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SerialCommunicator()
        {
            this.Dispose(false);
        }

        #endregion

        #region IDisposible Interface Implementation

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
        /// Data receiver handler.
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
                    // Get input buffer.
                    string messageString = serialPort.ReadExisting();

                    // Convert to bytes.
                    byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(messageString);

                    // Invoke event handlers.
                    this.OnMesage?.Invoke(this, new BytesEventArgs(messageBytes));

                    // Discard the duffer.
                    serialPort.DiscardInBuffer();
                }
                catch(Exception exception)
                {
                    // TODO: Create log.
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect to the serial port.
        /// </summary>
        public void Connect()
        {
            try
            {
                if (!this.IsConnected)
                {                    
                    this.SerialPort = new SerialPort(this.portName);
                    this.SerialPort.BaudRate = 115200;
                    this.SerialPort.DataBits = 8;
                    this.SerialPort.StopBits = StopBits.One;
                    this.SerialPort.Parity = Parity.None;
                    this.SerialPort.DataReceived += new SerialDataReceivedEventHandler(this.DataReceivedHandler);
                    this.SerialPort.Encoding = System.Text.Encoding.ASCII;
                    this.SerialPort.Open();

                    this.OnConnect?.Invoke(this, null);
                }
            }
            catch(Exception exception)
            {
                // TODO: Create log.
                this.OnDisconnect?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (this.IsConnected)
                {
                    this.SerialPort.Close();
                    this.OnDisconnect?.Invoke(this, null);
                }
            }
            catch (Exception exception)
            {
                // TODO: Create log.
                this.OnDisconnect?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Write command.
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public void Write(byte[] buffer, int offset, int count)
        {
            lock (this.requestLock)
            {
                try
                {
                    if (this.IsConnected)
                    {
                        this.SerialPort.Write(buffer, offset, count);
                    }
                }
                catch (Exception exception)
                {
                    // TODO: Create log.

                    this.OnDisconnect?.Invoke(this, null);

                    if (this.Reconnect)
                    {
                        // Reconnect.
                        this.Connect();
                    }
                }
            }
        }

        /// <summary>
        /// Knock-Knock - function.
        /// </summary>
        public void KnockKnock()
        {
            if (!SerialPort.IsOpen) return;
            // If Create's power is off, turn it on
            this.SerialPort.DtrEnable = false;
            System.Threading.Thread.Sleep(100);  // Delay in this state
            this.SerialPort.DtrEnable = true;
            System.Threading.Thread.Sleep(750);  // Delay in this state
        }

        #endregion

    }
}
