using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ReadySubPacket : ShipAction
    {
     
        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new ReadySubPacket(value)));
        }

        public ReadySubPacket(int value) : base(value) { }
        public ReadySubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
