﻿/*
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
    partial class ScheduleForm
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tbMonHour = new System.Windows.Forms.TextBox();
            this.tbMonMinute = new System.Windows.Forms.TextBox();
            this.tbTueHour = new System.Windows.Forms.TextBox();
            this.tbTueMinute = new System.Windows.Forms.TextBox();
            this.tbWedHour = new System.Windows.Forms.TextBox();
            this.tbWedMinute = new System.Windows.Forms.TextBox();
            this.tbThuHour = new System.Windows.Forms.TextBox();
            this.tbThuMinute = new System.Windows.Forms.TextBox();
            this.tbFriHour = new System.Windows.Forms.TextBox();
            this.tbFriMinute = new System.Windows.Forms.TextBox();
            this.lblMonday = new System.Windows.Forms.Label();
            this.lblTuesday = new System.Windows.Forms.Label();
            this.lblThursday = new System.Windows.Forms.Label();
            this.lblWednesday = new System.Windows.Forms.Label();
            this.lblFriday = new System.Windows.Forms.Label();
            this.tbSatMinute = new System.Windows.Forms.TextBox();
            this.tbSatHour = new System.Windows.Forms.TextBox();
            this.tbSunMinute = new System.Windows.Forms.TextBox();
            this.tbSunHour = new System.Windows.Forms.TextBox();
            this.lblSaturday = new System.Windows.Forms.Label();
            this.lblSunday = new System.Windows.Forms.Label();
            this.cbMonEnb = new System.Windows.Forms.CheckBox();
            this.cbTueEnb = new System.Windows.Forms.CheckBox();
            this.cbThuEnb = new System.Windows.Forms.CheckBox();
            this.cbWedEnb = new System.Windows.Forms.CheckBox();
            this.cbFriEnb = new System.Windows.Forms.CheckBox();
            this.cbSatEnb = new System.Windows.Forms.CheckBox();
            this.cbSunEnb = new System.Windows.Forms.CheckBox();
            this.lblDays = new System.Windows.Forms.Label();
            this.lblEnable = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(158, 229);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(264, 229);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tbMonHour
            // 
            this.tbMonHour.Location = new System.Drawing.Point(158, 73);
            this.tbMonHour.Name = "tbMonHour";
            this.tbMonHour.Size = new System.Drawing.Size(100, 20);
            this.tbMonHour.TabIndex = 2;
            // 
            // tbMonMinute
            // 
            this.tbMonMinute.Location = new System.Drawing.Point(264, 73);
            this.tbMonMinute.Name = "tbMonMinute";
            this.tbMonMinute.Size = new System.Drawing.Size(100, 20);
            this.tbMonMinute.TabIndex = 3;
            // 
            // tbTueHour
            // 
            this.tbTueHour.Location = new System.Drawing.Point(158, 99);
            this.tbTueHour.Name = "tbTueHour";
            this.tbTueHour.Size = new System.Drawing.Size(100, 20);
            this.tbTueHour.TabIndex = 4;
            // 
            // tbTueMinute
            // 
            this.tbTueMinute.Location = new System.Drawing.Point(264, 99);
            this.tbTueMinute.Name = "tbTueMinute";
            this.tbTueMinute.Size = new System.Drawing.Size(100, 20);
            this.tbTueMinute.TabIndex = 5;
            // 
            // tbWedHour
            // 
            this.tbWedHour.Location = new System.Drawing.Point(158, 125);
            this.tbWedHour.Name = "tbWedHour";
            this.tbWedHour.Size = new System.Drawing.Size(100, 20);
            this.tbWedHour.TabIndex = 6;
            // 
            // tbWedMinute
            // 
            this.tbWedMinute.Location = new System.Drawing.Point(264, 125);
            this.tbWedMinute.Name = "tbWedMinute";
            this.tbWedMinute.Size = new System.Drawing.Size(100, 20);
            this.tbWedMinute.TabIndex = 7;
            // 
            // tbThuHour
            // 
            this.tbThuHour.Location = new System.Drawing.Point(158, 151);
            this.tbThuHour.Name = "tbThuHour";
            this.tbThuHour.Size = new System.Drawing.Size(100, 20);
            this.tbThuHour.TabIndex = 8;
            // 
            // tbThuMinute
            // 
            this.tbThuMinute.Location = new System.Drawing.Point(264, 151);
            this.tbThuMinute.Name = "tbThuMinute";
            this.tbThuMinute.Size = new System.Drawing.Size(100, 20);
            this.tbThuMinute.TabIndex = 9;
            // 
            // tbFriHour
            // 
            this.tbFriHour.Location = new System.Drawing.Point(158, 177);
            this.tbFriHour.Name = "tbFriHour";
            this.tbFriHour.Size = new System.Drawing.Size(100, 20);
            this.tbFriHour.TabIndex = 10;
            // 
            // tbFriMinute
            // 
            this.tbFriMinute.Location = new System.Drawing.Point(264, 177);
            this.tbFriMinute.Name = "tbFriMinute";
            this.tbFriMinute.Size = new System.Drawing.Size(100, 20);
            this.tbFriMinute.TabIndex = 11;
            // 
            // lblMonday
            // 
            this.lblMonday.AutoSize = true;
            this.lblMonday.Location = new System.Drawing.Point(42, 76);
            this.lblMonday.Name = "lblMonday";
            this.lblMonday.Size = new System.Drawing.Size(45, 13);
            this.lblMonday.TabIndex = 12;
            this.lblMonday.Text = "Monday";
            // 
            // lblTuesday
            // 
            this.lblTuesday.AutoSize = true;
            this.lblTuesday.Location = new System.Drawing.Point(39, 102);
            this.lblTuesday.Name = "lblTuesday";
            this.lblTuesday.Size = new System.Drawing.Size(48, 13);
            this.lblTuesday.TabIndex = 13;
            this.lblTuesday.Text = "Tuesday";
            // 
            // lblThursday
            // 
            this.lblThursday.AutoSize = true;
            this.lblThursday.Location = new System.Drawing.Point(36, 154);
            this.lblThursday.Name = "lblThursday";
            this.lblThursday.Size = new System.Drawing.Size(51, 13);
            this.lblThursday.TabIndex = 14;
            this.lblThursday.Text = "Thursday";
            // 
            // lblWednesday
            // 
            this.lblWednesday.AutoSize = true;
            this.lblWednesday.Location = new System.Drawing.Point(23, 128);
            this.lblWednesday.Name = "lblWednesday";
            this.lblWednesday.Size = new System.Drawing.Size(64, 13);
            this.lblWednesday.TabIndex = 15;
            this.lblWednesday.Text = "Wednesday";
            // 
            // lblFriday
            // 
            this.lblFriday.AutoSize = true;
            this.lblFriday.Location = new System.Drawing.Point(52, 180);
            this.lblFriday.Name = "lblFriday";
            this.lblFriday.Size = new System.Drawing.Size(35, 13);
            this.lblFriday.TabIndex = 16;
            this.lblFriday.Text = "Friday";
            // 
            // tbSatMinute
            // 
            this.tbSatMinute.Location = new System.Drawing.Point(264, 203);
            this.tbSatMinute.Name = "tbSatMinute";
            this.tbSatMinute.Size = new System.Drawing.Size(100, 20);
            this.tbSatMinute.TabIndex = 18;
            // 
            // tbSatHour
            // 
            this.tbSatHour.Location = new System.Drawing.Point(158, 203);
            this.tbSatHour.Name = "tbSatHour";
            this.tbSatHour.Size = new System.Drawing.Size(100, 20);
            this.tbSatHour.TabIndex = 17;
            // 
            // tbSunMinute
            // 
            this.tbSunMinute.Location = new System.Drawing.Point(264, 45);
            this.tbSunMinute.Name = "tbSunMinute";
            this.tbSunMinute.Size = new System.Drawing.Size(100, 20);
            this.tbSunMinute.TabIndex = 20;
            // 
            // tbSunHour
            // 
            this.tbSunHour.Location = new System.Drawing.Point(158, 45);
            this.tbSunHour.Name = "tbSunHour";
            this.tbSunHour.Size = new System.Drawing.Size(100, 20);
            this.tbSunHour.TabIndex = 19;
            // 
            // lblSaturday
            // 
            this.lblSaturday.AutoSize = true;
            this.lblSaturday.Location = new System.Drawing.Point(38, 206);
            this.lblSaturday.Name = "lblSaturday";
            this.lblSaturday.Size = new System.Drawing.Size(49, 13);
            this.lblSaturday.TabIndex = 21;
            this.lblSaturday.Text = "Saturday";
            // 
            // lblSunday
            // 
            this.lblSunday.AutoSize = true;
            this.lblSunday.Location = new System.Drawing.Point(44, 48);
            this.lblSunday.Name = "lblSunday";
            this.lblSunday.Size = new System.Drawing.Size(43, 13);
            this.lblSunday.TabIndex = 22;
            this.lblSunday.Text = "Sunday";
            // 
            // cbMonEnb
            // 
            this.cbMonEnb.AutoSize = true;
            this.cbMonEnb.Location = new System.Drawing.Point(93, 75);
            this.cbMonEnb.Name = "cbMonEnb";
            this.cbMonEnb.Size = new System.Drawing.Size(59, 17);
            this.cbMonEnb.TabIndex = 23;
            this.cbMonEnb.Text = "Enable";
            this.cbMonEnb.UseVisualStyleBackColor = true;
            this.cbMonEnb.CheckedChanged += new System.EventHandler(this.cbMonEnb_CheckedChanged);
            // 
            // cbTueEnb
            // 
            this.cbTueEnb.AutoSize = true;
            this.cbTueEnb.Location = new System.Drawing.Point(93, 101);
            this.cbTueEnb.Name = "cbTueEnb";
            this.cbTueEnb.Size = new System.Drawing.Size(59, 17);
            this.cbTueEnb.TabIndex = 24;
            this.cbTueEnb.Text = "Enable";
            this.cbTueEnb.UseVisualStyleBackColor = true;
            this.cbTueEnb.CheckedChanged += new System.EventHandler(this.cbTueEnb_CheckedChanged);
            // 
            // cbThuEnb
            // 
            this.cbThuEnb.AutoSize = true;
            this.cbThuEnb.Location = new System.Drawing.Point(93, 153);
            this.cbThuEnb.Name = "cbThuEnb";
            this.cbThuEnb.Size = new System.Drawing.Size(59, 17);
            this.cbThuEnb.TabIndex = 25;
            this.cbThuEnb.Text = "Enable";
            this.cbThuEnb.UseVisualStyleBackColor = true;
            this.cbThuEnb.CheckedChanged += new System.EventHandler(this.cbThuEnb_CheckedChanged);
            // 
            // cbWedEnb
            // 
            this.cbWedEnb.AutoSize = true;
            this.cbWedEnb.Location = new System.Drawing.Point(93, 127);
            this.cbWedEnb.Name = "cbWedEnb";
            this.cbWedEnb.Size = new System.Drawing.Size(59, 17);
            this.cbWedEnb.TabIndex = 26;
            this.cbWedEnb.Text = "Enable";
            this.cbWedEnb.UseVisualStyleBackColor = true;
            this.cbWedEnb.CheckedChanged += new System.EventHandler(this.cbWedEnb_CheckedChanged);
            // 
            // cbFriEnb
            // 
            this.cbFriEnb.AutoSize = true;
            this.cbFriEnb.Location = new System.Drawing.Point(93, 179);
            this.cbFriEnb.Name = "cbFriEnb";
            this.cbFriEnb.Size = new System.Drawing.Size(59, 17);
            this.cbFriEnb.TabIndex = 27;
            this.cbFriEnb.Text = "Enable";
            this.cbFriEnb.UseVisualStyleBackColor = true;
            this.cbFriEnb.CheckedChanged += new System.EventHandler(this.cbFriEnb_CheckedChanged);
            // 
            // cbSatEnb
            // 
            this.cbSatEnb.AutoSize = true;
            this.cbSatEnb.Location = new System.Drawing.Point(93, 205);
            this.cbSatEnb.Name = "cbSatEnb";
            this.cbSatEnb.Size = new System.Drawing.Size(59, 17);
            this.cbSatEnb.TabIndex = 28;
            this.cbSatEnb.Text = "Enable";
            this.cbSatEnb.UseVisualStyleBackColor = true;
            this.cbSatEnb.CheckedChanged += new System.EventHandler(this.cbSatEnb_CheckedChanged);
            // 
            // cbSunEnb
            // 
            this.cbSunEnb.AutoSize = true;
            this.cbSunEnb.Location = new System.Drawing.Point(93, 47);
            this.cbSunEnb.Name = "cbSunEnb";
            this.cbSunEnb.Size = new System.Drawing.Size(59, 17);
            this.cbSunEnb.TabIndex = 29;
            this.cbSunEnb.Text = "Enable";
            this.cbSunEnb.UseVisualStyleBackColor = true;
            this.cbSunEnb.CheckedChanged += new System.EventHandler(this.cbSunEnb_CheckedChanged);
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.Location = new System.Drawing.Point(23, 19);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(31, 13);
            this.lblDays.TabIndex = 30;
            this.lblDays.Text = "Days";
            // 
            // lblEnable
            // 
            this.lblEnable.AutoSize = true;
            this.lblEnable.Location = new System.Drawing.Point(90, 19);
            this.lblEnable.Name = "lblEnable";
            this.lblEnable.Size = new System.Drawing.Size(40, 13);
            this.lblEnable.TabIndex = 31;
            this.lblEnable.Text = "Enable";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Location = new System.Drawing.Point(155, 19);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(35, 13);
            this.lblHours.TabIndex = 32;
            this.lblHours.Text = "Hours";
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Location = new System.Drawing.Point(261, 19);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(44, 13);
            this.lblMinutes.TabIndex = 33;
            this.lblMinutes.Text = "Minutes";
            // 
            // ScheduleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 262);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.lblEnable);
            this.Controls.Add(this.lblDays);
            this.Controls.Add(this.cbSunEnb);
            this.Controls.Add(this.cbSatEnb);
            this.Controls.Add(this.cbFriEnb);
            this.Controls.Add(this.cbWedEnb);
            this.Controls.Add(this.cbThuEnb);
            this.Controls.Add(this.cbTueEnb);
            this.Controls.Add(this.cbMonEnb);
            this.Controls.Add(this.lblSunday);
            this.Controls.Add(this.lblSaturday);
            this.Controls.Add(this.tbSunMinute);
            this.Controls.Add(this.tbSunHour);
            this.Controls.Add(this.tbSatMinute);
            this.Controls.Add(this.tbSatHour);
            this.Controls.Add(this.lblFriday);
            this.Controls.Add(this.lblWednesday);
            this.Controls.Add(this.lblThursday);
            this.Controls.Add(this.lblTuesday);
            this.Controls.Add(this.lblMonday);
            this.Controls.Add(this.tbFriMinute);
            this.Controls.Add(this.tbFriHour);
            this.Controls.Add(this.tbThuMinute);
            this.Controls.Add(this.tbThuHour);
            this.Controls.Add(this.tbWedMinute);
            this.Controls.Add(this.tbWedHour);
            this.Controls.Add(this.tbTueMinute);
            this.Controls.Add(this.tbTueHour);
            this.Controls.Add(this.tbMonMinute);
            this.Controls.Add(this.tbMonHour);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScheduleForm";
            this.Text = "Schedule";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScheduleForm_FormClosing);
            this.Load += new System.EventHandler(this.ScheduleForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tbMonHour;
        private System.Windows.Forms.TextBox tbMonMinute;
        private System.Windows.Forms.TextBox tbTueHour;
        private System.Windows.Forms.TextBox tbTueMinute;
        private System.Windows.Forms.TextBox tbWedHour;
        private System.Windows.Forms.TextBox tbWedMinute;
        private System.Windows.Forms.TextBox tbThuHour;
        private System.Windows.Forms.TextBox tbThuMinute;
        private System.Windows.Forms.TextBox tbFriHour;
        private System.Windows.Forms.TextBox tbFriMinute;
        private System.Windows.Forms.Label lblMonday;
        private System.Windows.Forms.Label lblTuesday;
        private System.Windows.Forms.Label lblThursday;
        private System.Windows.Forms.Label lblWednesday;
        private System.Windows.Forms.Label lblFriday;
        private System.Windows.Forms.TextBox tbSatMinute;
        private System.Windows.Forms.TextBox tbSatHour;
        private System.Windows.Forms.TextBox tbSunMinute;
        private System.Windows.Forms.TextBox tbSunHour;
        private System.Windows.Forms.Label lblSaturday;
        private System.Windows.Forms.Label lblSunday;
        private System.Windows.Forms.CheckBox cbMonEnb;
        private System.Windows.Forms.CheckBox cbTueEnb;
        private System.Windows.Forms.CheckBox cbThuEnb;
        private System.Windows.Forms.CheckBox cbWedEnb;
        private System.Windows.Forms.CheckBox cbFriEnb;
        private System.Windows.Forms.CheckBox cbSatEnb;
        private System.Windows.Forms.CheckBox cbSunEnb;
        private System.Windows.Forms.Label lblDays;
        private System.Windows.Forms.Label lblEnable;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblMinutes;
    }
}