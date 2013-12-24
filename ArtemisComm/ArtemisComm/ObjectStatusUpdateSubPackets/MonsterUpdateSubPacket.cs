using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class MonsterUpdateSubPacket : AnomalyUpdateSubPacket
    {
        public MonsterUpdateSubPacket()
            : base()
        {

        }
        public MonsterUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
    }
}
