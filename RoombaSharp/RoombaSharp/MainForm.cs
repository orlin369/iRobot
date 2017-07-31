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

using AForge.Video;
using AForge.Video.DirectShow;

using Newtonsoft.Json;

using iRobot;
using iRobot.Data;
using iRobot.Events;
using iRobot.Communicators;

using RoombaSharp.Video;
using iRobotRemoteControl;

namespace RoombaSharp
{
    public partial class MainForm : Form
    {

        #region Variables

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
        /// Log messages sync lock object.
        /// </summary>
        private object syncLockLogs = new object();

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

        /// <summary>
        /// Send image timer.
        /// </summary>
        private System.Windows.Forms.Timer sendImageTimer;

        /// <summary>
        /// Send image timer.
        /// </summary>
        private System.Windows.Forms.Timer sendDataTimer;


        /// <summary>
        /// Connector
        /// </summary>
        private RemoteController remoteController;
                
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

        /// <summary>
        /// Log Message
        /// </summary>
        /// <param name="message">The message.</param>
        private void LogMessage(string message)
        {
            lock (this.syncLockLogs)
            {
                string dataTime = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");

                if (this.tbConsole.InvokeRequired)
                {
                    this.tbConsole.BeginInvoke((MethodInvoker)delegate ()
                    {
                        this.tbConsole.AppendText(dataTime + " -> " + message + Environment.NewLine);
                    });
                }
                else
                {
                    this.tbConsole.AppendText(dataTime + " -> " + message + Environment.NewLine);
                }
            }
        }

        #endregion

        #region Main Form

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SearchForPorts();
            this.SearchForCameras();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.StopSendImageTimer();
            this.StopSendDataTimer();

            this.DisconnectFromRobot();
            this.DisconnectFromCamera();

            this.DisconnectFromServer();
        }

        #endregion

        #region Tool Strip Menu Items

        #region Robot

        private void tsmiConnect_Click(object sender, EventArgs e)
        {
            this.DisconnectFromRobot();
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            this.ConnectToRobot(item.Text);
        }

        #region Beep

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

        private void tsmiLedCheckRobot_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedCheckRobot.Checked = !this.tsmiLedCheckRobot.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }


        private void tsmiLedSpot_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedSpot.Checked = !this.tsmiLedSpot.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        private void tsmiLedDock_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedDock.Checked = !this.tsmiLedDock.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

        private void tsmiLedDirtDetect_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiLedDirtDetect.Checked = !this.tsmiLedDirtDetect.Checked;

            this.robot.LEDs(this.tsmiLedCheckRobot.Checked, this.tsmiLedDock.Checked, this.tsmiLedSpot.Checked, this.tsmiLedDirtDetect.Checked, this.cleanLedColor, this.cleanLedIntensity);
        }

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

        private void tsmiMainBrush_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiMainBrush.Checked = !this.tsmiMainBrush.Checked;

            this.robot.Motors(this.tsmiMainBrush.Checked, this.tsmiVacuum.Checked, this.tsmiSideBrush.Checked);
        }

        private void tsmiVacuum_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiVacuum.Checked = !this.tsmiVacuum.Checked;

            this.robot.Motors(this.tsmiMainBrush.Checked, this.tsmiVacuum.Checked, this.tsmiSideBrush.Checked);
        }

        private void tsmiSideBrush_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.tsmiSideBrush.Checked = !this.tsmiSideBrush.Checked;

            this.robot.Motors(this.tsmiMainBrush.Checked, this.tsmiVacuum.Checked, this.tsmiSideBrush.Checked);
        }

        #endregion

        #region Buttons

        private void tsmiBtnClean_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Clean();
        }

        private void tsmiBtnSpot_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Spot();
        }

        private void tsmiBtnDock_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.ForceSeekingDock();
        }

        private void tsmiBtnPower_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Power();
        }

        private void tsmiBtnMax_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Max();
        }

        #endregion

        #region LED Display

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

        private void shutdownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            // Shutdown the display.
            this.robot.DigitLEDsRawOff();
        }

        #endregion

        #region Sensors

        private void tsmiParamettersGroup6_Click(object sender, EventArgs e)
        {
            // Create the worker thread.
            Thread worker = new Thread(
                new ThreadStart(
                    delegate ()
                    {
                        if (this.robot == null || !this.robot.IsConnected) return;

                        int waitTime = 1000;

                        this.LogMessage("Group 6");
                        this.robot.Sensors(SensorPacketsIDs.Group6);
                        Thread.Sleep(waitTime);
                    }
                )
            );

            // Start the Melodie thread.
            worker.Start();
        }

        #endregion

        #endregion

        private void tsmiSettings_Click(object sender, EventArgs e)
        {
            using (Settings.SettingsForm sf = new Settings.SettingsForm())
            {
                sf.ShowDialog();
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region Camera

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

        private void tsmiCaptureeDevice_Click(object sender, EventArgs e)
        {
            // Create instance of caller.
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            // Get device.
            VideoDevice videoDevice = (VideoDevice)item.Tag;

            // Uncheck all cameras.
            foreach (ToolStripMenuItem mItem in this.tsmiCameraCapture.DropDown.Items)
            {
                item.Checked = false;
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
            this.StartSendImageTimer();
        }

        private void tsmiServerDisconnect_Click(object sender, EventArgs e)
        {
            this.StopSendImageTimer();
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

            this.LogMessage("Send test data to the server.");
        }

        private void tsmiEnableUpdateSensorsData_Click(object sender, EventArgs e)
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

        #endregion

        #endregion

        #region Buttons

        private void btnRight_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)this.trbSpeed.Value, (short)-this.trbRadius.Value);
        }

        private void btnLeft_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)this.trbSpeed.Value, (short)this.trbRadius.Value);
        }

        private void btnUp_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)this.trbSpeed.Value, 0);
        }

        private void btnDown_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive((short)-this.trbSpeed.Value, 0);
        }
        
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        private void btnUp_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        private void btnRight_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        private void btnDown_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
        }

        private void btnLeft_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;
            this.robot.Drive(0, 0);
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

        #region Robot

        /// <summary>
        /// Search for serial port.
        /// </summary>
        private void SearchForPorts()
        {
            this.tsmiConnect.DropDown.Items.Clear();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                string message = "A serial port device was not detected.";
                this.LogMessage(message);
                MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
                this.LogMessage(message);
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
                this.LogMessage(message);
            }
        }

        /// <summary>
        /// On robot message handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void robot_OnMesage(object sender, BytesEventArgs e)
        {
            // Get the data.
            byte[] byteData = e.Message;

            // Convert it to sensor structure.
            Struct6 sensros = Utils.ByteArrayToStructure<Struct6>(byteData);

            // 
            //string wheelDrops = sensros.BumpersAndWheelDrops.ToString("X2");
            //// Log it.
            //this.LogMessage("Robot: " + wheelDrops);

            // Convert it to hex text.
            //string text = Utils.ToHexText(byteData);
            //// Log it.
            //this.LogMessage("Robot: " + text);

            //
            string serialSensors = JsonConvert.SerializeObject(sensros);
            
            //
            this.SendTextData(serialSensors);

            //
            this.LogMessage("Send data to the server.");
        }

        #endregion

        #region Camera

        private void SearchForCameras()
        {
            // Check to see what video inputs we have available.
            this.videoDevices = this.GetDevices();

            if (videoDevices.Length == 0)
            {
                string message = "A camera device was not detected.";
                this.LogMessage(message);
                MessageBox.Show(message, "",  MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }

            // Add cameras to the menus.
            this.AddCameras(this.videoDevices, this.tsmiCameraCapture, this.tsmiCaptureeDevice_Click);
        }

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
            this.LogMessage("Video Capture: Started");
        }

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
            this.LogMessage("Video Capture: Stopped");
        }

        /// <summary>
        /// Get list of all available devices on the PC.
        /// </summary>
        /// <returns></returns>
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

                if ((this.pbMain.Size.Width > 1 && this.pbMain.Size.Height > 1) && this.WindowState != FormWindowState.Minimized)
                {
                    this.ShowImage(Utils.ResizeImage(this.capturedImage, this.pbMain.Size));
                }
            }
        }

        /// <summary>
        /// Process rocks in the image.
        /// </summary>
        private void ShowImage(Bitmap image)
        {
            // Display image.
            if (this.pbMain.InvokeRequired)
            {
                this.pbMain.BeginInvoke((MethodInvoker)delegate ()
                {
                    this.pbMain.Image = image;
                });
            }
            else
            {
                this.pbMain.Image = image;
            }
        }

        #endregion

        #region Send Image Timer

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

        private void StopSendImageTimer()
        {
            if (this.sendImageTimer == null) return;

            if (this.sendImageTimer.Enabled)
            {
                this.sendImageTimer.Stop();
            }

            this.sendImageTimer.Stop();
        }

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

        private void StopSendDataTimer()
        {
            if (this.sendDataTimer == null) return;

            if (this.sendDataTimer.Enabled)
            {
                this.sendDataTimer.Stop();
            }

            this.sendDataTimer.Stop();
        }

        private void SendDataTimer_Tick(object sender, EventArgs e)
        {
            if (this.robot == null || !this.robot.IsConnected) return;

            this.robot.Sensors(SensorPacketsIDs.Group6);
        }

        #endregion

        #region Data Connector

        /// <summary>
        /// Connect vision system.
        /// </summary>
        private void ConnectToServer()
        {
            try
            {
                this.remoteController = new RemoteController(Properties.Settings.Default.BrokerHost);
                    

                this.remoteController.OnMessage += mqttCommunicator_OnMessage;
                this.remoteController.Connect();
                this.remoteController.SubscribeToInputTopic(new string[] { Properties.Settings.Default.MqttInputTopic }, new byte[] { 0 });

                string message = "MQTT Connection: " + this.remoteController.IsConnected.ToString();
                this.LogMessage(message);
                this.tsslMQTTConnection.Text = message;
            }
            catch (Exception exception)
            {
                this.LogMessage(exception.ToString());
            }
        }

        /// <summary>
        /// Disconnect vision system.
        /// </summary>
        private void DisconnectFromServer()
        {
            try
            {
                if (this.remoteController != null && this.remoteController.IsConnected)
                {
                    this.remoteController.OnMessage -= mqttCommunicator_OnMessage;
                    this.remoteController.Disconnect();

                    string message = "MQTT Connection: " + this.remoteController.IsConnected.ToString();
                    this.LogMessage(message);
                    this.tsslMQTTConnection.Text = message;
                }
            }
            catch (Exception exception)
            {
                this.LogMessage(exception.ToString());
            }
        }

        /// <summary>
        /// Send image.
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
            catch
            { }
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
            catch
            { }
        }

        /// <summary>
        /// MQTT connector message handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mqttCommunicator_OnMessage(object sender, iRobotRemoteControl.Events.BytesEventArgs e)
        {
            string text = Encoding.ASCII.GetString(e.Message);
            this.LogMessage(text);
        }

        #endregion

    }
}
