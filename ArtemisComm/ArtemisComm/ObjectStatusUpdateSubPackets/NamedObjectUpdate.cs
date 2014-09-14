
using System.IO;
namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class NamedObjectUpdate : VariablePackage
    {


        public NamedObjectUpdate(Stream stream, int index)
            : base(stream, index)
        {

        }


        public byte? Unknown { get; set; }

        public float? X { get; set; }
        public float? Y { get; set; }
        public float? Z { get; set; }

        public ArtemisString Name { get; set; }

        public int? Unknown2 { get; set; }

        public float? Unknown3 { get; set; }

        public float? Unknown4 { get; set; }

        public int? Unknown5 { get; set; }

        public int? Unknown6 { get; set; }

    }
}
