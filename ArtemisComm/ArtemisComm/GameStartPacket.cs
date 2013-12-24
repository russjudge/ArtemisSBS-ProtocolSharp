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
    public class GameStartPacket : IPackage
    {
#if LOG4NET

        static readonly ILog _log = LogManager.GetLogger(typeof(GameStartPacket));   
#endif
        public GameStartPacket()
        {
#if LOG4NET

            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif
        }
        public GameStartPacket(byte[] byteArray)
        {
#if LOG4NET

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
#endif
            Unknown1 = BitConverter.ToInt32(byteArray, 0);
#if LOG4NET

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
#endif

        }
        public int Unknown1 { get; set; }
        public byte[] GetBytes()
        {
            return BitConverter.GetBytes(Unknown1);
        }
    }
}
