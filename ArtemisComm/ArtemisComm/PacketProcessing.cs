using ArtemisComm.ShipAction2SubPackets;
using ArtemisComm.ShipAction3SubPackets;
using ArtemisComm.ShipActionSubPackets;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;

namespace ArtemisComm
{
    public class PacketProcessing : IDisposable
    {

        #region Events

        public event EventHandler<ConnectionEventArgs> NewConnectionCreated;
        /// <summary>
        /// Occurs when [package received].  Raised first for all packets.  Use for pass-through processing.
        /// </summary>
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
        public event EventHandler<PackageEventArgs> Unknown3PacketReceived;
        public event EventHandler<PackageEventArgs> IntelPacketReceived;
        public event EventHandler<PackageEventArgs> VersionPacketReceived;
        public event EventHandler<PackageEventArgs> WelcomePacketReceived;
        public event EventHandler<PackageEventArgs> UndefinedPacketReceived;

        public event EventHandler<ExceptionEventArgs> ExceptionEncountered;
        #endregion

        #region Fields

        #region Threads
        System.Threading.Thread RaisePackageReceivedThread;
        System.Threading.Thread RaiseSpecificPackageReceivedThread;

        System.Threading.Thread QueueToPacketThread;
        System.Threading.Thread ClientListenerThread;

        #endregion
        #region Queues

        Queue<PackageEventArgs> PackageReceivedQueue = new Queue<PackageEventArgs>();
        Queue<PackageEventArgs> SpecificPacketQueue = new Queue<PackageEventArgs>();
        Queue<KeyValuePair<Stream, Guid>> ProcessQueue = new Queue<KeyValuePair<Stream, Guid>>();
        #endregion

        int Port { get; set; }
        bool abort = false;

        private ManualResetEvent mreListener = new ManualResetEvent(false);
        private ManualResetEvent mrePacketReceived = new ManualResetEvent(false);
        private ManualResetEvent mreSpecificPacketReceived = new ManualResetEvent(false);

        #endregion
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


        public void Send(Guid connectionID, Packet packet)
        {
            if (packet != null && connections != null && connections.ContainsKey(connectionID))
            {
                using (MemoryStream stream = packet.GetRawData())
                {
                    connections[connectionID].Send(stream);
                }
            }
        }

        public void Send(Packet packet)
        {
            if (packet != null && connections.Count > 0)
            {
                foreach (Connector conn in connections.Values)
                {
                    using (MemoryStream stream = packet.GetRawData())
                    {
                        conn.Send(stream);
                    }
                }

            }
        }

        #region Client To Server sending

        public void SendAudioCommandPacket(Guid connectionID, int id, int playOrDismiss)
        {
            Send(connectionID, AudioCommandPacket.GetPacket(id, playOrDismiss));
        }
        public void SendCommsOutgoingPacket(Guid connectionID, int recipientType, int recipientID, int messageID, int targetObjectID, int unknown)
        {
            Send(connectionID, CommsOutgoingPacket.GetPacket(recipientType, recipientID, messageID, targetObjectID, unknown));
        }



        #region Ship Action Sub Packets


        public void SendCaptainSelectSubPacket(Guid connectionID, int targetID)
        {
            Send(connectionID, CaptainSelectSubPacket.GetPacket(targetID));
        }
        public void SendDiveRiseSubPacket(Guid connectionID, int delta)
        {
            Send(connectionID, DiveRiseSubPacket.GetPacket(delta));
        }
        public void SendEngSetAutoDamconSubPacket(Guid connectionID, bool damComIsAutonomous)
        {
            Send(connectionID, EngSetAutoDamconSubPacket.GetPacket(damComIsAutonomous));
        }

        public void SendFireTubeSubPacket(Guid connectionID, int tubeIndex)
        {
            Send(connectionID, FireTubeSubPacket.GetPacket(tubeIndex));
        }


        public void SendHelmRequestDockSubPacket(Guid connectionID, int value)
        {
            Send(connectionID, HelmRequestDockSubPacket.GetPacket(value));
        }
        public void SendHelmSetWarpSubPacket(Guid connectionID, int warpFactor)
        {
            Send(connectionID, HelmSetWarpSubPacket.GetPacket(warpFactor));
        }


        public void SendHelmToggleReverseSubPacket(Guid connectionID, int value)
        {
            Send(connectionID, HelmToggleReverseSubPacket.GetPacket(value));
        }

        public void SendReadySubPacket(Guid connectionID, int value)
        {
            Send(connectionID, ReadySubPacket.GetPacket(value));
        }



        public void SendReady2SubPacket(Guid connectionID, int value)
        {
            Send(connectionID, Ready2SubPacket.GetPacket(value));
        }


        public void SendSciScanSubPacket(Guid connectionID, int targetID)
        {
            Send(connectionID, SciScanSubPacket.GetPacket(targetID));
        }

        public void SendSciSelectSubPacket(Guid connectionID, int targetID)
        {
            Send(connectionID, SciSelectSubPacket.GetPacket(targetID));
        }



        public void SendSetBeamFreqSubPacket(Guid connectionID, int frequencyIndex)
        {
            Send(connectionID, SetBeamFreqSubPacket.GetPacket(frequencyIndex));
        }


        public void SendSetMainScreenSubPacket(Guid connectionID, int value)
        {
            Send(connectionID, SetMainScreenSubPacket.GetPacket(value));
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



        public void SendSetShipSettingsSubPacket(Guid connectionID, DriveType drive, int shipType, int unknown, string shipName)
        {
            Send(connectionID, SetShipSettingsSubPacket.GetPacket(drive, shipType, unknown, shipName));
        }

        public void SendSetStationSubPacket(Guid connectionID, StationType station, bool isSelected)
        {

            Send(connectionID, SetStationSubPacket.GetPacket(station, isSelected));

        }


        public void SendSetWeaponsTargetSubPacket(Guid connectionID, int tubeIndex)
        {
            Send(connectionID, SetWeaponsTargetSubPacket.GetPacket(tubeIndex));
        }

        public void SendToggleAutoBeamsSubPacket(Guid connectionID, int value)
        {
            Send(connectionID, ToggleAutoBeamsSubPacket.GetPacket(value));
        }


        public void SendTogglePerspectiveSubPacket(Guid connectionID, int value)
        {
            Send(connectionID, TogglePerspectiveSubPacket.GetPacket(value));
        }
        public void SendToggleRedAlert(Guid connectionID, int value)
        {
            Send(connectionID, ToggleRedAlertSubPacket.GetPacket(value));
        }
        public void SendToggleShields(Guid connectionID, int value)
        {
            Send(connectionID, ToggleShieldsSubPacket.GetPacket(value));
        }

        public void SendUnloadTubeSubPacket(Guid connectionID, int tubeIndex)
        {
            Send(connectionID, UnloadTubeSubPacket.GetPacket(tubeIndex));
        }

        #endregion

        #region Ship Action Packet 2 Sub-packets
        public void SendConvertTorpedoSubPacket(Guid connectionID, float direction, int unknown1, int unknown2, int unknown3)
        {
            Send(connectionID, ConvertTorpedoSubPacket.GetPacket(direction, unknown1, unknown2, unknown3));
        }
        public void SendEngSendDamconSubPacket(Guid connectionID, int teamNumber, int x, int y, int z)
        {
            Send(connectionID, EngSendDamconSubPacket.GetPacket(teamNumber, x, y, z));
        }
        public void SendEngSetCoolantSubPacket(Guid connectionID, ShipSystem system, int value)
        {
            Send(connectionID, EngSetCoolantSubPacket.GetPacket(system, value));
        }

        public void SendLoadTubeSubPacket(Guid connectionID, int tubeIndex, int ordinance)
        {
            Send(connectionID, LoadTubeSubPacket.GetPacket(tubeIndex, ordinance));
        }

        #endregion
        #region Ship Action Packet 3 Sub packets

        public void SendEngSetEnergySubPacket(Guid connectionID, ShipSystem system, float value)
        {
            Send(connectionID, EngSetEnergySubPacket.GetPacket(system, value));
        }
        public void SendHelmJumpSubPacket(Guid connectionID, float bearing, float distance)
        {
            Send(connectionID, HelmJumpSubPacket.GetPacket(bearing, distance));
        }
        public void SendHelmSetImpulseSubPacket(Guid connectionID, float velocity)
        {
            Send(connectionID, HelmSetImpulseSubPacket.GetPacket(velocity));
        }

        public void SendHelmSetSteeringSubPacket(Guid connectionID, float turnValue)
        {
            Send(connectionID, HelmSetSteeringSubPacket.GetPacket(turnValue));
        }


        #endregion
        #endregion

        void Initialize()
        {

            ThreadStart start = new ThreadStart(QueueToPacketProcessor);

            QueueToPacketThread = new Thread(start);
            QueueToPacketThread.Priority = ThreadPriority.AboveNormal;
            QueueToPacketThread.Start();


            start = new ThreadStart(RaisePackageReceived);
            RaisePackageReceivedThread = new Thread(start);

            RaisePackageReceivedThread.Start();

            start = new ThreadStart(RaiseSpecificPacketEvents);
            RaiseSpecificPackageReceivedThread = new Thread(start);

            RaiseSpecificPackageReceivedThread.Start();

        }
        public bool IsConnectedToServer { get; private set; }
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

            Enqueue(e.DataStream, e.ID);
        }


        void conn_ExceptionEncountered(object sender, ExceptionEventArgs e)
        {
            RaiseExceptionEncountered(e.CapturedException, e.ID);
        }
        #region Client
        
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
        TcpListener listener = null;

        void ListenForConnections()
        {
            listener = new TcpListener(System.Net.IPAddress.Any, Port);

            try
            {
                listener.Start();
                while (!abort)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    if (client != null)
                    {
                        StartClientConnection(client);
                    }
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
                    throw new PacketProcessingException(e);
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

                    if (listener != null)
                    {
                        listener.Stop();
                        
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
                    try
                    {
                        if (RaisePackageReceivedThread != null && RaisePackageReceivedThread.ThreadState == ThreadState.Running)
                        {
                            RaisePackageReceivedThread.Abort();
                        }
                    }
                    catch { }
                    try
                    {
                        if (RaiseSpecificPackageReceivedThread != null && RaiseSpecificPackageReceivedThread.ThreadState == ThreadState.Running)
                        {
                            RaiseSpecificPackageReceivedThread.Abort();
                        }
                    }
                    catch { }
                    try
                    {
                        if (QueueToPacketThread != null && QueueToPacketThread.ThreadState == ThreadState.Running)
                        {
                            QueueToPacketThread.Abort();
                        }
                    }
                    catch { }
                    try
                    {
                        if (ClientListenerThread != null && ClientListenerThread.ThreadState == ThreadState.Running)
                        {
                            ClientListenerThread.Abort();
                        }
                    }
                    catch { }
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

        void Enqueue(Stream stream, Guid ID)
        {



            KeyValuePair<Stream, Guid> bytes = new KeyValuePair<Stream, Guid>(stream, ID);

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
        #region EventRaising
        
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
                            throw new PacketProcessingException(ex);

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
                            FieldInfo f = t.GetField(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
                            if (f != null)
                            {
                                var eventDelegate = (MulticastDelegate)f.GetValue(this);
                                if (eventDelegate != null)
                                {
                                    OnEvent(eventDelegate as EventHandler<PackageEventArgs>, pea);
                                }
                                else
                                {

                                }
                            }
                            else
                            {

                            }

                            //EventInfo ev = t.GetEvent(methodName);
                            //MethodInfo m = null;
                            //if (ev!= null)
                            //{
                            //    m = ev.GetRaiseMethod();
                            //    if (m != null)
                            //    {
                            //        object[] parms = new object[] { this, pea };

                            //        m.Invoke(this, parms);
                            //    }
                            //}
                            //else
                            //{
                            //    OnEvent(UndefinedPacketReceived, pea);

                            //}

                        }
                        catch (Exception e)
                        {
                            if (CrashOnException)
                            {
                                throw new PacketProcessingException(e);
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
        //void ProcessSpecificPacket(object state)
        //{
        //    PackageEventArgs pea = state as PackageEventArgs;
        //    if (pea != null)
        //    {

        //        string methodName = pea.ReceivedPacket.PacketType.ToString() + "Received";

        //        Type t = this.GetType();

        //        var eventDelegate = (MulticastDelegate)t.GetField(methodName, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(this);
        //        OnEvent(eventDelegate as EventHandler<PackageEventArgs>, pea);

        //    }
        //}
        #endregion
        void EnqueueSpecificPacket(PackageEventArgs pea)
        {

            EnqueuePacket(SpecificPacketQueue, pea, mreSpecificPacketReceived);
        }
        void EnqueueReceivedPacket(PackageEventArgs pea)
        {
           
            if (PackageReceived != null)
            {
                EnqueuePacket(PackageReceivedQueue, pea, mrePacketReceived);
            }
        }
        static void EnqueuePacket(Queue<PackageEventArgs> que, PackageEventArgs pea, ManualResetEvent mre)
        {

            que.Enqueue(pea);

            mre.Set();


        }
        void QueueToPacketProcessor()
        {
           
            do
            {
                lock (ProcessQueue)
                {
                    while (ProcessQueue.Count > 0)
                    {
                        KeyValuePair<Stream, Guid> que = ProcessQueue.Dequeue();

                        if (que.Key != null)
                        {
                            try
                            {

                                Packet p = new Packet(que.Key);
                                if (p != null)
                                {
                                    PackageEventArgs pea = new PackageEventArgs(p, que.Value);
                                    EnqueueReceivedPacket(pea);
                                    if (p.ConversionException != null)
                                    {
                                        if (CrashOnException)
                                        {
                                            throw new InvalidPacketException(p.ConversionException);
                                        }
                                        else
                                        {
                                            RaiseExceptionEncountered(p.ConversionException, que.Value);
                                        }
                                    }
                                    if (p.Package != null)
                                    {
                                        ReadOnlyCollection<Exception> packetErrors = p.Package.GetErrors();
                                        if (packetErrors.Count > 0)
                                        {
                                            foreach (Exception e in packetErrors)
                                            {
                                                RaiseExceptionEncountered(e, que.Value);
                                            }
                                        }
                                    }
                                    

                                    EnqueueSpecificPacket(pea);

                                }
                                else
                                {

                                }


                            }
                            
                            catch (Exception ex)
                            {
                                if (!abort)
                                {
                                    if (CrashOnException)
                                    {
                                        throw new InvalidPacketException(ex);
                                    }
                                    else
                                    {
                                        RaiseExceptionEncountered(ex, que.Value);
                                    }
                                }
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
