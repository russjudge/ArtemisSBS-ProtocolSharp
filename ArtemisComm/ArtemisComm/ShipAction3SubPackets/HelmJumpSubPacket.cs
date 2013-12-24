using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class HelmJumpSubPacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(HelmJumpSubPacket));
        public HelmJumpSubPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public HelmJumpSubPacket(byte[] byteArray)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }


            Bearing = BitConverter.ToSingle(byteArray, 0);
            Distance = BitConverter.ToSingle(byteArray, 4);
            


            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public float Bearing { get; set; }
        
        public float Distance
        {
            get;
            set;
        }
        

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(Bearing));
            retVal.AddRange(BitConverter.GetBytes(Distance));
            return retVal.ToArray();
        }
    }
}