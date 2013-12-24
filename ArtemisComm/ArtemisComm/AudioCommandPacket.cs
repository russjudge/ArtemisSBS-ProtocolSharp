using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class AudioCommandPacket : IPackage
    {
         static readonly ILog _log = LogManager.GetLogger(typeof(AudioCommandPacket));
        public AudioCommandPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
           
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public AudioCommandPacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

                ID = BitConverter.ToInt32(byteArray, 0);
                PlayOrDismiss = BitConverter.ToInt32(byteArray, 4);

             
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }

        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the play or dismiss.
        /// </summary>
        /// <value>
        ///Play (0) or dismiss (1).
        /// </value>
        public int PlayOrDismiss { get; set; }

        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(ID));
            retVal.AddRange(BitConverter.GetBytes(PlayOrDismiss));
            return retVal.ToArray();
        }
    }
}
