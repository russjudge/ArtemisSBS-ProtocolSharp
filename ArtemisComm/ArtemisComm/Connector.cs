using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ArtemisComm
{
    internal class Connector: IDisposable
    {
        const int ConnectionTimeout = 15000;
        public const uint StandardID = 0xdeadbeef;
        public Guid ID { get; set; }
        public Connector(int port)
        {
            ID = Guid.NewGuid();
            Port = port;
        }




        protected int Port { get; set; }
        protected TcpClient Client { get; set; }
        internal bool Abort { get; set; }


        System.Threading.Thread ServerConnectionThread { get; set; }
        System.Threading.Thread SendingThread { get; set; }


        Queue<byte[]> SendQueue = new Queue<byte[]>();

        public NetworkStream ServerStream = null;
        private ManualResetEvent mreSender = new ManualResetEvent(false);

        public event EventHandler<ConnectionEventArgs> Connected;
        public event EventHandler<ConnectionEventArgs> ConnectionLost;
        public event EventHandler<ExceptionEventArgs> ExceptionEncountered;
        public event EventHandler<BytesReceivedEventArgs> BytesReceived;

        protected void RaiseExceptionEncountered(Exception ex)
        {
            if (ExceptionEncountered != null)
            {
                ExceptionEncountered(this, new ExceptionEventArgs(ex, this.ID));
            }
        }
        protected void RaiseBytesReceived(byte[] buffer)
        {
            if (BytesReceived != null)
            {
                BytesReceived(this, new BytesReceivedEventArgs(buffer, this.ID));
            }
        }
        public bool IsConnected
        {
            get
            {
                return Client.Connected;
            }
        }
        
        public void Start(TcpClient client)
        {

            if (Client != null)
            {
                Client.Close();
            }
            Client = client;
            Client.ReceiveTimeout = ConnectionTimeout;
            Client.SendTimeout = ConnectionTimeout;

            ThreadStart start = new ThreadStart(BytesToQueueProcessor);
            ServerConnectionThread = new Thread(start);

            ServerConnectionThread.Start();



            start = new ThreadStart(SendProcessor);
            SendingThread = new Thread(start);
            SendingThread.Priority = ThreadPriority.AboveNormal;
            SendingThread.Start();


        }

     

        public void Stop()
        {

            Abort = true;
            mreSender.Set();

            Client.Close();

        }
        
        void BytesToQueueProcessor()
        {
            try
            {
              

                ServerStream = Client.GetStream();
                if (this.Connected != null)
                {
                    Connected(this, new ConnectionEventArgs(this.ID));
                }
                
                byte[] buff = null;
                List<byte> buffer = null;
                int bytesRead = 0;
                int currentBlock = 0;
                
                do
                {

                    do
                    {
                        buff = new byte[8];
                        currentBlock = 0;
                        buffer = new List<byte>();
                        bytesRead = ServerStream.Read(buff, 0, buff.Length);
                        if (bytesRead > 0)
                        {
                            
                            byte[] wrkByte = new byte[bytesRead];
                            Array.Copy(buff, 0, wrkByte, 0, bytesRead);
                            currentBlock += bytesRead;

                            buffer.AddRange(buff);

                            //Code here to fix error with packet and try to self-adjust.  This may cause packets to be ignored.
                            if (buffer.Count >= 4)
                            {
                                uint headerID = 0;
                                do
                                {
                                    headerID = BitConverter.ToUInt32(buffer.ToArray(), 0);
                                    if (headerID != StandardID)
                                    {
                                        buffer.RemoveAt(0);
                                    }
                                } while (buffer.Count > 3 && headerID != StandardID);
                            }
                        }

                    } while (buffer.Count < 8 && bytesRead > 0);
                    if (bytesRead > 0)
                    {
                        
                        int ln = BitConverter.ToInt32(buffer.ToArray(), 4);
                       
                        buff = new byte[ln];
                        int remainToRead = ln - 8;
                        do
                        {


                            bytesRead = ServerStream.Read(buff, 0, remainToRead);
                            if (bytesRead > 0)
                            {
                                
                                byte[] wrkByte = new byte[bytesRead];
                                Array.Copy(buff, 0, wrkByte, 0, bytesRead);
                                currentBlock += bytesRead;

                                buffer.AddRange(wrkByte);
                                remainToRead -= bytesRead;
                            }



                        } while (buffer.Count < ln && bytesRead > 0);
                        if (bytesRead > 0)
                        {
                            RaiseBytesReceived(buffer.ToArray());
                           

                        }
                    }
                    else
                    {
                        Abort = true;
                        
                        mreSender.Set();
                    }

                } while (!Abort);

            }
            catch (ThreadAbortException)
            {

            }
            catch (System.IO.IOException e)
            {
                RaiseExceptionEncountered(e);
            }
            catch (System.Net.Sockets.SocketException e)
            {
                RaiseExceptionEncountered(e);
            }
            if (this.ConnectionLost != null)
            {
                ConnectionLost(this, new ConnectionEventArgs(this.ID));
            }
          
        }

        public void ClearSendQueue()
        {
            SendQueue.Clear();
        }
        
        public void Send(byte[] byteArray)
        {
            if (byteArray != null)
            {
                SendQueue.Enqueue(byteArray);
                mreSender.Set();
            }
        }
        void SendProcessor()
        {
            try
            {
                do
                {
                    mreSender.WaitOne();
                    if (!Abort)
                    {
                        if (ServerStream != null)
                        {
                            while (SendQueue.Count > 0)
                            {

                                byte[] buff = SendQueue.Dequeue();
                                ServerStream.Write(buff, 0, buff.Length);
                            }
                        }

                    }
                    if (!Abort)
                    {
                        mreSender.Reset();
                    }
                } while (!Abort);
            }
            catch (ThreadAbortException)
            {

            }
            catch (SocketException e)
            {
                RaiseExceptionEncountered(e);
            }

        }

        bool isDisposed = false;
        protected virtual void Dispose(bool Disposing)
        {
            if (!isDisposed)
            {
                if (Disposing)
                {

                    Abort = true;
                    if (mreSender != null)
                    {
                        mreSender.Set();
                        mreSender.Dispose();
                    }
                    
                    if (ServerConnectionThread != null && ServerConnectionThread.ThreadState == ThreadState.Running)
                    {
                        ServerConnectionThread.Abort();

                    }
                  
                    if (SendingThread != null && SendingThread.ThreadState == ThreadState.Running)
                    {
                        SendingThread.Abort();
                    }
                    if (ServerStream != null)
                    {
                        ServerStream.Dispose();
                    }
                    try
                    {
                        if (Client != null)
                        {
                            Client.Close();
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

    }
}
