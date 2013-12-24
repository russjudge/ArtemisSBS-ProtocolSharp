using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class StationSubPacket : VariablePackage
    {
       
        public StationSubPacket()
            : base()
        {

        }
        public StationSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }

        public ArtemisString Name { get; set; }
        public float? Shields { get; set; }
        public float? AftShields { get; set; }
        public int? Unknown { get; set; }
        public int? Unknown2 { get; set; }
        public float? X { get; set; }

        public float? Y { get; set; }
        public float? Z { get; set; }

        public int? Unknown3 { get; set; }
        public int? Unknown4 { get; set; }
        public int? Unknown5 { get; set; }

        public int? Unknown6 { get; set; }
        public byte? Unknown7 { get; set; }
        public byte? Unknown8 { get; set; }

    }
}
