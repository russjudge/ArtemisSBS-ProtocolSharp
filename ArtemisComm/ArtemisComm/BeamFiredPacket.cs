using System;
using System.Collections.Generic;
using System.IO;

namespace ArtemisComm
{
    public class BeamFiredPacket : BasePacket
    {
         public BeamFiredPacket() : base()
        {

        }
         public BeamFiredPacket(MemoryStream stream, int index) : base(stream, index) { }
        


         public int Unknown1 { get; set; }
         public int Unknown2 { get; set; }
         public int Unknown3 { get; set; }
         public int Unknown4 { get; set; }
         public int Unknown5 { get; set; }
         public int Unknown6 { get; set; }
         public int OriginObjectID { get; set; }
         public int TargetObjectID { get; set; }
         public int Unknown9 { get; set; }
         public int Unknown10 { get; set; }
         public int Unknown11 { get; set; }
         public int Unknown12 { get; set; }
        //Sample data:
         //44:3E:00:00:00:00:00:00:64:00:00:00:00:00:00:00:04:00:00:00:05:00:00:00:EA:39:00:00:C6:38:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00

        //try #1:
         //44:3E:00:00:
        //00:00:00:00:
        //64:00:00:00:
        //00:00:00:00:
        //04:00:00:00:
        //05:00:00:00:
        //EA:39:00:00:
        //C6:38:00:00:
        //00:00:00:00:
        //00:00:00:00:
        //00:00:00:00:
        //00:00:00:00

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
