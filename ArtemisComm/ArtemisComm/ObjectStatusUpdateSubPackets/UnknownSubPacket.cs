using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class UnknownSubPacket : IPackage
    {
        public UnknownSubPacket()
        {

        }
        public UnknownSubPacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
                Unknown1 = byteArray[0];
                Unknown2 = byteArray[1];
                Unknown3 = byteArray[2];
            }
        }
        public byte Unknown1 { get; set; }
        public byte Unknown2 { get; set; }
        public byte Unknown3 { get; set; }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.Add(Unknown1);
            retVal.Add(Unknown2);
            retVal.Add(Unknown3);
            return retVal.ToArray();
        }
    }
}
