using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoombaSharp.iRobot.Messages
{
    public class MessageString : EventArgs
    {
        public string Message { get; private set; }
    
        public MessageString(string message)
        {
            this.Message = message;
        }
    }
}
