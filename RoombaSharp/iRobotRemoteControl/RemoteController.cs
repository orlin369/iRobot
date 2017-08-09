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
using System.Text;

using iRobotRemoteControl.Events;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace iRobotRemoteControl
{

    /// <summary>
    /// Remote controller class. Its purpose is to communicate 
    /// </summary>
    public class RemoteController
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

        #endregion

        #region Properties

        /// <summary>
        /// Is connected flag.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                if (this.mqttClient == null) return false;

                return this.mqttClient.IsConnected;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// On message received event.
        /// </summary>
        public event EventHandler<BytesEventArgs> OnMessage;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="address">Broker address.</param>
        public RemoteController(string address)
        {
            this.address = address;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Connect
        /// </summary>
        public void Connect()
        {
            try
            {
                // Create MQTT communicator.
                this.mqttClient = new MqttClient(this.address);

                // Attach events.
                this.mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

                // Connect to broker.
                this.mqttClient.Connect(Guid.NewGuid().ToString());
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));
            }
        }

        /// <summary>
        /// Disconnect
        /// </summary>
        public void Disconnect()
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;

            try
            {
                this.mqttClient.Disconnect();
                this.mqttClient = null;
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));
            }
        }
        
        /// <summary>
        /// Subscribe to input topic.
        /// </summary>
        /// <param name="inputTopic"></param>
        public void SubscribeToInputTopic(string[] inputTopics, byte[] QoS)
        {
            // Check and subscribe.
            if (this.mqttClient.IsConnected)
            {
                if (inputTopics != null)
                {
                    this.mqttClient.Subscribe(inputTopics, QoS);
                }
            }
        }
        
        /// <summary>
        /// Unsubscribe from topic.
        /// </summary>
        /// <param name="inputTopic"></param>
        public void UnsubscribeToInputTopic(string inputTopic)
        {
            // Check and subscribe.
            if (this.mqttClient.IsConnected)
            {
                if (inputTopic != null)
                {
                    this.mqttClient.Unsubscribe(new string[] { inputTopic });
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="image"></param>
        public void SendImageData(string topic, Bitmap image)
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;
            if (image == null) return;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Jpeg);
                    this.mqttClient.Publish(topic, ms.ToArray());
                }
            }
            catch
            { }
        }

        /// <summary>
        /// Send text data.
        /// </summary>
        /// <param name="topic">Topic</param>
        /// <param name="data">Data</param>
        public void SendTextData(string topic, string data)
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;
            if (data == null) return;
            try
            {
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                this.mqttClient.Publish(topic, byteData);
            }
            catch
            { }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// MQTT message handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            this.OnMessage?.Invoke(this, new BytesEventArgs(e.Message));
        }

        #endregion

    }
}
