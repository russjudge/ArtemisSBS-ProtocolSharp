using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class InvalidPacketException: Exception
    {
        public InvalidPacketException() : base("Packet is not valid Artemis Protocol packet") { }

    }
}
