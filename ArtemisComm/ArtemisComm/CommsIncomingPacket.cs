using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class CommsIncomingPacket : IPackage
    {

        //CONFIRMED
        static readonly ILog _log = LogManager.GetLogger(typeof(CommsIncomingPacket));
        public CommsIncomingPacket()
        {
            if (_log.IsDebugEnabled) { _log.DebugFormat("Starting {0}", MethodBase.GetCurrentMethod().ToString()); }
            Sender = new ArtemisString();
            Message = new ArtemisString();
            if (_log.IsDebugEnabled) { _log.DebugFormat("Ending {0}", MethodBase.GetCurrentMethod().ToString()); }   

        }
        public CommsIncomingPacket(byte[] byteArray)
        {
            if (byteArray != null)
            {
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--bytes in: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(byteArray)); }

                Priority = BitConverter.ToInt32(byteArray, 0);

                Sender = new ArtemisString(byteArray, 4);
                int newStart = 4 + (Sender.Length * 2) + 4;
                Message = new ArtemisString(byteArray, newStart);
                if (_log.IsInfoEnabled) { _log.InfoFormat("{0}--Result bytes: {1}", MethodBase.GetCurrentMethod().ToString(), Utility.BytesToDebugString(this.GetBytes())); }
            }
        }
        public byte[] GetBytes()
        {
            List<byte> retVal = new List<byte>();
            retVal.AddRange(BitConverter.GetBytes(Priority));
            retVal.AddRange(Sender.GetBytes());
            retVal.AddRange(Message.GetBytes());
            return retVal.ToArray();
        }
        /// <summary>
        /// Gets or sets the priority.  Marks background color of incoming Comms packet.
        /// </summary>
        /// <value>
        /// The priority.
        /// </value>
        public int Priority { get; set; }
        /// <summary>
        /// Gets or sets the name of the sender.
        /// </summary>
        /// <value>
        /// The sender.
        /// </value>
        public ArtemisString Sender { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public ArtemisString Message { get; set; }

//        Priority (int)

//Values appear to range from 0x00 (high priority) to 0x08 (low priority). In the stock client, this affects the message's background color.

//Sender (string)

//The name of the entity (ship or station) that send the message.

//Message (string)
    }
}
