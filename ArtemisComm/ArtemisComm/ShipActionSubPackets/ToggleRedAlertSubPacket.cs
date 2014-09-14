using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ToggleRedAlertSubPacket : ShipAction
    {
        public static Packet GetPacket()
        {
            return new Packet(new ShipActionPacket(new ToggleRedAlertSubPacket()));
        }


        public ToggleRedAlertSubPacket() : base(ShipActionSubPacketType.ToggleRedAlertSubPacket, 0) { }


        public ToggleRedAlertSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
       
    }
}
