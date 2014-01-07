using ArtemisComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.Proxy.Logger
{
    public interface IPacketToLogging
    {
        /// <summary>
        /// Processes the specified packet.
        /// </summary>
        /// <param name="packet">The packet.</param>
        /// <param name="sourceID">The source identifier.</param>
        /// <param name="targetID">The target identifier.</param>
        /// <returns>Key to be used to match to the values</returns>
        object Process(Packet packet, Guid sourceID, Guid targetID, int subPacketType);
        void ProcessValues(object key, string propertyName, object value, string propertyType, string hexValue);
        void AddException(Guid sourceID, Exception error);
    }
}
