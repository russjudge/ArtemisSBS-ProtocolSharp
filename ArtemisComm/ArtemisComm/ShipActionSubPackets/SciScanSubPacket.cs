
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class SciScanSubPacket : ShipAction
    {
        public static Packet GetPacket(int targetID)
        {
            return new Packet(new ShipActionPacket(new SciScanSubPacket(targetID)));
        }

        public SciScanSubPacket(int targetID) : base(ShipActionSubPacketType.SciScanSubPacket, targetID) { }
        public SciScanSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public int TargetID
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
