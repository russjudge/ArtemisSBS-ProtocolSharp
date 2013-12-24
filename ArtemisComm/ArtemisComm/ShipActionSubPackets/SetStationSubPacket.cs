using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetStationSubPacket : IPackage
    {
        //**CONFIRMED
        public static Packet GetPacket(StationTypes station, bool isSelected)
        {

            SetStationSubPacket sstp = new SetStationSubPacket(station, isSelected);
            ShipActionPacket sap = new ShipActionPacket(sstp);

            return new Packet(sap);

        }
        static readonly ILog _log = LogManager.GetLogger(typeof(SetStationSubPacket));
        //public SetStationSubPacket()
        //{
        //    if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
        //    _isSelected = 0;
        //    if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        //}
        public SetStationSubPacket(StationTypes station, bool isSelected)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            IsSelected = isSelected;
            Station = station;
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public SetStationSubPacket(byte[] byteArray)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

                if (byteArray.Length > 3)
                {
                    Station = (StationTypes)BitConverter.ToInt32(byteArray, 0);
                }
                if (byteArray.Length > 7)
                {
                    _isSelected = BitConverter.ToInt32(byteArray, 4);
                }
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        public StationTypes Station { get; set; }

        int _isSelected = 0;
        public bool IsSelected
        {
            get
            {
                return Convert.ToBoolean(_isSelected);
            }
            set
            {
                _isSelected = Convert.ToInt32(value);
            }
        }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes((int)Station));
            retVal.AddRange(BitConverter.GetBytes(_isSelected));
            return retVal.ToArray();
        }
    }
}