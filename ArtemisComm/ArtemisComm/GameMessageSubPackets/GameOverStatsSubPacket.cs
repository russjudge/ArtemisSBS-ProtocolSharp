using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class GameOverStatsSubPacket : ParentPacket
    {
        //Subtype 

        public GameOverStatsSubPacket(Stream stream, int index) : base(stream, index) { }

        public byte ColumnIndex { get; set; }



        //Stats array.  0x00 delimited, terminated with 0xce.
        //public int Value{get;set;}
        //public ArtemisString Label { get; set; }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
