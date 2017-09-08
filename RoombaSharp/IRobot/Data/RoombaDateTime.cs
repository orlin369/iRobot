using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRobot.Data
{
    [Serializable]
    public class RoombaDateTime
    {

        public byte Hour { get; set; }

        public byte Minute { get; set; }

        public RoombaDateTime()
        {

        }

    }
}
