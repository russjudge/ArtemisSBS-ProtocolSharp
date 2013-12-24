using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetShipSettingsSubPacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(SetShipSettingsSubPacket));
        public SetShipSettingsSubPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public SetShipSettingsSubPacket(byte[] byteArray)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

            DriveType = (DriveTypes)BitConverter.ToInt32(byteArray, 0);

            ShipType = BitConverter.ToInt32(byteArray, 4);
            Unknown = BitConverter.ToInt32(byteArray, 8);
            
            ShipName = new ArtemisString(byteArray, 12);
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        //ef:be:ad:de:38:00:00:00:02:00:00:00:00:00:00:00:24:00:00:00:
        //3c:1d:82:4c:
        //16:00:00:00:
        //01:00:00:00:
        //00:00:00:00:
        //01:00:00:00:
        //06:00:00:00:
        //44:00:
        //69:00:
        //61:00:
        //6e:00:
        //61:00:
        //00:00
        public DriveTypes DriveType { get; set; }
        public int ShipType { get; set; }
        public int Unknown { get; set; }
       
        public ArtemisString ShipName{get;set;}
        
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes((int)DriveType));
            retVal.AddRange(BitConverter.GetBytes(ShipType));
            retVal.AddRange(BitConverter.GetBytes(Unknown));
            retVal.AddRange(ShipName.GetBytes());
            return retVal.ToArray();
        }
    }
}