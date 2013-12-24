using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class AsteroidUpdateSubPacket :MineUpdateSubPacket
    {
        public AsteroidUpdateSubPacket()
            : base()
        {

        }
        public AsteroidUpdateSubPacket(byte[] byteArray)
            : base(byteArray)
        {

        }
    }
}
