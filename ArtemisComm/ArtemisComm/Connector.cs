using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ArtemisComm
{
    internal sealed class Connector: IDisposable
    {
        const int ConnectionTimeout = 0;
        public static readonly int StandardID = BitConverter.ToInt32(BitConverter.GetBytes(0xdeadbeef), 0);
        public Guid ID { get; set; }
        public Connector(int port)
        {
            ID = Guid.NewGuid();
            Port = port;
        }




        int Port { get; set; }
        TcpClient Client { get; set; }
        internal bool Abort { get; set; }


        System.Threading.Thread ServerConnectionThread { get; set; }
        System.Threading.Thread SendingThread { get; set; }


        Queue<MemoryStream> SendQueue = new Queue<MemoryStream>();

        public NetworkStream ServerStream = null;
        private ManualResetEvent mreSender = new ManualResetEvent(false);

        public event EventHandler<ConnectionEventArgs> Connected;
        public event EventHandler<ConnectionEventArgs> ConnectionLost;
        public event EventHandler<ExceptionEventArgs> ExceptionEncountered;
        public event EventHandler<BytesReceivedEventArgs> BytesReceived;

        void RaiseExceptionEncountered(Exception ex)
        {
            if (ExceptionEncountered != null)
            {
                ExceptionEncountered(this, new ExceptionEventArgs(ex, this.ID));
            }
        }
      
        void RaiseBytesReceived(Stream buffer)
        {
            if (BytesReceived != null)
            {
                BytesReceived(this, new BytesReceivedEventArgs(buffer.GetMemoryStream(0), this.ID));
            }
        }
        /// <summary>
        /// Gets a value indicating whether [is connected].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [is connected]; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected
        {
            get
            {
                return Client.Connected;
            }
        }

        /// <summary>
        /// Starts the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
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



        /// <summary>
        /// Stops the connection
        /// </summary>
        public void Stop()
        {

            Abort = true;
            mreSender.Set();

            Client.Close();

        }

        //Converted to MemoryStream
        /// <summary>
        /// Processes Byte from the TCP/IP stream to queue processor.
        /// </summary>
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
                //List<byte> buffer = null;
                int bytesRead = 0;
                int currentBlock = 0;

                do
                {
                    using (MemoryStream msBuffer = new MemoryStream())
                    {
                        byte[] wrkBuff = null;
                        do
                        {
                            buff = new byte[8];
                            currentBlock = 0;
                            //buffer = new List<byte>();



                            #region Get Packet Length

                            bytesRead = ServerStream.Read(buff, 0, buff.Length);
                            if (bytesRead > 0)
                            {

                                byte[] wrkByte = new byte[bytesRead];
                                Array.Copy(buff, 0, wrkByte, 0, bytesRead);
                                currentBlock += bytesRead;

                                //buffer.AddRange(buff);
                                msBuffer.Write(buff, 0, bytesRead);

                                //Code here to fix error with packet and try to self-adjust.  This may cause packets to be ignored.
                                //if (buffer.Count >= 4)
                                if (msBuffer.Length >= 4)
                                {
                                    wrkBuff = msBuffer.GetBuffer();
                                    int pos = 0;
                                    int headerID = 0;
                                    do
                                    {
                                        //headerID = BitConverter.ToInt32(buffer.ToArray(), 0);
                                        headerID = BitConverter.ToInt32(wrkBuff, pos);
                                        if (headerID != StandardID)
                                        {
                                            pos++;
                                            //buffer.RemoveAt(0);
                                        }
                                    } while (pos < wrkBuff.Length - 3 && headerID != StandardID);
                                }
                            }
                        } while (msBuffer != null && msBuffer.Length < 8 && bytesRead > 0);


                            #endregion



                        if (bytesRead > 0 && msBuffer != null && msBuffer.Length >= 8)
                        {


                            int ln = BitConverter.ToInt32(wrkBuff, 4);

                            buff = new byte[ln];
                            int remainToRead = ln - 8;
                            if (remainToRead > 0)
                            {
                                do
                                {


                                    bytesRead = ServerStream.Read(buff, 0, remainToRead);
                                    if (bytesRead > 0)
                                    {

                                        byte[] wrkByte = new byte[bytesRead];
                                        Array.Copy(buff, 0, wrkByte, 0, bytesRead);
                                        currentBlock += bytesRead;

                                        //buffer.AddRange(wrkByte);
                                        msBuffer.Write(wrkByte, 0, bytesRead);
                                        remainToRead -= bytesRead;
                                    }



                                    //} while (buffer.Count < ln && bytesRead > 0);
                                } while (msBuffer.Length < ln && bytesRead > 0);
                                if (bytesRead > 0)
                                {
                                    //RaiseBytesReceived(buffer.ToArray());
                                    msBuffer.Position = 0;
                                    RaiseBytesReceived(msBuffer);
                                    
                                }



                            }


                        }
                        else
                        {
                            Abort = true;

                            mreSender.Set();
                        }
                    }

                } while (!Abort);

            }
            catch (ThreadAbortException)
            {

            }
            catch (System.IO.IOException)
            {

                //RaiseExceptionEncountered(e);
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
        public void Send(MemoryStream stream)
        {
            if (stream != null && stream.Length > 0)
            {
                
                    stream.Position = 0;
                    //try
                    //{
                    lock (SendQueue)
                    {
                    SendQueue.Enqueue(stream.GetMemoryStream(0));
                }
                    mreSender.Set();
                
                //}
                //catch (ArgumentException)
                //{ 
                //    //For some odd reason I'm getting an occasional ArgumentException:
                //    //Source array was not long enough. Check srcIndex and length, and the array's lower bounds.
                //    //This does not make sense, and there is nothing I can do to fix.  So I am choosing to ignore it,
                //    //  hoping that things will still work.


                //}
            }
        }
        //TODO: remove this routine in favor of the MemoryStream version.
        //public void Send(byte[] byteArray)
        //{
        //    if (byteArray != null && byteArray.Length > 0)
        //    {
        //        //below retry logic due to getting this error:
        //        //InnerException: System.ArgumentException
        //        //HResult=-2147024809
        //        //Message=Source array was not long enough. Check srcIndex and length, and the array's lower bounds.
        //        //Reason for this is complete unknown, and solution is unknown.
        //        //  Retry logic still might not solve issue, but it is worth a try.

        //        Send(new MemoryStream(byteArray));
                
        //    }
        //}
        void SendProcessor()
        {
            MemoryStream ms = null;
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
                                lock (SendQueue)
                                {
                                    ms = SendQueue.Dequeue();
                                }
                                if (ms != null)
                                {
                                    ms.Position = 0;
                                    
                                    ms.CopyTo(ServerStream);


                                }

                                //byte[] buff = SendQueue.Dequeue();
                                //if (buff != null)
                                //{
                                //    ServerStream.Write(buff, 0, buff.Length);
                                //}
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
            finally
            {
                if (ms != null)
                {
                    ms.Dispose();
                }
            }
        }

        bool isDisposed = false;
        void Dispose(bool Disposing)
        {
            if (!isDisposed)
            {
                if (Disposing)
                {

                    Abort = true;
                    while (SendQueue.Count > 0)
                    {
                        try
                        {
                            SendQueue.Dequeue().Dispose();
                        }
                        catch { }
                    }
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
