using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class FireTubeSubPacket : ShipAction
    {
        
        public FireTubeSubPacket() : base() { }

        public FireTubeSubPacket(byte[] byteArray) : base(byteArray) { }

        public int TubeIndex
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }

    }
}
