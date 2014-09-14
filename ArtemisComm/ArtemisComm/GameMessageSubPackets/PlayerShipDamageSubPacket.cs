using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class PlayerShipDamageSubPacket : ParentPacket
    {
        //Subtype 05
        public PlayerShipDamageSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        public int Unknown1 { get; set; }

        public float Unknown2 { get; set; }
    }
}
