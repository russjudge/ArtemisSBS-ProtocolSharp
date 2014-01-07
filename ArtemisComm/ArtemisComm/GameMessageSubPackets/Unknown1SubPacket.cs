using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class Unknown1SubPacket : BasePacket
    {
        //Subtype 00
        public Unknown1SubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public int Unknown1 { get; set; }

        public int Unknown2 { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
