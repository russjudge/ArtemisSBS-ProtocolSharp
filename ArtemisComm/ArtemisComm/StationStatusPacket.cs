using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class StationStatusPacket : ParentPacket
    {


        public StationStatusPacket(Stream stream, int index) : base(stream, index) { }


        /// <summary>
        /// Gets or sets the ship number.  One based.
        /// </summary>
        /// <value>
        /// The ship number.
        /// </value>
        public int ShipNumber { get; set; }

        public BridgeStationStatus MainScreen { get; set; }
        public BridgeStationStatus Helm { get; set; }
        public BridgeStationStatus Weapons { get; set; }
        public BridgeStationStatus Engineering { get; set; }
        public BridgeStationStatus Science { get; set; }
        public BridgeStationStatus Communications { get; set; }
        public BridgeStationStatus Data { get; set; }
        public BridgeStationStatus Observer { get; set; }
        public BridgeStationStatus CaptainMap { get; set; }
        public BridgeStationStatus GameMaster { get; set; }

        /*

        [ArtemisType(typeof(byte))]
        public bool MainScreenTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool HelmTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool WeaponsTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool EngineeringTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool ScienceTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool CommunicationsTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool DataTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool ObserverTaken { get; set; }

        [ArtemisType(typeof(byte))]
        public bool CaptainsMapTaken { get; set; }


        [ArtemisType(typeof(byte))]
        public bool GameMasterTaken { get; set; }
        */
        public override OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }

    }
}
