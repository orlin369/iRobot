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

namespace RoombaSharp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRobot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBeep = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBeepTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLEDs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedCheckRobot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedDock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedSpot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedDirtDetect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedClean = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedCleanOff = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedCleanGreen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiLedCleanRed = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMotors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMainBrush = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVacuum = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSideBrush = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiButtons = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBtnClean = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSpot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBtnDock = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBtnPower = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBtnMax = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisplay = new System.Windows.Forms.ToolStripMenuItem();
            this.setToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisplayShutdown = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDisplayTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSensors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiParamettersGroup0 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiParamettersGroup1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiParamettersGroup2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiParamettersGroup3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiParamettersGroup4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiParamettersGroup5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSensorsTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCameraCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiStopCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMqttServer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMqttConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMqttDisconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbpMotion = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.pnlManualControl = new System.Windows.Forms.Panel();
            this.lblHandPosition = new System.Windows.Forms.Label();
            this.lblRadius = new System.Windows.Forms.Label();
            this.trbRadius = new System.Windows.Forms.TrackBar();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trbSpeed = new System.Windows.Forms.TrackBar();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.tbpCamera = new System.Windows.Forms.TabPage();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.ssMain = new System.Windows.Forms.StatusStrip();
            this.tsslRobotConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslMQTTConnection = new System.Windows.Forms.ToolStripStatusLabel();
            this.msMain.SuspendLayout();
            this.tbcMain.SuspendLayout();
            this.tbpMotion.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.pnlManualControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).BeginInit();
            this.tbpCamera.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.ssMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiRobot,
            this.tsmiCameraCapture,
            this.tsmiMqttServer});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(1056, 24);
            this.msMain.TabIndex = 16;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator2,
            this.tsmiSettings,
            this.toolStripSeparator1,
            this.tsmiExit});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(37, 20);
            this.tsmiFile.Text = "File";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(113, 6);
            // 
            // tsmiSettings
            // 
            this.tsmiSettings.Name = "tsmiSettings";
            this.tsmiSettings.Size = new System.Drawing.Size(116, 22);
            this.tsmiSettings.Text = "Settings";
            this.tsmiSettings.Click += new System.EventHandler(this.tsmiSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(113, 6);
            // 
            // tsmiExit
            // 
            this.tsmiExit.Name = "tsmiExit";
            this.tsmiExit.Size = new System.Drawing.Size(116, 22);
            this.tsmiExit.Text = "Exit";
            this.tsmiExit.Click += new System.EventHandler(this.tsmiExit_Click);
            // 
            // tsmiRobot
            // 
            this.tsmiRobot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConnect,
            this.tsmiBeep,
            this.tsmiLEDs,
            this.tsmiMotors,
            this.tsmiButtons,
            this.tsmiDisplay,
            this.tsmiSensors});
            this.tsmiRobot.Name = "tsmiRobot";
            this.tsmiRobot.Size = new System.Drawing.Size(51, 20);
            this.tsmiRobot.Text = "Robot";
            // 
            // tsmiConnect
            // 
            this.tsmiConnect.Name = "tsmiConnect";
            this.tsmiConnect.Size = new System.Drawing.Size(152, 22);
            this.tsmiConnect.Text = "Connect";
            this.tsmiConnect.Click += new System.EventHandler(this.tsmiConnect_Click);
            // 
            // tsmiBeep
            // 
            this.tsmiBeep.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBeepTest});
            this.tsmiBeep.Name = "tsmiBeep";
            this.tsmiBeep.Size = new System.Drawing.Size(152, 22);
            this.tsmiBeep.Text = "Beep";
            // 
            // tsmiBeepTest
            // 
            this.tsmiBeepTest.Name = "tsmiBeepTest";
            this.tsmiBeepTest.Size = new System.Drawing.Size(152, 22);
            this.tsmiBeepTest.Text = "Test";
            this.tsmiBeepTest.Click += new System.EventHandler(this.tsmiBeepTest_Click);
            // 
            // tsmiLEDs
            // 
            this.tsmiLEDs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLedCheckRobot,
            this.tsmiLedDock,
            this.tsmiLedSpot,
            this.tsmiLedDirtDetect,
            this.tsmiLedClean});
            this.tsmiLEDs.Name = "tsmiLEDs";
            this.tsmiLEDs.Size = new System.Drawing.Size(152, 22);
            this.tsmiLEDs.Text = "LEDs";
            // 
            // tsmiLedCheckRobot
            // 
            this.tsmiLedCheckRobot.Name = "tsmiLedCheckRobot";
            this.tsmiLedCheckRobot.Size = new System.Drawing.Size(142, 22);
            this.tsmiLedCheckRobot.Text = "Check Robot";
            this.tsmiLedCheckRobot.Click += new System.EventHandler(this.tsmiLedCheckRobot_Click);
            // 
            // tsmiLedDock
            // 
            this.tsmiLedDock.Name = "tsmiLedDock";
            this.tsmiLedDock.Size = new System.Drawing.Size(142, 22);
            this.tsmiLedDock.Text = "Dock";
            this.tsmiLedDock.Click += new System.EventHandler(this.tsmiLedDock_Click);
            // 
            // tsmiLedSpot
            // 
            this.tsmiLedSpot.Name = "tsmiLedSpot";
            this.tsmiLedSpot.Size = new System.Drawing.Size(142, 22);
            this.tsmiLedSpot.Text = "Spot";
            this.tsmiLedSpot.Click += new System.EventHandler(this.tsmiLedSpot_Click);
            // 
            // tsmiLedDirtDetect
            // 
            this.tsmiLedDirtDetect.Name = "tsmiLedDirtDetect";
            this.tsmiLedDirtDetect.Size = new System.Drawing.Size(142, 22);
            this.tsmiLedDirtDetect.Text = "Dirt Detect";
            this.tsmiLedDirtDetect.Click += new System.EventHandler(this.tsmiLedDirtDetect_Click);
            // 
            // tsmiLedClean
            // 
            this.tsmiLedClean.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiLedCleanOff,
            this.tsmiLedCleanGreen,
            this.tsmiLedCleanRed});
            this.tsmiLedClean.Name = "tsmiLedClean";
            this.tsmiLedClean.Size = new System.Drawing.Size(142, 22);
            this.tsmiLedClean.Text = "Clean";
            // 
            // tsmiLedCleanOff
            // 
            this.tsmiLedCleanOff.Name = "tsmiLedCleanOff";
            this.tsmiLedCleanOff.Size = new System.Drawing.Size(105, 22);
            this.tsmiLedCleanOff.Text = "OFF";
            this.tsmiLedCleanOff.Click += new System.EventHandler(this.tsmiLedCleanOff_Click);
            // 
            // tsmiLedCleanGreen
            // 
            this.tsmiLedCleanGreen.Name = "tsmiLedCleanGreen";
            this.tsmiLedCleanGreen.Size = new System.Drawing.Size(105, 22);
            this.tsmiLedCleanGreen.Text = "Green";
            this.tsmiLedCleanGreen.Click += new System.EventHandler(this.tsmiLedCleanGreen_Click);
            // 
            // tsmiLedCleanRed
            // 
            this.tsmiLedCleanRed.Name = "tsmiLedCleanRed";
            this.tsmiLedCleanRed.Size = new System.Drawing.Size(105, 22);
            this.tsmiLedCleanRed.Text = "Red";
            this.tsmiLedCleanRed.Click += new System.EventHandler(this.tsmiLedCleanRed_Click);
            // 
            // tsmiMotors
            // 
            this.tsmiMotors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMainBrush,
            this.tsmiVacuum,
            this.tsmiSideBrush});
            this.tsmiMotors.Name = "tsmiMotors";
            this.tsmiMotors.Size = new System.Drawing.Size(152, 22);
            this.tsmiMotors.Text = "Motors";
            // 
            // tsmiMainBrush
            // 
            this.tsmiMainBrush.Name = "tsmiMainBrush";
            this.tsmiMainBrush.Size = new System.Drawing.Size(134, 22);
            this.tsmiMainBrush.Text = "Main Brush";
            this.tsmiMainBrush.Click += new System.EventHandler(this.tsmiMainBrush_Click);
            // 
            // tsmiVacuum
            // 
            this.tsmiVacuum.Name = "tsmiVacuum";
            this.tsmiVacuum.Size = new System.Drawing.Size(134, 22);
            this.tsmiVacuum.Text = "Vacuum";
            this.tsmiVacuum.Click += new System.EventHandler(this.tsmiVacuum_Click);
            // 
            // tsmiSideBrush
            // 
            this.tsmiSideBrush.Name = "tsmiSideBrush";
            this.tsmiSideBrush.Size = new System.Drawing.Size(134, 22);
            this.tsmiSideBrush.Text = "Side Brush";
            this.tsmiSideBrush.Click += new System.EventHandler(this.tsmiSideBrush_Click);
            // 
            // tsmiButtons
            // 
            this.tsmiButtons.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBtnClean,
            this.tsmiSpot,
            this.tsmiBtnDock,
            this.tsmiBtnPower,
            this.tsmiBtnMax});
            this.tsmiButtons.Name = "tsmiButtons";
            this.tsmiButtons.Size = new System.Drawing.Size(152, 22);
            this.tsmiButtons.Text = "Buttons";
            // 
            // tsmiBtnClean
            // 
            this.tsmiBtnClean.Name = "tsmiBtnClean";
            this.tsmiBtnClean.Size = new System.Drawing.Size(107, 22);
            this.tsmiBtnClean.Text = "Clean";
            this.tsmiBtnClean.Click += new System.EventHandler(this.tsmiBtnClean_Click);
            // 
            // tsmiSpot
            // 
            this.tsmiSpot.Name = "tsmiSpot";
            this.tsmiSpot.Size = new System.Drawing.Size(107, 22);
            this.tsmiSpot.Text = "Spot";
            this.tsmiSpot.Click += new System.EventHandler(this.tsmiBtnSpot_Click);
            // 
            // tsmiBtnDock
            // 
            this.tsmiBtnDock.Name = "tsmiBtnDock";
            this.tsmiBtnDock.Size = new System.Drawing.Size(107, 22);
            this.tsmiBtnDock.Text = "Dock";
            this.tsmiBtnDock.Click += new System.EventHandler(this.tsmiBtnDock_Click);
            // 
            // tsmiBtnPower
            // 
            this.tsmiBtnPower.Name = "tsmiBtnPower";
            this.tsmiBtnPower.Size = new System.Drawing.Size(107, 22);
            this.tsmiBtnPower.Text = "Power";
            this.tsmiBtnPower.Click += new System.EventHandler(this.tsmiBtnPower_Click);
            // 
            // tsmiBtnMax
            // 
            this.tsmiBtnMax.Name = "tsmiBtnMax";
            this.tsmiBtnMax.Size = new System.Drawing.Size(107, 22);
            this.tsmiBtnMax.Text = "Max";
            this.tsmiBtnMax.Click += new System.EventHandler(this.tsmiBtnMax_Click);
            // 
            // tsmiDisplay
            // 
            this.tsmiDisplay.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setToolStripMenuItem,
            this.tsmiDisplayShutdown,
            this.tsmiDisplayTest});
            this.tsmiDisplay.Name = "tsmiDisplay";
            this.tsmiDisplay.Size = new System.Drawing.Size(152, 22);
            this.tsmiDisplay.Text = "Display";
            // 
            // setToolStripMenuItem
            // 
            this.setToolStripMenuItem.Name = "setToolStripMenuItem";
            this.setToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.setToolStripMenuItem.Text = "Set";
            // 
            // tsmiDisplayShutdown
            // 
            this.tsmiDisplayShutdown.Name = "tsmiDisplayShutdown";
            this.tsmiDisplayShutdown.Size = new System.Drawing.Size(128, 22);
            this.tsmiDisplayShutdown.Text = "Shutdown";
            this.tsmiDisplayShutdown.Click += new System.EventHandler(this.shutdownToolStripMenuItem_Click);
            // 
            // tsmiDisplayTest
            // 
            this.tsmiDisplayTest.Name = "tsmiDisplayTest";
            this.tsmiDisplayTest.Size = new System.Drawing.Size(128, 22);
            this.tsmiDisplayTest.Text = "Test";
            this.tsmiDisplayTest.Click += new System.EventHandler(this.tsmiDisplayTest_Click);
            // 
            // tsmiSensors
            // 
            this.tsmiSensors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiParamettersGroup0,
            this.tsmiParamettersGroup1,
            this.tsmiParamettersGroup2,
            this.tsmiParamettersGroup3,
            this.tsmiParamettersGroup4,
            this.tsmiParamettersGroup5,
            this.tsmiSensorsTest});
            this.tsmiSensors.Name = "tsmiSensors";
            this.tsmiSensors.Size = new System.Drawing.Size(152, 22);
            this.tsmiSensors.Text = "Sensors";
            // 
            // tsmiParamettersGroup0
            // 
            this.tsmiParamettersGroup0.Name = "tsmiParamettersGroup0";
            this.tsmiParamettersGroup0.Size = new System.Drawing.Size(194, 22);
            this.tsmiParamettersGroup0.Text = "Group 0";
            this.tsmiParamettersGroup0.Click += new System.EventHandler(this.tsmiParamettersGroup0_Click);
            // 
            // tsmiParamettersGroup1
            // 
            this.tsmiParamettersGroup1.Name = "tsmiParamettersGroup1";
            this.tsmiParamettersGroup1.Size = new System.Drawing.Size(194, 22);
            this.tsmiParamettersGroup1.Text = "Group 1";
            this.tsmiParamettersGroup1.Click += new System.EventHandler(this.tsmiParamettersGroup1_Click);
            // 
            // tsmiParamettersGroup2
            // 
            this.tsmiParamettersGroup2.Name = "tsmiParamettersGroup2";
            this.tsmiParamettersGroup2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.F2)));
            this.tsmiParamettersGroup2.Size = new System.Drawing.Size(194, 22);
            this.tsmiParamettersGroup2.Text = "Group 2";
            this.tsmiParamettersGroup2.Click += new System.EventHandler(this.tsmiParamettersGroup2_Click);
            // 
            // tsmiParamettersGroup3
            // 
            this.tsmiParamettersGroup3.Name = "tsmiParamettersGroup3";
            this.tsmiParamettersGroup3.Size = new System.Drawing.Size(194, 22);
            this.tsmiParamettersGroup3.Text = "Group 3";
            this.tsmiParamettersGroup3.Click += new System.EventHandler(this.tsmiParamettersGroup3_Click);
            // 
            // tsmiParamettersGroup4
            // 
            this.tsmiParamettersGroup4.Name = "tsmiParamettersGroup4";
            this.tsmiParamettersGroup4.Size = new System.Drawing.Size(194, 22);
            this.tsmiParamettersGroup4.Text = "Group 4";
            this.tsmiParamettersGroup4.Click += new System.EventHandler(this.tsmiParamettersGroup4_Click);
            // 
            // tsmiParamettersGroup5
            // 
            this.tsmiParamettersGroup5.Name = "tsmiParamettersGroup5";
            this.tsmiParamettersGroup5.Size = new System.Drawing.Size(194, 22);
            this.tsmiParamettersGroup5.Text = "Group 5";
            this.tsmiParamettersGroup5.Click += new System.EventHandler(this.tsmiParamettersGroup5_Click);
            // 
            // tsmiSensorsTest
            // 
            this.tsmiSensorsTest.Name = "tsmiSensorsTest";
            this.tsmiSensorsTest.Size = new System.Drawing.Size(194, 22);
            this.tsmiSensorsTest.Text = "Test";
            this.tsmiSensorsTest.Click += new System.EventHandler(this.tsmiSensorsTest_Click);
            // 
            // tsmiCameraCapture
            // 
            this.tsmiCameraCapture.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiStopCapture});
            this.tsmiCameraCapture.Name = "tsmiCameraCapture";
            this.tsmiCameraCapture.Size = new System.Drawing.Size(60, 20);
            this.tsmiCameraCapture.Text = "Camera";
            // 
            // tsmiStopCapture
            // 
            this.tsmiStopCapture.Name = "tsmiStopCapture";
            this.tsmiStopCapture.Size = new System.Drawing.Size(98, 22);
            this.tsmiStopCapture.Text = "Stop";
            this.tsmiStopCapture.Click += new System.EventHandler(this.tsmiStopCaptureeDevice_Click);
            // 
            // tsmiMqttServer
            // 
            this.tsmiMqttServer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMqttConnect,
            this.tsmiMqttDisconnect});
            this.tsmiMqttServer.Name = "tsmiMqttServer";
            this.tsmiMqttServer.Size = new System.Drawing.Size(51, 20);
            this.tsmiMqttServer.Text = "Server";
            // 
            // tsmiMqttConnect
            // 
            this.tsmiMqttConnect.Name = "tsmiMqttConnect";
            this.tsmiMqttConnect.Size = new System.Drawing.Size(133, 22);
            this.tsmiMqttConnect.Text = "Connect";
            this.tsmiMqttConnect.Click += new System.EventHandler(this.tsmiMqttConnect_Click);
            // 
            // tsmiMqttDisconnect
            // 
            this.tsmiMqttDisconnect.Name = "tsmiMqttDisconnect";
            this.tsmiMqttDisconnect.Size = new System.Drawing.Size(133, 22);
            this.tsmiMqttDisconnect.Text = "Disconnect";
            this.tsmiMqttDisconnect.Click += new System.EventHandler(this.tsmiMqttDisconnect_Click);
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tbpMotion);
            this.tbcMain.Controls.Add(this.tbpCamera);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcMain.Location = new System.Drawing.Point(0, 24);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(1056, 663);
            this.tbcMain.TabIndex = 34;
            // 
            // tbpMotion
            // 
            this.tbpMotion.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpMotion.Controls.Add(this.tableLayoutPanel1);
            this.tbpMotion.Location = new System.Drawing.Point(4, 26);
            this.tbpMotion.Name = "tbpMotion";
            this.tbpMotion.Padding = new System.Windows.Forms.Padding(3);
            this.tbpMotion.Size = new System.Drawing.Size(1048, 633);
            this.tbpMotion.TabIndex = 0;
            this.tbpMotion.Text = "Motion";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tbConsole, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlManualControl, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 71.79054F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.20946F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1042, 627);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // tbConsole
            // 
            this.tbConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbConsole.Location = new System.Drawing.Point(4, 454);
            this.tbConsole.Margin = new System.Windows.Forms.Padding(4);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(1034, 169);
            this.tbConsole.TabIndex = 41;
            // 
            // pnlManualControl
            // 
            this.pnlManualControl.Controls.Add(this.lblHandPosition);
            this.pnlManualControl.Controls.Add(this.lblRadius);
            this.pnlManualControl.Controls.Add(this.trbRadius);
            this.pnlManualControl.Controls.Add(this.btnStop);
            this.pnlManualControl.Controls.Add(this.lblSpeed);
            this.pnlManualControl.Controls.Add(this.trbSpeed);
            this.pnlManualControl.Controls.Add(this.btnDown);
            this.pnlManualControl.Controls.Add(this.btnRight);
            this.pnlManualControl.Controls.Add(this.btnLeft);
            this.pnlManualControl.Controls.Add(this.btnUp);
            this.pnlManualControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlManualControl.Location = new System.Drawing.Point(3, 3);
            this.pnlManualControl.Name = "pnlManualControl";
            this.pnlManualControl.Size = new System.Drawing.Size(1036, 444);
            this.pnlManualControl.TabIndex = 42;
            // 
            // lblHandPosition
            // 
            this.lblHandPosition.AutoSize = true;
            this.lblHandPosition.Location = new System.Drawing.Point(34, 383);
            this.lblHandPosition.Name = "lblHandPosition";
            this.lblHandPosition.Size = new System.Drawing.Size(40, 17);
            this.lblHandPosition.TabIndex = 51;
            this.lblHandPosition.Text = "Pos:";
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRadius.Location = new System.Drawing.Point(23, 303);
            this.lblRadius.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(77, 17);
            this.lblRadius.TabIndex = 50;
            this.lblRadius.Text = "Radius: 1";
            // 
            // trbRadius
            // 
            this.trbRadius.Location = new System.Drawing.Point(27, 333);
            this.trbRadius.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.trbRadius.Maximum = 200;
            this.trbRadius.Minimum = 1;
            this.trbRadius.Name = "trbRadius";
            this.trbRadius.Size = new System.Drawing.Size(312, 45);
            this.trbRadius.TabIndex = 49;
            this.trbRadius.Value = 1;
            this.trbRadius.ValueChanged += new System.EventHandler(this.trbRadius_ValueChanged);
            // 
            // btnStop
            // 
            this.btnStop.Image = global::RoombaSharp.Images.Stop;
            this.btnStop.Location = new System.Drawing.Point(135, 121);
            this.btnStop.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 96);
            this.btnStop.TabIndex = 48;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSpeed.Location = new System.Drawing.Point(256, 15);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(152, 17);
            this.lblSpeed.TabIndex = 47;
            this.lblSpeed.Text = "Speed: 0,000[mm/s]";
            // 
            // trbSpeed
            // 
            this.trbSpeed.Location = new System.Drawing.Point(376, 40);
            this.trbSpeed.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.trbSpeed.Maximum = 200;
            this.trbSpeed.Minimum = 15;
            this.trbSpeed.Name = "trbSpeed";
            this.trbSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbSpeed.Size = new System.Drawing.Size(45, 308);
            this.trbSpeed.TabIndex = 46;
            this.trbSpeed.Value = 15;
            this.trbSpeed.ValueChanged += new System.EventHandler(this.trbSpeed_ValueChanged);
            // 
            // btnDown
            // 
            this.btnDown.Image = global::RoombaSharp.Images.ArrowDown;
            this.btnDown.Location = new System.Drawing.Point(135, 227);
            this.btnDown.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(96, 96);
            this.btnDown.TabIndex = 45;
            this.btnDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseUp);
            // 
            // btnRight
            // 
            this.btnRight.Image = global::RoombaSharp.Images.ArrowRight;
            this.btnRight.Location = new System.Drawing.Point(243, 121);
            this.btnRight.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(96, 96);
            this.btnRight.TabIndex = 44;
            this.btnRight.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseUp);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::RoombaSharp.Images.ArrowLeft;
            this.btnLeft.Location = new System.Drawing.Point(27, 121);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(96, 96);
            this.btnLeft.TabIndex = 43;
            this.btnLeft.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::RoombaSharp.Images.ArrowUp;
            this.btnUp.Location = new System.Drawing.Point(135, 15);
            this.btnUp.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(96, 96);
            this.btnUp.TabIndex = 42;
            this.btnUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // tbpCamera
            // 
            this.tbpCamera.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tbpCamera.Controls.Add(this.pbMain);
            this.tbpCamera.Location = new System.Drawing.Point(4, 26);
            this.tbpCamera.Name = "tbpCamera";
            this.tbpCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tbpCamera.Size = new System.Drawing.Size(1048, 633);
            this.tbpCamera.TabIndex = 1;
            this.tbpCamera.Text = "Camera";
            // 
            // pbMain
            // 
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMain.Location = new System.Drawing.Point(3, 3);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(1042, 627);
            this.pbMain.TabIndex = 34;
            this.pbMain.TabStop = false;
            // 
            // ssMain
            // 
            this.ssMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslRobotConnection,
            this.tsslMQTTConnection});
            this.ssMain.Location = new System.Drawing.Point(0, 665);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(1056, 22);
            this.ssMain.TabIndex = 35;
            this.ssMain.Text = "statusStrip1";
            // 
            // tsslRobotConnection
            // 
            this.tsslRobotConnection.Name = "tsslRobotConnection";
            this.tsslRobotConnection.Size = new System.Drawing.Size(110, 17);
            this.tsslRobotConnection.Text = "Robot Connection: ";
            // 
            // tsslMQTTConnection
            // 
            this.tsslMQTTConnection.Name = "tsslMQTTConnection";
            this.tsslMQTTConnection.Size = new System.Drawing.Size(111, 17);
            this.tsslMQTTConnection.Text = "MQTT Connection: ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 687);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tbcMain);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MainForm";
            this.Text = "Roomba#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tbcMain.ResumeLayout(false);
            this.tbpMotion.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.pnlManualControl.ResumeLayout(false);
            this.pnlManualControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).EndInit();
            this.tbpCamera.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ssMain.ResumeLayout(false);
            this.ssMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmiExit;
        private System.Windows.Forms.ToolStripMenuItem tsmiRobot;
        private System.Windows.Forms.ToolStripMenuItem tsmiBeep;
        private System.Windows.Forms.ToolStripMenuItem tsmiLEDs;
        private System.Windows.Forms.ToolStripMenuItem tsmiMotors;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedSpot;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedDirtDetect;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedCheckRobot;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedDock;
        private System.Windows.Forms.ToolStripMenuItem tsmiMainBrush;
        private System.Windows.Forms.ToolStripMenuItem tsmiVacuum;
        private System.Windows.Forms.ToolStripMenuItem tsmiSideBrush;
        private System.Windows.Forms.ToolStripMenuItem tsmiButtons;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnClean;
        private System.Windows.Forms.ToolStripMenuItem tsmiSpot;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnDock;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnPower;
        private System.Windows.Forms.ToolStripMenuItem tsmiBtnMax;
        private System.Windows.Forms.ToolStripMenuItem tsmiCameraCapture;
        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpMotion;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.Panel pnlManualControl;
        private System.Windows.Forms.Label lblHandPosition;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.TrackBar trbRadius;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TrackBar trbSpeed;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TabPage tbpCamera;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiSettings;
        private System.Windows.Forms.ToolStripMenuItem tsmiStopCapture;
        private System.Windows.Forms.StatusStrip ssMain;
        private System.Windows.Forms.ToolStripStatusLabel tsslRobotConnection;
        private System.Windows.Forms.ToolStripMenuItem tsmiMqttServer;
        private System.Windows.Forms.ToolStripMenuItem tsmiMqttConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiMqttDisconnect;
        private System.Windows.Forms.ToolStripStatusLabel tsslMQTTConnection;
        private System.Windows.Forms.ToolStripMenuItem tsmiConnect;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedClean;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedCleanOff;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedCleanGreen;
        private System.Windows.Forms.ToolStripMenuItem tsmiLedCleanRed;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplay;
        private System.Windows.Forms.ToolStripMenuItem tsmiSensors;
        private System.Windows.Forms.ToolStripMenuItem tsmiSensorsTest;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayTest;
        private System.Windows.Forms.ToolStripMenuItem setToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiDisplayShutdown;
        private System.Windows.Forms.ToolStripMenuItem tsmiBeepTest;
        private System.Windows.Forms.ToolStripMenuItem tsmiParamettersGroup0;
        private System.Windows.Forms.ToolStripMenuItem tsmiParamettersGroup1;
        private System.Windows.Forms.ToolStripMenuItem tsmiParamettersGroup2;
        private System.Windows.Forms.ToolStripMenuItem tsmiParamettersGroup3;
        private System.Windows.Forms.ToolStripMenuItem tsmiParamettersGroup4;
        private System.Windows.Forms.ToolStripMenuItem tsmiParamettersGroup5;
    }
}

