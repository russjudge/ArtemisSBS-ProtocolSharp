using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class AudioCommandPacket : ParentPacket
    {
        public static Packet GetPacket(int id, int playOrDismiss)
        {
            return new Packet(new AudioCommandPacket(id, playOrDismiss));
        }
        public AudioCommandPacket(int id, int playOrDismiss) : base()
        {
            ID = id;
            Command = (AudioCommands)playOrDismiss;
        }
        public AudioCommandPacket(int id, AudioCommands playOrDismiss)
            : base()
        {
            ID = id;
            Command = playOrDismiss;
        }
        public AudioCommandPacket(Stream stream, int index) : base(stream, index) { }
        
        //public AudioCommandPacket(byte[] byteArray)
        //{
        //    if (byteArray != null)
        //    {
        //        try
        //        {
        //            ID = BitConverter.ToInt32(byteArray, 0);
        //            PlayOrDismiss = BitConverter.ToInt32(byteArray, 4);
        //        }
        //        catch (Exception ex)
        //        {
        //            errors.Add(ex);
        //        }
        //    }
        //}

        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the play or dismiss.
        /// </summary>
        /// <value>
        ///Play (0) or dismiss (1).
        /// </value>
        public AudioCommands Command { get; set; }

        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    retVal.AddRange(BitConverter.GetBytes(ID));
        //    retVal.AddRange(BitConverter.GetBytes(PlayOrDismiss));
        //    return retVal.ToArray();
        //}

        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
       
    }
}
