using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class NpcUpdateSubPacket : VariablePackage
    {
          public NpcUpdateSubPacket() : base()
        {

        }
          public NpcUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
        //should be 47 properties

        //1
        public ArtemisString Name { get; set; }
        public float? Unknown1 { get; set; }
        public float? Unknown2 { get; set; }
        public float? Unknown3 { get; set; }
        public float? Unknown4 { get; set; }
        public int? IsHostile { get; set; }
        public int? ShipType { get; set; }
        public float? X { get; set; }
        public float? Y { get; set; }
        //10:
        public float? Z { get; set; }
        public float? Unknown5 { get; set; }
        public float? Steering { get; set; }
        public float? Heading { get; set; }
        public float? Velocity { get; set; }
        public byte? Unknown6 { get; set; }
        public short? Unknown7 { get; set; }
        public float? ForeShields { get; set; }
        public float? ForeShieldsMax { get; set; }
        public float? AftShields { get; set; }
        public float? AftShieldsMax { get; set; }
        public short? Unknown8 { get; set; }
        //20:
        public byte? Unknown9 { get; set; }
        public int? Elite { get; set; }
        public int? EliteAbilities { get; set; }
        public int? Scanned { get; set; }
        public int? Side { get; set; }
        public int? Unknown10 { get; set; }
        public float? Unknown11 { get; set; }
        public float? Unknown12 { get; set; }
        public int? Unknown13 { get; set; }
        public int? Unknown14 { get; set; }
        //30:
        public int? Unknown15 { get; set; }
        public int? Unknown16 { get; set; }
        public int? Unknown17 { get; set; }
        public float? ShieldFrequencyA { get; set; }
        public float? ShieldFrequencyB { get; set; }
        public float? ShieldFrequencyC { get; set; }
        public float? ShieldFrequencyD { get; set; }
        //38:
        public float? ShieldFrequencyE { get; set; }

        public byte? Unknown18 { get; set; }
        //40:
        public byte? Unknown19 { get; set; }
        public byte? Unknown20 { get; set; }
        public byte? Unknown21 { get; set; }
        public byte? Unknown22 { get; set; }
        public byte? Unknown23 { get; set; }
        public byte? Unknown24 { get; set; }
        public byte? Unknown25 { get; set; }
        public int? Unknown26 { get; set; }
       
    }
}
