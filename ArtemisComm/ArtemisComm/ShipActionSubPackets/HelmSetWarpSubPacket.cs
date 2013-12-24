using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmSetWarpSubPacket : ShipAction
    {
        
        public HelmSetWarpSubPacket() : base() { }

        public HelmSetWarpSubPacket(byte[] byteArray) : base(byteArray) { }

        public int WarpFactor
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
