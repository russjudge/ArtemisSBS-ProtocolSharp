using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class MonsterUpdateSubPacket : NamedObjectUpdate
    {
        public MonsterUpdateSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
    }
}
