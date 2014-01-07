using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class BlackHoleUpdateSubPacket : NamedObjectUpdate
    {
        public BlackHoleUpdateSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
    }
}
