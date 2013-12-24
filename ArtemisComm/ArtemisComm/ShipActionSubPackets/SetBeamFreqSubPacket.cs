using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetBeamFreqSubPacket : ShipAction
    {
        
        public SetBeamFreqSubPacket() : base() { }

        public SetBeamFreqSubPacket(byte[] byteArray) : base(byteArray) { }

        public int FrequencyIndex
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
