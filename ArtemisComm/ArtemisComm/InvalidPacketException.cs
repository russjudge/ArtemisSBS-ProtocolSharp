using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArtemisComm
{
    [Serializable]
    public class InvalidPacketException : Exception
    {
        public InvalidPacketException() : base("Packet is not valid Artemis Protocol packet") { }
        public InvalidPacketException(Exception innerException) : base("Packet is not valid Artemis Protocol packet", innerException) { }
        public InvalidPacketException(string message, Exception innerException) : base(message, innerException) { }
        public InvalidPacketException(string message) : base(message) { }
        protected InvalidPacketException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
