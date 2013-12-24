using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class BlackHoleUpdateSubPacket : AnomalyUpdateSubPacket
    {
        public BlackHoleUpdateSubPacket()
            : base()
        {

        }
        public BlackHoleUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
    }
}
