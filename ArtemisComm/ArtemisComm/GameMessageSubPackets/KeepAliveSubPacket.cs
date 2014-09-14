using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class KeepAliveSubPacket : ParentPacket
    {
        public KeepAliveSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }


        public float Unknown1 { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
