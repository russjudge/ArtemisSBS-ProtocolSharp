using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class LoadTubeSubPacket : ShipAction2 
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket(int tubeIndex, OrdinanceType ordinance)
        {
            LoadTubeSubPacket ltb = new LoadTubeSubPacket(tubeIndex, ordinance);
            ShipAction2Packet sap2 = new ShipAction2Packet(ltb);
            return new Packet(sap2);
        }

        public LoadTubeSubPacket(int tubeIndex, OrdinanceType ordinance)
            : base(ShipAction2SubPacketType.LoadTubeSubPacket, tubeIndex, Convert.ToInt32(ordinance), 0, 0)
        { }
      
        public LoadTubeSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public int TubeIndex
        {
            get
            {
                return Value1;
            }
            set
            {
                Value1 = value;
            }
        }

        [ArtemisExcluded]
        public OrdinanceType Ordinance
        {
            get
            {
                return (OrdinanceType)Convert.ToByte(Value2);
            }
            set
            {
                Value2 = Convert.ToInt32(value);
            }
        }



    }
}