using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.ShipActionSubPackets
{
    public enum ShipActionSubPacketTypes : uint
    {
        CaptainSelectSubPacket = 0x11,
        DiveRiseSubPacket = 0x1b,
        EngSetAutoDamconSubPacket = 0x0c,
        FireTubeSubPacket = 0x08,
        HelmRequestDockSubPacket = 0x07,
        HelmSetWarpSubPacket = 0x00,
        HelmToggleReverseSubPacket = 0x18,
        ReadySubPacket = 0x0f,
        Ready2SubPacket = 0x19,
        SciScanSubPacket = 0x13,
        SciSelectSubPacket = 0x10,
        SetBeamFreqSubPacket = 0x0b,
        SetMainScreenSubPacket = 0x01,
        SetShipSubPacket = 0x0d,
        SetShipSettingsSubPacket = 0x16,
        SetStationSubPacket = 0x0e,
        SetWeaponsTargetSubPacket = 0x02,
        ToggleAutoBeamsSubPacket = 0x03,
        TogglePerspectiveSubPacket = 0x1a,
        ToggleRedAlertSubPacket = 0x0a,
        ToggleShieldsSubPacket = 0x04,
        UnloadTubeSubPacket = 0x09
    }
}
