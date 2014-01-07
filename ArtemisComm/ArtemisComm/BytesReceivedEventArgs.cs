using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    internal class BytesReceivedEventArgs : ConnectionEventArgs
    {
        //public BytesReceivedEventArgs(byte[] byteArray, Guid connectionID) : base(connectionID)
        //{
            
        //    Buffer = byteArray;
        //    DataStream = new MemoryStream(Buffer);
        //}
        public BytesReceivedEventArgs(Stream stream, Guid connectionID)
            : base(connectionID)
        {
            DataStream = stream.GetMemoryStream(0);
            //Buffer = DataStream.GetBuffer();
        }
        //public byte[] Buffer { get; private set; }
        public Stream DataStream { get; private set; }
    }
}
