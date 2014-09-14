using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class KeystrokeSubPacket : ShipAction
    {
     
        public static Packet GetPacket(int keyCode)
        {
            return new Packet(new ShipActionPacket(new KeystrokeSubPacket(keyCode)));
        }

        public KeystrokeSubPacket(int keyCode) : base(ShipActionSubPacketType.KeystrokePacket, keyCode) { }
        public KeystrokeSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

        [ArtemisExcluded]
        public int Keycode
        {
            get
            {
                return Value;
            }
            set
            {
                Value = value;
            }
        }

    }
}
