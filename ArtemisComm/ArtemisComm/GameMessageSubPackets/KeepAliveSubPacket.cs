using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class KeepAliveSubPacket : IPackage
    {
        public KeepAliveSubPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }
        }

        public KeepAliveSubPacket(byte[] byteArray)
        {
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
            Unknown1 = BitConverter.ToSingle(byteArray, 0);
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
        }
        static readonly ILog _log = LogManager.GetLogger(typeof(KeepAliveSubPacket));
        public float Unknown1 { get; set; }
        public byte[] GetBytes()
        {
            return BitConverter.GetBytes(Unknown1);
        }
    }
}
