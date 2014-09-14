using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetWeaponsTargetSubPacket : ShipAction
    {
        
        public static Packet GetPacket(int targetId)
        {
            return new Packet(new ShipActionPacket(new SetWeaponsTargetSubPacket(targetId)));
        }


        public SetWeaponsTargetSubPacket(int targetId) : base(ShipActionSubPacketType.SetWeaponsTargetSubPacket, targetId) { }


        public SetWeaponsTargetSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public int TargetID
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }

    }
}
