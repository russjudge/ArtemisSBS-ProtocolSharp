using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class DMXMessageSubPacket : ParentPacket
    {
        //**CONFIRMED



        public DMXMessageSubPacket(Stream stream, int index)
            : base(stream, index)
        {

         
        }
        



        public ArtemisString  Name { get; private set; }

        public int State { get; private set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
    }
}
