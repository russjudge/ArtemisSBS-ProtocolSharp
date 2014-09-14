using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class PlayerShip : ParentPacket
    {
        //A list of the eight available player ships. Each ship is structured as follows:

        //Drive type (int)
        //Ship type (int)
        //Unknown (int): So far, the only value that has been observed here is 1. This field is new as of Artemis 2.0.
        //Name (string): Name of the ship
        //See Enumerations for drive and ship type values.
        public PlayerShip() : base()
        {
            
        }
        public PlayerShip(Stream stream, int index)
            : base(stream, index)
        {
           
        }

        public DriveType DriveType { get; set; }  
        public int ShipType { get; set; }  //Always 1?

        //new in 2.0? 
        public int Unknown2 { get; set; }  //Always 0?
        /// <summary>
        /// Gets or sets the name, unicode, null-terminated.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public ArtemisString Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        ArtemisString _name = null;


        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
      
    }
}
