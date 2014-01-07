using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ShipAction: BasePacket
    {

       
        public ShipAction(int value)
        {
            Value = value;
        }
        public ShipAction(Stream stream, int index)
            : base(stream, index)
        {

        }
        public int Value { get; set; }



        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }

    }
}