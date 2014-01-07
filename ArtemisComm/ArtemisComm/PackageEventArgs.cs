using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class PackageEventArgs : ConnectionEventArgs
    {
        public PackageEventArgs(Packet packet, Guid id)
            : base(id)
        {
            ReceivedPacket = packet;

        }
        public Packet ReceivedPacket { get; private set; }

    }
}
