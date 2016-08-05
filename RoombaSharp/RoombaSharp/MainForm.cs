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
using System.Windows.Forms;
using System.Threading;

using iRobot.RoombaSharp;
using iRobot.Events;
using Leap;

namespace RoombaSharp
{
    public partial class MainForm : Form
    {

        #region Variables

        private string robotSerialPortName;

        private Roomba robot;

        private Controller controller;

        private SampleListener listener = new SampleListener();

        private object syncLock = new object();

        private int motionDebonceCounter = 0;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Main Form

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DisconnectFromRobot();
            this.DisconnectFromLeapMotion();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Searc for serial port.
        /// </summary>
        private void SearchForPorts()
        {
            this.tsmiConnect.DropDown.Items.Clear();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                return;
            }

            foreach (string item in portNames)
            {
                //store the each retrieved available prot names into the MenuItems...
                this.tsmiConnect.DropDown.Items.Add(item);
            }

            foreach (ToolStripMenuItem item in this.tsmiConnect.DropDown.Items)
            {
                item.Click += tsmiConnect_Click;
                item.Enabled = true;
                item.Checked = false;
            }
        }

        /// <summary>
        /// Log Message
        /// </summary>
        /// <param name="message">The message.</param>
        private void LogMessage(string message)
        {
            lock (this.syncLock)
            {
                if (this.tbConsole.InvokeRequired)
                {
                    this.tbConsole.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.tbConsole.AppendText(message + Environment.NewLine);
                    });
                }
                else
                {
                    this.tbConsole.AppendText(message + Environment.NewLine);
                }
            }
        }

        /// <summary>
        /// Connect to Leapmotion.
        /// </summary>
        private void ConnectToLeapMotion()
        {
            this.controller = new Controller();
            // Have the sample listener receive events from the controller
            this.listener.FrameGrabed += this.listener_FrameGrabed;
            this.controller.AddListener(this.listener);

            // Log.
            this.LogMessage("Leap connection state: " + this.controller.IsConnected);
        }

        /// <summary>
        /// Disconnect from Leapmotion.
        /// </summary>
        private void DisconnectFromLeapMotion()
        {
            try
            {
                // Remove the sample listener when done
                this.controller.RemoveListener(this.listener);
                this.controller.Dispose();
            }
            catch
            { }
            // Log.
            //this.LogMessage("Leap connection state: " + this.controller.IsConnected);
        }

        /// <summary>
        /// Transform radians to degree.
        /// </summary>
        /// <param name="radians">Value</param>
        /// <returns>Transformed value.</returns>
        private float ToDegree(float radians)
        {
            return radians * 180.0f / (float)Math.PI;
        }

        private void OnFrame(Controller controller)
        {
            if (this.robot == null || this.controller == null) return;

            string message = "";

            // Get the most recent frame and report some basic information
            Frame frame = controller.Frame();

            //message += "Frame id: " + frame.Id
            //            + ", timestamp: " + frame.Timestamp
            //            + ", hands: " + frame.Hands.Count
            //            + ", fingers: " + frame.Fingers.Count
            //            + ", tools: " + frame.Tools.Count
            //            + ", gestures: " + frame.Gestures().Count;
            //message += Environment.NewLine;



            foreach (Hand hand in frame.Hands)
            {
                message += "Hand id: " + hand.Id + "\r\n";
                //            + ", palm position: " + hand.PalmPosition;
                //message += Environment.NewLine;

                // Get the hand's normal vector and direction
                Vector normal = hand.PalmNormal;
                Vector direction = hand.Direction;

                // Convert to degree.
                float pitch = -((this.ToDegree(normal.Pitch) + 90) * 1.5F);
                float roll = this.ToDegree(normal.Roll);
                float yaw = this.ToDegree(normal.Yaw);
                
                // Calculate the hand's pitch, roll, and yaw angles
                message += String.Format("P: {0}\r\nR: {1}\r\nY: {2}\r\n",
                    (int)pitch,
                    (int)roll,
                    (int)yaw);
                
                // Band filter.
                if (roll < 20 && roll > -20) roll = 0;


                this.motionDebonceCounter++;
                if ((this.motionDebonceCounter % 8) == 0)
                {
                    // Move my dorabal vacuum cleaner robot maaaaan!
                    this.robot.Drive((short)pitch, (short)roll);
                    this.motionDebonceCounter = 0;
                }
            }

            if (!frame.Hands.IsEmpty && !frame.Gestures().IsEmpty)
            {
                message += "";
                if (this.robot != null && this.robot.IsConnected)
                {
                    this.robot.Drive(0, 0);
                }
            }

            if (this.lblHandPosition.InvokeRequired)
            {
                this.lblHandPosition.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.lblHandPosition.Text = message;
                });
            }
            else
            {
                this.lblHandPosition.Text = message;
            }
        }

        #endregion

        #region Robot

        /// <summary>
        /// Connect to the robot.
        /// </summary>
        /// <param name="portName"></param>
        private void ConnectToRobot(string portName)
        {
            this.robot = new Roomba(portName);
            this.robot.OnMesage += this.robot_OnMesage;
            this.robot.OnConnect += Robot_OnConnect;
            this.robot.OnDisconnect += Robot_OnDisconnect;
            this.robot.Connect();
        }

        /// <summary>
        /// Disconnect from the robot.
        /// </summary>
        private void DisconnectFromRobot()
        {
            if (this.robot == null) return;
            this.robot.OnMesage -= this.robot_OnMesage;
            this.robot.OnConnect -= Robot_OnConnect;
            this.robot.OnDisconnect -= Robot_OnDisconnect;

            this.robot.Disconnect();
        }

        private void Robot_OnDisconnect(object sender, EventArgs e)
        {
            this.LogMessage("Disconnected from robot robot.");
        }

        private void Robot_OnConnect(object sender, EventArgs e)
        {
            Roomba robot = (Roomba)sender;
            this.LogMessage("Connected to robot port: " + robot.PortName);
        }

        private void robot_OnMesage(object sender, MessageString e)
        {
            this.LogMessage("Robot: " + e.Message);
        }

        #endregion

        #region Tool Stript Menu Items

        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobot();
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.robotSerialPortName = item.Text;
            this.ConnectToRobot(this.robotSerialPortName);
        }

        private void tsmiBeep_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;

            // Creat the melodie thread.
            Thread worker = new Thread(
                new ThreadStart(
                    delegate ()
                    {
                        this.robot.Start();
                        this.robot.Control();
                        System.Threading.Thread.Sleep(20);
                        for (byte i = 31; i <= 127; i++)
                        {
                            this.robot.Play(i);
                            System.Threading.Thread.Sleep(100);
                        }
                    }
                )
            );
            
            // Start the melodie thread.
            worker.Start();
        }

        #region Function Buttons

        private void cleanToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Start();
            this.robot.Clean();
        }

        private void spotToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Start();
            this.robot.Spot();
        }

        private void dockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Start();
            this.robot.ForceSeekingDock();
        }

        private void powerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Start();
            this.robot.Power();
        }

        private void maxToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Start();
            this.robot.Max();
        }

        #endregion

        #endregion

        #region Buttons

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive((short)this.trbSpeed.Value, (short)-this.trbRadius.Value);
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive((short)this.trbSpeed.Value, (short)this.trbRadius.Value);
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive((short)this.trbSpeed.Value, 0);
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive((short)-this.trbSpeed.Value, 0);
        }



        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(0, 0);
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(0, 0);
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(0, 0);
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(0, 0);
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(0, 0);
        }


        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConnectToLeapMotion();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DisconnectFromLeapMotion();
        }

        #endregion

        #region Track bar

        private void trbSpeed_ValueChanged(object sender, EventArgs e)
        {
            this.lblSpeed.Text = String.Format("Speed: {0:F3}[mm/s]", ((float)this.trbSpeed.Value / 5.0));
        }

        private void trbRadius_ValueChanged(object sender, EventArgs e)
        {
            this.lblRadius.Text = String.Format("Radius: {0}", this.trbRadius.Value);
        }


        #endregion
        
        #region Leapmotion frame graber

        private void listener_FrameGrabed(object sender, ControlerArg e)
        {
            //e.Controller.
            this.OnFrame(e.Controller);
        }

        #endregion
    }
}
