using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class CommsIncomingPacket : BasePacket
    {

       
        public CommsIncomingPacket() : base()
        {
            Sender = new ArtemisString();
            Message = new ArtemisString();

        }
        public CommsIncomingPacket(Stream stream, int index) : base(stream, index) { }
        //public CommsIncomingPacket(byte[] byteArray)
        //{
        //    try
        //    {
        //        if (byteArray != null)
        //        {

        //            if (byteArray.Length > 3) Priority = BitConverter.ToInt32(byteArray, 0);

        //            if (byteArray.Length > 7) Sender = new ArtemisString(byteArray, 4);

        //            int newStart = 4 + (Sender.Length * 2) + 4;
        //            if (byteArray.Length > 11 + newStart) Message = new ArtemisString(byteArray, newStart);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add(ex);
        //    }
        //}
        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    retVal.AddRange(BitConverter.GetBytes(Priority));
        //    if (Sender != null)
        //    {
        //        retVal.AddRange(Sender.GetBytes());
        //    }
        //    else
        //    {
        //        retVal.AddRange(new byte[] { 0, 0, 0, 0 });
        //    }
        //    if (Message != null)
        //    {
        //        retVal.AddRange(Message.GetBytes());
        //    }
        //    else
        //    {
        //        retVal.AddRange(new byte[] { 0, 0, 0, 0 });
        //    }
        //    return retVal.ToArray();
        //}
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

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
