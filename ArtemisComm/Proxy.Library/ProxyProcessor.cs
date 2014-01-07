using ArtemisComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm.Proxy.Library
{
    public class ProxyProcessor : IDisposable
    {

        PacketProcessing serverProcessor = null;
        PacketProcessing clientProcessor = null;
        Dictionary<Guid, Guid> ClientsToServers = new Dictionary<Guid, Guid>();
        Dictionary<Guid, Guid> ServersToClients = new Dictionary<Guid, Guid>();

        ProxyType proxyType;
        List<int> filteredPackets;
        string ServerHost;
        int ServerPort;

        public ProxyProcessor(string serverHost, int serverPort, int listeningPort, ProxyType pType, int[] packetFilter)
        {
            PacketProcessing.CrashOnException = false;
            ServerHost = serverHost;
            ServerPort = serverPort;
            if (packetFilter != null)
            {
                filteredPackets = new List<int>(packetFilter);
            }
            else
            {
                filteredPackets = new List<int>();
            }
            proxyType = pType;
            clientProcessor = new PacketProcessing();
            clientProcessor.SetPort(listeningPort);

            Subscribe(clientProcessor);
            
            clientProcessor.StartClientListener();
        }
        void Subscribe(PacketProcessing Processor)
        {
            Processor.Connected += ServerProcessor_Connected;
            Processor.ConnectionLost += ServerProcessor_ConnectionLost;
            Processor.AudioCommandPacketReceived += ServerProcessor_AudioCommandPacketReceived;
            Processor.CommsIncomingPacketReceived += ServerProcessor_CommsIncomingPacketReceived;
            Processor.CommsOutgoingPacketReceived += ServerProcessor_CommsOutgoingPacketReceived;
            Processor.DestroyObjectPacketReceived += ServerProcessor_DestroyObjectPacketReceived;
            Processor.EngGridUpdatePacketReceived += ServerProcessor_EngGridUpdatePacketReceived;
            Processor.ExceptionEncountered += ServerProcessor_ExceptionEncountered;
            Processor.GameMessagePacketReceived += ServerProcessor_GameMessagePacketReceived;
            Processor.GameStartPacketReceived += ServerProcessor_GameStartPacketReceived;
            Processor.IncomingAudioPacketReceived += ServerProcessor_IncomingAudioPacketReceived;
            Processor.IntelPacketReceived += ServerProcessor_IntelPacketReceived;
            Processor.ObjectStatusUpdatePacketReceived += ServerProcessor_ObjectStatusUpdatePacketReceived;
            Processor.PackageReceived += ServerProcessor_PackageReceived;
            Processor.ShipAction2PacketReceived += ServerProcessor_ShipAction2PacketReceived;
            Processor.ShipAction3PacketReceived += ServerProcessor_ShipAction3PacketReceived;
            Processor.ShipActionPacketReceived += ServerProcessor_ShipActionPacketReceived;
            Processor.StationStatusPacketReceived += ServerProcessor_StationStatusPacketReceived;
            Processor.UndefinedPacketReceived += ServerProcessor_UndefinedPacketReceived;
            Processor.Unknown2PacketReceived += ServerProcessor_Unknown2PacketReceived;
            Processor.VersionPacketReceived += ServerProcessor_VersionPacketReceived;
            Processor.WelcomePacketReceived += ServerProcessor_WelcomePacketReceived;
            Processor.NewConnectionCreated += Processor_NewClientConnected;
        }

        void Unsubscribe(PacketProcessing Processor)
        {
            Processor.Connected -= ServerProcessor_Connected;
            Processor.ConnectionLost -= ServerProcessor_ConnectionLost;
            Processor.AudioCommandPacketReceived -= ServerProcessor_AudioCommandPacketReceived;
            Processor.CommsIncomingPacketReceived -= ServerProcessor_CommsIncomingPacketReceived;
            Processor.CommsOutgoingPacketReceived -= ServerProcessor_CommsOutgoingPacketReceived;
            Processor.DestroyObjectPacketReceived -= ServerProcessor_DestroyObjectPacketReceived;
            Processor.EngGridUpdatePacketReceived -= ServerProcessor_EngGridUpdatePacketReceived;
            Processor.ExceptionEncountered -= ServerProcessor_ExceptionEncountered;
            Processor.GameMessagePacketReceived -= ServerProcessor_GameMessagePacketReceived;
            Processor.GameStartPacketReceived -= ServerProcessor_GameStartPacketReceived;
            Processor.IncomingAudioPacketReceived -= ServerProcessor_IncomingAudioPacketReceived;
            Processor.IntelPacketReceived -= ServerProcessor_IntelPacketReceived;
            Processor.ObjectStatusUpdatePacketReceived -= ServerProcessor_ObjectStatusUpdatePacketReceived;
            Processor.PackageReceived -= ServerProcessor_PackageReceived;
            Processor.ShipAction2PacketReceived -= ServerProcessor_ShipAction2PacketReceived;
            Processor.ShipAction3PacketReceived -= ServerProcessor_ShipAction3PacketReceived;
            Processor.ShipActionPacketReceived -= ServerProcessor_ShipActionPacketReceived;
            Processor.StationStatusPacketReceived -= ServerProcessor_StationStatusPacketReceived;
            Processor.UndefinedPacketReceived -= ServerProcessor_UndefinedPacketReceived;
            Processor.Unknown2PacketReceived -= ServerProcessor_Unknown2PacketReceived;
            Processor.VersionPacketReceived -= ServerProcessor_VersionPacketReceived;
            Processor.WelcomePacketReceived -= ServerProcessor_WelcomePacketReceived;
            Processor.NewConnectionCreated -= Processor_NewClientConnected;
        }


        Guid LastclientID = Guid.Empty;
        void Processor_NewClientConnected(object sender, ConnectionEventArgs e)
        {
            if (sender == clientProcessor)
            {
                LastclientID = e.ID;
                StartServerConnection();
            }
            else
            {
                if (proxyType == ProxyType.OneServerConnectionToOneClientConnection)
                {
                    ServersToClients.Add(e.ID, LastclientID);
                    ClientsToServers.Add(LastclientID, e.ID);
                    LastclientID = Guid.Empty;
                }
            }
        }
        void StartServerConnection()
        {
            if (serverProcessor == null)
            {
                serverProcessor = new PacketProcessing();
                serverProcessor.SetPort(ServerPort);
                serverProcessor.SetServerHost(ServerHost);

                Subscribe(serverProcessor);
                serverProcessor.StartServerConnection();
            }
            else
            {
                if (proxyType == ProxyType.OneServerConnectionToOneClientConnection)
                {
                    serverProcessor.StartServerConnection();

                }
            }

        }
        public event EventHandler<ProxyPackageEventArgs> WelcomePacketReceived;
        void ServerProcessor_WelcomePacketReceived(object sender, PackageEventArgs e)
        {
            if (WelcomePacketReceived != null)
            {
                KeyValuePair<PacketProcessing, ProxyPackageEventArgs> key = GetProxyPacketEventArgs(e);

                WelcomePacketReceived(this, key.Value);
            }
        }


        public event EventHandler<ProxyPackageEventArgs> VersionPacketReceived;
        void ServerProcessor_VersionPacketReceived(object sender, PackageEventArgs e)
        {
            if (VersionPacketReceived != null)
            {
                KeyValuePair<PacketProcessing, ProxyPackageEventArgs> key = GetProxyPacketEventArgs(e);

                VersionPacketReceived(this, key.Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> Unknown2PacketReceived;
        void ServerProcessor_Unknown2PacketReceived(object sender, PackageEventArgs e)
        {
            if (Unknown2PacketReceived != null)
            {
                Unknown2PacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> UndefinedPacketReceived;
        void ServerProcessor_UndefinedPacketReceived(object sender, PackageEventArgs e)
        {
            if (UndefinedPacketReceived != null)
            {
                UndefinedPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> StationStatusPacketReceived;
        void ServerProcessor_StationStatusPacketReceived(object sender, PackageEventArgs e)
        {
            if (StationStatusPacketReceived != null)
            {
                StationStatusPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }
        public event EventHandler<ProxyPackageEventArgs> ShipActionPacketReceived;
        void ServerProcessor_ShipActionPacketReceived(object sender, PackageEventArgs e)
        {

            if (ShipActionPacketReceived != null)
            {
                ShipActionPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }
        public event EventHandler<ProxyPackageEventArgs> ShipAction3PacketReceived;
        void ServerProcessor_ShipAction3PacketReceived(object sender, PackageEventArgs e)
        {
            if (ShipAction3PacketReceived != null)
            {
                ShipAction3PacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }
        public event EventHandler<ProxyPackageEventArgs> ShipAction2PacketReceived;
        void ServerProcessor_ShipAction2PacketReceived(object sender, PackageEventArgs e)
        {
            if (ShipAction2PacketReceived != null)
            {
                ShipAction2PacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }
        KeyValuePair<PacketProcessing, ProxyPackageEventArgs> GetProxyPacketEventArgs(PackageEventArgs e)
        {
            Guid TargetID = Guid.Empty;
            KeyValuePair<PacketProcessing, ProxyPackageEventArgs> retVal;
            PacketProcessing pp = (e.ReceivedPacket.Origin == OriginType.Client) ? serverProcessor : clientProcessor;
            if (e.ReceivedPacket.Origin == OriginType.Client)
            {
                pp = serverProcessor;
                if (proxyType == ProxyType.OneServerConnectionToOneClientConnection)
                {
                    TargetID = ClientsToServers[e.ID];
                }
            }
            else
            {
                pp = clientProcessor;
                if (proxyType == ProxyType.OneServerConnectionToOneClientConnection)
                {
                    TargetID = ServersToClients[e.ID];

                }
            }
            retVal = new KeyValuePair<PacketProcessing, ProxyPackageEventArgs>(pp, new ProxyPackageEventArgs(e.ID, TargetID, e.ReceivedPacket));
            return retVal;
        }
        public event EventHandler<ProxyPackageEventArgs> PackageReceived;
        void ServerProcessor_PackageReceived(object sender, PackageEventArgs e)
        {
            Guid TargetID = Guid.Empty;

            //Guid SourceID = e.ID;
            PacketProcessing pp = null;




            if (!filteredPackets.Contains((int)e.ReceivedPacket.PacketType))
            {
                KeyValuePair<PacketProcessing, ProxyPackageEventArgs> key = GetProxyPacketEventArgs(e);
                pp = key.Key;
                TargetID = key.Value.TargetID;
                if (proxyType == ProxyType.OneServerConnectionToOneClientConnection)
                {

                    pp.Send(TargetID, e.ReceivedPacket);
                }
                else
                {
                    pp.Send(e.ReceivedPacket);
                }
                if (PackageReceived != null)
                {
                    System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(RaisePackageRecieved), key.Value);
                }
            }
            //@@@
        }
        void RaisePackageRecieved(object state)
        {

            PackageReceived(this, (ProxyPackageEventArgs)state);
        }
        public event EventHandler<ProxyPackageEventArgs> ObjectStatusUpdatePacketReceived;
        void ServerProcessor_ObjectStatusUpdatePacketReceived(object sender, PackageEventArgs e)
        {
            if (ObjectStatusUpdatePacketReceived != null)
            {
                ObjectStatusUpdatePacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }
        public event EventHandler<ProxyPackageEventArgs> IntelPacketReceived;
        void ServerProcessor_IntelPacketReceived(object sender, PackageEventArgs e)
        {
            if (IntelPacketReceived != null)
            {
                IntelPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> IncomingAudioPacketReceived;
        void ServerProcessor_IncomingAudioPacketReceived(object sender, PackageEventArgs e)
        {
            if (IncomingAudioPacketReceived != null)
            {
                IncomingAudioPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> GameStartPacketReceived;
        void ServerProcessor_GameStartPacketReceived(object sender, PackageEventArgs e)
        {
            if (GameStartPacketReceived != null)
            {
                GameStartPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> GameMessagePacketReceived;
        void ServerProcessor_GameMessagePacketReceived(object sender, PackageEventArgs e)
        {
            if (GameMessagePacketReceived != null)
            {
                GameMessagePacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ExceptionEventArgs> ExceptionEncountered;
        void ServerProcessor_ExceptionEncountered(object sender, ExceptionEventArgs e)
        {
            if (ExceptionEncountered != null)
            {
                ExceptionEncountered(this, e);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> EngGridUpdatePacketReceived;
        void ServerProcessor_EngGridUpdatePacketReceived(object sender, PackageEventArgs e)
        {
            if (EngGridUpdatePacketReceived != null)
            {
                EngGridUpdatePacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> DestroyObjectPacketReceived;
        void ServerProcessor_DestroyObjectPacketReceived(object sender, PackageEventArgs e)
        {
            if (DestroyObjectPacketReceived != null)
            {
                DestroyObjectPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> CommsOutgoingPacketReceived;
        void ServerProcessor_CommsOutgoingPacketReceived(object sender, PackageEventArgs e)
        {
            if (CommsOutgoingPacketReceived != null)
            {
                CommsOutgoingPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> CommsIncomingPacketReceived;
        void ServerProcessor_CommsIncomingPacketReceived(object sender, PackageEventArgs e)
        {
            if (CommsIncomingPacketReceived != null)
            {
                CommsIncomingPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ProxyPackageEventArgs> AudioCommandPacketReceived;
        void ServerProcessor_AudioCommandPacketReceived(object sender, PackageEventArgs e)
        {
            if (AudioCommandPacketReceived != null)
            {
                AudioCommandPacketReceived(this, GetProxyPacketEventArgs(e).Value);
            }
        }

        public event EventHandler<ConnectionEventArgs> ConnectionLost;
        void ServerProcessor_ConnectionLost(object sender, ConnectionEventArgs e)
        {
            if (ConnectionLost != null)
            {
                ConnectionLost(this, e);
            }
        }

        public event EventHandler<ConnectionEventArgs> Connected;
        void ServerProcessor_Connected(object sender, ConnectionEventArgs e)
        {
            if (Connected != null)
            {
                Connected(this, e);
            }
        }


        bool isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {

                    if (serverProcessor != null)
                    {
                        Unsubscribe(serverProcessor);
                        serverProcessor.Dispose();
                    }
                    if (clientProcessor != null)
                    {
                        Unsubscribe(clientProcessor);
                        clientProcessor.Dispose();
                    }


                    isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
