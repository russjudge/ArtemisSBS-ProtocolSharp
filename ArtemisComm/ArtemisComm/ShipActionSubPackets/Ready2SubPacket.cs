
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class Ready2SubPacket : ShipAction
    {
      
        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new Ready2SubPacket(value)));
        }

        public Ready2SubPacket(int value) : base(value) { }

        public Ready2SubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
