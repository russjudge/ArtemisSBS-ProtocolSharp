using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class EngSetAutoDamconSubPacket : ShipAction
    {
        
        public EngSetAutoDamconSubPacket() : base() { }

        public EngSetAutoDamconSubPacket(byte[] byteArray) : base(byteArray) { }

        public bool DamComIsAutonomous
        {
            get
            {
                return Convert.ToBoolean(Value);
            }
            set
            {
                Value = Convert.ToInt32(value);
            }
        }

    }
}
