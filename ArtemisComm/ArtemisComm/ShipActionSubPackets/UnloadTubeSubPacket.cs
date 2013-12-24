using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class UnloadTubeSubPacket : ShipAction
    {
        
        
        public UnloadTubeSubPacket() : base() { }

        public UnloadTubeSubPacket(byte[] byteArray) : base(byteArray) { }

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
