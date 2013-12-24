using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    internal class BytesReceivedEventArgs : ConnectionEventArgs
    {
        public BytesReceivedEventArgs(byte[] byteArray, Guid connectionID) : base(connectionID)
        {
            
            Buffer = byteArray;
        }
        
        public byte[] Buffer { get; private set; }
    }
}
