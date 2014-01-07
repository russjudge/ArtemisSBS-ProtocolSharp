using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class TorpedoUpdateSubPacket : UnnamedObjectUpdate
    {
        public TorpedoUpdateSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
