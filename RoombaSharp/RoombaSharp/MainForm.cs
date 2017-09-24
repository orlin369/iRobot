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
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.IO;

using AForge.Video;
using AForge.Video.DirectShow;

using Newtonsoft.Json;

using iRobot;
using iRobot.Data;
using iRobot.Events;
using iRobot.Communicators;

using iRobotRemoteControl;

using Logger;

using RoombaSharp.Settings;
using RoombaSharp.Video;
using iRobotRemoteControl.Events;

namespace RoombaSharp
{
    public partial class MainForm : Form
    {

        #region Variables

        #region Robot

        /// <summary>
        /// Robot
        /// </summary>
        private Roomba robot;

        /// <summary>
        /// Clean LED intensity.
        /// </summary>
        private byte cleanLedIntensity = 0;

        /// <summary>
        /// Clean LED color.
        /// </summary>
        private byte cleanLedColor = 0;

        /// <summary>
        /// Remote controller
        /// </summary>
        private RemoteController remoteController;

        /// <summary>
        /// Sensors dump
        /// </summary>
        private Struct6 sensrosDump;

        #endregion

        #region Camera

        /// <summary>
        /// Video capture device.
        /// </summary>
        private VideoCaptureDevice videoDevice = null;

        /// <summary>
        /// Video devices.
        /// </summary>
        private VideoDevice[] videoDevices;

        /// <summary>
        ///Captured image.
        /// </summary>
        private Bitmap capturedImage;

        /// <summary>
        /// Sync object for video.
        /// </summary>
        private object syncLockVideo = new object();

        #endregion

        /// <summary>
        /// Log messages sync lock object.
        /// </summary>
        private object syncLockLogs = new object();
        
        /// <summary>
        /// Send image timer.
        /// </summary>
        private System.Windows.Forms.Timer sendImageTimer;

        /// <summary>
        /// Send image timer.
        /// </summary>
        private System.Windows.Forms.Timer sendDataTimer;

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

        #region Private Methods

        #endregion

        #region Main Form

        /// <summary>
        /// Form Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SetupLog();
            this.SearchForPorts();
            this.SearchForCameras();
            this.LogMessage("MainForm.MainForm_Load()", "Application started.", LogMessageTypes.Info);
        }

        /// <summary>
        /// Form Closing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopSendImageTimer();
            this.StopSendDataTimer();

            this.DisconnectFromRobot();
            this.DisconnectFromCamera();

            this.DisconnectFromServer();

            this.LogMessage("MainForm.MainForm_FormClosing()", "Application stopped.", LogMessageTypes.Info, true);
        }

        #endregion

        #region Tool Strip Menu Items

        #region Robot

        /// <summary>
        /// Refresh port list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiRobot_Click(object sender, EventArgs e)
        {
            this.SearchForPorts();
        }

        /// <summary>
        /// Connect to the robot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobot();
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.ConnectToRobot(item.Text);
        }

        #region Beep

        /// <summary>
        /// Makes robot to beep.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBeepTest_Click(object sender, EventArgs e)
        {
            // Create the worker thread.
            Thread worker = new Thread(
                new ThreadStart(
                    delegate ()
                    {
                        // Check the robot.
                        if (this.robot == null || !this.robot.IsConnected) return;

                        // Song number.
                        byte songNumber = 0;

                        // Song data.
                        List<byte> notes = new List<byte>();
                        for (byte i = 0; i <= 5; i++)
                        {
                            notes.Add(80);
                            notes.Add(32);
                        }

                        // Set song.
                        this.robot.Song(songNumber, notes.ToArray());

                        // Play song.
                        this.robot.Play(songNumber);
                    }
                )
            );

            // Start the Melodie thread.
            worker.Start();
        }

        #endregion

        #region LED

        /// <summary>
        /// Set/Reset "Check the robot" LED.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLedCheckRobot_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedCheckRobot.Checked = !this.tsmiLedCheckRobot.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        /// <summary>
        /// Set/Reset "Spot" LED.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLedSpot_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedSpot.Checked = !this.tsmiLedSpot.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        /// <summary>
        /// Set/Reset "Dock" LED.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLedDock_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedDock.Checked = !this.tsmiLedDock.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        /// <summary>
        /// Set/Reset "Dirt detect" LED.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLedDirtDetect_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedDirtDetect.Checked = !this.tsmiLedDirtDetect.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        /// <summary>
        /// Set/Reset "Clean" LED.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLedCleanOff_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedCleanOff.Checked = true;

            if (this.tsmiLedCleanOff.Checked)
            {
                this.tsmiLedCleanGreen.Checked = false;
                this.tsmiLedCleanRed.Checked = false;
            }

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        /// <summary>
        /// Set/Reset "Clean" LED.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLedCleanGreen_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedCleanGreen.Checked = true;

            if (this.tsmiLedCleanGreen.Checked)
            {
                this.tsmiLedCleanOff.Checked = false;
                this.tsmiLedCleanRed.Checked = false;
            }
            
            if (this.tsmiLedCleanGreen.Checked || this.tsmiLedCleanRed.Checked)
            {
                this.cleanLedIntensity = 255;
            }

            this.cleanLedColor = 0;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        /// <summary>
        /// Set/Reset "Clean" LED.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiLedCleanRed_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedCleanRed.Checked = false;

            if (this.tsmiLedCleanRed.Checked)
            {
                this.tsmiLedCleanOff.Checked = false;
                this.tsmiLedCleanGreen.Checked = false;
            }

            if (this.tsmiLedCleanGreen.Checked || this.tsmiLedCleanRed.Checked)
            {
                this.cleanLedIntensity = 255;
            }

            this.cleanLedColor = 255;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }


        #endregion

        #region Motors

        /// <summary>
        /// Set/Reset main brush motor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiMainBrush_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiMainBrush.Checked = !this.tsmiMainBrush.Checked;

            this.robot.Motors(this.tsmiMainBrush.Checked, this.tsmiVacuum.Checked, this.tsmiSideBrush.Checked);
        }

        /// <summary>
        /// Set/Reset vacuum motor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiVacuum_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiVacuum.Checked = !this.tsmiVacuum.Checked;

            this.robot.Motors(this.tsmiMainBrush.Checked, this.tsmiVacuum.Checked, this.tsmiSideBrush.Checked);
        }

        /// <summary>
        /// Set/Reset side brush motor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSideBrush_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiSideBrush.Checked = !this.tsmiSideBrush.Checked;

            this.robot.Motors(this.tsmiMainBrush.Checked, this.tsmiVacuum.Checked, this.tsmiSideBrush.Checked);
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Set/Reset button clean.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBtnClean_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Clean();
        }

        /// <summary>
        /// Set/Reset button spot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBtnSpot_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Spot();
        }

        /// <summary>
        /// Set/Reset button dock.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBtnDock_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.ForceSeekingDock();
        }

        /// <summary>
        /// Set/Reset button power.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBtnPower_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Power();
        }

        /// <summary>
        /// Set/Reset button max.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiBtnMax_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Max();
        }

        #endregion

        #region LED Display

        /// <summary>
        /// Test the LED display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDisplayTest_Click(object sender, EventArgs e)
        {
            // Create worker thread.
            Thread worker = new Thread(
                new ThreadStart(
                    delegate ()
                    {
                        if (this.robot == null || !this.robot.IsConnected) return;

                        // Show all digits on the screen.
                        for (int index = 0; index < 16; index++)
                        {
                            this.robot.DigitLEDsRaw(index, index, index, index);
                            Thread.Sleep(1000);
                        }

                        // Shutdown the display.
                        this.robot.DigitLEDsRawOff();
                    }));

            worker.Start();
        }

        /// <summary>
        /// Shutdown the LED display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDisplayShutdown_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            // Shutdown the display.
            this.robot.DigitLEDsRawOff();
        }

        #endregion

        #region Sensors
        
        /// <summary>
        /// Get package ID6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiParamettersGroup6_Click(object sender, EventArgs e)
        {
            // Create the worker thread.
            Thread worker = new Thread(
                new ThreadStart(
                    delegate ()
                    {
                        if (this.robot == null || !this.robot.IsConnected) return;

                        int waitTime = 1000;

                        this.LogMessage("MainForm.tsmiParamettersGroup6_Click()", "Group 6", LogMessageTypes.Info);

                        this.robot.Sensors(SensorPacketsIDs.Group6);
                        Thread.Sleep(waitTime);
                    }
                )
            );

            // Start the Melodie thread.
            worker.Start();
        }

        #endregion

        #region Day Time

        private void tsmiSetFromPC_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            robot.SetDayTime(DateTime.Now);
        }

        #endregion

        #region Schedule

        private void tsmiSchedule_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            using (ScheduleForm sf = new ScheduleForm())
            {
                DialogResult result = sf.ShowDialog();

                if (result == DialogResult.OK)
                {
                    ScheduleData scheduleDate = sf.ScheduleData;

                    robot.Schedule(scheduleDate);
                }
            }
        }

        #endregion

        #endregion

        /// <summary>
        /// Show settings form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            using (Settings.SettingsForm sf = new Settings.SettingsForm())
            {
                sf.ShowDialog();
            }
        }

        /// <summary>
        /// Exit the application.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Camera

        /// <summary>
        /// Stop the video capture.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiStopCaptureeDevice_Click(object sender, EventArgs e)
        {
            // Disconnect from the camera.
            this.DisconnectFromCamera();

            // Stop the image timer.
            this.StopSendImageTimer();

            // Uncheck all cameras.
            foreach (ToolStripMenuItem cameraItem in this.tsmiCameraCapture.DropDown.Items)
            {
                cameraItem.Enabled = true;
                cameraItem.Checked = false;
            }
        }

        /// <summary>
        /// Capture device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiCaptureeDevice_Click(object sender, EventArgs e)
        {
            // Create instance of caller.
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            // Get device.
            VideoDevice videoDevice = (VideoDevice)item.Tag;

            // Uncheck all cameras.
            foreach (ToolStripMenuItem mItem in this.tsmiCameraCapture.DropDown.Items)
            {
                mItem.Checked = false;
            }

            // Check only this camera.
            item.Checked = true;

            // Disconnect from the camera.
            this.DisconnectFromCamera();

            // Stop the image timer.
            this.StopSendImageTimer();

            // Connect to camera.
            this.ConnecToCamera(videoDevice.MonikerString);            
        }

        #endregion

        #region Server

        private void tsmiServerConnect_Click(object sender, EventArgs e)
        {
            this.ConnectToServer();
        }

        private void tsmiServerDisconnect_Click(object sender, EventArgs e)
        {
            this.StopSendImageTimer();
            this.StopSendDataTimer();
            this.DisconnectFromServer();
        }

        private void tsmiServerTest_Click(object sender, EventArgs e)
        {
            iRobot.Data.Struct6 testData = new Struct6();

            // Test 1
            testData.Wall = 1;
            // Test 2
            testData.Voltage = 5000;
            // Test 3
            testData.Current = 5000;
            // Test 4
            testData.CliffLeft = 1;
            // Test 5
            testData.CliffFrontLeft = 0;
            // Test 6
            testData.CliffRight = 1;
            // Test 7
            testData.CliffFrontRight = 0;
            // Test 8
            testData.BumpersAndWheelDrops = 1 + 0 + 4 + 0;

            testData.BatteryTemperature = 22;

            string stringTestData = JsonConvert.SerializeObject(testData);

            this.SendTextData(stringTestData);

            this.LogMessage("MainForm.tsmiServerTest_Click()", "Send test data to the server.", LogMessageTypes.Info);
        }

        private void tsmiEnableSendingSensors_Click(object sender, EventArgs e)
        {
            // Get the sender.
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            // Invert the check state.
            item.Checked = !item.Checked;

            // Apply changes.
            if(item.Checked)
            {
                this.StartSendDataTimer();
            }
            else
            {
                this.StopSendDataTimer();
            }
        }

        private void tsmiEnableSendingImages_Click(object sender, EventArgs e)
        {
            // Get the sender.
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            // Invert the check state.
            item.Checked = !item.Checked;

            // Apply changes.
            if (item.Checked)
            {
                this.StartSendImageTimer();
            }
            else
            {
                this.StopSendImageTimer();
            }
        }

        #endregion

        #endregion

        #region Buttons

        /// <summary>
        /// Turn the robot CW.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)this.trbSpeed.Value, (short)-this.trbRadius.Value);
        }

        /// <summary>
        /// Turn the robot CCW.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)this.trbSpeed.Value, (short)this.trbRadius.Value);
        }

        /// <summary>
        /// Move the robot Forward.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)this.trbSpeed.Value, 0);
        }

        /// <summary>
        /// Move the robot Backward.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)-this.trbSpeed.Value, 0);
        }
        
        /// <summary>
        /// Stop the robot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        /// <summary>
        /// Stop the robot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        /// <summary>
        /// Stop the robot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        /// <summary>
        /// Stop the robot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        /// <summary>
        /// Stop the robot.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        #endregion

        #region Track bar
        
        /// <summary>
        /// Changes speed value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbSpeed_ValueChanged(object sender, EventArgs e)
        {
            this.lblSpeed.Text = String.Format("Speed: {0}[mm/s]", this.trbSpeed.Value);
        }

        /// <summary>
        /// Radius value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trbRadius_ValueChanged(object sender, EventArgs e)
        {
            this.lblRadius.Text = String.Format("Radius: {0}", this.trbRadius.Value);
        }

        #endregion

        #region pbSCADA

        /// <summary>
        /// Paints the SCADA.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbSCADA_Paint(object sender, PaintEventArgs e)
        {
            // Get sensor data.
            bool cliffLeft       = this.sensrosDump.CliffLeft != 0;
            bool cliffFrontLeft  = this.sensrosDump.CliffFrontLeft != 0;
            bool cliffFrontRight = this.sensrosDump.CliffFrontRight != 0;
            bool cliffRight      = this.sensrosDump.CliffRight != 0;
            bool wheelDropLeft   = (this.sensrosDump.BumpersAndWheelDrops & BumpersAndWheelDropsBits.WHEEL_DROP_LEFT) != 0;
            bool wheelDropRight  = (this.sensrosDump.BumpersAndWheelDrops & BumpersAndWheelDropsBits.WHEEL_DROP_RIGHT) != 0;
            bool bumperLeft      = (this.sensrosDump.BumpersAndWheelDrops & BumpersAndWheelDropsBits.BUMP_LEFT) != 0;
            bool bumperRight     = (this.sensrosDump.BumpersAndWheelDrops & BumpersAndWheelDropsBits.BUMP_RIGHT) != 0;
            bool wallSensor      = this.sensrosDump.Wall != 0;

            // Brushes
            SolidBrush brushRed = new SolidBrush(Color.Red);
            SolidBrush brushLimeGreen = new SolidBrush(Color.LimeGreen);
            SolidBrush brushBase = new SolidBrush(Color.Black);
            SolidBrush brushWheel = new SolidBrush(Color.Gray);

            // Pens
            Pen penBumperRed = new Pen(brushRed, 5);
            Pen penBumperLimeGreen = new Pen(brushLimeGreen, 5);
            Pen penWallSensor = new Pen(wallSensor ? brushRed : brushLimeGreen, 5);
            penWallSensor.DashStyle = DashStyle.Dash;

            // Shapes sizes.
            Size sizeBase = new Size(300, 300);
            Size sizeSensors = new Size(20, 10);
            Size sizeWheels = new Size(30, 70);

            // Main center.
            Point mainCenter = CreateCenter(new Point(this.pbSCADA.Size), this.pbSCADA.Size);

            // Center of shapes.
            Point centerBase = CreateCenter(new Point(mainCenter.X, mainCenter.Y), sizeBase);
            Point centerCliffLeft = CreateCenter(new Point(mainCenter.X - 100, mainCenter.Y - 70), sizeSensors);
            Point centerCliffCenterLeft = CreateCenter(new Point(mainCenter.X - 50, mainCenter.Y - 120), sizeSensors);
            Point centerCliffCenterRight = CreateCenter(new Point(mainCenter.X + 50, mainCenter.Y - 120), sizeSensors);
            Point centerCliffRight = CreateCenter(new Point(mainCenter.X + 100, mainCenter.Y - 70), sizeSensors);
            Point centerWheelLeft = CreateCenter(new Point(mainCenter.X - 100, mainCenter.Y), sizeWheels);
            Point centerWheelRight = CreateCenter(new Point(mainCenter.X + 100, mainCenter.Y), sizeWheels);

            // Graphics modes.
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Clear background.
            e.Graphics.Clear(Color.White);

            // Base contour.
            e.Graphics.FillEllipse(brushBase, new RectangleF(centerBase, sizeBase));

            // Cliff left.
            e.Graphics.FillRectangle(cliffLeft ? brushRed : brushLimeGreen, new RectangleF(centerCliffLeft, sizeSensors));
            // Cliff center left.
            e.Graphics.FillRectangle(cliffFrontLeft ? brushRed : brushLimeGreen, new RectangleF(centerCliffCenterLeft, sizeSensors));
            // Cliff center right.
            e.Graphics.FillRectangle(cliffFrontRight ? brushRed : brushLimeGreen, new RectangleF(centerCliffCenterRight, sizeSensors));
            // Cliff right.
            e.Graphics.FillRectangle(cliffRight ? brushRed : brushLimeGreen, new RectangleF(centerCliffRight, sizeSensors));

            // Left wheel.
            e.Graphics.FillRectangle(brushWheel, new RectangleF(centerWheelLeft, sizeWheels));
            // Left wheel drop.
            e.Graphics.DrawRectangle(wheelDropLeft ? penBumperRed : penBumperLimeGreen, new Rectangle(centerWheelLeft, sizeWheels));
            // Right wheel.
            e.Graphics.FillRectangle(brushWheel, new RectangleF(centerWheelRight, sizeWheels));
            // Right wheel drop.
            e.Graphics.DrawRectangle(wheelDropRight ? penBumperRed : penBumperLimeGreen, new Rectangle(centerWheelRight, sizeWheels));

            // Draw bumper left.
            e.Graphics.DrawArc(bumperRight ? penBumperRed : penBumperLimeGreen, new RectangleF(centerBase, sizeBase), 270, 90);
            // Draw bumper left.
            e.Graphics.DrawArc(bumperLeft ? penBumperRed : penBumperLimeGreen, new RectangleF(centerBase, sizeBase), 180, 90);

            // Draw wall.
            e.Graphics.DrawLine(penWallSensor, mainCenter.X - 200, mainCenter.Y - 170, mainCenter.X + 200, mainCenter.Y - 170);
            e.Graphics.DrawLine(penWallSensor, mainCenter.X - 200, mainCenter.Y - 180, mainCenter.X + 200, mainCenter.Y - 180);
            e.Graphics.DrawLine(penWallSensor, mainCenter.X - 200, mainCenter.Y - 190, mainCenter.X + 200, mainCenter.Y - 190);
        }

        /// <summary>
        /// Draw the sensors.
        /// </summary>
        private void DrawSCADA()
        {
            if (this.pbSCADA.InvokeRequired)
            {
                this.pbSCADA.BeginInvoke(
                    (MethodInvoker)delegate ()
                    {
                        this.pbSCADA.Refresh();
                    });
            }
            else
            {
                this.pbSCADA.Refresh();
            }
        }

        /// <summary>
        /// Create center point.
        /// </summary>
        /// <param name="center">Center of the object.</param>
        /// <param name="size">Size of the system.</param>
        /// <returns>Center of the coordinate system.</returns>
        private Point CreateCenter(Point center, Size size)
        {
            int x = center.X - (int)(size.Width / 2);
            int y = center.Y - (int)(size.Height / 2);

            return new Point(x, y);
        }


        #endregion

        #region Log

        /// <summary>
        /// Setup log system.
        /// </summary>
        private void SetupLog()
        {
            string logPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Logs");

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            Log.SetLogPath(logPath);
            Log.SetColectionSize(0);
            Log.OnLoggedMessage += Log_OnLoggedMessage;
        }

        /// <summary>
        /// Log Message
        /// </summary>
        /// <param name="message">The message.</param>
        private void LogMessage(string logSource, string messageText, LogMessageTypes messageType, bool endOfLogs = false)
        {
            lock (this.syncLockLogs)
            {
                Log.CreateRecord(logSource, messageText, messageType, endOfLogs);
            }
        }

        private void Log_OnLoggedMessage(object sender, Logger.Events.StringEventArgs e)
        {
            if (this.tbConsole.InvokeRequired)
            {
                this.tbConsole.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.tbConsole.AppendText(e.Message + Environment.NewLine);
                });
            }
            else
            {
                this.tbConsole.AppendText(e.Message + Environment.NewLine);
            }
        }

        #endregion

        #region Robot

        /// <summary>
        /// Search for serial port.
        /// </summary>
        private void SearchForPorts()
        {
            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                string message = "A serial port device was not detected.";
                this.LogMessage("MainForm.SearchForPorts()", message, LogMessageTypes.Warning);
                MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                return;
            }

            this.tsmiConnect.DropDown.Items.Clear();

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
        /// Connect to the robot.
        /// </summary>
        /// <param name="portName"></param>
        private void ConnectToRobot(string portName)
        {
            // Create robot.
            this.robot = new Roomba(new SerialCommunicator(portName));

            // Attach events.
            this.robot.OnMesage += this.robot_OnMesage;

            // Connect to robot.
            this.robot.Connect();

            // Wakeup procedure.
            this.robot.Start();
            this.robot.Start();
            this.robot.Safe();
            this.robot.Start();
            this.robot.Safe();

            // Show connect message.
            if(robot.IsConnected)
            {
                string message = "Robot Connection: Connected@" + portName;
                this.tsslRobotConnection.Text = message;
                this.LogMessage("MainForm.ConnectToRobot()", message, LogMessageTypes.Info);
            }
        }

        /// <summary>
        /// Disconnect from the robot.
        /// </summary>
        private void DisconnectFromRobot()
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.robot.Drive(0, 0);
            this.robot.OnMesage -= this.robot_OnMesage;
            this.robot.Disconnect();

            // Show connect message.
            if (!robot.IsConnected)
            {
                string message = "Robot Connection: Disconnected";
                this.tsslRobotConnection.Text = message;
                this.LogMessage("MainForm.DisconnectFromRobot()", message, LogMessageTypes.Info);
            }
        }

        /// <summary>
        /// On robot message handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void robot_OnMesage(object sender, iRobot.Events.BytesEventArgs e)
        {
            // Get the data.
            byte[] byteData = e.Message;

            // Convert it to sensor structure.
            Struct6 sensros = iRobot.Utils.ByteArrayToStructure<Struct6>(byteData);

            // Dump all the data.
            this.sensrosDump = sensros;

            // Convert the structure to JSON.
            string serialSensors = JsonConvert.SerializeObject(sensros);
            
            // Send text data to the server.
            this.SendTextData(serialSensors);

            // Log the event.
            this.LogMessage("MainForm.robot_OnMesage()", Utils.ToHexText(byteData), LogMessageTypes.Info);

            // Draw the SCADA.
            this.DrawSCADA();


        }
        
        #endregion

        #region Camera

        /// <summary>
        /// Search the for video cameras.
        /// </summary>
        private void SearchForCameras()
        {
            for(;;)
            {
                // Check to see what video inputs we have available.
                this.videoDevices = this.GetDevices();

                if (videoDevices.Length == 0)
                {
                    string message = "A camera device was not detected.\r\nWolud you like to search again?";
                    DialogResult result = MessageBox.Show(message, "", MessageBoxButtons.RetryCancel, MessageBoxIcon.Question);
                    if(result == DialogResult.Cancel)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            
            // Add cameras to the menus.
            this.AddCameras(this.videoDevices, this.tsmiCameraCapture, this.tsmiCaptureeDevice_Click);
        }

        /// <summary>
        /// Connect to the video camera.
        /// </summary>
        /// <param name="monikerString">Moniker string.</param>
        private void ConnecToCamera(string monikerString)
        {
            // Create camera.
            this.videoDevice = new VideoCaptureDevice(monikerString);
            this.videoDevice.NewFrame += new NewFrameEventHandler(this.videoDevice_NewFrame);

            // Stop if other stream was displaying.
            if (this.videoDevice.IsRunning)
            {
                this.videoDevice.Stop();
            }

            // Start the new stream.
            this.videoDevice.Start();

            // Log this event.
            this.LogMessage("MainForm.ConnecToCamera(string monikerString)", "Video Capture: Started", LogMessageTypes.Info);
        }

        /// <summary>
        /// Disconnect from video camera.
        /// </summary>
        private void DisconnectFromCamera()
        {
            if (this.videoDevice == null) return;

            // Stop if other stream was displaying.
            if (this.videoDevice.IsRunning)
            {
                this.videoDevice.Stop();
            }

            this.videoDevice.NewFrame -= new NewFrameEventHandler(this.videoDevice_NewFrame);
            this.videoDevice = null;

            // Log this event.
            this.LogMessage("MainForm.ConnecToCamera(string monikerString)", "Video Capture: Stopped", LogMessageTypes.Info);
        }

        /// <summary>
        /// Get list of all available devices on the PC.
        /// </summary>
        /// <returns>Video device.</returns>
        private VideoDevice[] GetDevices()
        {
            //Set up the capture method 
            //-> Find systems cameras with DirectShow.Net DLL, thanks to Charles Lorette.
            //DsDevice[] systemCamereas = DsDevice.GetDevicesOfCat(AForge.Video.DirectShow.FilterCategory.VideoInputDevice);

            // Enumerate video devices
            FilterInfoCollection systemCamereas = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            VideoDevice[] videoDevices = new VideoDevice[systemCamereas.Count];

            for (int index = 0; index < systemCamereas.Count; index++)
            {
                videoDevices[index] = new VideoDevice(index, systemCamereas[index].Name, systemCamereas[index].MonikerString);
            }

            return videoDevices;
        }

        /// <summary>
        /// Add video devices to the tool strip menu.
        /// </summary>
        /// <param name="videoDevices">List of cameras.</param>
        /// <param name="menu">Menu item.</param>
        /// <param name="callback">Callback</param>
        private void AddCameras(VideoDevice[] videoDevices, ToolStripMenuItem menu, EventHandler callback)
        {
            if (videoDevices.Length == 0)
            {
                return;
            }

            // Clear the list.
            menu.DropDown.Items.Clear();

            //
            ToolStripMenuItem stopItem = new ToolStripMenuItem();
            stopItem.Text = "Stop";
            stopItem.Enabled = true;
            stopItem.Checked = false;
            stopItem.Click += this.tsmiStopCaptureeDevice_Click;
            menu.DropDown.Items.Add(stopItem);

            // Add cameras.
            foreach (VideoDevice device in videoDevices)
            {
                // Store the each retrieved available capture device into the MenuItems.
                ToolStripMenuItem cameraItem = new ToolStripMenuItem();

                cameraItem.Text = String.Format("{0:D2} / {1}", device.Index, device.Name);
                cameraItem.Tag = device;
                cameraItem.Enabled = true;
                cameraItem.Checked = false;
                cameraItem.Click += callback;

                menu.DropDown.Items.Add(cameraItem);
            }

        }

        /// <summary>
        /// New frame handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void videoDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            lock (this.syncLockVideo)
            {
                // Dispose last frame.
                if (this.capturedImage != null)
                {
                    this.capturedImage.Dispose();
                    this.capturedImage = null;
                }

                // Clone the content.
                this.capturedImage = (Bitmap)eventArgs.Frame.Clone();

                // Exit there is a problem with data cloning.
                if (this.capturedImage == null)
                {
                    return;
                }

                if ((this.pbCamera.Size.Width > 1 && this.pbCamera.Size.Height > 1) && this.WindowState != FormWindowState.Minimized)
                {
                    this.ShowImage(Utils.ResizeImage(this.capturedImage, this.pbCamera.Size));
                }
            }
        }

        /// <summary>
        /// Process rocks in the image.
        /// </summary>
        private void ShowImage(Bitmap image)
        {
            // Display image.
            if (this.pbCamera.InvokeRequired)
            {
                this.pbCamera.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.pbCamera.Image = image;
                });
            }
            else
            {
                this.pbCamera.Image = image;
            }
        }

        #endregion

        #region Send Image Timer
        
        /// <summary>
        /// Start frame send timer.
        /// </summary>
        private void StartSendImageTimer()
        {
            if (this.sendImageTimer == null)
            {
                this.sendImageTimer = new System.Windows.Forms.Timer();
                this.sendImageTimer.Stop();
                this.sendImageTimer.Tick += SendImageTimer_Tick;
                this.sendImageTimer.Interval = 1000;
            }

            if(!this.sendImageTimer.Enabled)
            {
                this.sendImageTimer.Start();
            }

        }

        /// <summary>
        /// Stop frame send timer.
        /// </summary>
        private void StopSendImageTimer()
        {
            if (this.sendImageTimer == null) return;

            if (this.sendImageTimer.Enabled)
            {
                this.sendImageTimer.Stop();
            }

            this.sendImageTimer.Stop();
        }

        /// <summary>
        /// Send frame handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendImageTimer_Tick(object sender, EventArgs e)
        {
            lock (this.syncLockVideo)
            {
                if (this.capturedImage == null) return;
                this.SendImageData(this.capturedImage);
            }
        }

        #endregion

        #region Send Data Timer

        /// <summary>
        /// Start sensor data send timer.
        /// </summary>
        private void StartSendDataTimer()
        {
            if (this.sendDataTimer == null)
            {
                this.sendDataTimer = new System.Windows.Forms.Timer();
                this.sendDataTimer.Stop();
                this.sendDataTimer.Tick += SendDataTimer_Tick;
                this.sendDataTimer.Interval = Properties.Settings.Default.UpdateInterval;
            }

            if (!this.sendDataTimer.Enabled)
            {
                this.sendDataTimer.Start();
            }
        }

        /// <summary>
        /// Stop sensor data send timer.
        /// </summary>
        private void StopSendDataTimer()
        {
            if (this.sendDataTimer == null) return;

            if (this.sendDataTimer.Enabled)
            {
                this.sendDataTimer.Stop();
            }

            this.sendDataTimer.Stop();
        }

        /// <summary>
        /// Sensor data send handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendDataTimer_Tick(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.robot.Sensors(SensorPacketsIDs.Group6);
        }

        #endregion

        #region Data Connector

        /// <summary>
        /// Connect to broker server.
        /// </summary>
        private void ConnectToServer()
        {
            try
            {
                this.remoteController = new RemoteController(Properties.Settings.Default.BrokerHost);
                    

                this.remoteController.OnRequest += mqttCommunicator_OnRequest;
                this.remoteController.Connect();
                this.remoteController.SubscribeToInputTopic(new string[] { Properties.Settings.Default.MqttInputTopic }, new byte[] { 0 });

                string message = "MQTT Connection: " + this.remoteController.IsConnected.ToString();
                this.LogMessage("MainForm.ConnectToServer()", message, LogMessageTypes.Info);
                this.tsslMQTTConnection.Text = message;
            }
            catch (Exception exception)
            {
                this.LogMessage("MainForm.ConnectToServer()", exception.ToString(), LogMessageTypes.Error);
            }
        }

        /// <summary>
        /// Disconnect from broker server.
        /// </summary>
        private void DisconnectFromServer()
        {
            try
            {
                if (this.remoteController != null && this.remoteController.IsConnected)
                {
                    this.remoteController.OnRequest -= mqttCommunicator_OnRequest;
                    this.remoteController.Disconnect();

                    string message = "MQTT Connection: " + this.remoteController.IsConnected.ToString();
                    this.LogMessage("MainForm.DisconnectFromServer()", message, LogMessageTypes.Info);

                    this.tsslMQTTConnection.Text = message;
                }
            }
            catch (Exception exception)
            {
                this.LogMessage("MainForm.DisconnectFromServer()", exception.ToString(), LogMessageTypes.Error);
            }
        }

        /// <summary>
        /// Send image data.
        /// </summary>
        /// <param name="image"></param>
        private void SendImageData(Bitmap image)
        {
            if (this.remoteController == null || !this.remoteController.IsConnected) return;
            if (image == null) return;
            try
            {
                this.remoteController.SendImageData(Properties.Settings.Default.MqttImageTopic, Utils.ResizeImage(image, Properties.Settings.Default.ImageSize));
            }
            catch (Exception exception)
            {
                this.LogMessage("MainForm.SendImageData(Bitmap image)", exception.ToString(), LogMessageTypes.Error);
            }
        }

        /// <summary>
        /// Send data from robot.
        /// </summary>
        /// <param name="image"></param>
        private void SendTextData(string text)
        {
            if (this.remoteController == null || !this.remoteController.IsConnected) return;
            if (text == null) return;
            try
            {
                this.remoteController.SendTextData(Properties.Settings.Default.MqttOutputTopic, text);
            }
            catch (Exception exception)
            {
                this.LogMessage("MainForm.SendTextData(string text)", exception.ToString(), LogMessageTypes.Error);
            }
        }

        /// <summary>
        /// MQTT connector message handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mqttCommunicator_OnRequest(object sender, RequestMessageEventArgs e)
        {
            byte[] command = e.Request.ToCommand();

            this.LogMessage("MainForm.mqttCommunicator_OnMessage(object sender, RequestMessageEventArgs e)", Utils.ToHexText(command), LogMessageTypes.Info);

            if (this.robot == null || !this.robot.IsConnected) return;

            this.robot.Command(command);
        }

        #endregion

    }
}
