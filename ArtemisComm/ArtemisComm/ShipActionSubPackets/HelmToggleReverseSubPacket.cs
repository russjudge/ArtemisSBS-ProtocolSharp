using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmToggleReverseSubPacket : ShipAction
    {
        
        public HelmToggleReverseSubPacket() : base() { }

        public HelmToggleReverseSubPacket(byte[] byteArray) : base(byteArray) { }

      
    }
}
