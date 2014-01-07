using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class HelmJumpSubPacket : BasePacket
    {
        public static Packet GetPacket(float bearing, float distance)
        {
            HelmJumpSubPacket escsp = new HelmJumpSubPacket(bearing, distance);
            ShipAction3Packet sap2 = new ShipAction3Packet(escsp);
            return new Packet(sap2);
        }
        public HelmJumpSubPacket(float bearing, float distance)
        {
            Bearing = bearing;
            Distance = distance;
        }
        public HelmJumpSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public float Bearing { get; set; }

        public float Distance { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
        
    }
}