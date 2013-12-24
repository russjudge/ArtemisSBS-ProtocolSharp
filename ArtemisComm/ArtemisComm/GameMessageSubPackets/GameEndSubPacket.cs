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
    public class GameEndSubPacket : IPackage
    {
#if LOG4NET
        static readonly ILog _log = LogManager.GetLogger(typeof(GameEndSubPacket));
#endif
        public GameEndSubPacket()
        {
#if LOG4NET
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif
        }
        public GameEndSubPacket(byte[] byteArray)
        {
#if LOG4NET
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
         
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
#endif
        }


        public byte[] GetBytes()
        {
            return new byte[0];
        }

    }
}
//Message type (int)

//Always 0x00 for this packet class.

//Unknown (int)

//Unknown (int)

//Speculated to be the starting offset value for ship IDs.