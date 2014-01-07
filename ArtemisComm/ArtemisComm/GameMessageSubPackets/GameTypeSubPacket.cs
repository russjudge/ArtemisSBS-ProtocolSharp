using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class GameTypeSubPacket : BasePacket
    {
        
        public GameTypeSubPacket(Stream stream, int index) : base(stream, index)
        {
          
        }
        public int GameType { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
