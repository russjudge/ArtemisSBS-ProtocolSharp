using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class CommsOutgoingPacket : BasePacket
    {
        public static Packet GetPacket(int recipientType, int recipientID, int messageID, int targetObjectID, int unknown)
        {
            return new Packet(new CommsOutgoingPacket(recipientType, recipientID, messageID, targetObjectID, unknown));
        }
        public CommsOutgoingPacket(int recipientType, int recipientID, int messageID, int targetObjectID, int unknown) : base()
        {
            RecipientType = recipientType;
            RecipientID = recipientID;
            MessageID = messageID;
            TargetObjectID = targetObjectID;
            Unknown = unknown;
        }
        public CommsOutgoingPacket() : base()
        {
            Unknown = 0x004f005e;
            MessageID = 0x00730078;
        }
        public CommsOutgoingPacket(Stream stream, int index) : base(stream, index) { }
        //public CommsOutgoingPacket(byte[] byteArray)
        //{
        //    try
        //    {
        //        if (byteArray != null)
        //        {

        //            RecipientType = BitConverter.ToInt32(byteArray, 0);
        //            RecipientID = BitConverter.ToInt32(byteArray, 4);
        //            MessageID = BitConverter.ToInt32(byteArray, 8);
        //            TargetObjectID = BitConverter.ToInt32(byteArray, 12);
        //            Unknown = BitConverter.ToInt32(byteArray, 16);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex);
        //    }
        //}
        public int RecipientType { get; set; }
        public int RecipientID { get; set; }
        public int MessageID { get; set; }
        public int TargetObjectID { get; set; }
        public int Unknown { get; set; }

        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    retVal.AddRange(BitConverter.GetBytes(RecipientType));
        //    retVal.AddRange(BitConverter.GetBytes(RecipientID));
        //    retVal.AddRange(BitConverter.GetBytes(MessageID));
        //    retVal.AddRange(BitConverter.GetBytes(TargetObjectID));
        //    retVal.AddRange(BitConverter.GetBytes(Unknown));
        //    return retVal.ToArray();
            
        //}

        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
       
    }
}
