
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class SciSelectSubPacket : ShipAction
    {

        public static Packet GetPacket(int targetID)
        {
            return new Packet(new ShipActionPacket(new SciSelectSubPacket(targetID)));
        }

        public SciSelectSubPacket(int targetID) : base(ShipActionSubPacketType.SciSelectSubPacket, targetID) { }

        public SciSelectSubPacket(Stream stream, int index)
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
