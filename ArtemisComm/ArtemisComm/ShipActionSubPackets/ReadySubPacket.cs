using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ReadySubPacket : ShipAction
    {
        public static Packet GetPacket()
        {
            ReadySubPacket rsp = new ReadySubPacket();
            ShipActionPacket sap = new ShipActionPacket(rsp);
            return new Packet(sap);
        }
        public ReadySubPacket() : base() { }

        public ReadySubPacket(byte[] byteArray) : base(byteArray) { }

    }
}
