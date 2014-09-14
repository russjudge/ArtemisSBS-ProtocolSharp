
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class SetMainScreenSubPacket : ShipAction
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket(MainScreenViewTypes value)
        {
            return new Packet(new ShipActionPacket(new SetMainScreenSubPacket(value)));
        }

        public SetMainScreenSubPacket(MainScreenViewTypes value) : base(ShipActionSubPacketType.SetMainScreenSubPacket, (int)value) { }

        public SetMainScreenSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public MainScreenViewTypes View
        {
            get
            {
                return (MainScreenViewTypes)Value;
            }
            set
            {
                Value = (int)value;
            }
        }

    }
}
