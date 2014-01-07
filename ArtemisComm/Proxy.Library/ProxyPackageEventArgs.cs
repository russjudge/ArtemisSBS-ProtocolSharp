using ArtemisComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.Proxy.Library
{
    public class ProxyPackageEventArgs : PackageEventArgs
    {
        public ProxyPackageEventArgs(Guid sourceID, Guid targetID, Packet package)
            : base(package, sourceID)
        {
            TargetID = targetID;
        }
        public Guid TargetID { get; private set; }
    }
}
