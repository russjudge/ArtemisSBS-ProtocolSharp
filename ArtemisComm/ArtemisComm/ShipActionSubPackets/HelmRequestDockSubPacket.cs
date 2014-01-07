
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmRequestDockSubPacket : ShipAction
    {

     
        
        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new HelmRequestDockSubPacket(value)));
        }

        public HelmRequestDockSubPacket(int value) : base(value) { }

        public HelmRequestDockSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }


    }
}
