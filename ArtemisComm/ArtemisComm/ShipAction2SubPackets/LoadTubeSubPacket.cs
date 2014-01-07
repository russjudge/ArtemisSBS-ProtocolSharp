using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class LoadTubeSubPacket : BasePacket 
    {
        public static Packet GetPacket(int tubeIndex, int ordinance)
        {
            LoadTubeSubPacket ltb = new LoadTubeSubPacket(tubeIndex, ordinance);
            ShipAction2Packet sap2 = new ShipAction2Packet(ltb);
            return new Packet(sap2);
        }
        
        public LoadTubeSubPacket(int tubeIndex, int ordinance)
        {
            TubeIndex = tubeIndex;
            Ordinance = ordinance;
        }
        public LoadTubeSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        public int TubeIndex { get; set; }

        public int Ordinance { get; set; }




        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
        
    }
}