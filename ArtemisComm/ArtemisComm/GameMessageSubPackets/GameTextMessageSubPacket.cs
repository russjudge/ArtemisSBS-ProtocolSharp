using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class GameTextMessageSubPacket : ParentPacket
    {
        public GameTextMessageSubPacket(Stream stream, int index) : base(stream, index)
        {
        }

        
        public ArtemisString MessageText { get; set; }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
