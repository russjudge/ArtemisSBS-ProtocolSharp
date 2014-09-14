using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class StationSubPacket : VariablePackage
    {

        public StationSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

        public ArtemisString Name { get; set; }
        public float? Shields { get; set; }
        public float? AftShields { get; set; }
        /// <summary>
        /// Gets or sets the index of the station.
        /// </summary>
        /// <value>
        /// The index of the station. zero-based
        /// </value>
        public int? StationIndex { get; set; }
        public int? VesselTypeID { get; set; }  //or float?
        public float? X { get; set; }

        public float? Y { get; set; }
        public float? Z { get; set; }

        // Following 4 fields may likely be the torpedo stock.
        public int? Unknown3 { get; set; }
        public int? Unknown4 { get; set; }
        public int? Unknown5 { get; set; }
        public int? Unknown6 { get; set; }

        public byte? Unknown7 { get; set; }
        public byte? Unknown8 { get; set; }

    }
}
