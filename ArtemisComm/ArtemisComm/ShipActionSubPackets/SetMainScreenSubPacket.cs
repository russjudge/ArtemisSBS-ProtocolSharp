using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetMainScreenSubPacket : ShipAction
    {
        
        public SetMainScreenSubPacket() : base() { }

        public SetMainScreenSubPacket(byte[] byteArray) : base(byteArray) { }

        public int View
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
