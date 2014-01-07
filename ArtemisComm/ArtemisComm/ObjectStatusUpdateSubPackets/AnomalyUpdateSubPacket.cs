
using System.IO;
namespace ArtemisComm.ObjectStatusUpdateSubPackets
{
    public class AnomalyUpdateSubPacket : NamedObjectUpdate
    {

        
        public AnomalyUpdateSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }


    }
}
