using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class GameResetSubPacket : BasePacket
    {
        //Subtype 06

        public GameResetSubPacket(Stream stream, int index) : base(stream, index) { }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
