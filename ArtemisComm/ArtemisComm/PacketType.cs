using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public enum PacketType : int
    {
        InvalidPacket = 0x00000000,
        //Server to Client
        BeamFiredPacket = -1203776828,          //0xb83fd2c4 - c4 d2 3f b8  (+3091190468)   (-1203776828)
        CommsIncomingPacket = -697121953,       //0xd672c35f - 5f c3 72 d6  (+3597845343)   (-697121953)
        DestroyObjectPacket = -866501072,       //0xcc5a3e30 - 30 3e 5a cc  (+3428466224)   (-866501072)
        DifficultyPacket = 0X3de66711,          //0X3de66711 - 11 67 e6 3d  (+1038509841)


        EngGridUpdatePacket = 0x077e9f3c,       //0x077e9f3c - 3c 9f 7e 07  (+125738812)

        IncomingAudioPacket = -1366761384,      //0xae88e058 - 58 e0 88 ae  (+2928205912)   (-1366761384)
        IntelPacket = -295284103,               //0xee665279 - 79 52 66 ee  (+3999683193)   (-295284103)



        GameMessagePacket = -145438466,         //0xf754c8fe - fe c8 54 f7  (+4149528830)   (-145438466)
        ObjectStatusUpdatePacket = -2139079175, //0x80803df9 - f9 3d 80 80  (+2155888121)   (-2139079175)

        //ConsoleStatusPacket = 0x19c6e2d4, 
        StationStatusPacket = 0x19c6e2d4,       //0x19c6e2d4 - d4 e2 c6 19  (+432464596)

        VersionPacket = -448207030,             //0xe548e74a - 4a e7 48 e5  (+3846760266)   (-448207030)

        WelcomePacket = 0x6d04b3da,             //0x6d04b3da - da b3 04 6d  (+1829024730)
        GameStartPacket = 0x3de66711,           //0x3de66711 - 11 67 e6 3d  (+1038509841)
        Unknown2Packet = -176025050,            //0xf5821226 - 26 12 82 f5  (+4118942246)   (-176025050)

        //Client to Server
        AudioCommandPacket = 0x6aadc57f,        //0x6aadc57f - 7f c5 ad 6a  (+1789773183)
        CommsOutgoingPacket = 0x574c4c4b,       //0x574c4c4b - 4b rc 4c 57  (+1464618059)
        GameMasterMessagePacket = -2137848409,  //0x809305a7 - a7 05 93 80  (+2157118887)   (-2137848409)

        ShipActionPacket = 0x4c821d3c,          //0x4c821d3c - 3c 1d 82 4c  (+1283595580)

        ShipAction2Packet = 0x69cc01d9,         //0x69cc01d9 - d9 01 cc 69  (+1774977497)
        ShipAction3Packet = 0x0351a5ac,         //0x0351a5ac - ac a5 51 03  (+55682476)

         //0xb83fd2c4
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
