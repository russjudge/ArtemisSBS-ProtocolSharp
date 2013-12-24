﻿using ArtemisComm;
using ArtemisComm.GameMessageSubPackets;
using ArtemisComm.ObjectStatusUpdateSubPackets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigRedButtonOfDeath.Library
{
    public class Controller : IDisposable
    {
        public const decimal MinimumSupportedArtemisVersion = 2.0M;
        public const decimal ExpectedArtemisVersion = 2.0M;

        public Controller(IView view)
        {
            View = view;
            Subscribe();

        }
        void Subscribe()
        {
            View.ConnectRequested += View_ConnectRequested;
            View.StartSelfDestruct += View_StartSelfDestruct;
            View.CancelSelfDestruct += View_CancelSelfDestruct;
            View.DisconnectRequested += View_DisconnectRequested;
            View.ShipSelected += View_ShipSelected;
            View.DisposeRequested += View_Disposing;
        }

        void View_ShipSelected(object sender, EventArgs e)
        {
            SelectedShip = View.SelectedShip;
            shipSelected = true;
            SelectStationAndReady();
        }
        void UnSubscribe()
        {
            View.ConnectRequested -= View_ConnectRequested;
            View.StartSelfDestruct -= View_StartSelfDestruct;
            View.CancelSelfDestruct -= View_CancelSelfDestruct;
            View.DisconnectRequested -= View_DisconnectRequested;
            View.ShipSelected -= View_ShipSelected;
            View.DisposeRequested -= View_Disposing;
        }
        void View_Disposing(object sender, EventArgs e)
        {
            Dispose();
        }

        void View_DisconnectRequested(object sender, EventArgs e)
        {
            Disconnect();
        }
        void Disconnect()
        {
            if (connector != null)
            {
                ConnectorUnsubscribe();
                connector.Dispose();
                connector = null;
            }
        }
        
        void View_CancelSelfDestruct(object sender, EventArgs e)
        {

            CancelSelfDestruct();
        }

        void View_StartSelfDestruct(object sender, EventArgs e)
        {

            StartSelfDestruct();
        }

        void View_ConnectRequested(object sender, EventArgs e)
        {
            Connect();

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

        bool shieldsRaised = false;
        bool redAlertEnabled = false;
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
        bool ShieldsMustBeDown = false;
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
                        connector.SendEngSetEngerySubPacket(serverID, st, 0);
                    }
                }
            }
        }
        void MaxWarpEnergy()
        {
            if (GameInProgress)
            {
                //connector.SendPackage(EngSetEnergySubPacket.GetPacket(ShipSystems.WarpJumpDrive, 1));
                connector.SendEngSetEngerySubPacket(serverID, ShipSystem.WarpJumpDrive, 1);
            }
        }
        void ZeroWarpEnergy()
        {
            if (GameInProgress)
            {
                //connector.SendPackage(EngSetEnergySubPacket.GetPacket(ShipSystems.WarpJumpDrive, 0));
                connector.SendEngSetEngerySubPacket(serverID, ShipSystem.WarpJumpDrive, 0);
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
       
        void ConnectorSubscribe()
        {
            connector.ConnectionLost += connector_ConnectionLost;
            connector.ObjectStatusUpdatePacketReceived += connector_ObjectStatusUpdatePacketReceived;
            connector.GameMessagePacketReceived += connector_GamesMessagePacketReceived;
            connector.GameStartPacketReceived += connector_GameStartPacketReceived;

            connector.Connected += connector_Connected;

            connector.UnableToConnect += connector_UnableToConnect;
        }

        void connector_UnableToConnect(object sender, EventArgs e)
        {
            View.UnableToConnect();
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
        void connector_GameStartPacketReceived(object sender, PackageEventArgs e)
        {
            GameInProgress = true;
            View.GameStarted();
            StartReady2Timer();
        }
        int SelectedShip = 0;
        bool shipSelected = false;
        void connector_GamesMessagePacketReceived(object sender, PackageEventArgs e)
        {
            //GameStart and GameOver are all that matter.
            if (e != null && e.ReceivedPacket != null)
            {
                GameMessagePacket p = e.ReceivedPacket.Package as GameMessagePacket;
                if (p != null)
                {
                    if (p.SubPacketType == GameMessageSubPacketTypes.GameEndSubPacket)
                    {
                        
                        GameInProgress = false;
                        View.GameEnded();
                    }
                    if (p.SubPacketType == GameMessageSubPacketTypes.EndSimulationSubPacket)
                    {
                        
                        GameInProgress = false;
                        View.SimulationEnded();
                    }
                    if (p.SubPacketType == GameMessageSubPacketTypes.AllShipSettingsSubPacket)
                    {
                        AllShipSettingsSubPacket allships = p.SubPacket as AllShipSettingsSubPacket;
                        if (allships != null && allships.Ships != null && !shipSelected)
                        {
                            View.GetShipSelection(allships.Ships);
                            
                        }
                    }
                }
            }

        }
        bool GameInProgress = false;
        System.Timers.Timer Ready2Timer = null;
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
        Guid serverID;
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
                            if (objectStat.SubPacketType == ObjectStatusUpdateSubPacketTypes.MainPlayerUpdateSubPacket)
                            {
                                MainPlayerUpdateSubPacket mainPlayer = objectStat.SubPacket as MainPlayerUpdateSubPacket;
                                if (mainPlayer != null)
                                {
                                    // || mainPlayer.ShipNumber == null
                                    if (mainPlayer.RedAlert != null && ((mainPlayer.ShipNumber != null && mainPlayer.ShipNumber == (SelectedShip + 1))))
                                    {
                                        
                                        redAlertEnabled = Convert.ToBoolean(mainPlayer.RedAlert.Value);
                                        View.RedAlertEnabled = redAlertEnabled;
                                    }
                                    // || mainPlayer.ShipNumber == null
                                    if (mainPlayer.ShieldState != null && ((mainPlayer.ShipNumber != null && mainPlayer.ShipNumber == (SelectedShip + 1))))
                                    {
                                        shieldsRaised = Convert.ToBoolean(mainPlayer.ShieldState.Value);
                                        View.ShieldsRaised = shieldsRaised;
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

        void Connect()
        {


            if (!string.IsNullOrEmpty(View.Host) && View.Port > 0)
            {
                Disconnect();
                connector = new PacketProcessing();


                ConnectorSubscribe();
                connector.SetPort(View.Port);
                connector.SetServerHost(View.Host);
                connector.StartServerConnection();

            }
            else
            {
                View.UnableToConnect();
            }
          
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
                connector.SendSetStationSubPacket(serverID, StationTypes.Observer, true);

                //Ready

                connector.SendReadySubPacket(serverID);

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
       
        void connector_ConnectionLost(object sender, ConnectionEventArgs e)
        {
            if (Ready2Timer != null)
            {
                Ready2Timer.Stop();
            }
            View.ConnectionFailed();
            GameInProgress = false;
            shipSelected = false;

        }


        PacketProcessing connector = null;
        IView View = null;
        bool isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!isDisposed)
                {
                    if (connector != null)
                    {
                        ConnectorUnsubscribe();
                        connector.Dispose();
                    }
                    UnSubscribe();
                    isDisposed = true;
                }
            }
        }


    }
}