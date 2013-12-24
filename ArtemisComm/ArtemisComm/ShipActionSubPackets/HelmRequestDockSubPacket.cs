using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class HelmRequestDockSubPacket : ShipAction
    {

     
        public HelmRequestDockSubPacket() : base() { }

        public HelmRequestDockSubPacket(byte[] byteArray) : base(byteArray) { }


    }
}
