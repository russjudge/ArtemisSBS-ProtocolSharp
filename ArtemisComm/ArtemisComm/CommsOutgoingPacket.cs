using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class CommsOutgoingPacket : IPackage
    {
          static readonly ILog _log = LogManager.GetLogger(typeof(CommsOutgoingPacket));
        public CommsOutgoingPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            Unknown = 0x004f005e;
            MessageID = 0x00730078;

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public CommsOutgoingPacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

                RecipientType = BitConverter.ToInt32(byteArray, 0);
                RecipientID = BitConverter.ToInt32(byteArray, 4);
                MessageID = BitConverter.ToInt32(byteArray, 8);
                TargetObjectID = BitConverter.ToInt32(byteArray, 12);
                Unknown = BitConverter.ToInt32(byteArray, 16);


             
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }
        public int RecipientType { get; set; }
        public int RecipientID { get; set; }
        public int MessageID { get; set; }
        public int TargetObjectID { get; set; }
        public int Unknown { get; set; }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(RecipientType));
            retVal.AddRange(BitConverter.GetBytes(RecipientID));
            retVal.AddRange(BitConverter.GetBytes(MessageID));
            retVal.AddRange(BitConverter.GetBytes(TargetObjectID));
            retVal.AddRange(BitConverter.GetBytes(Unknown));
            return retVal.ToArray();
            
        }
    }
}
