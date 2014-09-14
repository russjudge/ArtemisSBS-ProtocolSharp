using System;
using System.IO;

namespace ArtemisComm.ShipActionSubPackets
{
    public class EngSetAutoDamconSubPacket : ShipAction
    {

        public static Packet GetPacket(bool damComIsAutonomous)
        {
            return new Packet(new ShipActionPacket(new EngSetAutoDamconSubPacket(damComIsAutonomous)));
        }

        public EngSetAutoDamconSubPacket(bool damComIsAutonomous) : base(ShipActionSubPacketType.EngSetAutoDamconSubPacket, Convert.ToInt32(damComIsAutonomous)) { }

        public EngSetAutoDamconSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        [ArtemisExcluded]
        public bool DamComIsAutonomous
        {
            get
            {
                return Convert.ToBoolean(Value);
            }
            set
            {
                Value = Convert.ToInt32(value);
            }
        }

    }
}
