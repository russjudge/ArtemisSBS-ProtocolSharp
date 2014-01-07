using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArtemisComm
{
  
    [Serializable]
    public class PacketProcessingException : Exception
    {
        public PacketProcessingException() : base("Exception processing packet") { }
        public PacketProcessingException(Exception innerException) : base("Exception processing packet", innerException) { }
        public PacketProcessingException(string message, Exception innerException) : base(message, innerException) { }
        public PacketProcessingException(string message) : base(message) { }
        protected PacketProcessingException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
