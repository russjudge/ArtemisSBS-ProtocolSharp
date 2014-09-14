using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class GameOverReasonSubPacket : ParentPacket
    {
        //Subtype 

        public GameOverReasonSubPacket(Stream stream, int index) : base(stream, index) { }

        public ArtemisString Text { get; set; }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
