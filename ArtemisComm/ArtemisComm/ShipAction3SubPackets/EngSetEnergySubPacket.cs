using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class EngSetEnergySubPacket : BasePacket
    {
        public static Packet GetPacket(ShipSystem system, float value)
        {
            EngSetEnergySubPacket esesp = new EngSetEnergySubPacket(system, value);
            ShipAction3Packet sap3 = new ShipAction3Packet(esesp);
            return new Packet(sap3);
        }

        public EngSetEnergySubPacket(ShipSystem system, float value)
        {
            System = system;
            Value = value;
        }
        public EngSetEnergySubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public float Value { get; set; }

        public ShipSystem System { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
        
    }
}