using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class NpcUpdateSubPacket : VariablePackage
    {

        public NpcUpdateSubPacket(MemoryStream stream, int index)
            : base(stream, index)
        {

        }
        //should be 47 properties

        //1
        public ArtemisString Name { get; set; } //bit 1.1
        public float? Unknown1 { get; set; } //bit 1.2
        public float? Rudder { get; set; } //bit 1.3
        public float? MaxImpulse { get; set; } //bit 1.4
        public float? MaxTurnRate { get; set; } //bit 1.5
        public int? IsHostile { get; set; } //bit 1.6
        public int? ShipType { get; set; } //bit 1.7
        public float? X { get; set; } //bit 1.8
        public float? Y { get; set; } //bit 2.1
        //10:
        public float? Z { get; set; }//bit 2.2
        public float? Pitch { get; set; }//bit 2.3
        public float? Roll { get; set; }//bit 2.4
        public float? Heading { get; set; }//bit 2.5
        public float? Velocity { get; set; }//bit 2.6
        public byte? HasSurrendered { get; set; }//bit 2.7
        public short? Unknown7 { get; set; }//bit 2.8
        public float? ForeShields { get; set; }//bit 3.1
        public float? ForeShieldsMax { get; set; }//bit 3.2
        public float? AftShields { get; set; }//bit 3.3
        public float? AftShieldsMax { get; set; }//bit 3.4
        public short? Unknown8 { get; set; }//bit 3.5
        //20:
        public byte? Unknown9 { get; set; }//bit 3.6
        public int? Elite { get; set; }//bit 3.7
        public int? EliteAbilities { get; set; }//bit 3.8
        public int? Scanned { get; set; }//bit 4.1
        public int? Side { get; set; }//bit 4.2
        public int? Unknown10 { get; set; }//bit 4.3
        public byte? Unknown11 { get; set; } //bit 4.4
        public byte? Unknown12 { get; set; } //bit 4.5
        public byte? Unknown13 { get; set; } //bit 4.6
        public byte? Unknown14 { get; set; } //bit 4.7
        //30:
        public float? Unknown15 { get; set; }//bit 4.8
        public int? Unknown16 { get; set; }//bit 5.1
        public int? Unknown17 { get; set; }//bit 5.2

        public float? SystemDamage1 { get; set; } //bit 5.3
        public float? SystemDamage2 { get; set; } //bit 5.4
        public float? SystemDamage3 { get; set; } //bit 5.5
        public float? SystemDamage4 { get; set; } //bit 5.6
        public float? SystemDamage5 { get; set; } //bit 5.7
        public float? SystemDamage6 { get; set; } //bit 5.8
        public float? SystemDamage7 { get; set; } //bit 6.2



        public float? ShieldFrequencyA { get; set; }//bit 6.3
        public float? ShieldFrequencyB { get; set; }//bit 6.4
        public float? ShieldFrequencyC { get; set; }//bit 6.5
        public float? ShieldFrequencyD { get; set; }//bit 6.6
        //38:
        public float? ShieldFrequencyE { get; set; } //bit 6.7
       
    }
}
