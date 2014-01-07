
using System.IO;
namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class MineUpdateSubPacket : UnnamedObjectUpdate
    {

        public MineUpdateSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }

    }
}
