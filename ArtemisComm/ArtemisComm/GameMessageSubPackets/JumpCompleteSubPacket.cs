using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class JumpCompleteSubPacket : BasePacket 
    {
        public JumpCompleteSubPacket(Stream stream, int index)
            : base(stream, index)
        {
          
        }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
