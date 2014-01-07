using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class FireTubeSubPacket : ShipAction
    {

        public static Packet GetPacket(int tubeIndex)
        {
            return new Packet(new ShipActionPacket(new FireTubeSubPacket(tubeIndex)));
        }

        public FireTubeSubPacket(int tubeIndex) : base(tubeIndex) { }

        public FireTubeSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public int TubeIndex
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
