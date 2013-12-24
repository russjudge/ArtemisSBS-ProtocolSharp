using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SciSelectSubPacket : ShipAction
    {
        
        public SciSelectSubPacket() : base() { }

        public SciSelectSubPacket(byte[] byteArray) : base(byteArray) { }

        public int TargetID
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
