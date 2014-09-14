using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class ShipAction3: ParentPacket
    {


        public ShipAction3(ShipAction3SubPacketType actionType, float value1, float value2)
        {
            ActionType = actionType;
            Value1 = value1;
            Value2 = value2;
        }
        public ShipAction3(Stream stream, int index)
            : base(stream, index)
        {

        }

        [ArtemisExcluded]
        public ShipAction3SubPacketType ActionType { get; set; }


        public float Value1 { get; set; }
        public float Value2 { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
    }
}
