using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class HelmSetSteeringSubPacket : ShipAction3
    {
        public static Packet GetPacket(float turnValue)
        {
            HelmSetSteeringSubPacket escsp = new HelmSetSteeringSubPacket(turnValue);
            ShipAction3Packet sap2 = new ShipAction3Packet(escsp);
            return new Packet(sap2);
        }
        public HelmSetSteeringSubPacket(float turnValue) : base(ShipAction3SubPacketType.HelmSetSteeringSubPacket, turnValue, 0.0F) { }
      
        public HelmSetSteeringSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public float TurnValue { get; set; }

        
    }
}