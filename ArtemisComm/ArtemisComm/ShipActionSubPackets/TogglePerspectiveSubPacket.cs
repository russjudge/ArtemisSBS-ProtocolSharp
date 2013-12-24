using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class TogglePerspectiveSubPacket : ShipAction
    {
        
      
        public TogglePerspectiveSubPacket() : base() { }

        public TogglePerspectiveSubPacket(byte[] byteArray) : base(byteArray) { }

       
    }
}
