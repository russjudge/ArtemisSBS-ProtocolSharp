using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipAction3SubPackets
{
    public class EngSetEnergySubPacket : IPackage
    {
        public static Packet GetPacket(ShipSystem system, float value)
        {
            EngSetEnergySubPacket esesp = new EngSetEnergySubPacket(system, value);
            ShipAction3Packet sap3 = new ShipAction3Packet(esesp);
            return new Packet(sap3);
        }
        static readonly ILog _log = LogManager.GetLogger(typeof(EngSetEnergySubPacket));
        public EngSetEnergySubPacket(ShipSystem system, float value)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            System = system;
            Value = value;
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public EngSetEnergySubPacket(byte[] byteArray)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }


            Value = BitConverter.ToSingle(byteArray, 0);
            System = (ShipSystem)BitConverter.ToInt32(byteArray, 4);
            


            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public float Value { get; set; }

        public ShipSystem System
        {
            get;
            set;
        }
        

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(Value));
            retVal.AddRange(BitConverter.GetBytes((int)System));
            return retVal.ToArray();
        }
    }
}