using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class DiveRiseSubPacket : ShipAction
    {
        
        
        public static Packet GetPacket(int delta)
        {
            return new Packet(new ShipActionPacket(new DiveRiseSubPacket(delta)));
        }

        public DiveRiseSubPacket(int delta) : base(ShipActionSubPacketType.DiveRiseSubPacket, delta) { }

        public DiveRiseSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public int Delta
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }

    }
}
