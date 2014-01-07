using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class WelcomePacket : BasePacket
    {
        //**CONFIRMED
      
        public WelcomePacket(MemoryStream stream, int index) : base(stream, index)
        {
            //try
            //{
            //    if (byteArray != null)
            //    {
            //        if (byteArray.Length > 3) Unknown = BitConverter.ToInt32(byteArray, 0);
            //        if (byteArray.Length > 7) Message = System.Text.ASCIIEncoding.ASCII.GetString(byteArray, 4, byteArray.Length - 4);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    errors.Add(ex);
            //}
        }

        public int Unknown { get; set; }
        
        public string Message { get; set; }

        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    retVal.AddRange(BitConverter.GetBytes(Unknown));
        //    if (Message != null)
        //    {
        //        retVal.AddRange(System.Text.ASCIIEncoding.ASCII.GetBytes(Message));
        //    }
            
        //    return retVal.ToArray();
        //}

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
