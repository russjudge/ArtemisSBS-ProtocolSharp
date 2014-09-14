using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class DifficultyPacket : ParentPacket
    {

        public DifficultyPacket(Stream stream, int index) : base(stream, index) { }
    
        //1 through 11.
        public int Difficulty{ get; set; }


        public GameTypes GameType { get; set; }
        //Target type (byte)

        //Indicates the type of object being destroyed. (see Enumerations)

        //Target ID (int)

        //ID of the object being destroyed.

        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        
    }
}
