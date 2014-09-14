
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class Ready2SubPacket : ShipAction
    {

        /*
       Believed to have something to do with a client's ready state, but it's currently unknown exactly what this packet means.
        */

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket()
        {
            return new Packet(new ShipActionPacket(new Ready2SubPacket()));
        }

        public Ready2SubPacket() : base(ShipActionSubPacketType.Ready2SubPacket, 0) { }

        public Ready2SubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
