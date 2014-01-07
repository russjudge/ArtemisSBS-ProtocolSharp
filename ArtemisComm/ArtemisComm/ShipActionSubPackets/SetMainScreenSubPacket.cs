
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class SetMainScreenSubPacket : ShipAction
    {

        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new SetMainScreenSubPacket(value)));
        }

        public SetMainScreenSubPacket(int value) : base(value) { }

        public SetMainScreenSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public int View
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }

    }
}
