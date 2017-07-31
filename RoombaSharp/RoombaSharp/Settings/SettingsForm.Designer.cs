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

namespace RoombaSharp.Settings
{
    partial class SettingsForm
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
            this.tcSettings = new System.Windows.Forms.TabControl();
            this.tpServerSettings = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblImageSize = new System.Windows.Forms.Label();
            this.tbImageHeight = new System.Windows.Forms.TextBox();
            this.tbImageWidth = new System.Windows.Forms.TextBox();
            this.tbImageTopic = new System.Windows.Forms.TextBox();
            this.lblImageTopic = new System.Windows.Forms.Label();
            this.tbBrokerPort = new System.Windows.Forms.TextBox();
            this.lblBrokerPort = new System.Windows.Forms.Label();
            this.tbBrokerDomain = new System.Windows.Forms.TextBox();
            this.lblBrokerDomain = new System.Windows.Forms.Label();
            this.tbInputTopic = new System.Windows.Forms.TextBox();
            this.tbOutputTopic = new System.Windows.Forms.TextBox();
            this.lblOutputTopic = new System.Windows.Forms.Label();
            this.lblInputTopic = new System.Windows.Forms.Label();
            this.tbUpdateInterval = new System.Windows.Forms.TextBox();
            this.lblUpdateInterval = new System.Windows.Forms.Label();
            this.tcSettings.SuspendLayout();
            this.tpServerSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcSettings
            // 
            this.tcSettings.Controls.Add(this.tpServerSettings);
            this.tcSettings.Controls.Add(this.tabPage2);
            this.tcSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcSettings.Location = new System.Drawing.Point(0, 0);
            this.tcSettings.Name = "tcSettings";
            this.tcSettings.SelectedIndex = 0;
            this.tcSettings.Size = new System.Drawing.Size(916, 422);
            this.tcSettings.TabIndex = 3;
            // 
            // tpServerSettings
            // 
            this.tpServerSettings.BackColor = System.Drawing.SystemColors.Control;
            this.tpServerSettings.Controls.Add(this.tbUpdateInterval);
            this.tpServerSettings.Controls.Add(this.lblUpdateInterval);
            this.tpServerSettings.Controls.Add(this.lblImageSize);
            this.tpServerSettings.Controls.Add(this.tbImageHeight);
            this.tpServerSettings.Controls.Add(this.tbImageWidth);
            this.tpServerSettings.Controls.Add(this.tbImageTopic);
            this.tpServerSettings.Controls.Add(this.lblImageTopic);
            this.tpServerSettings.Controls.Add(this.tbBrokerPort);
            this.tpServerSettings.Controls.Add(this.lblBrokerPort);
            this.tpServerSettings.Controls.Add(this.tbBrokerDomain);
            this.tpServerSettings.Controls.Add(this.lblBrokerDomain);
            this.tpServerSettings.Controls.Add(this.tbInputTopic);
            this.tpServerSettings.Controls.Add(this.tbOutputTopic);
            this.tpServerSettings.Controls.Add(this.lblOutputTopic);
            this.tpServerSettings.Controls.Add(this.lblInputTopic);
            this.tpServerSettings.Location = new System.Drawing.Point(4, 22);
            this.tpServerSettings.Name = "tpServerSettings";
            this.tpServerSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tpServerSettings.Size = new System.Drawing.Size(908, 396);
            this.tpServerSettings.TabIndex = 0;
            this.tpServerSettings.Text = "Server";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(908, 396);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblImageSize
            // 
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.Location = new System.Drawing.Point(39, 187);
            this.lblImageSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(62, 13);
            this.lblImageSize.TabIndex = 28;
            this.lblImageSize.Text = "Image Size:";
            // 
            // tbImageHeight
            // 
            this.tbImageHeight.Location = new System.Drawing.Point(176, 184);
            this.tbImageHeight.Margin = new System.Windows.Forms.Padding(2);
            this.tbImageHeight.Name = "tbImageHeight";
            this.tbImageHeight.Size = new System.Drawing.Size(66, 20);
            this.tbImageHeight.TabIndex = 27;
            // 
            // tbImageWidth
            // 
            this.tbImageWidth.Location = new System.Drawing.Point(107, 184);
            this.tbImageWidth.Margin = new System.Windows.Forms.Padding(2);
            this.tbImageWidth.Name = "tbImageWidth";
            this.tbImageWidth.Size = new System.Drawing.Size(66, 20);
            this.tbImageWidth.TabIndex = 26;
            // 
            // tbImageTopic
            // 
            this.tbImageTopic.Location = new System.Drawing.Point(107, 161);
            this.tbImageTopic.Margin = new System.Windows.Forms.Padding(2);
            this.tbImageTopic.Name = "tbImageTopic";
            this.tbImageTopic.Size = new System.Drawing.Size(228, 20);
            this.tbImageTopic.TabIndex = 25;
            // 
            // lblImageTopic
            // 
            this.lblImageTopic.AutoSize = true;
            this.lblImageTopic.Location = new System.Drawing.Point(32, 161);
            this.lblImageTopic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImageTopic.Name = "lblImageTopic";
            this.lblImageTopic.Size = new System.Drawing.Size(69, 13);
            this.lblImageTopic.TabIndex = 24;
            this.lblImageTopic.Text = "Image Topic:";
            // 
            // tbBrokerPort
            // 
            this.tbBrokerPort.Location = new System.Drawing.Point(107, 48);
            this.tbBrokerPort.Margin = new System.Windows.Forms.Padding(2);
            this.tbBrokerPort.Name = "tbBrokerPort";
            this.tbBrokerPort.Size = new System.Drawing.Size(228, 20);
            this.tbBrokerPort.TabIndex = 23;
            // 
            // lblBrokerPort
            // 
            this.lblBrokerPort.AutoSize = true;
            this.lblBrokerPort.Location = new System.Drawing.Point(40, 51);
            this.lblBrokerPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBrokerPort.Name = "lblBrokerPort";
            this.lblBrokerPort.Size = new System.Drawing.Size(63, 13);
            this.lblBrokerPort.TabIndex = 22;
            this.lblBrokerPort.Text = "Broker Port:";
            // 
            // tbBrokerDomain
            // 
            this.tbBrokerDomain.Location = new System.Drawing.Point(107, 25);
            this.tbBrokerDomain.Margin = new System.Windows.Forms.Padding(2);
            this.tbBrokerDomain.Name = "tbBrokerDomain";
            this.tbBrokerDomain.Size = new System.Drawing.Size(228, 20);
            this.tbBrokerDomain.TabIndex = 21;
            // 
            // lblBrokerDomain
            // 
            this.lblBrokerDomain.AutoSize = true;
            this.lblBrokerDomain.Location = new System.Drawing.Point(23, 28);
            this.lblBrokerDomain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBrokerDomain.Name = "lblBrokerDomain";
            this.lblBrokerDomain.Size = new System.Drawing.Size(80, 13);
            this.lblBrokerDomain.TabIndex = 20;
            this.lblBrokerDomain.Text = "Broker Domain:";
            // 
            // tbInputTopic
            // 
            this.tbInputTopic.Location = new System.Drawing.Point(107, 88);
            this.tbInputTopic.Margin = new System.Windows.Forms.Padding(2);
            this.tbInputTopic.Name = "tbInputTopic";
            this.tbInputTopic.Size = new System.Drawing.Size(228, 20);
            this.tbInputTopic.TabIndex = 19;
            // 
            // tbOutputTopic
            // 
            this.tbOutputTopic.Location = new System.Drawing.Point(107, 125);
            this.tbOutputTopic.Margin = new System.Windows.Forms.Padding(2);
            this.tbOutputTopic.Name = "tbOutputTopic";
            this.tbOutputTopic.Size = new System.Drawing.Size(228, 20);
            this.tbOutputTopic.TabIndex = 18;
            // 
            // lblOutputTopic
            // 
            this.lblOutputTopic.AutoSize = true;
            this.lblOutputTopic.Location = new System.Drawing.Point(32, 125);
            this.lblOutputTopic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOutputTopic.Name = "lblOutputTopic";
            this.lblOutputTopic.Size = new System.Drawing.Size(72, 13);
            this.lblOutputTopic.TabIndex = 17;
            this.lblOutputTopic.Text = "Output Topic:";
            // 
            // lblInputTopic
            // 
            this.lblInputTopic.AutoSize = true;
            this.lblInputTopic.Location = new System.Drawing.Point(41, 90);
            this.lblInputTopic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInputTopic.Name = "lblInputTopic";
            this.lblInputTopic.Size = new System.Drawing.Size(64, 13);
            this.lblInputTopic.TabIndex = 16;
            this.lblInputTopic.Text = "Input Topic:";
            // 
            // tbUpdateInterval
            // 
            this.tbUpdateInterval.Location = new System.Drawing.Point(107, 223);
            this.tbUpdateInterval.Margin = new System.Windows.Forms.Padding(2);
            this.tbUpdateInterval.Name = "tbUpdateInterval";
            this.tbUpdateInterval.Size = new System.Drawing.Size(228, 20);
            this.tbUpdateInterval.TabIndex = 30;
            // 
            // lblUpdateInterval
            // 
            this.lblUpdateInterval.AutoSize = true;
            this.lblUpdateInterval.Location = new System.Drawing.Point(18, 226);
            this.lblUpdateInterval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblUpdateInterval.Name = "lblUpdateInterval";
            this.lblUpdateInterval.Size = new System.Drawing.Size(83, 13);
            this.lblUpdateInterval.TabIndex = 29;
            this.lblUpdateInterval.Text = "Update Interval:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 422);
            this.Controls.Add(this.tcSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.tcSettings.ResumeLayout(false);
            this.tpServerSettings.ResumeLayout(false);
            this.tpServerSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcSettings;
        private System.Windows.Forms.TabPage tpServerSettings;
        private System.Windows.Forms.Label lblImageSize;
        private System.Windows.Forms.TextBox tbImageHeight;
        private System.Windows.Forms.TextBox tbImageWidth;
        private System.Windows.Forms.TextBox tbImageTopic;
        private System.Windows.Forms.Label lblImageTopic;
        private System.Windows.Forms.TextBox tbBrokerPort;
        private System.Windows.Forms.Label lblBrokerPort;
        private System.Windows.Forms.TextBox tbBrokerDomain;
        private System.Windows.Forms.Label lblBrokerDomain;
        private System.Windows.Forms.TextBox tbInputTopic;
        private System.Windows.Forms.TextBox tbOutputTopic;
        private System.Windows.Forms.Label lblOutputTopic;
        private System.Windows.Forms.Label lblInputTopic;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox tbUpdateInterval;
        private System.Windows.Forms.Label lblUpdateInterval;
    }
}