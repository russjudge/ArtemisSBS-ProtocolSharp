using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class DiveRiseSubPacket : ShipAction
    {
        
        public DiveRiseSubPacket() : base() { }

        public DiveRiseSubPacket(byte[] byteArray) : base(byteArray) { }

        public int Delta
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
