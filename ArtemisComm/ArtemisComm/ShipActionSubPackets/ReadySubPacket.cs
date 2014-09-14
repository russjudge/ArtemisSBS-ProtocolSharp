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
            /*
         Indicates that this client is ready to join the game. The client must select at least one station before sending this packet.
         When a game ends, the client typically sends this packet again immediately, on the assumption that they will play again with the same station(s).
         */
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket()
        {
            return new Packet(new ShipActionPacket(new ReadySubPacket()));
        }

        public ReadySubPacket() : base(ShipActionSubPacketType.ReadySubPacket, 0) { }
        public ReadySubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
