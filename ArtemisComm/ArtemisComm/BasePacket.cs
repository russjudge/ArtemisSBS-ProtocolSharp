using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class BasePacket : ParentPacket
    {
        
      
        public BasePacket(Stream stream, int index) : base(stream, index) { }

        
    }
}
