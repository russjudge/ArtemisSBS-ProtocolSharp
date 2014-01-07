using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class Unknown2Packet : BasePacket
    {
        public Unknown2Packet() : base()
        {

        }
        public Unknown2Packet(Stream stream, int index) : base(stream, index)
        {

        }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
