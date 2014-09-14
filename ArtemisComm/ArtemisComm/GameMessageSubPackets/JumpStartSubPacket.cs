using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class JumpStartSubPacket: ParentPacket
    {
        public JumpStartSubPacket(Stream stream, int index)
            : base(stream, index)
        {
          
        }

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

        
    }
}
