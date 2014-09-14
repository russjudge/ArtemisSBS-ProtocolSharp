using ArtemisComm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sandbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
     
        public MainWindow()
        {

            Packet.ThrowWhenInvalid = false;
            Result = new ObservableCollection<PropertyValue>();
            InitializeComponent();
        }

        public static readonly DependencyProperty HostProperty =
           DependencyProperty.Register("Host", typeof(string),
               typeof(MainWindow), new PropertyMetadata("192.168.15.127"));

        public string Host
        {
            get
            {
                return (string)this.GetValue(HostProperty);

            }
            set
            {
                this.SetValue(HostProperty, value);

            }
        }
        public static readonly DependencyProperty PortProperty =
          DependencyProperty.Register("Port", typeof(int),
              typeof(MainWindow), new PropertyMetadata(2010));

        public int Port
        {
            get
            {
                return (int)this.GetValue(PortProperty);

            }
            set
            {
                this.SetValue(PortProperty, value);

            }
        }

        public static readonly DependencyProperty RawDataProperty =
           DependencyProperty.Register("RawData", typeof(string),
               typeof(MainWindow));

        public string RawData
        {
            get
            {
                return (string)this.GetValue(RawDataProperty);

            }
            set
            {
                this.SetValue(RawDataProperty, value);

            }
        }


        public static readonly DependencyProperty FilterPacketsProperty =
          DependencyProperty.Register("FilterPackets", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(true));

        public bool FilterPackets
        {
            get
            {
                return (bool)this.GetValue(FilterPacketsProperty);

            }
            set
            {
                this.SetValue(FilterPacketsProperty, value);

            }
        }


        public static readonly DependencyProperty ResultProperty =
          DependencyProperty.Register("Result", typeof(ObservableCollection<PropertyValue>),
              typeof(MainWindow));

        public ObservableCollection<PropertyValue> Result
        {
            get
            {
                return (ObservableCollection<PropertyValue>)this.GetValue(ResultProperty);

            }
            set
            {
                this.SetValue(ResultProperty, value);

            }
        }

        ArtemisComm.PacketProcessing ServerConnection = null;
        private void OnStartServer(object sender, RoutedEventArgs e)
        {
            if (ServerConnection != null)
            {
                ServerConnection.Dispose();
            }
            ServerConnection = new ArtemisComm.PacketProcessing();
            ServerConnection.SetPort(Port);
            ServerConnection.SetServerHost(Host);

            ServerConnection.AudioCommandPacketReceived += conn_AudioCommandPacketReceived;
            ServerConnection.CommsIncomingPacketReceived += conn_CommsIncomingPacketReceived;
            ServerConnection.CommsOutgoingPacketReceived += conn_CommsOutgoingPacketReceived;
            ServerConnection.DestroyObjectPacketReceived += conn_DestroyObjectPacketReceived;
            ServerConnection.EngGridUpdatePacketReceived += conn_EngGridUpdatePacketReceived;
            ServerConnection.GameMessagePacketReceived += conn_GamesMessagePacketReceived;
            ServerConnection.IncomingAudioPacketReceived += conn_IncomingAudioPacketReceived;
            ServerConnection.ObjectStatusUpdatePacketReceived += conn_ObjectStatusUpdatePacketReceived;

            ServerConnection.PackageReceived += conn_PackageReceived;  //This is not necessary (using only for logging) since all other events are subscribed to.

            ServerConnection.ShipActionPacketReceived += conn_ShipActionPacketReceived;
            ServerConnection.ShipAction2PacketReceived += conn_ShipActionPacket2Received;
            ServerConnection.ShipAction3PacketReceived += conn_ShipActionPacket3Received;
            ServerConnection.StationStatusPacketReceived += conn_StationStatusPacketReceived;
            ServerConnection.UndefinedPacketReceived += conn_UndefinedPacketReceived;
            ServerConnection.GameStartPacketReceived += conn_UnknownPacket1Received;
            ServerConnection.Unknown2PacketReceived += conn_UnknownPacket2Received;
            ServerConnection.VersionPacketReceived += conn_VersionPacketReceived;
            ServerConnection.WelcomePacketReceived += conn_WelcomePacketReceived;
            ServerConnection.StartServerConnection();
            btnStartServer.Visibility = Visibility.Collapsed;
            btnStopServer.Visibility = Visibility.Visible;

        }
        void AddPacket(Packet p)
        {
            if (this.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
            {
                this.Dispatcher.Invoke(new Action<Packet>(AddPacket), p);
            }
            else
            {
                GetPropertyInformation(Result, p, p.PacketType.ToString(), 0);

                PropertyValue pv = new PropertyValue("----------", "-----------", "-----------", "-----------");
                Result.Add(pv);
                ResultList.ScrollIntoView(ResultList.Items[Result.Count - 1]);
            }
        }

        //Yeah--all events could have subscribed to the same routine. 
        // But, I might want to do something in my tests specific to certain packets later.
        void conn_WelcomePacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_VersionPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_UnknownPacket2Received(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_UnknownPacket1Received(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_UndefinedPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_StationStatusPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_ShipActionPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_ShipActionPacket3Received(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_ShipActionPacket2Received(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_PackageReceived(object sender, PackageEventArgs e)
        {
            //AddPacket(e.ReceivedPacket);
        }

        void conn_ObjectStatusUpdatePacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_IncomingAudioPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_GamesMessagePacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_EngGridUpdatePacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_DestroyObjectPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_CommsOutgoingPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_CommsIncomingPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }

        void conn_AudioCommandPacketReceived(object sender, PackageEventArgs e)
        {
            AddPacket(e.ReceivedPacket);
        }


        private void OnConvert(object sender, RoutedEventArgs e)
        {
            RawData = OnConvert(RawData);
        }

        static byte[] ConvertToByteArray(string dataForProcessing)
        {
            string[] data = dataForProcessing.Replace("\r", string.Empty).Replace("\n", string.Empty).Replace(" ", ":").Replace('!', ':').Split(':');

            List<byte> byteArray = new List<byte>();
            foreach (string item in data)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    try
                    {
                        byteArray.Add(Convert.ToByte(item, 16));
                    }
                    catch { }
                }
            }
            return byteArray.ToArray();
        }
        /// <summary>
        /// Called when [convert].
        /// </summary>
        /// <param name="dataForProcessing">The data for processing.</param>
        /// <returns>Unprocessed, incomplete packet</returns>
        private string OnConvert(string dataForProcessing)
        {
            
            string retVal = string.Empty;
            //Could have multiple packets: need to code to handle.
            //Packet p = new Packet(me.RawData);
            //How Data looks from Wireshark:
            //ef:be:ad:de:8c:00:00:00:01:00:00:00:00:00:00:00:78:00:00:00:da:b3:04:6d:70:00:00:00:59:6f:75:20:68:61:76:65:20:63:6f:6e:6e:65:63:74:65:64:20:74:6f:20:54:68:6f:6d:20:52:6f:62:65:72:74:73:6f:6e:27:73:20:41:72:74:65:6d:69:73:20:42:72:69:64:67:65:20:53:69:6d:75:6c:61:74:6f:72:2e:20:20:50:6c:65:61:73:65:20:63:6f:6e:6e:65:63:74:20:77:69:74:68:20:61:6e:20:61:75:74:68:6f:72:69:7a:65:64:20:67:61:6d:65:20:63:6c:69:65:6e:74:2e
            //2222
            List<byte> byteArray = new List<byte>(ConvertToByteArray(dataForProcessing));

            //byteArray is now total message--need to parse out multiple messages.
            int ln = 0;
            List<byte> workArray = null;
            int startIndex = 0;
            if (byteArray.Count > 7)
            {
                do
                {

                    ln = BitConverter.ToInt32(byteArray.ToArray(), 4);


                    if (byteArray.Count < startIndex + ln)
                    {
                        break;
                    }


                    if (startIndex + ln > byteArray.Count)
                    {
                        ln = byteArray.Count - startIndex;
                    }

                    workArray = new List<byte>();
                    for (int i = startIndex; i < startIndex + ln; i++)
                    {
                        workArray.Add(byteArray[i]);
                    }
                    using (MemoryStream ms = new MemoryStream(byteArray.ToArray()))
                    {
                        using (Packet p = new Packet(ms.GetMemoryStream(0)))
                        {
                            bool ignore = false;

                            if (FilterPackets && p.PacketType == PacketType.ObjectStatusUpdatePacket)
                            {
                                ObjectStatusUpdatePacket objP = p.Package as ObjectStatusUpdatePacket;
                                if (objP != null && objP.SubPacketType == ArtemisComm.ObjectStatusUpdateSubPackets.ObjectStatusUpdateSubPacketType.UnknownSubPacket)
                                {
                                    ignore = true;
                                }
                            }
                            if (!ignore)
                            {
                                GetPropertyInformation(Result, p, p.PacketType.ToString(), 0);

                                PropertyValue pv = new PropertyValue("----------", "-----------", "-----------", "-----------");
                                Result.Add(pv);
                            }
                        }
                    }
                    startIndex += ln;
                } while (startIndex < byteArray.Count - ArtemisComm.Packet.HeaderLength);
            }
            if (startIndex < byteArray.Count)
            {
                List<string> elem = new List<string>();
                for (int i = startIndex; i < byteArray.Count; i++)
                {
                    elem.Add(byteArray[i].ToString("X", CultureInfo.InvariantCulture).PadLeft(2,'0'));
                }
                retVal = string.Join(":", elem.ToArray());
            }
            if (Result.Count > 0)
            {
                ResultList.ScrollIntoView(ResultList.Items[Result.Count - 1]);
            }
            System.Media.SystemSounds.Beep.Play();
            return retVal;
        }
        void GetPropertyInformation(IList<PropertyValue> infoList, object obj, string propertyName, int depth)
        {
            ICollection ar = obj as ICollection;
            if (ar != null)
            {

                int i = 0;
                foreach(object o1 in ar)
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
        void DisposeServer()
        {
            if (ServerConnection != null)
            {
                ServerConnection.AudioCommandPacketReceived -= conn_AudioCommandPacketReceived;
                ServerConnection.CommsIncomingPacketReceived -= conn_CommsIncomingPacketReceived;
                ServerConnection.CommsOutgoingPacketReceived -= conn_CommsOutgoingPacketReceived;
                ServerConnection.DestroyObjectPacketReceived -= conn_DestroyObjectPacketReceived;
                ServerConnection.EngGridUpdatePacketReceived -= conn_EngGridUpdatePacketReceived;
                ServerConnection.GameMessagePacketReceived -= conn_GamesMessagePacketReceived;
                ServerConnection.IncomingAudioPacketReceived -= conn_IncomingAudioPacketReceived;
                ServerConnection.ObjectStatusUpdatePacketReceived -= conn_ObjectStatusUpdatePacketReceived;

                ServerConnection.PackageReceived -= conn_PackageReceived;  //This is not necessary (using only for logging) since all other events are subscribed to.

                ServerConnection.ShipActionPacketReceived -= conn_ShipActionPacketReceived;
                ServerConnection.ShipAction2PacketReceived -= conn_ShipActionPacket2Received;
                ServerConnection.ShipAction3PacketReceived -= conn_ShipActionPacket3Received;
                ServerConnection.StationStatusPacketReceived -= conn_StationStatusPacketReceived;
                ServerConnection.UndefinedPacketReceived -= conn_UndefinedPacketReceived;
                ServerConnection.GameStartPacketReceived -= conn_UnknownPacket1Received;
                ServerConnection.Unknown2PacketReceived -= conn_UnknownPacket2Received;
                ServerConnection.VersionPacketReceived -= conn_VersionPacketReceived;
                ServerConnection.WelcomePacketReceived -= conn_WelcomePacketReceived;
                ServerConnection.Dispose();
            }
        }
        private void OnStopServer(object sender, RoutedEventArgs e)
        {
            DisposeServer();
            btnStopServer.Visibility = Visibility.Collapsed;
            btnStartServer.Visibility = Visibility.Visible;
        }
        bool isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!isDisposed)
                {
                    DisposeServer();
                    
                    isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void OnClosed(object sender, EventArgs e)
        {
            Dispose();
        }


        public static readonly DependencyProperty BitConverterValueProperty =
          DependencyProperty.Register("BitConverterValue", typeof(string),
              typeof(MainWindow));

        public string BitConverterValue
        {
            get
            {
                return (string)this.GetValue(BitConverterValueProperty);

            }
            set
            {
                this.SetValue(BitConverterValueProperty, value);

            }
        }

        public static readonly DependencyProperty BitConverterResultProperty =
          DependencyProperty.Register("BitConverterResult", typeof(string),
              typeof(MainWindow));

        public string BitConverterResult
        {
            get
            {
                return (string)this.GetValue(BitConverterResultProperty);

            }
            set
            {
                this.SetValue(BitConverterResultProperty, value);

            }
        }

        static void OnIntConvertChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow me = sender as MainWindow;
            if (me != null)
            {
                if (me.IntConvert)
                {
                    me.ShortConvert = false;
                    me.UIntConvert = false;
                    me.UShortConvert = false;
                    me.FloatConvert = false;
                    me.ByteConvert = false;
                    me.StringConvert = false;
                }
            }
        }

        static void OnShortConvertChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow me = sender as MainWindow;
            if (me != null)
            {
                if (me.ShortConvert)
                {

                    me.UIntConvert = false;
                    me.UShortConvert = false;
                    me.FloatConvert = false;
                    me.ByteConvert = false;
                    me.StringConvert = false;
                    me.IntConvert = false;
                }
            }
        }

        static void OnUIntConvertChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow me = sender as MainWindow;
            if (me != null)
            {
                if (me.UIntConvert)
                {
                    me.ShortConvert = false;

                    me.UShortConvert = false;
                    me.FloatConvert = false;
                    me.ByteConvert = false;
                    me.StringConvert = false;
                    me.IntConvert = false;
                }
            }
        }

        static void OnUShortConvertChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow me = sender as MainWindow;
            if (me != null)
            {
                if (me.UShortConvert)
                {
                    me.ShortConvert = false;
                    me.UIntConvert = false;

                    me.FloatConvert = false;
                    me.ByteConvert = false;
                    me.StringConvert = false;
                    me.IntConvert = false;
                }
            }
        }

        static void OnFloatConvertChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow me = sender as MainWindow;
            if (me != null)
            {
                if (me.FloatConvert)
                {
                    me.ShortConvert = false;
                    me.UIntConvert = false;
                    me.UShortConvert = false;

                    me.ByteConvert = false;
                    me.StringConvert = false;
                    me.IntConvert = false;
                }
            }
        }

        static void OnByteConvertChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow me = sender as MainWindow;
            if (me != null)
            {
                if (me.ByteConvert)
                {
                    me.ShortConvert = false;
                    me.UIntConvert = false;
                    me.UShortConvert = false;
                    me.FloatConvert = false;

                    me.StringConvert = false;
                    me.IntConvert = false;
                }
            }
        }

        static void OnStringConvertChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            MainWindow me = sender as MainWindow;
            if (me != null)
            {
                if (me.StringConvert)
                {
                    me.ShortConvert = false;
                    me.UIntConvert = false;
                    me.UShortConvert = false;
                    me.FloatConvert = false;
                    me.ByteConvert = false;
                    me.IntConvert = false;
                }
            }
        }
        public static readonly DependencyProperty IntConvertProperty =
          DependencyProperty.Register("IntConvert", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(true, OnIntConvertChanged));

        public bool IntConvert
        {
            get
            {
                return (bool)this.GetValue(IntConvertProperty);

            }
            set
            {
                this.SetValue(IntConvertProperty, value);

            }
        }


        public static readonly DependencyProperty ShortConvertProperty =
          DependencyProperty.Register("ShortConvert", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(OnShortConvertChanged));

        public bool ShortConvert
        {
            get
            {
                return (bool)this.GetValue(ShortConvertProperty);

            }
            set
            {
                this.SetValue(ShortConvertProperty, value);

            }
        }


        public static readonly DependencyProperty UIntConvertProperty =
          DependencyProperty.Register("UIntConvert", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(OnUIntConvertChanged));

        public bool UIntConvert
        {
            get
            {
                return (bool)this.GetValue(UIntConvertProperty);

            }
            set
            {
                this.SetValue(UIntConvertProperty, value);

            }
        }


        public static readonly DependencyProperty UShortConvertProperty =
          DependencyProperty.Register("UShortConvert", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(OnUShortConvertChanged));

        public bool UShortConvert
        {
            get
            {
                return (bool)this.GetValue(UShortConvertProperty);

            }
            set
            {
                this.SetValue(UShortConvertProperty, value);

            }
        }




        public static readonly DependencyProperty FloatConvertProperty =
          DependencyProperty.Register("FloatConvert", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(OnFloatConvertChanged));

        public bool FloatConvert
        {
            get
            {
                return (bool)this.GetValue(FloatConvertProperty);

            }
            set
            {
                this.SetValue(FloatConvertProperty, value);

            }
        }



        public static readonly DependencyProperty ByteConvertProperty =
          DependencyProperty.Register("ByteConvert", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(OnByteConvertChanged));

        public bool ByteConvert
        {
            get
            {
                return (bool)this.GetValue(ByteConvertProperty);

            }
            set
            {
                this.SetValue(ByteConvertProperty, value);

            }
        }





        public static readonly DependencyProperty StringConvertProperty =
          DependencyProperty.Register("StringConvert", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(OnStringConvertChanged));

        public bool StringConvert
        {
            get
            {
                return (bool)this.GetValue(StringConvertProperty);

            }
            set
            {
                this.SetValue(StringConvertProperty, value);

            }
        }



      

        private void OnBitConvert(object sender, RoutedEventArgs e)
        {
            BitConverterResult = string.Empty;
            if (IntConvert)
            {
                byte[] bytes = ConvertToByteArray(BitConverterValue);
                if (bytes.Length == 4)
                {
                    BitConverterResult = BitConverter.ToInt32(bytes, 0).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    MessageBox.Show("Error: Integer type MUST be 4 bytes long!!", "BitConverter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                //ConvertToByteArray
            }
            else if (ByteConvert)
            {
                StringBuilder sb = new StringBuilder();
                byte[] bytes = ConvertToByteArray(BitConverterValue);
                foreach (byte b in bytes)
                {
                    sb.Append(b.ToString(CultureInfo.InvariantCulture));
                    sb.Append(", ");
                }
                BitConverterResult = sb.ToString();
            }
            else if (ShortConvert)
            {
                byte[] bytes = ConvertToByteArray(BitConverterValue);
                if (bytes.Length == 2)
                {
                    BitConverterResult = BitConverter.ToInt16(bytes, 0).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    MessageBox.Show("Error: Short type MUST be 2 bytes long!!", "BitConverter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (UIntConvert)
            {
                byte[] bytes = ConvertToByteArray(BitConverterValue);
                if (bytes.Length == 4)
                {
                    BitConverterResult = BitConverter.ToUInt32(bytes, 0).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    MessageBox.Show("Error: UInt type MUST be 4 bytes long!!", "BitConverter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (FloatConvert)
            {
                byte[] bytes = ConvertToByteArray(BitConverterValue);
                if (bytes.Length == 4)
                {
                    BitConverterResult = BitConverter.ToSingle(bytes, 0).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    MessageBox.Show("Error: Float type MUST be 4 bytes long!!", "BitConverter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (UShortConvert)
            {
                byte[] bytes = ConvertToByteArray(BitConverterValue);
                if (bytes.Length == 2)
                {
                    BitConverterResult = BitConverter.ToUInt16(bytes, 0).ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    MessageBox.Show("Error: UShort type MUST be 2 bytes long!!", "BitConverter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else if (StringConvert)
            {
                byte[] bytes = ConvertToByteArray(BitConverterValue);
                if (bytes.Length < 4)
                {
                    MessageBox.Show("Error: string types must include the length as an int type as the first 4 bytes!!",
                      "BitConverter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    int by = BitConverter.ToInt32(bytes, 0);
                    if (bytes.Length != by * 2 + 4)
                    {
                        MessageBox.Show(
                            string.Format(CultureInfo.InvariantCulture, "Error: String length is defined as {0} characters, but length definition sets it as {1} characters",
                            (bytes.Length - 4) / 2, by), "BitConverter Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        using (MemoryStream ms = new MemoryStream(bytes))
                        {
                            using (ArtemisString s = new ArtemisString(ms.GetMemoryStream(0), 0))
                            {
                                BitConverterResult = s.Value;
                            }
                        }
                    }

                }
            }
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            Result.Clear();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void OnPacketTester(object sender, RoutedEventArgs e)
        {
            PackageTester win = new PackageTester();
            win.Show();
        }
    }
}
