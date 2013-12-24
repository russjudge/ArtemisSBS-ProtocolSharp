using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetWeaponsTargetSubPacket : ShipAction
    {
        
        public SetWeaponsTargetSubPacket() : base() { }

        public SetWeaponsTargetSubPacket(byte[] byteArray) : base(byteArray) { }

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
