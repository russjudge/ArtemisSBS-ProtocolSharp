using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class TogglePerspectiveSubPacket : ShipAction
    {
        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new TogglePerspectiveSubPacket(value)));
        }

        public TogglePerspectiveSubPacket(int value) : base(value) { }

        public TogglePerspectiveSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }


    }
}
