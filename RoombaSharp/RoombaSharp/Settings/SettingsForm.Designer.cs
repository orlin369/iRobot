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
            this.gbMQTT = new System.Windows.Forms.GroupBox();
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
            this.gbMQTT.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbMQTT
            // 
            this.gbMQTT.Controls.Add(this.lblImageSize);
            this.gbMQTT.Controls.Add(this.tbImageHeight);
            this.gbMQTT.Controls.Add(this.tbImageWidth);
            this.gbMQTT.Controls.Add(this.tbImageTopic);
            this.gbMQTT.Controls.Add(this.lblImageTopic);
            this.gbMQTT.Controls.Add(this.tbBrokerPort);
            this.gbMQTT.Controls.Add(this.lblBrokerPort);
            this.gbMQTT.Controls.Add(this.tbBrokerDomain);
            this.gbMQTT.Controls.Add(this.lblBrokerDomain);
            this.gbMQTT.Controls.Add(this.tbInputTopic);
            this.gbMQTT.Controls.Add(this.tbOutputTopic);
            this.gbMQTT.Controls.Add(this.lblOutputTopic);
            this.gbMQTT.Controls.Add(this.lblInputTopic);
            this.gbMQTT.Location = new System.Drawing.Point(11, 11);
            this.gbMQTT.Margin = new System.Windows.Forms.Padding(2);
            this.gbMQTT.Name = "gbMQTT";
            this.gbMQTT.Padding = new System.Windows.Forms.Padding(2);
            this.gbMQTT.Size = new System.Drawing.Size(336, 199);
            this.gbMQTT.TabIndex = 2;
            this.gbMQTT.TabStop = false;
            this.gbMQTT.Text = "MQTT";
            // 
            // lblImageSize
            // 
            this.lblImageSize.AutoSize = true;
            this.lblImageSize.Location = new System.Drawing.Point(20, 168);
            this.lblImageSize.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(62, 13);
            this.lblImageSize.TabIndex = 15;
            this.lblImageSize.Text = "Image Size:";
            // 
            // tbImageHeight
            // 
            this.tbImageHeight.Location = new System.Drawing.Point(164, 166);
            this.tbImageHeight.Margin = new System.Windows.Forms.Padding(2);
            this.tbImageHeight.Name = "tbImageHeight";
            this.tbImageHeight.Size = new System.Drawing.Size(66, 20);
            this.tbImageHeight.TabIndex = 14;
            // 
            // tbImageWidth
            // 
            this.tbImageWidth.Location = new System.Drawing.Point(95, 166);
            this.tbImageWidth.Margin = new System.Windows.Forms.Padding(2);
            this.tbImageWidth.Name = "tbImageWidth";
            this.tbImageWidth.Size = new System.Drawing.Size(66, 20);
            this.tbImageWidth.TabIndex = 13;
            // 
            // tbImageTopic
            // 
            this.tbImageTopic.Location = new System.Drawing.Point(95, 143);
            this.tbImageTopic.Margin = new System.Windows.Forms.Padding(2);
            this.tbImageTopic.Name = "tbImageTopic";
            this.tbImageTopic.Size = new System.Drawing.Size(228, 20);
            this.tbImageTopic.TabIndex = 12;
            // 
            // lblImageTopic
            // 
            this.lblImageTopic.AutoSize = true;
            this.lblImageTopic.Location = new System.Drawing.Point(20, 143);
            this.lblImageTopic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblImageTopic.Name = "lblImageTopic";
            this.lblImageTopic.Size = new System.Drawing.Size(69, 13);
            this.lblImageTopic.TabIndex = 11;
            this.lblImageTopic.Text = "Image Topic:";
            // 
            // tbBrokerPort
            // 
            this.tbBrokerPort.Location = new System.Drawing.Point(95, 47);
            this.tbBrokerPort.Margin = new System.Windows.Forms.Padding(2);
            this.tbBrokerPort.Name = "tbBrokerPort";
            this.tbBrokerPort.Size = new System.Drawing.Size(228, 20);
            this.tbBrokerPort.TabIndex = 10;
            // 
            // lblBrokerPort
            // 
            this.lblBrokerPort.AutoSize = true;
            this.lblBrokerPort.Location = new System.Drawing.Point(28, 50);
            this.lblBrokerPort.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBrokerPort.Name = "lblBrokerPort";
            this.lblBrokerPort.Size = new System.Drawing.Size(63, 13);
            this.lblBrokerPort.TabIndex = 9;
            this.lblBrokerPort.Text = "Broker Port:";
            // 
            // tbBrokerDomain
            // 
            this.tbBrokerDomain.Location = new System.Drawing.Point(95, 24);
            this.tbBrokerDomain.Margin = new System.Windows.Forms.Padding(2);
            this.tbBrokerDomain.Name = "tbBrokerDomain";
            this.tbBrokerDomain.Size = new System.Drawing.Size(228, 20);
            this.tbBrokerDomain.TabIndex = 8;
            // 
            // lblBrokerDomain
            // 
            this.lblBrokerDomain.AutoSize = true;
            this.lblBrokerDomain.Location = new System.Drawing.Point(11, 27);
            this.lblBrokerDomain.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblBrokerDomain.Name = "lblBrokerDomain";
            this.lblBrokerDomain.Size = new System.Drawing.Size(80, 13);
            this.lblBrokerDomain.TabIndex = 7;
            this.lblBrokerDomain.Text = "Broker Domain:";
            // 
            // tbInputTopic
            // 
            this.tbInputTopic.Location = new System.Drawing.Point(95, 70);
            this.tbInputTopic.Margin = new System.Windows.Forms.Padding(2);
            this.tbInputTopic.Name = "tbInputTopic";
            this.tbInputTopic.Size = new System.Drawing.Size(228, 20);
            this.tbInputTopic.TabIndex = 3;
            // 
            // tbOutputTopic
            // 
            this.tbOutputTopic.Location = new System.Drawing.Point(95, 107);
            this.tbOutputTopic.Margin = new System.Windows.Forms.Padding(2);
            this.tbOutputTopic.Name = "tbOutputTopic";
            this.tbOutputTopic.Size = new System.Drawing.Size(228, 20);
            this.tbOutputTopic.TabIndex = 2;
            // 
            // lblOutputTopic
            // 
            this.lblOutputTopic.AutoSize = true;
            this.lblOutputTopic.Location = new System.Drawing.Point(20, 107);
            this.lblOutputTopic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOutputTopic.Name = "lblOutputTopic";
            this.lblOutputTopic.Size = new System.Drawing.Size(72, 13);
            this.lblOutputTopic.TabIndex = 1;
            this.lblOutputTopic.Text = "Output Topic:";
            // 
            // lblInputTopic
            // 
            this.lblInputTopic.AutoSize = true;
            this.lblInputTopic.Location = new System.Drawing.Point(29, 72);
            this.lblInputTopic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblInputTopic.Name = "lblInputTopic";
            this.lblInputTopic.Size = new System.Drawing.Size(64, 13);
            this.lblInputTopic.TabIndex = 0;
            this.lblInputTopic.Text = "Input Topic:";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 221);
            this.Controls.Add(this.gbMQTT);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.gbMQTT.ResumeLayout(false);
            this.gbMQTT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox gbMQTT;
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
    }
}