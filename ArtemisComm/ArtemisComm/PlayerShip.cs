using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class PlayerShip : IPackage
    {

        //**CONFIRMED (only for initial receipt on AllShipSettingsSubPacket)
        static readonly ILog _log = LogManager.GetLogger(typeof(PlayerShip));   
        //A list of the eight available player ships. Each ship is structured as follows:

        //Drive type (int)
        //Ship type (int)
        //Unknown (int): So far, the only value that has been observed here is 1. This field is new as of Artemis 2.0.
        //Name (string): Name of the ship
        //See Enumerations for drive and ship type values.
        public PlayerShip()
        {
            
        }
        public PlayerShip(byte[] byteArray, int startIndex)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }


                Unknown0 = BitConverter.ToInt32(byteArray, startIndex);
                if (_log.IsInfoEnabled) { _log.InfoFormat("Unknown0={0}", Unknown0.ToString()); }
                Unknown1 = BitConverter.ToInt32(byteArray, startIndex + 4);
                if (_log.IsInfoEnabled) { _log.InfoFormat("Unknown1={0}", Unknown1.ToString()); }

                Unknown2 = BitConverter.ToInt32(byteArray, startIndex + 8);
                if (_log.IsInfoEnabled) { _log.InfoFormat("Unknown2={0}", Unknown2.ToString()); }
                Name = new ArtemisString(byteArray, startIndex + 12);
                
                
               
                if (_log.IsInfoEnabled) { _log.InfoFormat("Name={0}", Name.ToString()); }






                Length = 16 + Name.Length * 2;
                if (_log.IsInfoEnabled) { _log.InfoFormat("Length={0}", Length.ToString()); }
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }
        public int Length { get; private set; }
        public int Unknown0 { get; set; }  //From Unique ID in vesselData.xml.
        public int Unknown1 { get; set; }  //Always 1?
        public int Unknown2 { get; set; }  //Always 0?
        /// <summary>
        /// Gets or sets the name, unicode, null-terminated.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public ArtemisString Name {get;set;}
        
      
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(Unknown0));
            retVal.AddRange(BitConverter.GetBytes(Unknown1));
            retVal.AddRange(BitConverter.GetBytes(Unknown2));
            retVal.AddRange(Name.GetBytes());

            
            retVal.Add(0);
            return retVal.ToArray();
        }
    }
}
