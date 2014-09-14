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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket()
        {
            return new Packet(new ShipActionPacket(new ToggleAutoBeamsSubPacket()));
        }

        public ToggleAutoBeamsSubPacket() : base(ShipActionSubPacketType.ToggleAutoBeamsSubPacket, 0) { }

        public ToggleAutoBeamsSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
