#if LOG4NET
using log4net;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class IntelPacket : IPackage
    {
#if LOG4NET
        static readonly ILog _log = LogManager.GetLogger(typeof(IntelPacket));   
#endif
        public IntelPacket()
        {
#if LOG4NET

            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif
        }
        public IntelPacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
#if LOG4NET

                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
#endif
                ID = BitConverter.ToInt32(byteArray, 0);
                Unknown2 = byteArray[4];

                Message = new ArtemisString(byteArray, 5);
#if LOG4NET

                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
#endif
            }
        }
        public int ID { get; set; }
        public byte Unknown2 { get; set; }

        public ArtemisString Message { get; set; }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(ID));
            retVal.Add(Unknown2);

            retVal.AddRange(Message.GetBytes());
            return retVal.ToArray();
        }
    }
}
