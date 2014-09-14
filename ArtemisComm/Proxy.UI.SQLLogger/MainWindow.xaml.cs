using ArtemisComm.Proxy.Logger;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArtemisComm.Proxy.UI.SQLLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        public MainWindow()
        {
            ServerHost = Properties.Settings.Default.LastServerHost;
            ServerPort = Properties.Settings.Default.LastServerPort;
            ListeningPort = Properties.Settings.Default.LastClientListeningPort;
            if (string.IsNullOrEmpty(SQLConnection))
            {
                SQLConnection = ArtemisComm.Proxy.UI.SQLLogger.Properties.Settings.Default.ArtemisProxyLoggingConnectionString;
                SQLConnection = "Data Source=.\\SQLEXPRESS;Initial Catalog=ArtemisProxyLogging;Integrated Security=True";
            }
            InitializeComponent();
        }



        public static readonly DependencyProperty SQLConnectionProperty =
           DependencyProperty.Register("SQLConnection", typeof(string),
               typeof(MainWindow), new PropertyMetadata(ArtemisComm.Proxy.UI.SQLLogger.Properties.Settings.Default.LastConnectionString));

        public string SQLConnection
        {
            get
            {
                return (string)this.UIThreadGetValue(SQLConnectionProperty);

            }
            set
            {
                this.UIThreadSetValue(SQLConnectionProperty, value);

            }
        }



        public static readonly DependencyProperty ServerHostProperty =
           DependencyProperty.Register("ServerHost", typeof(string),
               typeof(MainWindow), new PropertyMetadata());

        public string ServerHost
        {
            get
            {
                return (string)this.UIThreadGetValue(ServerHostProperty);

            }
            set
            {
                this.UIThreadSetValue(ServerHostProperty, value);

            }
        }

        public static readonly DependencyProperty ServerPortProperty =
          DependencyProperty.Register("ServerPort", typeof(int),
              typeof(MainWindow), new PropertyMetadata(2010));

        public int ServerPort
        {
            get
            {
                return (int)this.UIThreadGetValue(ServerPortProperty);

            }
            set
            {
                this.UIThreadSetValue(ServerPortProperty, value);

            }
        }

        public static readonly DependencyProperty ListeningPortProperty =
          DependencyProperty.Register("ListeningPort", typeof(int),
              typeof(MainWindow), new PropertyMetadata(2010));

        public int ListeningPort
        {
            get
            {
                return (int)this.UIThreadGetValue(ListeningPortProperty);

            }
            set
            {
                this.UIThreadSetValue(ListeningPortProperty, value);

            }
        }


        public static readonly DependencyProperty LogToSQLProperty =
          DependencyProperty.Register("LogToSQL", typeof(bool),
              typeof(MainWindow), new PropertyMetadata(true));

        public bool LogToSQL
        {
            get
            {
                return (bool)this.UIThreadGetValue(LogToSQLProperty);

            }
            set
            {
                this.UIThreadSetValue(LogToSQLProperty, value);

            }
        }


        public static readonly DependencyProperty ConnectionCountProperty =
          DependencyProperty.Register("ConnectionCount", typeof(int),
              typeof(MainWindow));

        public int ConnectionCount
        {
            get
            {
                return (int)this.UIThreadGetValue(ConnectionCountProperty);

            }
            set
            {
                this.UIThreadSetValue(ConnectionCountProperty, value);

            }
        }
        public static readonly DependencyProperty ProxyIsActiveProperty =
       DependencyProperty.Register("ProxyIsActive", typeof(bool),
           typeof(MainWindow));

        public bool ProxyIsActive
        {
            get
            {
                return (bool)this.UIThreadGetValue(ProxyIsActiveProperty);

            }
            set
            {
                this.UIThreadSetValue(ProxyIsActiveProperty, value);

            }
        }


        PacketToLogging Data = null;
        ProxyLogger pLogger = null;


        private void OnStartProxy(object sender, RoutedEventArgs e)
        {
            if (LogToSQL)
            {

                Data = new PacketToLogging(SQLConnection);
            }
            StartProxy();

        }
        
        void StartProxy()
        {
            Properties.Settings.Default.LastServerHost = ServerHost;
            Properties.Settings.Default.LastServerPort = ServerPort;
            Properties.Settings.Default.LastClientListeningPort = ListeningPort;
            ArtemisComm.Proxy.UI.SQLLogger.Properties.Settings.Default.Save();
            pLogger = new ProxyLogger(Data);
            pLogger.StartProxy(ServerHost, ServerPort, ListeningPort, ArtemisComm.Proxy.Library.ProxyType.OneServerConnectionToOneClientConnection, new int[0]);
            
            ProxyIsActive = true;
        }
        void StopProxy()
        {
            ProxyIsActive = false;
            if (pLogger != null)
            {
                pLogger.StopProxy();
                UnsubscribeProxy();
                pLogger.Dispose();
            }
            
        }
        void SubscribeProxy()
        {
            pLogger.NewConnection += pLogger_NewConnection;
            pLogger.LostConnection += pLogger_LostConnection;
           
        }

        void pLogger_LostConnection(object sender, EventArgs e)
        {
            ProxyIsActive = pLogger.IsActive;
            ConnectionCount = pLogger.ConnectionCount;
            
        }

        void pLogger_NewConnection(object sender, EventArgs e)
        {
            ProxyIsActive = pLogger.IsActive;
            ConnectionCount = pLogger.ConnectionCount;
        }


        void UnsubscribeProxy()
        {
            if (pLogger != null)
            {
                pLogger.NewConnection -= pLogger_NewConnection;
                pLogger.LostConnection -= pLogger_LostConnection;
            }
        }

        bool _isDisposed=false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (!_isDisposed)
            {
                if (isDisposing)
                {
                    if (Data != null)
                    {
                        Data.Dispose();
                    }
                    if (pLogger != null)
                    {
                        StopProxy();
                        
                    }
                    _isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            
        }

        private void OnStopProxy(object sender, RoutedEventArgs e)
        {
            StopProxy();
        }

        private void OnClosed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
