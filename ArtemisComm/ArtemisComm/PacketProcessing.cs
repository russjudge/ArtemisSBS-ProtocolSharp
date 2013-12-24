using ArtemisComm.ShipAction2SubPackets;
using ArtemisComm.ShipAction3SubPackets;
using ArtemisComm.ShipActionSubPackets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ArtemisComm
{
    public class PacketProcessing : IDisposable
    {
        public static bool CrashOnException { get; set; }
        static PacketProcessing()
        {
            CrashOnException = true;
        }
        public string Host { get; private set; }
       
        public PacketProcessing()
        {
            Initialize();
        }

        public void SetServerHost(string host)
        {
            if (!string.IsNullOrEmpty(Host))
            {
                throw new InvalidOperationException("Host already set--cannot be changed once the host is set.");
            }
            else
            {
                Host = host;
            }
        }
        public void SetPort(int port)
        {
            if (Port == 0)
            {
                Port = port;
            }
            else
            {
                throw new InvalidOperationException("Port already set--cannot be changed once the port is set.");
            }
        }
        public void Send(Guid connectionID, Packet p)
        {
            if (p != null && connections != null && connections.ContainsKey(connectionID))
            {

                connections[connectionID].Send(p.GetBytes());

            }
        }

        public void Send(Packet p)
        {
            if (p != null && connections.Count > 0)
            {
                foreach (Connector conn in connections.Values)
                {
                    conn.Send(p.GetBytes());
                }

            }
        }
       
        
        /// <summary>
        /// Sends the set ship sub packet.
        /// </summary>
        /// <param name="connectionID">The connection identifier.</param>
        /// <param name="selectedShip">The selected ship. (1-based)</param>
        public void SendSetShipSubPacket(Guid connectionID, int selectedShip)
        {
            Send(connectionID, SetShipSubPacket.GetPackage(selectedShip));
           
        }
        public void SendSetShipSubPacket(int selectedShip)
        {
            if (IsConnectedToServer && connections.Count == 1)
            {
                Send(SetShipSubPacket.GetPackage(selectedShip));
            }
            else
            {
                throw new InvalidOperationException("Connection ID MUST be specified.");
            }

        }
        
        public void SendSetStationSubPacket(Guid connectionID, StationTypes station, bool isSelected)
        {

            Send(connectionID, SetStationSubPacket.GetPacket(station, isSelected));
                
        }
        public void SendReadySubPacket(Guid connectionID)
        {
            Send(connectionID, ReadySubPacket.GetPacket());
        }
        public void SendReady2SubPacket(Guid connectionID)
        {
            Send(connectionID, Ready2SubPacket.GetPacket());
        }
        public void SendEngSetCoolantSubPacket(Guid connectionID, ShipSystem system, int value)
        {
            Send(connectionID, EngSetCoolantSubPacket.GetPacket(system, value));
        }

        public void SendEngSetEngerySubPacket(Guid connectionID, ShipSystem system, float value)
        {
            Send(connectionID, EngSetEnergySubPacket.GetPacket(system, value));
        }
        public void SendToggleRedAlert(Guid connectionID)
        {
            Send(connectionID, ToggleRedAlertSubPacket.GetPacket());
        }
        public void SendToggleShields(Guid connectionID)
        {
            Send(connectionID, ToggleShieldsSubPacket.GetPacket());
        }
       

        void Initialize()
        {

            ThreadStart start = new ThreadStart(QueueToPacketProcessor);

            QueueToPacketThread = new Thread(start);
            QueueToPacketThread.Priority = ThreadPriority.AboveNormal;
            QueueToPacketThread.Start();
        }
        public bool IsConnectedToServer{get; private set;}
        public bool IsConnectedToClients { get; private set; }

        Dictionary<Guid, Connector> connections = new Dictionary<Guid, Connector>();
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        public void StartServerConnection()
        {
            if (IsConnectedToClients)
            {
                throw new InvalidOperationException("Cannot set up a server connection when already listening on clients.");
            }
            
            IsConnectedToServer = true;
          
       
            Connector conn = new Connector(Port);

            connections.Add(conn.ID, conn);

            TcpClient client = new TcpClient();
            
            Subscribe(conn);
            try
            {

                client.Connect(Host, Port);

                conn.Start(client);
                OnEvent(NewConnectionCreated, new ConnectionEventArgs(conn.ID));
            }
            catch (SocketException)
            {
                if (UnableToConnect != null)
                {
                    UnableToConnect(this, EventArgs.Empty);
                }
            }
            catch (Exception)
            {
                if (CrashOnException)
                {
                    throw;
                }
            }
        }
        public event EventHandler UnableToConnect;

        void Subscribe(Connector conn)
        {

            conn.ExceptionEncountered += conn_ExceptionEncountered;
            conn.BytesReceived += conn_BytesReceived;
            conn.Connected += conn_Connected;
            conn.ConnectionLost += conn_ConnectionLost;
        }
        void Unsubscribe(Connector conn)
        {

            conn.ExceptionEncountered -= conn_ExceptionEncountered;
            conn.BytesReceived -= conn_BytesReceived;
            conn.Connected -= conn_Connected;
            conn.ConnectionLost -= conn_ConnectionLost;
        }
        void RaiseExceptionEncountered(Exception e, Guid id)
        {
            OnEvent(ExceptionEncountered, new ExceptionEventArgs(e, id));
           
        }
        public event EventHandler<ConnectionEventArgs> Connected;
        public event EventHandler<ConnectionEventArgs> ConnectionLost;
        void conn_ConnectionLost(object sender, ConnectionEventArgs e)
        {
           
            OnEvent(ConnectionLost, e);
        }

        void conn_Connected(object sender, ConnectionEventArgs e)
        {
            
            OnEvent(Connected, e);
        }

        void conn_BytesReceived(object sender, BytesReceivedEventArgs e)
        {

            Enqueue(e.Buffer, e.ID);
        }
      

        void conn_ExceptionEncountered(object sender, ExceptionEventArgs e)
        {
            RaiseExceptionEncountered(e.CapturedException, e.ID);
        }
        public void StartClientListener()
        {
            if (IsConnectedToServer)
            {
                throw new InvalidOperationException("Cannot set up a client listener when already connected to a server.");
            }
            if (IsConnectedToClients)
            {
                throw new InvalidOperationException("Cannot set up more than one client listener.");
            }
            IsConnectedToClients = true;

            ThreadStart start = new ThreadStart(ListenForConnections);
            ClientListenerThread = new Thread(start);
            ClientListenerThread.Start();
            
            
        }

       
        void ListenForConnections()
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, Port);
        
            try
            {
                while (!abort)
                {
                    StartClientConnection(listener.AcceptTcpClient());
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (System.IO.IOException e)
            {
                RaiseExceptionEncountered(e, Guid.Empty);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                RaiseExceptionEncountered(e, Guid.Empty);
            }
            catch (Exception e)
            {
                if (CrashOnException)
                {
                    throw;
                }
                else
                {
                    RaiseExceptionEncountered(e, Guid.Empty);
                }
            }
            
        }

        public void StartClientConnection(TcpClient client)
        {
          

            Connector conn = new Connector(Port);

            connections.Add(conn.ID, conn);

            Subscribe(conn);

            conn.Start(client);
            OnEvent(NewConnectionCreated, new ConnectionEventArgs(conn.ID));
          
        }

        public event EventHandler<ConnectionEventArgs> NewConnectionCreated;

        int Port { get; set; }
        bool abort = false;

        System.Threading.Thread QueueToPacketThread;
        System.Threading.Thread ClientListenerThread;

        private ManualResetEvent mreListener = new ManualResetEvent(false);
        private ManualResetEvent mrePacketReceived = new ManualResetEvent(false);
        private ManualResetEvent mreSpecificPacketReceived = new ManualResetEvent(false);
        Queue<KeyValuePair<List<byte>, Guid>> ProcessQueue = new Queue<KeyValuePair<List<byte>, Guid>>();
        #region Events
        
        public event EventHandler<PackageEventArgs> PackageReceived;
        public event EventHandler<PackageEventArgs> AudioCommandPacketReceived;
        public event EventHandler<PackageEventArgs> CommsIncomingPacketReceived;
        public event EventHandler<PackageEventArgs> CommsOutgoingPacketReceived;
        public event EventHandler<PackageEventArgs> DestroyObjectPacketReceived;
        public event EventHandler<PackageEventArgs> EngGridUpdatePacketReceived;
        public event EventHandler<PackageEventArgs> GameMessagePacketReceived;
        public event EventHandler<PackageEventArgs> IncomingAudioPacketReceived;
        public event EventHandler<PackageEventArgs> ObjectStatusUpdatePacketReceived;
        public event EventHandler<PackageEventArgs> ShipActionPacketReceived;
        public event EventHandler<PackageEventArgs> ShipAction2PacketReceived;
        public event EventHandler<PackageEventArgs> ShipAction3PacketReceived;
        public event EventHandler<PackageEventArgs> StationStatusPacketReceived;
        public event EventHandler<PackageEventArgs> GameStartPacketReceived;
        public event EventHandler<PackageEventArgs> Unknown2PacketReceived;
        public event EventHandler<PackageEventArgs> IntelPacketReceived;
        public event EventHandler<PackageEventArgs> VersionPacketReceived;
        public event EventHandler<PackageEventArgs> WelcomePacketReceived;
        public event EventHandler<PackageEventArgs> UndefinedPacketReceived;

        public event EventHandler<ExceptionEventArgs> ExceptionEncountered;
        #endregion
        #region IDisposable

        bool isDisposed = false;
        private void Dispose(bool Disposing)
        {
            if (!isDisposed)
            {
                if (Disposing)
                {
                    abort = true;

                    foreach (Connector connection in connections.Values)
                    {
                        if (connection != null)
                        {

                            connection.Stop();
                            Unsubscribe(connection);
                            connection.Dispose();
                        }
                    }


                    if (mrePacketReceived != null)
                    {
                        mrePacketReceived.Set();
                        mrePacketReceived.Dispose();
                    }
                    if (mreSpecificPacketReceived != null)
                    {
                        mreSpecificPacketReceived.Set();
                        mreSpecificPacketReceived.Dispose();
                    }

                    if (mreListener != null)
                    {
                        mreListener.Set();
                        mreListener.Dispose();
                    }
                    if (RaisePackageReceivedThread != null && RaisePackageReceivedThread.ThreadState == ThreadState.Running)
                    {
                        RaisePackageReceivedThread.Abort();
                    }
                    if (RaiseSpecificPackageReceivedThread != null && RaiseSpecificPackageReceivedThread.ThreadState == ThreadState.Running)
                    {
                        RaiseSpecificPackageReceivedThread.Abort();
                    }


                    if (QueueToPacketThread != null && QueueToPacketThread.ThreadState == ThreadState.Running)
                    {
                        QueueToPacketThread.Abort();
                    }
                    if (ClientListenerThread != null && ClientListenerThread.ThreadState == ThreadState.Running)
                    {
                        ClientListenerThread.Abort();
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
        
        #endregion

        #region ConnectionToPacket
        
        void Enqueue(byte[] byteArray, Guid ID)
        {
            List<byte> b = byteArray.ToList();

            KeyValuePair<List<byte>, Guid> bytes = new KeyValuePair<List<byte>, Guid>(b, ID);
            //System.Array.Copy(byteArray, 0, byteArray, 0, 0);
            ProcessQueue.Enqueue(bytes);
            mreListener.Set();
        }

        void RaisePackageReceived()
        {
            while (!abort)
            {
                while (PackageReceivedQueue.Count > 0)
                {
                    OnEvent(PackageReceived, PackageReceivedQueue.Dequeue());
                    //PackageReceived(this, PackageReceivedQueue.Dequeue());
                }
                if (!abort)
                {
                    mrePacketReceived.Reset();
                    mrePacketReceived.WaitOne();
                }
            }
        }
        protected virtual void OnEvent(EventHandler<ConnectionEventArgs> handler, ConnectionEventArgs e)
        {

            if (handler != null)
            {
                foreach (EventHandler<ConnectionEventArgs> singleCast in handler.GetInvocationList())
                {
                    ISynchronizeInvoke syncInvoke = singleCast.Target as ISynchronizeInvoke;
                    try
                    {
                        //This code is to make the raising of the event threadsafe with the UI thread.
                        if (syncInvoke != null && syncInvoke.InvokeRequired)
                        {
                            syncInvoke.BeginInvoke(singleCast, new object[] { this, e });
                        }
                        else
                        {
                            singleCast(this, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (CrashOnException)
                        {
                            throw;
                        }
                        else
                        {
                            if (e != null)
                            {
                                RaiseExceptionEncountered(ex, e.ID);
                            }
                            else
                            {
                                RaiseExceptionEncountered(ex, Guid.Empty);
                            }
                        }
                    }
                }

            }
        }
        protected virtual void OnEvent(EventHandler<ExceptionEventArgs> handler, ExceptionEventArgs e)
        {

            if (handler != null)
            {
                foreach (EventHandler<ExceptionEventArgs> singleCast in handler.GetInvocationList())
                {
                    ISynchronizeInvoke syncInvoke = singleCast.Target as ISynchronizeInvoke;
                    try
                    {
                        //This code is to make the raising of the event threadsafe with the UI thread.
                        if (syncInvoke != null && syncInvoke.InvokeRequired)
                        {
                            syncInvoke.BeginInvoke(singleCast, new object[] { this, e });
                        }
                        else
                        {
                            singleCast(this, e);
                        }
                    }
                    catch 
                    {
                        
                    }
                }

            }
        }
        protected virtual void OnEvent(EventHandler<PackageEventArgs> handler, PackageEventArgs e)
        {


            if (handler != null)
            {
                foreach (EventHandler<PackageEventArgs> singleCast in handler.GetInvocationList())
                {
                    ISynchronizeInvoke syncInvoke = singleCast.Target as ISynchronizeInvoke;
                    try
                    {
                        //This code is to make the raising of the event threadsafe with the UI thread.
                        if (syncInvoke != null && syncInvoke.InvokeRequired)
                        {
                            syncInvoke.BeginInvoke(singleCast, new object[] { this, e });
                        }
                        else
                        {
                            singleCast(this, e);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (CrashOnException)
                        {
                            throw;

                        }
                        else
                        {
                            if (e != null)
                            {
                                RaiseExceptionEncountered(ex, e.ID);
                            }
                            else
                            {
                                RaiseExceptionEncountered(ex, Guid.Empty);
                            }
                        }
                    }
                }

            }

        }
        void RaiseSpecificPacketEvents()
        {
            while (!abort)
            {
                while (SpecificPacketQueue.Count > 0)
                {
                    PackageEventArgs pea = SpecificPacketQueue.Dequeue();
                    if (pea != null)
                    {
                        try
                        {
                            string methodName = pea.ReceivedPacket.PacketType.ToString() + "Received";

                            Type t = this.GetType();
                            if (t.GetEvent(methodName) != null)
                            {

                                var eventDelegate = (MulticastDelegate)t.GetField(methodName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
                                OnEvent(eventDelegate as EventHandler<PackageEventArgs>, pea);
                             

                            }
                            else
                            {
                                OnEvent(UndefinedPacketReceived, pea);

                            }

                        }
                        catch (Exception e)
                        {
                            if (CrashOnException)
                            {
                                throw;
                            }
                            else
                            {
                                RaiseExceptionEncountered(e, pea.ID);
                            }
                        }
                    }
                    else
                    {
                        
                    }
                }
                if (!abort)
                {
                    mreSpecificPacketReceived.Reset();
                    mreSpecificPacketReceived.WaitOne();
                }
            }
        }
        System.Threading.Thread RaisePackageReceivedThread;
        System.Threading.Thread RaiseSpecificPackageReceivedThread;
        Queue<PackageEventArgs> PackageReceivedQueue = new Queue<PackageEventArgs>();
        Queue<PackageEventArgs> SpecificPacketQueue = new Queue<PackageEventArgs>();



        void QueueToPacketProcessor()
        {
            System.Threading.ThreadStart start = new ThreadStart(RaisePackageReceived);
            RaisePackageReceivedThread = new Thread(start);
            
            RaisePackageReceivedThread.Start();

            start = new ThreadStart(RaiseSpecificPacketEvents);
            RaiseSpecificPackageReceivedThread = new Thread(start);

            RaiseSpecificPackageReceivedThread.Start();

            do
            {
                lock (ProcessQueue)
                {
                    while (ProcessQueue.Count > 0)
                    {
                        KeyValuePair<List<byte>, Guid> que = ProcessQueue.Dequeue();
                        

                        try
                        {
                            Packet p = new Packet(que.Key.ToArray());
                            if (p != null)
                            {
                                PackageEventArgs pea = new PackageEventArgs(p, que.Value);
                                if (PackageReceived != null)
                                {

                                    PackageReceivedQueue.Enqueue(pea);

                                    mrePacketReceived.Set();
                                }
                                SpecificPacketQueue.Enqueue(pea);
                                mreSpecificPacketReceived.Set();
                            }
                            else
                            {

                            }


                        }
                        catch (ArgumentException ex)
                        {
                        }
                        catch (Exception ex)
                        {
                            if (CrashOnException)
                            {
                                throw;
                            }
                            else
                            {
                                RaiseExceptionEncountered(ex, que.Value);
                            }
                        }
                        if (abort)
                        {
                            break;
                        }
                    }


                }
                if (!abort)
                {
                    mreListener.Reset();
                    mreListener.WaitOne();
                }
            } while (!abort);
        }
        #endregion
    }
}
