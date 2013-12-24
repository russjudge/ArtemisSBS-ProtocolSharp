using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ToggleAutoBeamsSubPacket : ShipAction
    {

      
        public ToggleAutoBeamsSubPacket() : base() { }

        public ToggleAutoBeamsSubPacket(byte[] byteArray) : base(byteArray) { }

    }
}
