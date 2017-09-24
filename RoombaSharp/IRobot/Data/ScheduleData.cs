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
using System.IO;
using System.Xml.Serialization;

namespace iRobot.Data
{
    [Serializable]
    public class ScheduleData
    {

        /// <summary>
        /// Days
        /// </summary>
        public byte Days;

        public RoombaDateTime Sunday;

        public RoombaDateTime Monday;

        public RoombaDateTime Tuesday;

        public RoombaDateTime Wednesday;

        public RoombaDateTime Thursday;

        public RoombaDateTime Friday;

        public RoombaDateTime Saturday;

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ScheduleData()
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Validate data content.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            bool isValid =
                   (this.Sunday != null)
                && (this.Monday != null)
                && (this.Thursday != null)
                && (this.Wednesday != null)
                && (this.Thursday != null)
                && (this.Friday != null)
                && (this.Saturday != null);

            return isValid;
        }

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Create empty object.
        /// </summary>
        /// <returns></returns>
        public static ScheduleData Create()
        {
            ScheduleData scheduleData = new ScheduleData();

            scheduleData.Monday = new RoombaDateTime();
            scheduleData.Tuesday = new RoombaDateTime();
            scheduleData.Wednesday = new RoombaDateTime();
            scheduleData.Thursday = new RoombaDateTime();
            scheduleData.Friday = new RoombaDateTime();
            scheduleData.Saturday = new RoombaDateTime();
            scheduleData.Sunday = new RoombaDateTime();

            return scheduleData;
        }

        /// <summary>
        /// Save device descriptions to XML.
        /// </summary>
        /// <remarks>@"C:\Temp\Serialization.xml"</remarks>
        /// <param name="settings">Device Descriptions</param>
        /// <param name="path">File</param>
        public static void Save(ScheduleData settings, string path)
        {
            using (FileStream file = File.Create(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ScheduleData));
                serializer.Serialize(file, settings);
            }
        }

        /// <summary>
        /// Read device descriptions from XML.
        /// </summary>
        /// <remarks>@"C:\Temp\Serialization.xml"</remarks>
        /// <param name="path">File</param>
        /// <returns>Device Descriptions</returns>
        public static ScheduleData Load(string path)
        {
            ScheduleData settings = new ScheduleData();

            XmlSerializer serializer = new XmlSerializer(typeof(ScheduleData));
            using (StreamReader file = new StreamReader(path))
            {
                settings = (ScheduleData)serializer.Deserialize(file);
            }

            return settings;
        }


        #endregion
    }
}
