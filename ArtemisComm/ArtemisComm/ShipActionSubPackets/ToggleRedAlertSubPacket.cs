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
        public static Packet GetPacket(int value)
        {
            return new Packet(new ShipActionPacket(new ToggleRedAlertSubPacket(value)));
        }


        public ToggleRedAlertSubPacket(int value) : base(value) { }


        public ToggleRedAlertSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }


    }
}
