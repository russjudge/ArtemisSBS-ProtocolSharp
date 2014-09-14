using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class ShipAction2: ParentPacket
    {


        public ShipAction2(ShipAction2SubPacketType actionType, int value1, int value2, int value3, int value4)
        {
            ActionType = actionType;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
        }
        public ShipAction2(Stream stream, int index)
            : base(stream, index)
        {

        }

        [ArtemisExcluded]
        public ShipAction2SubPacketType ActionType { get; set; }


        public int Value1 { get; set; }
        public int Value2 { get; set; }
        public int Value3 { get; set; }
        public int Value4 { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
    }
}
