using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class ToggleShieldsSubPacket : ShipAction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public static Packet GetPacket()
        {
            return new Packet(new ShipActionPacket(new ToggleShieldsSubPacket()));
        }


        public ToggleShieldsSubPacket()  :base (ShipActionSubPacketType.ToggleShieldsSubPacket, 0)
        {
            
        }

       
        public ToggleShieldsSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
      

        
    }
}
