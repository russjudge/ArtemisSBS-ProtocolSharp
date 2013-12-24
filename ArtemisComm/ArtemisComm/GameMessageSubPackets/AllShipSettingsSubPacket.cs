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
    public class AllShipSettingsSubPacket : IPackage
    {
        //**CONFIRMED
#if LOG4NET
        static readonly ILog _log = LogManager.GetLogger(typeof(AllShipSettingsSubPacket));
#endif
        public AllShipSettingsSubPacket()
        {
#if LOG4NET

            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif
       }
        
        public AllShipSettingsSubPacket(byte[] byteArray)
        {
#if LOG4NET
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
#endif            

            int startIndex = 0;
            List<PlayerShip> ships = new List<PlayerShip>();
#if LOG4NET

            if (_log.IsInfoEnabled)
            {
                _log.InfoFormat("Adding PlayerShip objects, current startIndex={0}", startIndex.ToString());
            }
#endif
            do
            {
                PlayerShip p = new PlayerShip(byteArray, startIndex);
                ships.Add(p);
                startIndex += p.Length;
#if LOG4NET

                if (_log.IsInfoEnabled)
                {
                    _log.InfoFormat("PlayerShip object added, current startIndex={0}", startIndex.ToString());
                }
#endif
            } while (startIndex < byteArray.Length);

            Ships = ships.ToArray();
#if LOG4NET

            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif         
        }

        public byte[] GetBytes()
        {
#if LOG4NET

            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif
            List<byte> retVal = new List<byte>();
            

            foreach (PlayerShip p in Ships)
            {
                retVal.AddRange(p.GetBytes());

            }
#if LOG4NET

            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   
#endif
            return retVal.ToArray();
        }

        
        
        public PlayerShip[] Ships { get; private set; }
        //A list of the eight available player ships. Each ship is structured as follows:

        //Drive type (int)
        //Ship type (int)
        //Unknown (int): So far, the only value that has been observed here is 1. This field is new as of Artemis 2.0.
        //Name (string): Name of the ship
        //See Enumerations for drive and ship type values.
    }
}
