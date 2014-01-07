using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class HelmSetImpulseSubPacket : BasePacket
    {
        public static Packet GetPacket(float velocity)
        {
            HelmSetImpulseSubPacket ssp = new HelmSetImpulseSubPacket(velocity);
            ShipAction3Packet sap = new ShipAction3Packet(ssp);
            Packet p = new Packet(sap);
            return p;
        }
        public HelmSetImpulseSubPacket(float velocity)
        {
            Velocity = velocity;
        }
        public HelmSetImpulseSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public float Velocity { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
       
    }
}