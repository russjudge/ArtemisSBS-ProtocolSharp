using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class PackageEventArgs : ConnectionEventArgs
    {
        public PackageEventArgs(Packet p, Guid id)
            : base(id)
        {
            ReceivedPacket = p;

        }
        public Packet ReceivedPacket { get; private set; }

    }
}
