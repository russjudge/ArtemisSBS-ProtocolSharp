using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class IncomingAudioPacket : IPackage
    {
        static readonly ILog _log = LogManager.GetLogger(typeof(IncomingAudioPacket));
        public IncomingAudioPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public IncomingAudioPacket(byte[] byteArray)
        {
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }
            MessageID = BitConverter.ToInt32(byteArray, 0);
            AudioMode = (AudioModes)BitConverter.ToInt32(byteArray, 4);
            Title = new ArtemisString(byteArray, 8);
            File = new ArtemisString(byteArray, 8 + Title.Length);
            if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
        }
        public int MessageID { get; set; }
        public AudioModes AudioMode { get; set; }
        public ArtemisString Title { get; set; }
        public ArtemisString File { get; set; }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(MessageID));
            retVal.AddRange(BitConverter.GetBytes((int)AudioMode));
            retVal.AddRange(Title.GetBytes());
            retVal.AddRange(File.GetBytes());
            return retVal.ToArray();
        }
    }
}
