using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class GameTextMessageSubPacket : IPackage
    {
        public GameTextMessageSubPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }
        }

        public GameTextMessageSubPacket(byte[] byteArray)
        {
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
            MessageText = new ArtemisString(byteArray);
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
        }
        static readonly ILog _log = LogManager.GetLogger(typeof(GameTextMessageSubPacket));
        public ArtemisString MessageText { get; set; }


        public byte[] GetBytes()
        {
            return MessageText.GetBytes();
        }
    }
}
