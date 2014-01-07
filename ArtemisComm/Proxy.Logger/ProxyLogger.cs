using ArtemisComm.Proxy.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.Proxy.Logger
{
    public class ProxyLogger : IDisposable
    {
        public ProxyLogger(IPacketToLogging logger)
        {
            
            Logger = logger;
            EnableLogging = (Logger != null);
        }
        IPacketToLogging Logger = null;
        public ProxyProcessor Proxy { get; private set; }
        bool _enableLogging = true;
        public bool EnableLogging 
        {
            get
            {
                return _enableLogging;
            }
            set
            {
                if (!IsActive)
                {
                    _enableLogging = value;
                }
                if (Logger == null)
                {
                    _enableLogging = false;
                }
            }
        }
        public int ConnectionCount { get; set; }
        public event EventHandler NewConnection;
        public event EventHandler LostConnection;
        public bool IsActive { get; private set; }
        public bool ProxyIsExternal { get; private set; }
        public void StartProxy(ProxyProcessor proxy)
        {
            
            Proxy = proxy;
            SubscribeProxy();
            IsActive = true;
            ProxyIsExternal = true;
        }

        public void StartProxy(string serverHost, int serverPort, int listeningPort, ProxyType proxyType, int[] filteredPackets)
        {

            Proxy = new ProxyProcessor(serverHost, serverPort, listeningPort, proxyType, filteredPackets);
            SubscribeProxy();
            IsActive = true;
        }
        
        public void StopProxy()
        {
            IsActive = false;
            UnsubscribeProxy();
            if (!ProxyIsExternal)
            {
                Proxy.Dispose();
                Proxy = null;
            }
        }


        void SubscribeProxy()
        {
            Proxy.Connected += proxy_Connected;
            Proxy.ConnectionLost += proxy_ConnectionLost;
            Proxy.ExceptionEncountered += proxy_ExceptionEncountered;
            if (EnableLogging)
            {
                Proxy.PackageReceived += proxy_PackageReceived;
            }
        }
        void proxy_PackageReceived(object sender, ArtemisComm.Proxy.Library.ProxyPackageEventArgs e)
        {
            if (EnableLogging)
            {
                object key = Logger.Process(e.ReceivedPacket, e.ID, e.TargetID, GetSubPacketType(e.ReceivedPacket));
                List<PropertyValue> PropertyList = new List<PropertyValue>();
                GetPropertyInformation(PropertyList, e.ReceivedPacket, e.ReceivedPacket.PacketType.ToString(), 0);
                foreach (PropertyValue prop in PropertyList)
                {

                    Logger.ProcessValues(key, prop.PropertyName, prop.Value, prop.ObjectType, prop.HexValue);
                }
            }
        }
        static int GetSubPacketType(Packet p)
        {
            int retVal = -1;
            if (p.PacketType == PacketType.GameMessagePacket)
            {
                GameMessagePacket packet = (GameMessagePacket)p.Package;
                retVal = (int)packet.SubPacketType;
            }
            if (p.PacketType == PacketType.ObjectStatusUpdatePacket)
            {
                ObjectStatusUpdatePacket packet = (ObjectStatusUpdatePacket)p.Package;
                retVal = Convert.ToInt32((byte)packet.SubPacketType);

            }
            if (p.PacketType == PacketType.ShipActionPacket)
            {
                ShipActionPacket packet = (ShipActionPacket)p.Package;

                retVal = (int)packet.SubPacketType;
            }
            if (p.PacketType == PacketType.ShipAction2Packet)
            {
                ShipAction2Packet packet = (ShipAction2Packet)p.Package;

                retVal = (int)packet.SubPacketType;

            }
            if (p.PacketType == PacketType.ShipAction3Packet)
            {
                ShipAction3Packet packet = (ShipAction3Packet)p.Package;

                retVal = (int)packet.SubPacketType;

            }
            return retVal;
        }



        //void Build()
        static void GetPropertyInformation(IList<PropertyValue> infoList, object obj, string propertyName, int depth)
        {
            


            ICollection ar = obj as ICollection;
            if (ar != null)
            {

                int i = 0;
                foreach (object o1 in ar)
                {
                    if (o1 != null)
                    {
                        GetPropertyInformation(infoList, o1, propertyName + "." + i.ToString(CultureInfo.InvariantCulture), depth);
                    }
                    i++;
                }
            }
            else
            {
                foreach (PropertyInfo pi in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {


                    if (pi.PropertyType == typeof(IPackage) || pi.PropertyType.IsSubclassOf(typeof(VariablePackage)))
                    {
                        object o2 = pi.GetValue(obj, null);
                        if (o2 != null)
                        {
                            GetPropertyInformation(infoList, o2, propertyName + "." + pi.Name, depth + 1);
                        }
                    }
                    else
                    {
                        object objx = pi.GetValue(obj, null);
                        if (objx != null)
                        {
                            ICollection arx = objx as ICollection;
                            if (arx != null)
                            {
                                GetPropertyInformation(infoList, objx, propertyName + "." + pi.Name, depth);
                            }
                            else
                            {
                                string propType = pi.PropertyType.ToString();
                                if (pi.PropertyType.IsEnum)
                                {
                                    propType += "(" + pi.PropertyType.GetEnumUnderlyingType().ToString() + ")";
                                }
                                PropertyValue pv = new PropertyValue("".PadLeft(depth, '-') + propertyName + "." + pi.Name, objx.ToString(), propType, GetHexValue(objx));
                                if (!string.IsNullOrEmpty(pv.HexValue))
                                {
                                    infoList.Add(pv);
                                }
                            }
                        }
                    }
                }
            }
        }
        static string GetHexValue(object obj)
        {
            string retVal = string.Empty;
            byte[] byteArray = null;
            Type t = obj.GetType();
            if (t.IsEnum)
            {
                t = t.GetEnumUnderlyingType();


            }
            if (t == typeof(int))
            {
                byteArray = BitConverter.GetBytes((int)obj);
            }

            if (t == typeof(uint))
            {
                byteArray = BitConverter.GetBytes((uint)obj);
            }

            if (t == typeof(short))
            {
                byteArray = BitConverter.GetBytes((short)obj);
            }

            if (t == typeof(ushort))
            {
                byteArray = BitConverter.GetBytes((ushort)obj);
            }

            if (t == typeof(byte))
            {
                byteArray = new byte[1] { (byte)obj };
            }

            if (t == typeof(bool))
            {
                byteArray = new byte[1] { Convert.ToByte(obj, CultureInfo.InvariantCulture) };
            }

            if (t == typeof(ArtemisString))
            {
                using (MemoryStream ms = ((ArtemisString)obj).GetRawData())
                {
                    byteArray = new byte[ms.Length];
                    ms.Read(byteArray, 0, byteArray.Length);
                }
            }
            if (t == typeof(string))
            {
                byteArray = System.Text.ASCIIEncoding.ASCII.GetBytes((string)obj);
            }
            if (t == typeof(float))
            {
                byteArray = BitConverter.GetBytes((float)obj);
            }
            if (byteArray != null)
            {
                List<string> elem = new List<string>();
                for (int i = 0; i < byteArray.Length; i++)
                {
                    elem.Add(byteArray[i].ToString("X", CultureInfo.InvariantCulture).PadLeft(2, '0'));
                }
                retVal = string.Join(" ", elem.ToArray());
            }
            return retVal;

        }







        void proxy_ExceptionEncountered(object sender, ArtemisComm.ExceptionEventArgs e)
        {
            if (EnableLogging)
            {
                Logger.AddException(e.ID, e.CapturedException);
            }
        }


        void proxy_ConnectionLost(object sender, ArtemisComm.ConnectionEventArgs e)
        {
            ConnectionCount--;
            if (LostConnection != null)
            {
                LostConnection(this, EventArgs.Empty);
            }
        }

        void proxy_Connected(object sender, ArtemisComm.ConnectionEventArgs e)
        {
            ConnectionCount++;
        }

        void UnsubscribeProxy()
        {
            if (Proxy != null)
            {
                Proxy.Connected -= proxy_Connected;
                Proxy.ConnectionLost -= proxy_ConnectionLost;
                Proxy.ExceptionEncountered -= proxy_ExceptionEncountered;
                if (EnableLogging)
                {
                    Proxy.PackageReceived -= proxy_PackageReceived;
                }
            }
        }
        bool isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!isDisposed)
                {
                    if (Proxy != null)
                    {
                        Proxy.Dispose();
                    }
                    //IDisposable log = Logger as IDisposable;
                    //if (log != null)
                    //{
                    //    log.Dispose();
                    //}
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
