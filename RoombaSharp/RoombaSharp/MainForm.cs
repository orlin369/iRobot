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

using iRobot.RoombaSharp;
using iRobot.Events;

using Leap;

using Emgu.CV;

using AForge.Video.DirectShow;

using Video;
using Emgu.CV.Structure;
using Emgu.CV.UI;

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

        /// <summary>
        /// Camera 1.
        /// </summary>
        VideoCaptureDevice videoDevice = null;

        private VideoDevice[] videoDevices;

        private Bitmap inputImage;

        private Bitmap outputImage;

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

            // Check to see what video inputs we have available.
            this.videoDevices = this.GetDevices();

            if (videoDevices.Length == 0)
            {
                DialogResult res = MessageBox.Show("A camera device was not detected. Do you want to exit?", "",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Yes)
                {
                    Application.Exit();
                }
            }

            // Add cameras to the menus.
            this.AddCameras(this.videoDevices, this.captureToolStripMenuItem, this.mItCaptureeDevice_Click);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DisconnectFromRobot();
            this.DisconnectFromLeapMotion();

            // Stop if other stream was displaying.
            if (this.videoDevice.IsRunning)
            {
                this.videoDevice.Stop();
            }

            this.videoDevice.NewFrame -= VideoDevice_NewFrame;

            this.videoDevice = null;
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

        /// <summary>
        /// Get list of all available devices on the PC.
        /// </summary>
        /// <returns></returns>
        private VideoDevice[] GetDevices()
        {
            //Set up the capture method 
            //-> Find systems cameras with DirectShow.Net dll, thanks to Charles Lorette.
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

            menu.DropDown.Items.Clear();

            foreach (VideoDevice device in videoDevices)
            {
                // Store the each retrieved available capture device into the MenuItems.
                ToolStripMenuItem mItem = new ToolStripMenuItem();

                mItem.Text = String.Format("{0:D2} / {1}", device.Index, device.Name);
                mItem.Tag = device;
                mItem.Enabled = true;
                mItem.Checked = false;

                //TODO: Grozno
                mItem.Click += callback;

                menu.DropDown.Items.Add(mItem);
            }
        }

        /// <summary>
        /// Fit image from one size to input size.
        /// </summary>
        /// <param name="image">Input image.</param>
        /// <param name="size">New size.</param>
        /// <returns>Resized image.</returns>
        public Bitmap FitImage(Bitmap image, Size size)
        {
            var ratioX = (double)size.Width / image.Width;
            var ratioY = (double)size.Height / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);

            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            return newImage;
        }

        #endregion

        #region Image Processing

        /// <summary>
        /// Process rocks in the image.
        /// </summary>
        private void ProcessSand(Bitmap inputImage)
        {
            //Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(this.inputImage);
            Emgu.CV.Image<Bgr, byte> inpImg = new Emgu.CV.Image<Bgr, byte>(inputImage);

            //FF2038
            //Emgu.CV.Image<Gray, byte> sand = inpImg.InRange(new Bgr(20, 20, 255), new Bgr(150, 150, 255));
            //TODO: To check if we need mask?
            //water = water.Add(mask); 
            //water._Dilate(1);


            using (Image<Hsv, byte> hsv = inpImg.Convert<Hsv, byte>())
            {
                // 2. Obtain the 3 channels (hue, saturation and value) that compose the HSV image
                Image<Gray, byte>[] channels = hsv.Split();

                Image<Gray, byte> sand = channels[0].InRange(new Gray(0), new Gray(65)).ThresholdBinaryInv(new Gray(127), new Gray(255)).Erode(3);

                //ImageViewer.Show(sand, "Sand Mask");

                // Create the blobs.
                Emgu.CV.Cvb.CvBlobs blobs = new Emgu.CV.Cvb.CvBlobs();
                // Create blob detector.
                Emgu.CV.Cvb.CvBlobDetector dtk = new Emgu.CV.Cvb.CvBlobDetector();
                // Detect blobs.
                uint state = dtk.Detect(sand, blobs);

                foreach (Emgu.CV.Cvb.CvBlob blob in blobs.Values)
                {
                    //Console.WriteLine("Center: X:{0:F3} Y:{1:F3}", blob.Centroid.X, blob.Centroid.Y);
                    //Console.WriteLine("{0}", blob.Area);
                    if (blob.Area >= 50 && blob.Area < 700)
                    {
                        //Console.WriteLine("{0}", blob.Area);
                        inpImg.Draw(new CircleF(blob.Centroid, 5), new Bgr(Color.Red), 2);
                        inpImg.Draw(blob.BoundingBox, new Bgr(Color.Blue), 2);
                    }
                }
            }

            //return;



            //ImageViewer.Show(sand, "Sand Mask");
            //return;

            if (this.outputImage != null) this.outputImage.Dispose();
            // Dump the image.
            this.outputImage = inpImg.ToBitmap();
            // Show the new image.
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

        #region Tool Strip Menu Items

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

        private void mItCaptureeDevice_Click(object sender, EventArgs e)
        {
            // Create instance of caller.
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            // Display text.
            //this.pbMain.Tag = item.Text;

            // Get device.
            VideoDevice videoDevice = (VideoDevice)item.Tag;

            foreach(ToolStripMenuItem mItem in this.captureToolStripMenuItem.DropDown.Items)
            {
                item.Checked = false;
            }

            item.Checked = true;

            try
            {
                // Create camera.
                
                this.videoDevice = new VideoCaptureDevice(videoDevice.MonikerString);
                this.videoDevice.NewFrame += VideoDevice_NewFrame;

                // Stop if other stream was displaying.
                if (this.videoDevice.IsRunning)
                {
                    this.videoDevice.Stop();
                }

                // Start the new stream.
                this.videoDevice.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ConnectToLeapMotion();
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DisconnectFromLeapMotion();
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
        
        #region Leapmotion frame grabber

        private void listener_FrameGrabed(object sender, ControlerArg e)
        {
            //e.Controller.
            this.OnFrame(e.Controller);
        }

        #endregion

        #region Camera

        private void VideoDevice_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                if (this.inputImage != null) this.inputImage.Dispose();

                this.inputImage = (Bitmap)eventArgs.Frame.Clone();// clone the bitmap

                if (this.inputImage == null) return;

                this.ProcessSand((Bitmap)this.inputImage.Clone());
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }

        #endregion
    }
}
