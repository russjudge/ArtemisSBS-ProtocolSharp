using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class WelcomePacket : IPackage
    {
        //**CONFIRMED
        static readonly ILog _log = LogManager.GetLogger(typeof(WelcomePacket));
        public WelcomePacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public WelcomePacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }


                Unknown = BitConverter.ToInt32(byteArray, 0);
                if (_log.IsInfoEnabled) { _log.InfoFormat("Unknown={0}", Unknown.ToString()); }
                Message = System.Text.ASCIIEncoding.ASCII.GetString(byteArray, 4, byteArray.Length - 4);
                if (_log.IsInfoEnabled) { _log.InfoFormat("Message={0}", Message); }
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }

        public int Unknown { get; set; }
        
        public string Message { get; set; }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(Unknown));
            retVal.AddRange(System.Text.ASCIIEncoding.ASCII.GetBytes(Message));
            return retVal.ToArray();
        }
    }
}
