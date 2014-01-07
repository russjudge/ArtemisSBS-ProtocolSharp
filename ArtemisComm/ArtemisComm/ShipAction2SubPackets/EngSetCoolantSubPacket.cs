using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class EngSetCoolantSubPacket : BasePacket
    {
        public static Packet GetPacket(ShipSystem system, int value)
        {
            EngSetCoolantSubPacket escsp = new EngSetCoolantSubPacket(system, value);
            ShipAction2Packet sap2 = new ShipAction2Packet(escsp);
            return new Packet(sap2);
        }
        public EngSetCoolantSubPacket(ShipSystem system, int value)
        {
            System = system;
            Value = value;
        }
        public EngSetCoolantSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public ShipSystem System { get; set; }

        public int Value { get; set; }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
       
    }
}