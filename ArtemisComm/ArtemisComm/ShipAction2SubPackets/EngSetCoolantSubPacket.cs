using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class EngSetCoolantSubPacket : ShipAction2
    {
        public static Packet GetPacket(ShipSystem system, int value)
        {
            EngSetCoolantSubPacket escsp = new EngSetCoolantSubPacket(system, value);
            ShipAction2Packet sap2 = new ShipAction2Packet(escsp);
            return new Packet(sap2);
        }
        public EngSetCoolantSubPacket(ShipSystem system, int value)
            : base(ShipAction2SubPacketType.EngSetCoolantSubPacket, (int)system, value, 0, 0)
        {
        }
        public EngSetCoolantSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public ShipSystem System
        {
            get
            {
                return (ShipSystem)Value1;
            }
            set
            {
                Value1 = (int)value;
            }
        }

        [ArtemisExcluded]
        public int Value
        {
            get
            {
                return Value2;
            }
            set
            {
                Value2 = value;
            }
        }



    }
}