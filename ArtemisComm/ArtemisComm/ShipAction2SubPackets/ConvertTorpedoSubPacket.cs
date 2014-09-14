using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class ConvertTorpedoSubPacket : ShipAction2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket(TorpedoEnergyConversionTypes direction)
        {
            ConvertTorpedoSubPacket subpack = null;
            ShipAction2Packet pack = null;
            Packet pck = null;
            Packet retVal = null;
            try
            {
                subpack = new ConvertTorpedoSubPacket(direction);
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

        public ConvertTorpedoSubPacket(TorpedoEnergyConversionTypes direction)
            : base(ShipAction2SubPacketType.ConvertTorpedoSubPacket, (int)direction, 0, 0, 0)
        {
           
        }
      
        
        public ConvertTorpedoSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public TorpedoEnergyConversionTypes Direction
        {
            get
            {
                return (TorpedoEnergyConversionTypes)Value1;
            }
            set
            {
                Value1 = (int)value;
            }


        }
       
    }
}