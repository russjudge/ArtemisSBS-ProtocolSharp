using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class TorpedoUpdateSubPacket : MineUpdateSubPacket
    {
        public TorpedoUpdateSubPacket()
            : base()
        {

        }
        public TorpedoUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
    }
}
