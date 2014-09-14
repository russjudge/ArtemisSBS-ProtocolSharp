using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{

    public class SimulationEndSubPacket : ParentPacket
    {
        //Wiki (https://github.com/rjwut/ArtClientLib/wiki/Artemis-Packet-Protocol%3A-GameStartPacket) incorrectly documents this as GameStartPacket.
        // I have never observed this at GameStart.
        //Observed at simulation end (Artemis Destroyed).
        //Subtype 00
        public SimulationEndSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public int Unknown1 { get; set; }  //Observed value of 0x06

        //Observed value of 0x03f6.  (1014).
        public int Unknown2 { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
