using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class SkyboxSubPacket : ParentPacket
    {
        
        public SkyboxSubPacket(Stream stream, int index) : base(stream, index)
        {
          
        }
        public int SkyboxID { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
