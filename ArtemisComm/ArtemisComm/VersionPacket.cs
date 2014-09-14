using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class VersionPacket : ParentPacket
    {
        //**CONFIRMED
        public VersionPacket() : base()
        {
            MajorVersion= 2;
            MinorVersion =1;
            PatchVersion = 1;

            //LegacyVersion = 2.1.1F;  //Current version of Artemis as of 12/5/2013.
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
        //1264 F0 04 00 00 seen consistently here
        public int Unknown { get; set; }
        public float LegacyVersion { get; set; }
        public int MajorVersion { get; set; }
        public int MinorVersion { get; set; }
        public int PatchVersion { get; set; }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
       
    }
}
