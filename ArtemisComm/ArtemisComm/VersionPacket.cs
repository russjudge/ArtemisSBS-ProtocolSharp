using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class VersionPacket : BasePacket
    {
        //**CONFIRMED
        public VersionPacket() : base()
        {
            Version = 2.0F;  //Current version of Artemis as of 12/5/2013.
        }
        public VersionPacket(Stream stream, int index) : base(stream, index) {}
        //{
        //    try
        //    {
        //        if (byteArray != null)
        //        {

        //            if (byteArray.Length > 3)  //Protection in case of bad packet.
        //            {
        //                Unknown = BitConverter.ToInt32(byteArray, 0);
        //            }
        //            if (byteArray.Length > 7)  //Protection in case of bad packet.
        //            {
        //                Version = BitConverter.ToSingle(byteArray, 4);
        //                Packet.CurrentActiveArtemisVersion = Version;
        //            }
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
        //    retVal.AddRange(BitConverter.GetBytes(Unknown));
        //    retVal.AddRange(BitConverter.GetBytes(Version));
        //    return retVal.ToArray();
        //}
        public int Unknown { get; set; }
        public float Version { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
