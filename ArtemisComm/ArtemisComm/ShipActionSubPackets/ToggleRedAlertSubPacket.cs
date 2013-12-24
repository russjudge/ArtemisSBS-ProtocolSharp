using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ToggleRedAlertSubPacket : ShipAction
    {
        public static Packet GetPacket()
        {
            ToggleRedAlertSubPacket trasp = new ToggleRedAlertSubPacket();
            ShipActionPacket sap = new ShipActionPacket(trasp);
            return new Packet(sap);
        }
     
        public ToggleRedAlertSubPacket() : base() { }

        public ToggleRedAlertSubPacket(byte[] byteArray) : base(byteArray) { }


    }
}
