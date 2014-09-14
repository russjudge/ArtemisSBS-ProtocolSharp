using ArtemisComm;
using ArtemisComm.GameMessageSubPackets;
using ArtemisComm.ObjectStatusUpdateSubPackets;
using ArtemisComm.ShipAction2SubPackets;
using ArtemisComm.ShipAction3SubPackets;
using ArtemisComm.ShipActionSubPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sandbox
{
    /// <summary>
    /// Interaction logic for PackageTester.xaml
    /// </summary>
    public partial class PackageTester : Window, IDisposable 
    {
        public PackageTester()
        {
            InitializeComponent();
        }

        //IView View = null;
        bool isDisposed = false;
        Guid serverID;
        bool shieldsRaised = false;
        bool redAlertEnabled = false;
        bool ShieldsMustBeDown = false;
        int SelectedShip = 0;
        bool shipSelected = false;
        bool GameInProgress = false;
        System.Timers.Timer Ready2Timer = null;
        PacketProcessing connector = null;
        private void OnConnect(object sender, RoutedEventArgs e)
        {
            string host = Host.Text;
            connector = new PacketProcessing();


            ConnectorSubscribe();
            connector.SetPort(2010);
            connector.SetServerHost(host);
            connector.StartServerConnection();

        }
        void connector_GameStartPacketReceived(object sender, PackageEventArgs e)
        {
            GameInProgress = true;
            //View.GameStarted();
            StartReady2Timer();

        }

        void connector_GamesMessagePacketReceived(object sender, PackageEventArgs e)
        {
            //GameStart and GameOver are all that matter.
            if (e != null && e.ReceivedPacket != null)
            {
                GameMessagePacket p = e.ReceivedPacket.Package as GameMessagePacket;
                if (p != null)
                {
                    if (p.SubPacketType == GameMessageSubPacketType.GameOverSubPacket)
                    {

                        GameInProgress = !GameInProgress;
                        if (GameInProgress)
                        {
                            //View.GameStarted();
                            StartReady2Timer();
                        }
                        else
                        {
                            //View.GameEnded();
                            StopReady2Timer();

                        }

                    }
                    if (p.SubPacketType == GameMessageSubPacketType.SimulationEndSubPacket)
                    {

                        GameInProgress = false;
                        //View.SimulationEnded();
                    }
                    if (p.SubPacketType == GameMessageSubPacketType.AllShipSettingsSubPacket)
                    {
                        AllShipSettingsSubPacket allships = p.SubPacket as AllShipSettingsSubPacket;
                        if (allships != null && allships.Ships != null && !shipSelected)
                        {
                            //Currently only selects first Ship (Artemis)
                            SelectStationAndReady();
                            //View.GetShipSelection(allships.Ships.ToArray<PlayerShip>());

                        }
                    }
                }
            }

        }
        void DisposeReady2Timer()
        {
            if (Ready2Timer != null)
            {
                Ready2Timer.Dispose();
            }
        }
        void StopReady2Timer()
        {
            Ready2Timer.Stop();
            DisposeReady2Timer();
        }
        void StartReady2Timer()
        {
            Ready2Timer = new System.Timers.Timer(15000);
            Ready2Timer.Elapsed += Ready2Timer_Elapsed;
            Ready2Timer.Start();
        }

        void Ready2Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {

            Ready2Timer.Stop();
            if (GameInProgress)
            {
                SendReady2();
                Ready2Timer.Start();
            }
        }
        void SendReady2()
        {
            if (GameInProgress)
            {
                if (connector != null)
                {
                    connector.SendReady2SubPacket(serverID);

                }

            }
        }
        void connector_Connected(object sender, ConnectionEventArgs e)
        {
            serverID = e.ID;

        }

        void connector_ObjectStatusUpdatePacketReceived(object sender, PackageEventArgs e)
        {
            if (GameInProgress)
            {
                if (e != null)
                {

                    if (e.ReceivedPacket != null)
                    {
                        ObjectStatusUpdatePacket objectStat = e.ReceivedPacket.Package as ObjectStatusUpdatePacket;
                        if (objectStat != null)
                        {
                            if (objectStat.SubPacketType == ObjectStatusUpdateSubPacketType.MainPlayerUpdateSubPacket)
                            {
                                MainPlayerUpdateSubPacket mainPlayer = objectStat.SubPacket as MainPlayerUpdateSubPacket;
                                if (mainPlayer != null)
                                {
                                    // || mainPlayer.ShipNumber == null
                                    if (mainPlayer.RedAlert != null && ((mainPlayer.ShipNumber != null && mainPlayer.ShipNumber == (SelectedShip + 1))))
                                    {

                                        redAlertEnabled = Convert.ToBoolean(mainPlayer.RedAlert.Value);
                                        //View.RedAlertEnabled = redAlertEnabled;
                                    }
                                    // || mainPlayer.ShipNumber == null
                                    if (mainPlayer.ShieldState != null && ((mainPlayer.ShipNumber != null && mainPlayer.ShipNumber == (SelectedShip + 1))))
                                    {
                                        shieldsRaised = Convert.ToBoolean(mainPlayer.ShieldState.Value);
                                        //View.ShieldsRaised = shieldsRaised;
                                        if (shieldsRaised && ShieldsMustBeDown)
                                        {
                                            SendDropShields();
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }
        void CancelSelfDestruct()
        {
            if (GameInProgress)
            {

                ShieldsMustBeDown = false;
                ZeroWarpEnergy();
                SendRedAlert();
            }
        }

        void StartSelfDestruct()
        {
            if (GameInProgress)
            {

                if (!redAlertEnabled)
                {
                    SendRedAlert();
                }

                SendDropShields();
                SetEngineeringSettings();

            }
        }

        void SendDropShields()
        {
            if (GameInProgress)
            {
                ShieldsMustBeDown = true;
                if (shieldsRaised)
                {

                    connector.SendToggleShields(serverID);
                }
            }
        }
        void SendRedAlert()
        {
            if (GameInProgress)
            {

                connector.SendToggleRedAlert(serverID);
            }
        }
        void SetEngineeringSettings()
        {
            if (GameInProgress)
            {
                ZeroAllCoolant();
                ZeroAllButWarpEnergy();
                MaxWarpEnergy();
            }
        }
        void ZeroAllButWarpEnergy()
        {
            if (GameInProgress)
            {
                foreach (ShipSystem st in Enum.GetValues(typeof(ShipSystem)))
                {
                    if (st != ShipSystem.WarpJumpDrive)
                    {
                        //connector.SendPackage(EngSetEnergySubPacket.GetPacket(st, 0));
                        connector.SendEngSetEnergySubPacket(serverID, st, 0);
                    }
                }
            }
        }
        void MaxWarpEnergy()
        {
            if (GameInProgress)
            {
                //connector.SendPackage(EngSetEnergySubPacket.GetPacket(ShipSystems.WarpJumpDrive, 1));
                connector.SendEngSetEnergySubPacket(serverID, ShipSystem.WarpJumpDrive, 1);
            }
        }
        void ZeroWarpEnergy()
        {
            if (GameInProgress)
            {
                //connector.SendPackage(EngSetEnergySubPacket.GetPacket(ShipSystems.WarpJumpDrive, 0));
                connector.SendEngSetEnergySubPacket(serverID, ShipSystem.WarpJumpDrive, 0);
            }
        }
        void ZeroAllCoolant()
        {
            if (GameInProgress)
            {
                foreach (ShipSystem st in Enum.GetValues(typeof(ShipSystem)))
                {

                    connector.SendEngSetCoolantSubPacket(serverID, st, 0);
                }
            }

        }
        void connector_StationStatusPacketReceived(object sender, PackageEventArgs e)
        {

        }
        void connector_UnableToConnect(object sender, EventArgs e)
        {
            //View.UnableToConnect();
        }
        void ConnectorUnsubscribe()
        {
            connector.ConnectionLost -= connector_ConnectionLost;
            connector.ObjectStatusUpdatePacketReceived -= connector_ObjectStatusUpdatePacketReceived;
            connector.GameMessagePacketReceived -= connector_GamesMessagePacketReceived;
            connector.Connected -= connector_Connected;
            connector.GameStartPacketReceived -= connector_GameStartPacketReceived;
            connector.UnableToConnect -= connector_UnableToConnect;
        }
        void ConnectorSubscribe()
        {
            connector.ConnectionLost += connector_ConnectionLost;
            connector.ObjectStatusUpdatePacketReceived += connector_ObjectStatusUpdatePacketReceived;
            connector.GameMessagePacketReceived += connector_GamesMessagePacketReceived;
            connector.GameStartPacketReceived += connector_GameStartPacketReceived;
            connector.StationStatusPacketReceived += connector_StationStatusPacketReceived;
            connector.Connected += connector_Connected;

            connector.UnableToConnect += connector_UnableToConnect;
        }
        void connector_ConnectionLost(object sender, ConnectionEventArgs e)
        {
            if (Ready2Timer != null)
            {
                Ready2Timer.Stop();
            }
            //View.ConnectionFailed();
            GameInProgress = false;
            shipSelected = false;

        }
        void SelectStationAndReady()
        {
            if (connector != null)
            {
                //Select Ship

                if (SelectedShip != 0)
                {

                    connector.SendSetShipSubPacket(serverID, SelectedShip + 1);

                }
                //Select Station
                connector.SendSetStationSubPacket(serverID, StationType.Observer, true);

                //Ready

                connector.SendReadySubPacket(serverID);

            }


        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    connector.Dispose();
                    DisposeReady2Timer();
                    isDisposed = true;
                }
            }
        }
        static string FormatIt(byte[] buffer)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
            {
                sb.Append(b.ToString("x").PadLeft(2,'0'));
                sb.Append(":");
            }
            return sb.ToString();
        }
        private void OnToggleShields(object sender, RoutedEventArgs e)
        {
            if (GenerateRaw.IsChecked == true)
            {
                RawData.Text = FormatIt(ToggleShieldsSubPacket.GetPacket().GetRawData().ToArray());
            }
            else
            {
                if (GameInProgress)
                {


                    connector.SendToggleShields(serverID);

                }
                else
                {
                    MessageBox.Show("Game is not in progress");
                }
            }
        }

        private void OnToggleRedAlert(object sender, RoutedEventArgs e)
        {
             if (GenerateRaw.IsChecked == true)
            {
                RawData.Text = FormatIt(ToggleRedAlertSubPacket.GetPacket().GetRawData().ToArray());
            }
            else
            {
                if (GameInProgress)
                {
                    connector.SendToggleRedAlert(serverID);
                }
                else
                {
                    MessageBox.Show("Game is not in progress");
                }
            }
        }

        private void EngSetCoolant(object sender, RoutedEventArgs e)
        {
            int parm1 = 1;
            int parm2 = 0;
            int.TryParse(Parm1.Text, out parm1);
            int.TryParse(Parm2.Text, out parm2);

            if (GenerateRaw.IsChecked == true)
            {
                RawData.Text = FormatIt(EngSetCoolantSubPacket.GetPacket((ShipSystem)parm1, parm2).GetRawData().ToArray());
            }
            else
            {
                if (GameInProgress)
                {
                   

                    connector.SendEngSetCoolantSubPacket(serverID, (ShipSystem)parm1, parm2 );
                }
                else
                {
                    MessageBox.Show("Game is not in progress");
                }
            }
        }
        private void EngSetEnergy(object sender, RoutedEventArgs e)
        {
            int parm1 = 1;
            int parm2 = 0;
            int.TryParse(Parm1.Text, out parm1);
            int.TryParse(Parm2.Text, out parm2);

            if (GenerateRaw.IsChecked == true)
            {
                RawData.Text = FormatIt(EngSetEnergySubPacket.GetPacket((ShipSystem)parm1, parm2).GetRawData().ToArray());
            }
            else
            {
                if (GameInProgress)
                {


                    connector.SendEngSetEnergySubPacket(serverID, (ShipSystem)parm1, parm2);
                }
                else
                {
                    MessageBox.Show("Game is not in progress");
                }
            }
        }

        private void SciSelect(object sender, RoutedEventArgs e)
        {
            
            int parm1 = 1;
           
            int.TryParse(Parm1.Text, out parm1);
           
            if (GenerateRaw.IsChecked == true)
            {
                RawData.Text = FormatIt(SciSelectSubPacket.GetPacket(parm1).GetRawData().ToArray());
            }
            else
            {
                if (GameInProgress)
                {


                    connector.SendSciSelectSubPacket(serverID, parm1);
                }
                else
                {
                    MessageBox.Show("Game is not in progress");
                }
            }

        }
    }
}
