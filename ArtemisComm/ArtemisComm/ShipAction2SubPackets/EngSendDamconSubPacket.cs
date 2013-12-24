using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class EngSendDamconSubPacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(EngSendDamconSubPacket));
        public EngSendDamconSubPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public EngSendDamconSubPacket(byte[] byteArray)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

            
            TeamNumber = BitConverter.ToInt32(byteArray, 0);
            X = BitConverter.ToInt32(byteArray, 4);
            Y = BitConverter.ToInt32(byteArray, 8);
            Z = BitConverter.ToInt32(byteArray, 12);


            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public int TeamNumber { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(TeamNumber));
            retVal.AddRange(BitConverter.GetBytes(X));
            retVal.AddRange(BitConverter.GetBytes(Y));
            retVal.AddRange(BitConverter.GetBytes(Z));
            return retVal.ToArray();
        }
    }
}