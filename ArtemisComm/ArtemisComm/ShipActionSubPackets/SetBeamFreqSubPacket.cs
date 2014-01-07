
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class SetBeamFreqSubPacket : ShipAction
    {


        public static Packet GetPacket(int frequencyIndex)
        {
            return new Packet(new ShipActionPacket(new SetBeamFreqSubPacket(frequencyIndex)));
        }

        public SetBeamFreqSubPacket(int frequencyIndex) : base(frequencyIndex) { }


        public SetBeamFreqSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public int FrequencyIndex
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
