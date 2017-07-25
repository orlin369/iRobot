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

using iRobot.Communicators;
using iRobot.Events;

using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace iRobotMQTTAdapter
{
    public class MQTTCommunicator : ICommunicationAddapter
    {

        #region Variables

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
        /// MQTT client.
        /// </summary>
        private MqttClient mqttClient;

        #endregion

        #region Properties

        /// <summary>
        /// Is connected flag.
        /// </summary>
        bool ICommunicationAddapter.IsConnected
        {
            get
            {
                if (this.mqttClient == null) return false;
                return this.mqttClient.IsConnected;
            }
        }

        /// <summary>
        /// Maximum timeout.
        /// </summary>
        public int MaxTimeout { get; set; }
        
        #endregion

        #region Events

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> OnConnect;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<EventArgs> OnDisconnect;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<BytesEventArgs> OnMesage;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="inputTopic"></param>
        /// <param name="outputTopic"></param>
        public MQTTCommunicator(string address, int port, string inputTopic, string outputTopic)
        {
            this.address = address;
            this.port = port;
            this.inputTopic = inputTopic;
            this.outputTopic = outputTopic;
        }

        event EventHandler<BytesEventArgs> ICommunicationAddapter.OnMesage
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event EventHandler<EventArgs> ICommunicationAddapter.OnConnect
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        event EventHandler<EventArgs> ICommunicationAddapter.OnDisconnect
        {
            add
            {
                throw new NotImplementedException();
            }

            remove
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region MQTT Events

        private void MqttClient_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            this.OnMesage?.Invoke(this, new BytesEventArgs(e.Message));
        }

        #endregion

        #region ICommunicationAddapter Implementation

        void ICommunicationAddapter.Connect()
        {
            try
            {

                this.mqttClient = new MqttClient(this.address);

                // Attach events.
                this.mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;

                // Connect to broker.
                this.mqttClient.Connect(Guid.NewGuid().ToString());

                // Check and subscribe.
                if (this.mqttClient.IsConnected)
                {
                    if (this.inputTopic != null)
                    {
                        this.mqttClient.Subscribe(new string[] { this.inputTopic }, new byte[] { 0 });

                        this.OnConnect?.Invoke(this, null);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));

                this.OnDisconnect?.Invoke(this, null);
            }
        }

        void ICommunicationAddapter.Disconnect()
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;

            try
            {
                this.mqttClient.Unsubscribe(new string[] { this.inputTopic });
                this.mqttClient.Disconnect();
                this.mqttClient = null;

                this.OnDisconnect?.Invoke(this, null);
            }
            catch (Exception exception)
            {
                Console.WriteLine(String.Format("Message: {0}\r\nSourece: {1}", exception.Message, exception.Source));
            }
        }

        void ICommunicationAddapter.Write(byte[] buffer, int offset, int count)
        {
            if (this.mqttClient == null || !this.mqttClient.IsConnected) return;

            this.mqttClient.Publish(this.outputTopic, buffer);
        }

        void ICommunicationAddapter.KnockKnock()
        {
            
        }

        #endregion
    }
}
