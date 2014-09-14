
using System.IO;
namespace ArtemisComm.ShipActionSubPackets
{
    public class SetBeamFreqSubPacket : ShipAction
    {


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket(BeamFrequencyTypes frequencyIndex)
        {
            return new Packet(new ShipActionPacket(new SetBeamFreqSubPacket(frequencyIndex)));
        }

        public SetBeamFreqSubPacket(BeamFrequencyTypes frequencyIndex) : base(ShipActionSubPacketType.SetBeamFreqSubPacket, (int)frequencyIndex) { }


        public SetBeamFreqSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public BeamFrequencyTypes FrequencyIndex
        {
            get
            {
                return (BeamFrequencyTypes)Value;
            }
            set
            {
                Value = (int)value;
            }
        }

    }
}
