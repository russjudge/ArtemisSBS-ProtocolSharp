using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class UnloadTubeSubPacket : ShipAction
    {
        public static Packet GetPacket(int tubeIndex)
        {
            return new Packet(new ShipActionPacket(new TogglePerspectiveSubPacket(tubeIndex)));
        }


        public UnloadTubeSubPacket(int tubeIndex) : base(tubeIndex) { }

        public UnloadTubeSubPacket(Stream stream, int index)
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
