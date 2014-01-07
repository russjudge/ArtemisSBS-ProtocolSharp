using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ToggleShieldsSubPacket : ShipAction
    {
        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new ToggleShieldsSubPacket(value)));
        }


        public ToggleShieldsSubPacket(int value) : base(value) { }


        public ToggleShieldsSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
