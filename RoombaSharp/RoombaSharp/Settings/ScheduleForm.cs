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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iRobot.Data;

namespace RoombaSharp.Settings
{
    public partial class ScheduleForm : Form
    {

        #region Properties

        /// <summary>
        /// Schedule data.
        /// </summary>
        public ScheduleData ScheduleData { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ScheduleForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Buttons

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.SaveFields();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion
        
        #region Form

        private void ScheduleForm_Load(object sender, EventArgs e)
        {
            this.LoadFields();
        }

        private void ScheduleForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        #endregion

        #region Check Boxes

        private void cbSunEnb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox component = (CheckBox)sender;

            this.SetDay(DayOfWeek.Sunday, component.Checked);
        }

        private void cbMonEnb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox component = (CheckBox)sender;

            this.SetDay(DayOfWeek.Monday, component.Checked);
        }

        private void cbTueEnb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox component = (CheckBox)sender;

            this.SetDay(DayOfWeek.Tuesday, component.Checked);
        }

        private void cbWedEnb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox component = (CheckBox)sender;

            this.SetDay(DayOfWeek.Wednesday, component.Checked);
        }

        private void cbThuEnb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox component = (CheckBox)sender;

            this.SetDay(DayOfWeek.Thursday, component.Checked);
        }

        private void cbFriEnb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox component = (CheckBox)sender;

            this.SetDay(DayOfWeek.Friday, component.Checked);
        }

        private void cbSatEnb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox component = (CheckBox)sender;

            this.SetDay(DayOfWeek.Saturday, component.Checked);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Load field to the form.
        /// </summary>
        private void LoadFields()
        {
            // Validate value.
            if(RoombaSharp.Properties.Settings.Default.SchedulingData == null)
            {
                RoombaSharp.Properties.Settings.Default.SchedulingData = new ScheduleData();
                RoombaSharp.Properties.Settings.Default.SchedulingData.Monday = new RoombaDateTime();
                RoombaSharp.Properties.Settings.Default.SchedulingData.Tuesday = new RoombaDateTime();
                RoombaSharp.Properties.Settings.Default.SchedulingData.Wednesday = new RoombaDateTime();
                RoombaSharp.Properties.Settings.Default.SchedulingData.Thursday = new RoombaDateTime();
                RoombaSharp.Properties.Settings.Default.SchedulingData.Friday = new RoombaDateTime();
                RoombaSharp.Properties.Settings.Default.SchedulingData.Saturday = new RoombaDateTime();
                RoombaSharp.Properties.Settings.Default.SchedulingData.Sunday = new RoombaDateTime();
            }

            // Set temporal data.
            this.ScheduleData = RoombaSharp.Properties.Settings.Default.SchedulingData;

            this.tbMonHour.Text = this.ScheduleData.Monday.Hour.ToString();
            this.tbMonMinute.Text = this.ScheduleData.Monday.Minute.ToString();
            this.cbMonEnb.Checked = this.GetDay(DayOfWeek.Monday);

            this.tbTueHour.Text = this.ScheduleData.Tuesday.Hour.ToString();
            this.tbTueMinute.Text = this.ScheduleData.Tuesday.Minute.ToString();
            this.cbTueEnb.Checked = this.GetDay(DayOfWeek.Thursday);

            this.tbWedHour.Text = this.ScheduleData.Wednesday.Hour.ToString();
            this.tbWedMinute.Text = this.ScheduleData.Wednesday.Minute.ToString();
            this.cbWedEnb.Checked = this.GetDay(DayOfWeek.Wednesday);

            this.tbThuHour.Text = this.ScheduleData.Thursday.Hour.ToString();
            this.tbThuMinute.Text = this.ScheduleData.Thursday.Minute.ToString();
            this.cbThuEnb.Checked = this.GetDay(DayOfWeek.Thursday);

            this.tbFriHour.Text = this.ScheduleData.Friday.Hour.ToString();
            this.tbFriMinute.Text = this.ScheduleData.Friday.Minute.ToString();
            this.cbFriEnb.Checked = this.GetDay(DayOfWeek.Friday);

            this.tbSatHour.Text = this.ScheduleData.Saturday.Hour.ToString();
            this.tbSatMinute.Text = this.ScheduleData.Saturday.Minute.ToString();
            this.cbSatEnb.Checked = this.GetDay(DayOfWeek.Saturday);

            this.tbSunHour.Text = this.ScheduleData.Sunday.Hour.ToString();
            this.tbSunMinute.Text = this.ScheduleData.Sunday.Minute.ToString();
            this.cbSunEnb.Checked = this.GetDay(DayOfWeek.Sunday);
        }

        /// <summary>
        /// Save field from form.
        /// </summary>
        private void SaveFields()
        {
            /*
            try
            {
                int monHour;

                // Validate baud rate.
                if (int.TryParse(this.tbMonHour.Text.Trim(), out monHour))
                {
                    if (monHour < 0 || monHour > 23)
                    {
                        MessageBox.Show("Invalid Monday hour. [0 - 23]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    DateTime t = new DateTime();
                    t.AddHours(monHour);
                    //Properties.Settings.Default.SchedulingData.Monday.Hour = (byte)monHour;
                    // Save settings.
                    Properties.Settings.Default.Save();
                }
                else
                {
                    MessageBox.Show("Invalid Monday.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(String.Format("Message: {0}\r\nSource: {1}", err.Message, err.Source), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            */
            RoombaSharp.Properties.Settings.Default.SchedulingData = this.ScheduleData;

            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// Sat day of scheduler.
        /// </summary>
        /// <param name="day">Day of week.</param>
        /// <param name="enableDay">Enable day.</param>
        private void SetDay(DayOfWeek day, bool enableDay)
        {
            if(enableDay)
            {
                this.ScheduleData.Days = (DayOfWeek)iRobot.Utils.BitSet((byte)this.ScheduleData.Days, (byte)day);
            }
            else
            {
                this.ScheduleData.Days = (DayOfWeek)iRobot.Utils.BitSet((byte)this.ScheduleData.Days, (byte)day);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private bool GetDay(DayOfWeek day)
        {
            return iRobot.Utils.GetBit((byte)this.ScheduleData.Days, (byte)day);
        }

        #endregion


    }
}
