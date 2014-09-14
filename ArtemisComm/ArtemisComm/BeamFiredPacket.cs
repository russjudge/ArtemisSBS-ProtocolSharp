using System;
using System.Collections.Generic;
using System.IO;

namespace ArtemisComm
{
    public class BeamFiredPacket : ParentPacket
    {
         public BeamFiredPacket() : base()
        {

        }
         public BeamFiredPacket(MemoryStream stream, int index) : base(stream, index) { }
        


        //Possible boolean: 0 from enemy when fired, 1 from Artemis fired.
         public int Unknown1 { get; set; }

        //Observed 1200 from Artemis, 100 from enemy.
         public int Unknown2 { get; set; }

         public int BeamPort { get; set; }

        /*
         public int Unknown4 { get; set; }
         public int Unknown5 { get; set; }
         public int Unknown6 { get; set; }
         * */
         public int OriginObjectID { get; set; }
         public int TargetObjectID { get; set; }


         public float ImpactX { get; set; }
         public float ImpactY { get; set; }
         public float ImpactZ { get; set; }
         public int Mode { get; set; } //0=auto, 1=manual
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
