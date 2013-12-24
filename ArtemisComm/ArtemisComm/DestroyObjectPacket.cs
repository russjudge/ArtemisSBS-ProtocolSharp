using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class DestroyObjectPacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(DestroyObjectPacket));
        public DestroyObjectPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public DestroyObjectPacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

                Target = (ObjectTypes)byteArray[0];
                ID = BitConverter.ToInt32(byteArray, 1);
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.Add((byte)Target);
            retVal.AddRange(BitConverter.GetBytes(ID));
            return retVal.ToArray();
        }
        public ObjectTypes Target { get; set; }
        public int ID { get; set; }
        //Target type (byte)

        //Indicates the type of object being destroyed. (see Enumerations)

        //Target ID (int)

        //ID of the object being destroyed.
    }
}
