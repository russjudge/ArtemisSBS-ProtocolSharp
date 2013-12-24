using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetShipSubPacket : IPackage
    {
        //**CONFIRMED
        static readonly ILog _log = LogManager.GetLogger(typeof(SetShipSubPacket));
        public static Packet GetPackage(int shipNumber)
        {
            SetShipSubPacket ssp = new SetShipSubPacket(shipNumber);
            ShipActionPacket sap = new ShipActionPacket(ssp);
            Packet p = new Packet(sap);
            return p;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SetShipSubPacket"/> class.  ship number is 1-based.
        /// </summary>
        /// <param name="shipNumber">The ship number.</param>
        public SetShipSubPacket(int shipNumber)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            ShipNumber = shipNumber;
            
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }

        }
        //public SetShipSubPacket()
        //{
        //    if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
        //    if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        //}
        public SetShipSubPacket(byte[] byteArray)
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }

            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

                _shipNumber = BitConverter.ToInt32(byteArray, 0);

                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
        }
        int _shipNumber;
        /// <summary>
        /// Gets or sets the ship number.  For consistency, this returns 1-8 as valid values.
        /// </summary>
        /// <value>
        /// The ship number.
        /// </value>
        public int ShipNumber
        {
            get
            {
                return _shipNumber + 1;
            }
            set
            {
                //if (value < 1 || value > 8)
                //{
                //    throw new InvalidOperationException("Valid values for Ship number are 1 through 8");
                //}
                _shipNumber = value - 1;
            }
        }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(_shipNumber));
            return retVal.ToArray();
        }
    }
}