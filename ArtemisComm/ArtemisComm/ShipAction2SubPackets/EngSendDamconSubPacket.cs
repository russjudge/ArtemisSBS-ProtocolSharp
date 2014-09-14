using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class EngSendDamconSubPacket : ShipAction2
    {
        public static Packet GetPacket(int teamNumber, int x, int y, int z)
        {
            return new Packet(new ShipAction2Packet(new EngSendDamconSubPacket(teamNumber, x, y, z)));
        }
        public EngSendDamconSubPacket(int teamNumber, int x, int y, int z)
            : base(ShipAction2SubPacketType.EngSendDamconSubPacket, teamNumber, x, y, z)
        {

        }
        public EngSendDamconSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

        [ArtemisExcluded]
        public int TeamNumber
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
        [ArtemisExcluded]
        public int X
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
        [ArtemisExcluded]
        public int Y
        {
            get
            {
                return Value3;
            }
            set
            {
                Value3 = value;
            }
        }
        [ArtemisExcluded]
        public int Z
        {
            get
            {
                return Value4;
            }
            set
            {
                Value4 = value;
            }
        }



    }
}