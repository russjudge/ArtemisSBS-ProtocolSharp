using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class GameStartPacket : BasePacket
    {

        public GameStartPacket(Stream stream, int index) : base(stream, index) { }
        public int Unknown1 { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
