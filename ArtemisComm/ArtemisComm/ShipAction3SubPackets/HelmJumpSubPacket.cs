using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class HelmJumpSubPacket : ShipAction3
    {
        public static Packet GetPacket(float bearing, float distance)
        {
            HelmJumpSubPacket escsp = new HelmJumpSubPacket(bearing, distance);
            ShipAction3Packet sap2 = new ShipAction3Packet(escsp);
            return new Packet(sap2);
        }
        public HelmJumpSubPacket(float bearing, float distance) 
            : base(ShipAction3SubPacketType.HelmJumpSubPacket, bearing, distance) { }

        public HelmJumpSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public float Bearing
        {
            get
            {
                return Value1;
            }
            set
            {
                Value1 = value;
            }
        }

        public float Distance
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