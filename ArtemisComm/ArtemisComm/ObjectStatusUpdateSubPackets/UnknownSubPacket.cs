using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class UnknownSubPacket : ParentPacket
    {
        public UnknownSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
