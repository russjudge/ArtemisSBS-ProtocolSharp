using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class Unknown3SubPacket : ParentPacket
    {
        //Subtype 07
        public Unknown3SubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
