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
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Roombacs;

namespace iRobot
{
    public partial class MainForm : Form
    {

        private RoombaControl robot = null;

        private string robotSerialPortName = "";

        public MainForm()
        {
            InitializeComponent();

            
        }

        /// <summary>
        /// Search serial port.
        /// </summary>
        private void SearchForPorts()
        {
            this.portToolStripMenuItem.DropDown.Items.Clear();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                return;
            }

            foreach (string item in portNames)
            {
                //store the each retrieved available prot names into the MenuItems...
                this.portToolStripMenuItem.DropDown.Items.Add(item);
            }

            foreach (ToolStripMenuItem item in this.portToolStripMenuItem.DropDown.Items)
            {
                item.Click += mItPorts_Click;
                item.Enabled = true;
                item.Checked = false;
            }
        }

        #region Menu Item

        private void mItPorts_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobot();
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.robotSerialPortName = item.Text;
            this.ConnectToRobot();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }

        #endregion

        private void ConnectToRobot()
        {
            robot = new RoombaControl(this.robotSerialPortName);
        }

        private void DisconnectFromRobot()
        {
            try
            {
                if (this.robot != null)
                {
                    this.robot = null;
                }
            }
            catch (Exception exception)
            {
                
            }
        }

        private void portToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            if(robot == null)
            {
                return;
            }

            this.robot.Clean();
        }
    }
}
