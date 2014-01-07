using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class CaptainSelectSubPacket : ShipAction
    {

        public static Packet GetPacket(int targetID)
        {
            return new Packet(new ShipActionPacket(new CaptainSelectSubPacket(targetID)));
        }

        public CaptainSelectSubPacket(int targetID) : base(targetID) { }

        public CaptainSelectSubPacket(Stream stream, int index)
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
