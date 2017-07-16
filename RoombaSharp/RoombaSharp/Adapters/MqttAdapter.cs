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
using System.Text;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

using RoombaSharp.Events;

namespace RoombaSharp.Adapters
{
    public class MqttAdapter : Adapter
    {

        #region Variables

        /// <summary>
        /// MQTT client.
        /// </summary>
        private MqttClient mqttClient;

        /// <summary>
        /// URI address.
        /// </summary>
        private string address;

        /// <summary>
        /// Port number.
        /// </summary>
        private int port;

        /// <summary>
        /// Input topic name.
        /// </summary>
        private string inputTopic;

        /// <summary>
        /// Output topic name.
        /// </summary>
        private string outputTopic;

        /// <summary>
        /// Output image topic.
        /// </summary>
        private string outputImageTopic;

        #endregion

        #region Properties

        /// <summary>
        /// Is connected flag.
        /// </summary>
        public override bool IsConnected
        {
            get
            {
                if (this.mqttClient == null) return false;
                return this.mqttClient.IsConnected;
            }

            protected set
            {

            }
        }

        /// <summary>
        /// Maximum timeout.
        /// </summary>
        public override int MaxTimeout { get; set; }

        #endregion

        #region Events

        /// <summary>
        /// On message received event.
        /// </summary>
        public override event EventHandler<StringEventArgs> OnMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="inputTopic"></param>
        /// <param name="outputTopic"></param>
        /// <param name="outputImageTopic"></param>
        public MqttAdapter(string address, int port, string inputTopic, string outputTopic, string outputImageTopic)
        {
            this.address = address;
            this.port = port;
            this.inputTopic = inputTopic;
            this.outputTopic = outputTopic;
            this.outputImageTopic = outputImageTopic;

            this.mqttClient = new MqttClient(this.address);
        }

        #endregion

        #region MQTT Events

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string message = Encoding.UTF8.GetString(e.Message);
            this.OnMessage?.Invoke(this, new StringEventArgs(message));
        }

        private void MqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            this.IsConnected = false;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect
        /// </summary>
        public override void Connect()
        {
            try
            {
                // Attach events.
                this.mqttClient.ConnectionClosed += MqttClient_ConnectionClosed;
                this.mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

                // Connect to broker.
                this.mqttClient.Connect(Guid.NewGuid().ToString());

                // Check and subscribe.
                if (this.mqttClient.IsConnected)
                {
                    if (this.inputTopic != null)
                    {
                        this.mqttClient.Subscribe(new string[] { this.inputTopic }, new byte[] { 0 });
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));
            }
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public override void Disconnect()
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;

            try
            {
                this.mqttClient.Unsubscribe(new string[] { this.inputTopic });
                this.mqttClient.Disconnect();
                this.mqttClient = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));
            }
        }

        /// <summary>
        /// Send string request.
        /// </summary>
        /// <param name="command"></param>
        public override void SendRequest(string command)
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;

            byte[] byteArray = Encoding.UTF8.GetBytes(command);
            this.mqttClient.Publish(this.outputTopic, byteArray);
        }

        /// <summary>
        /// Send image bytes.
        /// </summary>
        /// <param name="data"></param>
        public override void SendImageBytes(byte[] data)
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;

            this.mqttClient.Publish(this.outputImageTopic, data);
        }

        /// <summary>
        /// Reset
        /// </summary>
        public override void Reset()
        {

        }

        #endregion

        #region IDisposible Implementation

        /// <summary>
        /// Dispose the object.
        /// </summary>
        public override void Dispose()
        {
            this.Disconnect();
        }

        #endregion
    }

}
