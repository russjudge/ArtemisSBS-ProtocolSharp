using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class GameMasterMessagePacket : ParentPacket
    {
        public static Packet GetPacket(byte console, string sender, string message)
        {
            return new Packet(new GameMasterMessagePacket(console, sender, message));
        }
        public GameMasterMessagePacket(byte console, string sender, string message)
            : base()
        {
            Console = console;
            Sender = new ArtemisString(sender);
            Message = new ArtemisString(message);
        }
       

        public GameMasterMessagePacket(Stream stream, int index) : base(stream, index) { }
      


        public byte Console { get; set; }
        public ArtemisString Sender { get; set; }
        public ArtemisString Message { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
       
    }
}
