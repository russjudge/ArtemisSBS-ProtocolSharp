using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class SoundEffectSubPacket : ParentPacket
    {
        //0x03
        public SoundEffectSubPacket(Stream stream, int index) : base(stream, index) { }
        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        public ArtemisString Filename { get; set; }
    }
}
