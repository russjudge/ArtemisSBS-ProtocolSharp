using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class DroneUpdateSubPacket : VariablePackage
    {
        public DroneUpdateSubPacket() : base()
        {

        }
        public DroneUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
       



        public int? Unknown1 { get; set; }
        public float? X { get; set; }
        public int? Unknown2 { get; set; }
        public float? Z { get; set; }
        public int? Unknown3 { get; set; }
        public float? Y { get; set; }
        public float? Heading { get; set; }
        public int? Unknown4 { get; set; }

        public int? Unknown5 { get; set; }
        public int? Unknown6 { get; set; }
        public int? Unknown7 { get; set; }
        public int? Unknown8 { get; set; }
        public int? Unknown9 { get; set; }
        public int? Unknown10 { get; set; }
        public int? Unknown11 { get; set; }
        public int? Unknown12 { get; set; }
        public int? Unknown13 { get; set; }
        public int? Unknown14 { get; set; }
        public int? Unknown15 { get; set; }
        public int? Unknown16 { get; set; }


        
    }
}
