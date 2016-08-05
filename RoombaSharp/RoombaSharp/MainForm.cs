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
using System.Windows.Forms;
using RoombaSharp.iRobot.RoombaSharp;
using RoombaSharp.iRobot.Events;

namespace RoombaSharp
{
    public partial class MainForm : Form
    {

        private StringBuilder recievedData = new StringBuilder();

        private string robotSerialPortName;

        private Roomba robot;

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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DisconnectFromRobot();
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
            OutputWindow.Text += (Environment.NewLine + message).Trim();
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
            this.robot.Connect();
        }

        /// <summary>
        /// Disconnect from the robot.
        /// </summary>
        private void DisconnectFromRobot()
        {
            if (this.robot == null) return;
            this.robot.OnMesage -= this.robot_OnMesage;
            this.robot.Disconnect();
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

            this.robot.Start();
            this.robot.Control();
            System.Threading.Thread.Sleep(20);
            for (byte i = 31; i <= 127; i++)
            {
                this.robot.Play(i);
                System.Threading.Thread.Sleep(50);
            }
        }

        #endregion

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(this.trbSpeed.Value, -this.trbRadius.Value);
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(this.trbSpeed.Value, this.trbRadius.Value);
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(this.trbSpeed.Value, 0);
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(-this.trbSpeed.Value, 0);
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

        private void trbSpeed_ValueChanged(object sender, EventArgs e)
        {
            this.lblSpeed.Text = String.Format("Speed: {0}", this.trbSpeed.Value);
        }

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
    }
}
