using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class WhaleUpdateSubPacket : VariablePackage
    {
        public WhaleUpdateSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public ArtemisString Name { get; set; }
        public int? Unknown { get; set; }
        public int? Unknown2 { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        public float? Z { get; set; }
        public int? Unknown3 { get; set; }
        public int? Unknown4 { get; set; }
        public float? Heading { get; set; }
        public int? Unknown5 { get; set; }
        public int? Unknown6 { get; set; }

        public int? Unknown7 { get; set; }
        public int? Unknown8 { get; set; }
        public int? Unknown9 { get; set; }
        public int? Unknown10 { get; set; }
        public int? Unknown11 { get; set; }
    }
}
