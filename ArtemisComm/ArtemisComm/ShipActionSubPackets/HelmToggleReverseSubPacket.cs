
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmToggleReverseSubPacket : ShipAction
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket()
        {
            return new Packet(new ShipActionPacket(new HelmToggleReverseSubPacket()));
        }

        public HelmToggleReverseSubPacket() : base(ShipActionSubPacketType.HelmToggleReverseSubPacket, 0) { }
        public HelmToggleReverseSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

      
    }
}
