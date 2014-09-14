using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class IncomingAudioPacket : ParentPacket
    {
        public IncomingAudioPacket()
        {
        }
        public IncomingAudioPacket(Stream stream, int index) : base(stream, index) { }

        public int MessageID { get; set; }
        public AudioMode AudioMode { get; set; }
        public ArtemisString Title { get; set; }
        public ArtemisString File { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
