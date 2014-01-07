
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmSetWarpSubPacket : ShipAction
    {
        
        public static Packet GetPacket(int warpFactor)
        {
            return new Packet(new ShipActionPacket(new HelmSetWarpSubPacket(warpFactor)));
        }

        public HelmSetWarpSubPacket(int warpFactor) : base(warpFactor) { }

        public HelmSetWarpSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

        [ArtemisExcluded]
        public int WarpFactor
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
