using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class DestroyObjectPacket : BasePacket
    {

        public DestroyObjectPacket(Stream stream, int index) : base(stream, index) { }
        //public DestroyObjectPacket(byte[] byteArray)
        //{
        //    try
        //    {
        //        if (byteArray != null)
        //        {

        //            if (byteArray.Length > 0) Target = (ObjectType)byteArray[0];
        //            if (byteArray.Length > 4) ID = BitConverter.ToInt32(byteArray, 1);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex);
        //    }
        //}
        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    retVal.Add((byte)Target);
        //    retVal.AddRange(BitConverter.GetBytes(ID));
        //    return retVal.ToArray();
        //}
        public ObjectType Target { get; set; }
        public int ID { get; set; }
        //Target type (byte)

        //Indicates the type of object being destroyed. (see Enumerations)

        //Target ID (int)

        //ID of the object being destroyed.

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
