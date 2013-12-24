#if LOG4NET
using log4net;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class EndSimulationSubPacket : IPackage
    {
        public EndSimulationSubPacket()
        {
#if LOG4NET
if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }
#endif
        }

        public EndSimulationSubPacket(byte[] byteArray)
        {
#if LOG4NET

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
#endif
            Unknown1 = BitConverter.ToInt32(byteArray, 0);
            Unknown2 = BitConverter.ToInt32(byteArray, 4);
#if LOG4NET

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
#endif
        }
#if LOG4NET

        static readonly ILog _log = LogManager.GetLogger(typeof(EndSimulationSubPacket));
#endif
        public int Unknown1 { get; set; }

        public int Unknown2 { get; set; }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();

            retVal.AddRange(BitConverter.GetBytes(Unknown1));
            retVal.AddRange(BitConverter.GetBytes(Unknown2));
            return retVal.ToArray();
        }
    }
}
