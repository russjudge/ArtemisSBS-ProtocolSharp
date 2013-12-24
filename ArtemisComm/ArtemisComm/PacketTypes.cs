using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public enum PacketTypes : uint
    {
        InvalidPacket = 0x00000000,
        //Server to Client
        CommsIncomingPacket = 0xd672c35f,  //- 697121953, //3597845343, //0xd672c35f,
        DestroyObjectPacket = 0xcc5a3e30, //- 866501072, //3428466224, //0xcc5a3e30,
        EngGridUpdatePacket = 0x077e9f3c, //125738812, //0x077e9f3c,
        IncomingAudioPacket = 0xae88e058, //- 1366761384, //2928205912, //0xae88e058,
        //AllShipSettingsPacket = 0xf754c8fe, // - 145438466, //f754c8fe
        GameMessagePacket = 0xf754c8fe, //- 145438466, //4149528830, //0xf754c8fe,
        ObjectStatusUpdatePacket = 0x80803df9, // - 2139079175, //2155888121, //0x80803df9,
        StationStatusPacket = 0x19c6e2d4, // 432464596, //0x19c6e2d4,
        VersionPacket = 0xe548e74a, //- 448207030, //3846760266, //0xe548e74a,
        WelcomePacket = 0x6d04b3da, //1829024730, //0x6d04b3da,
        GameStartPacket = 0x3de66711, //1038509841, //0x3de66711, 
        Unknown2Packet = 0xf5821226, //- 176025050, //4118942246, //-176025050, //0xf5821226,

        IntelPacket = 0xee665279,
        //Client to Server
        AudioCommandPacket = 0x6aadc57f, //1789773183, //0x6aadc57f,
        CommsOutgoingPacket = 0x574c4c4b, // 1464618059, //0x574c4c4b,
        ShipActionPacket = 0x4c821d3c, //1283595580, //0x4c821d3c,
        ShipAction2Packet = 0x69cc01d9, //1774977497, //0x69cc01d9,
        ShipAction3Packet = 0x0351a5ac, //55682476, //0x0351a5ac,
    }
//    Server to Client
//CommsIncomingPacket (0xd672c35f)
//DestroyObjectPacket (0xcc5a3e30)
//EngGridUpdatePacket (0x077e9f3c)
//IncomingAudioPacket (0xae88e058)
//Game message packet (0xf754c8fe)
//... AllShipSettingsPacket (subtype 0x0f)
//... GameMessagePacket (subtypes 0x06 and 0x0a)
//... GameStartPacket (subtype 0x00)
//... JumpStatusPacket (subtypes 0x0c and 0x0d)
//... Unknown (subtypes 0x08 and 0x09)
//Object status update packet (0x80803df9)
//... EnemyUpdatePacket (subtype 0x04; now apparently used for both enemies and civilians)
//... EngPlayerUpdatePacket (subtype 0x03)
//... GenericMeshPacket (subtype 0x0d?)
//... GenericUpdatePacket (subtypes 0x06, 0x07, 0x09-0x0c, 0x0e?)
//... MainPlayerUpdatePacket (subtype 0x01)
//... OtherShipUpdatePacket (subtype 0x06 under Artemis 1.7, now defunct?)
//... StationPacket (subtype 0x05?)
//... WeapPlayerUpdatePacket (subtype 0x02)
//... WhaleUpdatePacket (subtype 0x0f?)
//StationStatusPacket (0x19c6e2d4)
//VersionPacket (0xe548e74a)
//WelcomePacket (0x6d04b3da)
//Unknown (0x3de66711, 0xf5821226)
//Client to Server
//AudioCommandPacket (0x6aadc57f)
//CommsOutgoingPacket (0x574c4c4b)
//Ship action packet (0x4c821d3c)
//... CaptainSelectPacket (subtype 0x11)
//... DiveRisePacket (subtype 0x1b)
//... EngSetAutoDamconPacket (subtype 0x0c)
//... FireTubePacket (subtype 0x08)
//... HelmRequestDockPacket (subtype 0x07)
//... HelmSetWarpPacket (subtype 0x00)
//... HelmToggleReversePacket (subtype 0x18)
//... ReadyPacket (subtype 0x0f)
//... ReadyPacket2 (subtype 0x19)
//... SciScanPacket (subtype 0x13)
//... SciSelectPacket (subtype 0x10)
//... SetBeamFreqPacket (subtype 0x0b)
//... SetMainScreenPacket (subtype 0x01)
//... SetShipPacket (subtype 0x0d)
//... SetShipSettingsPacket (subtype 0x16)
//... SetStationPacket (subtype 0x0e)
//... SetWeaponsTargetPacket (subtype 0x02)
//... ToggleAutoBeamsPacket (subtype 0x03)
//... TogglePerspectivePacket (subtype 0x1a)
//... ToggleRedAlertPacket (subtype 0x0a)
//... ToggleShieldsPacket (subtype 0x04)
//... UnloadTubePacket (subtype 0x09)
//Ship action packet 2 (0x69cc01d9)
//... ConvertTorpedoPacket (subtype 0x03)
//... EngSendDamconPacket (subtype 0x04)
//... EngSetCoolantPacket (subtype 0x00)
//... LoadTubePacket (subtype 0x02)
//Ship action packet 3 (0x0351a5ac)
//... EngSetEnergyPacket (subtype 0x04)
//... HelmJumpPacket (subtype 0x05)
//... HelmSetImpulsePacket (subtype 0x00)
//... HelmSetSteeringPacket (subtype 0x01)
}
