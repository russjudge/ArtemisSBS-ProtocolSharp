using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ShipAction: ParentPacket
    {


        public ShipAction(ShipActionSubPacketType actionType, int value)
        {
            ActionType = actionType;
            Value = value;
        }
        public ShipAction(Stream stream, int index)
            : base(stream, index)
        {

        }
        //Legacy code fills out the bytes--TODO is to change it to use the ActionType instead.  Need to examine code that generates.
        [ArtemisExcluded]
        public ShipActionSubPacketType ActionType { get; set; }


        public int Value { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }

    }
}