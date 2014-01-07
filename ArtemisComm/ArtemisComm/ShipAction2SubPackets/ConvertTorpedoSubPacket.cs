using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class ConvertTorpedoSubPacket : BasePacket
    {
        public static Packet GetPacket(float direction, int unknown1, int unknown2, int unknown3)
        {
            ConvertTorpedoSubPacket subpack = null;
            ShipAction2Packet pack = null;
            Packet pck = null;
            Packet retVal = null;
            try
            {
                subpack = new ConvertTorpedoSubPacket(direction, unknown1, unknown2, unknown3);
                pack = new ShipAction2Packet(subpack);
                pck = new Packet(pack);
                retVal = pck;
                pck = null;
                subpack = null;
                pack = null;
                
            }
            finally
            {
                if (subpack != null)
                {
                    subpack.Dispose();
                }
                if (pack != null)
                {
                    pack.Dispose();
                }
                if (pck != null)
                {
                    pck.Dispose();
                }
                
            }
            return retVal;
        }
        public ConvertTorpedoSubPacket(float direction, int unknown1, int unknown2, int unknown3)
        {
            Direction = direction;
            Unknown1 = unknown1;
            Unknown2 = unknown2;
            Unknown3 = unknown3;
        }
        public ConvertTorpedoSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public float Direction { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public int Unknown3 { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
       
    }
}