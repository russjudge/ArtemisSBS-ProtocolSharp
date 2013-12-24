using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class Ready2SubPacket : ShipAction
    {
        public static Packet GetPacket()
        {
            Ready2SubPacket rsp2 = new Ready2SubPacket();
            ShipActionPacket sap = new ShipActionPacket(rsp2);
            return new Packet(sap);
        }
       
        
        public Ready2SubPacket() : base() { }

        public Ready2SubPacket(byte[] byteArray) : base(byteArray) { }

    }
}
