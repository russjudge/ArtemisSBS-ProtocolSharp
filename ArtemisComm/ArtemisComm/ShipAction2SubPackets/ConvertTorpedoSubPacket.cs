using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction2SubPackets
{
    public class ConvertTorpedoSubPacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(ConvertTorpedoSubPacket));
        public ConvertTorpedoSubPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public ConvertTorpedoSubPacket(byte[] byteArray)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

            Direction = BitConverter.ToSingle(byteArray, 0);
            Unknown1 = BitConverter.ToInt32(byteArray, 4);
            Unknown2 = BitConverter.ToInt32(byteArray, 8);
            Unknown3 = BitConverter.ToInt32(byteArray, 12);

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public float Direction { get; set; }
        public int Unknown1 { get; set; }
        public int Unknown2 { get; set; }
        public int Unknown3 { get; set; }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(Direction));
            retVal.AddRange(BitConverter.GetBytes(Unknown1));
            retVal.AddRange(BitConverter.GetBytes(Unknown2));
            retVal.AddRange(BitConverter.GetBytes(Unknown3));
            return retVal.ToArray();
        }
    }
}