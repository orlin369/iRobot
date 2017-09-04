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
        public ScheduleData ScheduleData { get; private set; }

        public ScheduleForm()
        {
            InitializeComponent();

            this.ScheduleData = new ScheduleData();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.PaseData();
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        #region Private Methods

        private void PaseData()
        {
            throw new NotImplementedException();
        }

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

    }
}
