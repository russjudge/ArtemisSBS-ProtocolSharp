using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class GenericMeshSubPacket : VariablePackage
    {
        public GenericMeshSubPacket()
            : base()
        {

        }
        public GenericMeshSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }

        public float? X { get; set; }
        public float? Y { get; set; }
        public float? Z { get; set; }
        public int? Unknown1 { get; set; }
        public int? Unknown2 { get; set; }
        public long? Unknown3 { get; set; }
        public int? Unknown4 { get; set; }
        public int? Unknown5 { get; set; }
        public int? Unknown6 { get; set; }
        public int? Unknown7 { get; set; }

        public ArtemisString Name { get; set; }

        //public ArtemisString MeshName { get; set; }

        public ArtemisString TextureName { get; set; }

        public byte? Undefined1 { get; set; }

        public int? Unknown8 { get; set; }
        public short? Unknown9 { get; set; }
        public byte? Unknown10 { get; set; }

        public float? Red { get; set; }
        public float? Green { get; set; }
        public float? Blue { get; set; }

        public float? ForeShields { get; set; }

        public float? AftShields { get; set; }

        public byte? Unknown11 { get; set; }
        public int? Unknown12 { get; set; }

        public int? Unknown13 { get; set; }

        public int? Unknown14 { get; set; }

        public int? Unknown15 { get; set; }

        public int? Undefined2 { get; set; }
        public int? Undefined3 { get; set; }
        public int? Undefined4 { get; set; }
        public int? Undefined5 { get; set; }
        public int? Undefined6 { get; set; }
        public int? Undefined7 { get; set; }
        //        X position (bit 1.1, float)

        //Y position (bit 1.2, float)

        //Z position (bit 1.3, float)

        //Unknown (bit 1.4, int)

        //Unknown (bit 1.5, int)

        //Unknown (bit 1.6, long)

        //Unknown (bit 1.7, int)

        //Unknown (bit 1.8, int)

        //Unknown (bit 2.1, int)

        //Unknown (bit 2.2, long)

        //Name (bit 2.3, string)

        //Mesh name (bit 2.4, string)

        //Texture name (bit 2.4, string)

        //Note that this uses the same bit as mesh name.

        //Unknown (bit 2.6, int)

        //Unknown (bit 2.7, short)

        //Unknown (bit 2.8, byte)

        //Red color channel (bit 3.1, float)

        //Green color channel (bit 3.2, float)

        //Blue color channel (bit 3.3, float)

        //Fore shields (bit 3.4, float)

        //Aft shields (bit 3.5, float)

        //Unknown (bit 3.6, byte)

        //Unknown (bit 3.7, int)

        //Unknown (bit 3.8, int)

        //Unknown (bit 4.1, int)

        //Unknown (bit 4.2, int)

    }
}
