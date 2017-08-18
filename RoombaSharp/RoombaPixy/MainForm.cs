using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using AForge.Video;
using AForge.Video.DirectShow;
using iRobot;
using iRobot.Communicators;
using iRobot.Data;
using iRobot.Events;
using RoombaSharp.Video;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoombaPixy
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

        private Bitmap image = null;

        private BlobCounter blobCounter = new BlobCounter();

        private Blob[] blobs;

        private Font drawFont;

        private ColorFiltering colorFilter = new ColorFiltering(new IntRange(0, 30), new IntRange(150, 255), new IntRange(0, 30));

        private Invert invertFilter = new Invert();

        #endregion

        #region Main Form

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            this.drawFont = new Font(FontFamily.GenericSansSerif, 15.0F, FontStyle.Bold);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SearchForPorts();
            this.SearchForCameras();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DisconnectFromRobot();
            this.DisconnectFromCamera();
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
            if (robot.IsConnected)
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
            Struct6 sensros = iRobot.Utils.ByteArrayToStructure<Struct6>(byteData);

            // Dump all the data.
            this.sensrosDump = sensros;

            // Convert the structure to JSON.
            //string serialSensors = JsonConvert.SerializeObject(sensros);

            // Send text data to the server.
            //this.SendTextData(serialSensors);

            // Log the event.
            this.LogMessage("Send data to the server.");

            // Draw the SCADA.
            //this.DrawSCADA();
        }

        #endregion

        #region Camera

        /// <summary>
        /// Search the for video cameras.
        /// </summary>
        private void SearchForCameras()
        {
            // Check to see what video inputs we have available.
            this.videoDevices = this.GetDevices();

            if (videoDevices.Length == 0)
            {
                string message = "A camera device was not detected.";
                this.LogMessage(message);
                MessageBox.Show(message, "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            this.LogMessage("Video Capture: Started");
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
            this.LogMessage("Video Capture: Stopped");
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

                // Clone the resized content.
                if ((this.pbCamera.Size.Width > 1 && this.pbCamera.Size.Height > 1) && this.WindowState != FormWindowState.Minimized)
                {
                    this.capturedImage = Utils.ResizeImage((Bitmap)eventArgs.Frame.Clone(), this.pbCamera.Size);
                }

                // Exit there is a problem with data cloning.
                if (this.capturedImage == null)
                {
                    return;
                }

                this.ProcessImage(this.capturedImage);
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

            this.image = image;
        }

        #endregion

        #region Tool Strip Menu Items

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

            // Connect to camera.
            this.ConnecToCamera(videoDevice.MonikerString);
        }

        #endregion

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

        #endregion

        #region pbCamera

        private void pbCamera_Paint(object sender, PaintEventArgs e)
        {
            if (this.blobs == null) return;

            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

            foreach (Blob blob in this.blobs)
            {
                Rectangle currRect = blob.Rectangle;
                if (Utils.IfNotLaser(currRect.Size)) continue;

                Rectangle rect = Utils.OffsetRectangle(currRect, this.pbCamera.Image.Size, this.pbCamera.Size);
                e.Graphics.DrawRectangle(Pens.Red, rect);
                e.Graphics.DrawString(blob.ID.ToString(), drawFont, Brushes.Red, new System.Drawing.Point(rect.Location.X + 20, rect.Location.Y + 20));
            }
        }

        #endregion

        private void ProcessImage(Bitmap processedImage)
        {
            // Make a copy.
            this.image = AForge.Imaging.Image.Clone(processedImage, PixelFormat.Format24bppRgb);

            // Apply color filter.
            Bitmap filteredImage = this.colorFilter.Apply(this.image);
            // filteredImage = invertFilter.Apply(filteredImage);

            // Process blobs.
            this.blobCounter.ProcessImage(filteredImage);

            // Get blobs.
            this.blobs = blobCounter.GetObjectsInformation();

            // Draw image.
            this.ShowImage(filteredImage);
        }

        private void pbCamera_MouseDown(object sender, MouseEventArgs e)
        {
            Color color = this.image.GetPixel(e.X, e.Y);
            tbConsole.AppendText($"Red: {color.R} Green: {color.G} Blue: {color.B}");
        }

        private static int counter = 1;

        private void tsmiSnap_Click(object sender, EventArgs e)
        {
            this.image.Save("Image.png" + counter);

            counter++;
        }
    }
}
