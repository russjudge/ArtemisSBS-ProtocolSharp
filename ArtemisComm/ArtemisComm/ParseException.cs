using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ArtemisComm
{
   
    [Serializable]
    public class ParseException : Exception
    {
        public ParseException() : base("Error parsing Artemis Protocol packet") { }
        public ParseException(Exception innerException) : base("Error parsing Artemis Protocol packet", innerException) { }
        public ParseException(string message, Exception innerException) : base(message, innerException) { }
        public ParseException(string message) : base(message) { }
        protected ParseException(SerializationInfo info, StreamingContext context) : base(info, context) { }

    }
}
