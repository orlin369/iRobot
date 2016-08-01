using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using RoombaSharp.iRobot.RoombaSharp;
using RoombaSharp.iRobot.Messages;

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
            this.robot.PlayNotes();
        }

        #endregion

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(1, -1);
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(1, 1);
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(1, 0);
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null) return;
            this.robot.Drive(-1, 0);
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.robot == null) return;
            this.robot.LEDs(false, true, false);
        }
    }
}
