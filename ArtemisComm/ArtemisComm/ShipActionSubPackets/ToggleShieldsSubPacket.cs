using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ToggleShieldsSubPacket : ShipAction
    {

      
        public static Packet GetPacket()
        {
            ToggleShieldsSubPacket tssp = new ToggleShieldsSubPacket();
            ShipActionPacket sap = new ShipActionPacket(tssp);
            return new Packet(sap);
        }
        
        public ToggleShieldsSubPacket() : base() { }

        public ToggleShieldsSubPacket(byte[] byteArray) : base(byteArray) { }

    }
}
