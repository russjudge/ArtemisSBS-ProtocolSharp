using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public class SetShipSubPacket : BasePacket 
    {
        //**CONFIRMED
        public static Packet GetPackage(int shipNumber)
        {
            SetShipSubPacket ssp = new SetShipSubPacket(shipNumber);
            ShipActionPacket sap = new ShipActionPacket(ssp);
            Packet p = new Packet(sap);
            return p;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SetShipSubPacket"/> class.  ship number is 1-based.
        /// </summary>
        /// <param name="shipNumber">The ship number.</param>
        public SetShipSubPacket(int shipNumber)
        {
            ShipNumber = shipNumber;
        }

        public SetShipSubPacket(Stream stream, int index)
            : base(stream, index)
        {

        }
        int _shipNumber;
        /// <summary>
        /// Gets or sets the ship number.  For consistency, this returns 1-8 as valid values.
        /// </summary>
        /// <value>
        /// The ship number.
        /// </value>
        public int ShipNumber
        {
            get
            {
                return _shipNumber + 1;
            }
            set
            {
                if (value < 1)
                {
                    throw new InvalidOperationException("Ship number must be greater than 0.");
                }
                if (value > 8)
                {
                    throw new InvalidOperationException("Ship number must be less than 9.");
                }
                _shipNumber = value - 1;
            }
        }


        public override OriginType GetValidOrigin()
        {
            return OriginType.Client;
        }
        


    }
}