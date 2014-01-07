using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ToggleAutoBeamsSubPacket : ShipAction
    {

        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new ToggleAutoBeamsSubPacket(value)));
        }

        public ToggleAutoBeamsSubPacket(int value) : base(value) { }

        public ToggleAutoBeamsSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
