using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class EngSendDamconSubPacket : BasePacket
    {
        public static Packet GetPacket(int teamNumber, int x, int y, int z)
        {
            return new Packet(new ShipAction2Packet(new EngSendDamconSubPacket(teamNumber, x, y, z)));
        }
        public EngSendDamconSubPacket(int teamNumber, int x, int y, int z)
        {
            TeamNumber = teamNumber;
            X = x;
            Y = y;
            Z = z;
        }
        public EngSendDamconSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public int TeamNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
        
    }
}