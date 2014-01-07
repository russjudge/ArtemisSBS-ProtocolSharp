
using System.IO;
namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class UnnamedObjectUpdate : VariablePackage
    {

        public UnnamedObjectUpdate(Stream stream, int index)
            : base(stream, index)
        {

        }
        public byte? Unknown { get; set; }

        public float? X { get; set; }
        public float? Y { get; set; }
        public float? Z { get; set; }

        public int? Unknown1 { get; set; }

        public int? Unknown2 { get; set; }

        public int? Unknown3 { get; set; }

        public int? Unknown4 { get; set; }

        public int? Unknown5 { get; set; }

        public int? Unknown6 { get; set; }

    }
}
