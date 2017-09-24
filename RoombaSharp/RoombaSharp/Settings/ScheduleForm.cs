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

using iRobot.Data;

using System.IO;

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
            this.SetDefaultSettings();
            this.LoadFields();
        }

        private void ScheduleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.SaveFields();
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
        /// Set the default settings file.
        /// </summary>
        private void SetDefaultSettings()
        {
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Settings");

            // Create directory if does not exists.
            if (!Directory.Exists(path))
            {
                // Create directory.
                Directory.CreateDirectory(path);
            }

            // Create default settings if does not exists.
            if (!File.Exists(Properties.Settings.Default.SchedulingSettings))
            {
                // Create file.
                File.Create(Properties.Settings.Default.SchedulingSettings);

                // Save default settings path.
                Properties.Settings.Default.SchedulingSettings = Path.Combine(path, "Settings.XML");
                Properties.Settings.Default.Save();

                // Create empty object.
                ScheduleData scheduleData = ScheduleData.Create();

                // Save the object.
                ScheduleData.Save(scheduleData, Properties.Settings.Default.SchedulingSettings);
            }
        }

        /// <summary>
        /// Load field to the form.
        /// </summary>
        private void LoadFields()
        {
            // Set temporal data.
            this.ScheduleData = ScheduleData.Load(Properties.Settings.Default.SchedulingSettings);

            this.tbMonHour.Text = this.ScheduleData.Monday.Hour.ToString();
            this.tbMonMinute.Text = this.ScheduleData.Monday.Minute.ToString();
            this.cbMonEnb.Checked = this.GetDay(DayOfWeek.Monday);

            this.tbTueHour.Text = this.ScheduleData.Tuesday.Hour.ToString();
            this.tbTueMinute.Text = this.ScheduleData.Tuesday.Minute.ToString();
            this.cbTueEnb.Checked = this.GetDay(DayOfWeek.Tuesday);

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
            try
            {
                this.SetHour(this.tbMonHour.Text.Trim(), out this.ScheduleData.Monday.Hour);
                this.SetMinute(this.tbMonMinute.Text.Trim(), out this.ScheduleData.Monday.Minute);

                this.SetHour(this.tbTueHour.Text.Trim(), out this.ScheduleData.Tuesday.Hour);
                this.SetMinute(this.tbTueMinute.Text.Trim(), out this.ScheduleData.Tuesday.Minute);

                this.SetHour(this.tbWedHour.Text.Trim(), out this.ScheduleData.Wednesday.Hour);
                this.SetMinute(this.tbWedMinute.Text.Trim(), out this.ScheduleData.Wednesday.Minute);

                this.SetHour(this.tbThuHour.Text.Trim(), out this.ScheduleData.Thursday.Hour);
                this.SetMinute(this.tbThuMinute.Text.Trim(), out this.ScheduleData.Thursday.Minute);

                this.SetHour(this.tbFriHour.Text.Trim(), out this.ScheduleData.Friday.Hour);
                this.SetMinute(this.tbFriMinute.Text.Trim(), out this.ScheduleData.Friday.Minute);

                this.SetHour(this.tbSatHour.Text.Trim(), out this.ScheduleData.Saturday.Hour);
                this.SetMinute(this.tbSatMinute.Text.Trim(), out this.ScheduleData.Saturday.Minute);

                this.SetHour(this.tbSunHour.Text.Trim(), out this.ScheduleData.Sunday.Hour);
                this.SetMinute(this.tbSunMinute.Text.Trim(), out this.ScheduleData.Sunday.Minute);

                ScheduleData.Save(this.ScheduleData, Properties.Settings.Default.SchedulingSettings);
            }
            catch (Exception exception)
            {
                Logger.Log.CreateRecord("RoombaSharp.Settings.ScheduleForm.SaveFields()", exception.ToString(), Logger.LogMessageTypes.Error);
            }
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
                this.ScheduleData.Days = iRobot.Utils.BitSet(this.ScheduleData.Days, (byte)day);
            }
            else
            {
                this.ScheduleData.Days = iRobot.Utils.BitClear(this.ScheduleData.Days, (byte)day);
            }

            Console.WriteLine("Day value: {0}", this.ScheduleData.Days);
        }
        
        /// <summary>
        /// Return true if the day is set.
        /// </summary>
        /// <param name="day">Day of week.</param>
        /// <returns>Day state.</returns>
        private bool GetDay(DayOfWeek day)
        {
            return iRobot.Utils.GetBit((byte)this.ScheduleData.Days, (byte)day);
        }

        /// <summary>
        /// Parse minute from text.
        /// </summary>
        /// <param name="textValue">Text value.</param>
        /// <param name="intValue">Int value.</param>
        private void SetMinute(string textValue, out int intValue)
        {
            int tempValue = 0;
            intValue = tempValue;

            if (int.TryParse(textValue, out tempValue))
            {
                if (this.IsValiMinute(tempValue))
                {
                    intValue = tempValue;
                }
                else
                {
                    MessageBox.Show("Invalid minute. [0 - 59]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Invalid minute.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// Parse hour from text.
        /// </summary>
        /// <param name="textValue">Text value.</param>
        /// <param name="intValue">Int value.</param>
        private void SetHour(string textValue, out int intValue)
        {
            int tempValue = 0;
            intValue = tempValue;

            if (int.TryParse(textValue, out tempValue))
            {
                if (this.IsValiHour(tempValue))
                {
                    intValue = tempValue;
                }
                else
                {
                    MessageBox.Show("Invalid hour. [0 - 23]", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Invalid hour.", "Invalid value", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        /// <summary>
        /// Validate minute value.
        /// </summary>
        /// <param name="minute">Minute value.</param>
        /// <returns>Is valid state.</returns>
        private bool IsValiMinute(int minute)
        {
            return !(minute < 0 || minute > 59);
        }

        /// <summary>
        /// Validate hour value.
        /// </summary>
        /// <param name="hour">Hour value.</param>
        /// <returns>Is valid state.</returns>
        private bool IsValiHour(int hour)
        {
            return !(hour < 0 || hour > 23);
        }

        #endregion


    }
}
