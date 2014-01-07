
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmToggleReverseSubPacket : ShipAction
    {
        
        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new HelmToggleReverseSubPacket(value)));
        }

        public HelmToggleReverseSubPacket(int value) : base(value) { }
        public HelmToggleReverseSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

      
    }
}
