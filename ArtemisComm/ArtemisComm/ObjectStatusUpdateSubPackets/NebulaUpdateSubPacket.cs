using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class NebulaUpdateSubPacket : MineUpdateSubPacket
    {
        public NebulaUpdateSubPacket()
            : base()
        {

        }
        public NebulaUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
    }
}
