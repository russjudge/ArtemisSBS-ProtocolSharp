
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class IntelPacket : ParentPacket
    {


        public IntelPacket(Stream stream, int index) : base(stream, index) { }
        public int ID { get; set; }
        public byte Unknown2 { get; set; }

        public ArtemisString Message { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
