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
            this.lblOutput = new System.Windows.Forms.Label();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRobot = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiBeep = new System.Windows.Forms.ToolStripMenuItem();
            this.lEDsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dirtDetectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.motorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainBrushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vacuumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sideBrushToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cleanToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.spotToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.powerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.leapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.captureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblSpeed = new System.Windows.Forms.Label();
            this.trbSpeed = new System.Windows.Forms.TrackBar();
            this.trbRadius = new System.Windows.Forms.TrackBar();
            this.lblRadius = new System.Windows.Forms.Label();
            this.tbConsole = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnRight = new System.Windows.Forms.Button();
            this.btnLeft = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.lblHandPosition = new System.Windows.Forms.Label();
            this.pbMain = new System.Windows.Forms.PictureBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnRedSand = new System.Windows.Forms.Button();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).BeginInit();
            this.SuspendLayout();
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblOutput.Location = new System.Drawing.Point(22, 506);
            this.lblOutput.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(65, 20);
            this.lblOutput.TabIndex = 9;
            this.lblOutput.Text = "Output";
            // 
            // msMain
            // 
            this.msMain.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile,
            this.tsmiRobot,
            this.leapToolStripMenuItem,
            this.captureToolStripMenuItem});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.msMain.Size = new System.Drawing.Size(1290, 28);
            this.msMain.TabIndex = 16;
            this.msMain.Text = "menuStrip1";
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiConnect,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(44, 24);
            this.tsmiFile.Text = "File";
            // 
            // tsmiConnect
            // 
            this.tsmiConnect.Name = "tsmiConnect";
            this.tsmiConnect.Size = new System.Drawing.Size(138, 26);
            this.tsmiConnect.Text = "Connect";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(135, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(138, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // tsmiRobot
            // 
            this.tsmiRobot.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiBeep,
            this.lEDsToolStripMenuItem,
            this.motorsToolStripMenuItem,
            this.buttonsToolStripMenuItem});
            this.tsmiRobot.Name = "tsmiRobot";
            this.tsmiRobot.Size = new System.Drawing.Size(62, 24);
            this.tsmiRobot.Text = "Robot";
            // 
            // tsmiBeep
            // 
            this.tsmiBeep.Name = "tsmiBeep";
            this.tsmiBeep.Size = new System.Drawing.Size(134, 26);
            this.tsmiBeep.Text = "Beep";
            this.tsmiBeep.Click += new System.EventHandler(this.tsmiBeep_Click);
            // 
            // lEDsToolStripMenuItem
            // 
            this.lEDsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.spotToolStripMenuItem,
            this.cleanToolStripMenuItem,
            this.maxToolStripMenuItem,
            this.dirtDetectToolStripMenuItem});
            this.lEDsToolStripMenuItem.Name = "lEDsToolStripMenuItem";
            this.lEDsToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.lEDsToolStripMenuItem.Text = "LEDs";
            // 
            // spotToolStripMenuItem
            // 
            this.spotToolStripMenuItem.Name = "spotToolStripMenuItem";
            this.spotToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.spotToolStripMenuItem.Text = "Spot";
            // 
            // cleanToolStripMenuItem
            // 
            this.cleanToolStripMenuItem.Name = "cleanToolStripMenuItem";
            this.cleanToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.cleanToolStripMenuItem.Text = "Clean";
            // 
            // maxToolStripMenuItem
            // 
            this.maxToolStripMenuItem.Name = "maxToolStripMenuItem";
            this.maxToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.maxToolStripMenuItem.Text = "Max";
            // 
            // dirtDetectToolStripMenuItem
            // 
            this.dirtDetectToolStripMenuItem.Name = "dirtDetectToolStripMenuItem";
            this.dirtDetectToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.dirtDetectToolStripMenuItem.Text = "Dirt Detect";
            // 
            // motorsToolStripMenuItem
            // 
            this.motorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainBrushToolStripMenuItem,
            this.vacuumToolStripMenuItem,
            this.sideBrushToolStripMenuItem});
            this.motorsToolStripMenuItem.Name = "motorsToolStripMenuItem";
            this.motorsToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.motorsToolStripMenuItem.Text = "Motors";
            // 
            // mainBrushToolStripMenuItem
            // 
            this.mainBrushToolStripMenuItem.Name = "mainBrushToolStripMenuItem";
            this.mainBrushToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.mainBrushToolStripMenuItem.Text = "Main Brush";
            // 
            // vacuumToolStripMenuItem
            // 
            this.vacuumToolStripMenuItem.Name = "vacuumToolStripMenuItem";
            this.vacuumToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.vacuumToolStripMenuItem.Text = "Vacuum";
            // 
            // sideBrushToolStripMenuItem
            // 
            this.sideBrushToolStripMenuItem.Name = "sideBrushToolStripMenuItem";
            this.sideBrushToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.sideBrushToolStripMenuItem.Text = "Side Brush";
            // 
            // buttonsToolStripMenuItem
            // 
            this.buttonsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cleanToolStripMenuItem1,
            this.spotToolStripMenuItem1,
            this.dockToolStripMenuItem,
            this.powerToolStripMenuItem,
            this.maxToolStripMenuItem1});
            this.buttonsToolStripMenuItem.Name = "buttonsToolStripMenuItem";
            this.buttonsToolStripMenuItem.Size = new System.Drawing.Size(134, 26);
            this.buttonsToolStripMenuItem.Text = "Buttons";
            // 
            // cleanToolStripMenuItem1
            // 
            this.cleanToolStripMenuItem1.Name = "cleanToolStripMenuItem1";
            this.cleanToolStripMenuItem1.Size = new System.Drawing.Size(124, 26);
            this.cleanToolStripMenuItem1.Text = "Clean";
            this.cleanToolStripMenuItem1.Click += new System.EventHandler(this.cleanToolStripMenuItem1_Click);
            // 
            // spotToolStripMenuItem1
            // 
            this.spotToolStripMenuItem1.Name = "spotToolStripMenuItem1";
            this.spotToolStripMenuItem1.Size = new System.Drawing.Size(124, 26);
            this.spotToolStripMenuItem1.Text = "Spot";
            this.spotToolStripMenuItem1.Click += new System.EventHandler(this.spotToolStripMenuItem1_Click);
            // 
            // dockToolStripMenuItem
            // 
            this.dockToolStripMenuItem.Name = "dockToolStripMenuItem";
            this.dockToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.dockToolStripMenuItem.Text = "Dock";
            this.dockToolStripMenuItem.Click += new System.EventHandler(this.dockToolStripMenuItem_Click);
            // 
            // powerToolStripMenuItem
            // 
            this.powerToolStripMenuItem.Name = "powerToolStripMenuItem";
            this.powerToolStripMenuItem.Size = new System.Drawing.Size(124, 26);
            this.powerToolStripMenuItem.Text = "Power";
            this.powerToolStripMenuItem.Click += new System.EventHandler(this.powerToolStripMenuItem_Click);
            // 
            // maxToolStripMenuItem1
            // 
            this.maxToolStripMenuItem1.Name = "maxToolStripMenuItem1";
            this.maxToolStripMenuItem1.Size = new System.Drawing.Size(124, 26);
            this.maxToolStripMenuItem1.Text = "Max";
            this.maxToolStripMenuItem1.Click += new System.EventHandler(this.maxToolStripMenuItem1_Click);
            // 
            // leapToolStripMenuItem
            // 
            this.leapToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem});
            this.leapToolStripMenuItem.Name = "leapToolStripMenuItem";
            this.leapToolStripMenuItem.Size = new System.Drawing.Size(53, 24);
            this.leapToolStripMenuItem.Text = "Leap";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem
            // 
            this.disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            this.disconnectToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.disconnectToolStripMenuItem.Text = "Disconnect";
            this.disconnectToolStripMenuItem.Click += new System.EventHandler(this.disconnectToolStripMenuItem_Click);
            // 
            // captureToolStripMenuItem
            // 
            this.captureToolStripMenuItem.Name = "captureToolStripMenuItem";
            this.captureToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.captureToolStripMenuItem.Text = "Capture";
            // 
            // lblSpeed
            // 
            this.lblSpeed.AutoSize = true;
            this.lblSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSpeed.Location = new System.Drawing.Point(244, 42);
            this.lblSpeed.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSpeed.Name = "lblSpeed";
            this.lblSpeed.Size = new System.Drawing.Size(176, 20);
            this.lblSpeed.TabIndex = 23;
            this.lblSpeed.Text = "Speed: 0,000[mm/s]";
            // 
            // trbSpeed
            // 
            this.trbSpeed.Location = new System.Drawing.Point(368, 42);
            this.trbSpeed.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.trbSpeed.Maximum = 200;
            this.trbSpeed.Minimum = 15;
            this.trbSpeed.Name = "trbSpeed";
            this.trbSpeed.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trbSpeed.Size = new System.Drawing.Size(56, 308);
            this.trbSpeed.TabIndex = 22;
            this.trbSpeed.Value = 15;
            this.trbSpeed.ValueChanged += new System.EventHandler(this.trbSpeed_ValueChanged);
            // 
            // trbRadius
            // 
            this.trbRadius.Location = new System.Drawing.Point(15, 360);
            this.trbRadius.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.trbRadius.Maximum = 200;
            this.trbRadius.Minimum = 1;
            this.trbRadius.Name = "trbRadius";
            this.trbRadius.Size = new System.Drawing.Size(312, 56);
            this.trbRadius.TabIndex = 26;
            this.trbRadius.Value = 1;
            this.trbRadius.ValueChanged += new System.EventHandler(this.trbRadius_ValueChanged);
            // 
            // lblRadius
            // 
            this.lblRadius.AutoSize = true;
            this.lblRadius.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblRadius.Location = new System.Drawing.Point(11, 330);
            this.lblRadius.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRadius.Name = "lblRadius";
            this.lblRadius.Size = new System.Drawing.Size(89, 20);
            this.lblRadius.TabIndex = 27;
            this.lblRadius.Text = "Radius: 1";
            // 
            // tbConsole
            // 
            this.tbConsole.Location = new System.Drawing.Point(15, 539);
            this.tbConsole.Margin = new System.Windows.Forms.Padding(4);
            this.tbConsole.Multiline = true;
            this.tbConsole.Name = "tbConsole";
            this.tbConsole.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbConsole.Size = new System.Drawing.Size(487, 84);
            this.tbConsole.TabIndex = 28;
            // 
            // btnStop
            // 
            this.btnStop.Image = global::RoombaSharp.Images.Stop;
            this.btnStop.Location = new System.Drawing.Point(123, 148);
            this.btnStop.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(96, 96);
            this.btnStop.TabIndex = 24;
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnDown
            // 
            this.btnDown.Image = global::RoombaSharp.Images.ArrowDown;
            this.btnDown.Location = new System.Drawing.Point(123, 254);
            this.btnDown.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(96, 96);
            this.btnDown.TabIndex = 21;
            this.btnDown.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseDown);
            this.btnDown.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnDown_MouseUp);
            // 
            // btnRight
            // 
            this.btnRight.Image = global::RoombaSharp.Images.ArrowRight;
            this.btnRight.Location = new System.Drawing.Point(231, 148);
            this.btnRight.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnRight.Name = "btnRight";
            this.btnRight.Size = new System.Drawing.Size(96, 96);
            this.btnRight.TabIndex = 20;
            this.btnRight.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRight.UseVisualStyleBackColor = true;
            this.btnRight.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseDown);
            this.btnRight.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnRight_MouseUp);
            // 
            // btnLeft
            // 
            this.btnLeft.Image = global::RoombaSharp.Images.ArrowLeft;
            this.btnLeft.Location = new System.Drawing.Point(15, 148);
            this.btnLeft.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnLeft.Name = "btnLeft";
            this.btnLeft.Size = new System.Drawing.Size(96, 96);
            this.btnLeft.TabIndex = 19;
            this.btnLeft.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnLeft.UseVisualStyleBackColor = true;
            this.btnLeft.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseDown);
            this.btnLeft.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnLeft_MouseUp);
            // 
            // btnUp
            // 
            this.btnUp.Image = global::RoombaSharp.Images.ArrowUp;
            this.btnUp.Location = new System.Drawing.Point(123, 42);
            this.btnUp.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(96, 96);
            this.btnUp.TabIndex = 18;
            this.btnUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseDown);
            this.btnUp.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnUp_MouseUp);
            // 
            // lblHandPosition
            // 
            this.lblHandPosition.AutoSize = true;
            this.lblHandPosition.Location = new System.Drawing.Point(361, 383);
            this.lblHandPosition.Name = "lblHandPosition";
            this.lblHandPosition.Size = new System.Drawing.Size(47, 20);
            this.lblHandPosition.TabIndex = 29;
            this.lblHandPosition.Text = "Pos:";
            // 
            // pbMain
            // 
            this.pbMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMain.Location = new System.Drawing.Point(570, 42);
            this.pbMain.Name = "pbMain";
            this.pbMain.Size = new System.Drawing.Size(513, 404);
            this.pbMain.TabIndex = 30;
            this.pbMain.TabStop = false;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(570, 452);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(134, 47);
            this.btnCapture.TabIndex = 31;
            this.btnCapture.Text = "Capture";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnRedSand
            // 
            this.btnRedSand.Location = new System.Drawing.Point(710, 452);
            this.btnRedSand.Name = "btnRedSand";
            this.btnRedSand.Size = new System.Drawing.Size(134, 47);
            this.btnRedSand.TabIndex = 32;
            this.btnRedSand.Text = "Sand";
            this.btnRedSand.UseVisualStyleBackColor = true;
            this.btnRedSand.Click += new System.EventHandler(this.btnRedSand_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1290, 659);
            this.Controls.Add(this.btnRedSand);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.pbMain);
            this.Controls.Add(this.lblHandPosition);
            this.Controls.Add(this.tbConsole);
            this.Controls.Add(this.lblRadius);
            this.Controls.Add(this.trbRadius);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lblSpeed);
            this.Controls.Add(this.trbSpeed);
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.btnRight);
            this.Controls.Add(this.btnLeft);
            this.Controls.Add(this.btnUp);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MainMenuStrip = this.msMain;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "MainForm";
            this.Text = "Roomba#";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiRobot;
        private System.Windows.Forms.ToolStripMenuItem tsmiBeep;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblSpeed;
        private System.Windows.Forms.TrackBar trbSpeed;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnRight;
        private System.Windows.Forms.Button btnLeft;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.TrackBar trbRadius;
        private System.Windows.Forms.ToolStripMenuItem lEDsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem motorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spotToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dirtDetectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mainBrushToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vacuumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sideBrushToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buttonsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cleanToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem spotToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem dockToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem powerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maxToolStripMenuItem1;
        private System.Windows.Forms.Label lblRadius;
        private System.Windows.Forms.TextBox tbConsole;
        private System.Windows.Forms.ToolStripMenuItem leapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.Label lblHandPosition;
        private System.Windows.Forms.ToolStripMenuItem captureToolStripMenuItem;
        private System.Windows.Forms.PictureBox pbMain;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnRedSand;
    }
}

