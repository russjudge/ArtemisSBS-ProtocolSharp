using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class WeapPlayerUpdateSubPacket : VariablePackage
    {

        public WeapPlayerUpdateSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

        public byte? HomingMissileCount { get; set; }
        public byte? NukeMissileCount { get; set; }
        public byte? MineCount { get; set; }
        public byte? EMPCount { get; set; }

        public byte? Unknown { get; set; }
        public float? Tube1LoadTime { get; set; }
        public float? Tube2LoadTime { get; set; }
        public float? Tube3LoadTime { get; set; }
        public float? Tube4LoadTime { get; set; }
        public float? Tube5LoadTime { get; set; }
        public float? Tube6LoadTime { get; set; }

        public byte? Tube1Status { get; set; }
        public byte? Tube2Status { get; set; }
        public byte? Tube3Status { get; set; }
        public byte? Tube4Status { get; set; }
        public byte? Tube5Status { get; set; }
        public byte? Tube6Status { get; set; }

        public OrdinanceType? Tube1Content { get; set; }
        public OrdinanceType? Tube2Content { get; set; }
        public OrdinanceType? Tube3Content { get; set; }
        public OrdinanceType? Tube4Content { get; set; }
        public OrdinanceType? Tube5Content { get; set; }
        public OrdinanceType? Tube6Content { get; set; }
    }
}
