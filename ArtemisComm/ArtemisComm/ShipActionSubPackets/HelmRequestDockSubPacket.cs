
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmRequestDockSubPacket : ShipAction
    {



        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket()
        {
            return new Packet(new ShipActionPacket(new HelmRequestDockSubPacket()));
        }

        public HelmRequestDockSubPacket() : base(ShipActionSubPacketType.HelmRequestDockSubPacket, 0) { }

        public HelmRequestDockSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }


    }
}
