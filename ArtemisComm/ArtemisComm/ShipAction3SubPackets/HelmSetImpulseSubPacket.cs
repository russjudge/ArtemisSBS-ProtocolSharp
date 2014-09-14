using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class HelmSetImpulseSubPacket : ShipAction3
    {
        public static Packet GetPacket(float velocity)
        {
            HelmSetImpulseSubPacket ssp = new HelmSetImpulseSubPacket(velocity);
            ShipAction3Packet sap = new ShipAction3Packet(ssp);
            Packet p = new Packet(sap);
            return p;
        }
        public HelmSetImpulseSubPacket(float velocity) : base(ShipAction3SubPacketType.HelmSetImpulseSubPacket, velocity, 0.0F) { }
        
        public HelmSetImpulseSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public float Velocity { get; set; }



    }
}