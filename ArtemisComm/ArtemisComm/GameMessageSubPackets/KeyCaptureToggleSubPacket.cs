using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class KeyCaptureToggleSubPacket : ParentPacket
    {
        //0x11
        public KeyCaptureToggleSubPacket(Stream stream, int index) : base(stream, index) { }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        [ArtemisType(typeof(byte))]
        public bool Capture { get; set; }
    }
}
