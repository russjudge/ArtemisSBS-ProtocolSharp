using ArtemisComm;
using BigRedButtonOfDeath.Library;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BigRedButtonOfDeath.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IView
    {
        public MainWindow()
        {

            SetAbortSelfDestruct();
            SetSelfDestructImage();
            Host = Properties.Settings.Default.LastHost;
            Port = Properties.Settings.Default.LastPort;
            if (Port == 0)
            {
                Port = 2010;
            }

            InitializeComponent();
            new Controller(this);
        }
        void SetSelfDestructImage()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.SelfDestructPath))
            {
                SelfDestructImage = Properties.Resources.selfdestruct.ToBitmapSource();
            }
            else
            {
                ImageSource img = GetImage(Properties.Settings.Default.SelfDestructPath);
                if (img != null)
                {
                    SelfDestructImage = img;
                }
            }
        }
        void SetAbortSelfDestruct()
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.AbortSelfDestructPath))
            {

                AbortSelfDestructImage = Properties.Resources.cancelselfdestruct.ToBitmapSource();
            }
            else
            {
                ImageSource img = GetImage(Properties.Settings.Default.AbortSelfDestructPath);
                if (img != null)
                {
                    AbortSelfDestructImage = img;
                }
            }
        }

        static ImageSource GetImage(string path)
        {
            BitmapImage img = null;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int byRead = -1;
                        byte[] bytes = new byte[32768];
                        do
                        {
                            byRead = fs.Read(bytes, 0, bytes.Length);
                            if (byRead > 0)
                            {
                                ms.Write(bytes, 0, byRead);
                            }
                        } while (byRead > 0);
                        ms.Seek(0, SeekOrigin.Begin);
                        img = new BitmapImage();
                        img.BeginInit();
                        img.StreamSource = ms;
                        img.CacheOption = BitmapCacheOption.OnLoad;
                        img.EndInit();
                        img.Freeze();

                    }
                }
            }
            catch { }
            return img;
        }

        public static readonly DependencyProperty SelfDestructImageProperty =
           DependencyProperty.Register("SelfDestructImage", typeof(ImageSource),
               typeof(MainWindow), new PropertyMetadata());

        public ImageSource SelfDestructImage
        {
            get
            {
                return (ImageSource)this.UIThreadGetValue(SelfDestructImageProperty);

            }
            set
            {
                this.UIThreadSetValue(SelfDestructImageProperty, value);

            }
        }


        public static readonly DependencyProperty AbortSelfDestructImageProperty =
           DependencyProperty.Register("AbortSelfDestructImage", typeof(ImageSource),
               typeof(MainWindow), new PropertyMetadata());

        public ImageSource AbortSelfDestructImage
        {
            get
            {
                return (ImageSource)this.UIThreadGetValue(AbortSelfDestructImageProperty);

            }
            set
            {
                this.UIThreadSetValue(AbortSelfDestructImageProperty, value);

            }
        }

        public static readonly DependencyProperty HostProperty =
           DependencyProperty.Register("Host", typeof(string),
               typeof(MainWindow), new PropertyMetadata());

        public string Host
        {
            get
            {
                return (string)this.UIThreadGetValue(HostProperty);

            }
            set
            {
                this.UIThreadSetValue(HostProperty, value);

            }
        }

        public static readonly DependencyProperty PortProperty =
          DependencyProperty.Register("Port", typeof(int),
              typeof(MainWindow), new PropertyMetadata(2010));

        public int Port
        {
            get
            {
                return (int)this.UIThreadGetValue(PortProperty);

            }
            set
            {
                this.UIThreadSetValue(PortProperty, value);

            }
        }
        System.Media.SoundPlayer plyr = new System.Media.SoundPlayer(Properties.Resources.alert);
        public void AlertAboutArtemisVersionConflict(string message)
        {
            if (this.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
            {
                this.Dispatcher.Invoke(new Action<string>(AlertAboutArtemisVersionConflict), message);
            }
            else
            {
                MessageBox.Show(message);
            }
        }
        PlayerShip[] ships = null;
        void UserShipSelect()
        {

            ShipSelector win = new ShipSelector(ships);
            if (win.ShowDialog() == true)
            {
                SelectedShip = win.SelectedShip;
                if (ShipSelected != null)
                {
                    ShipSelected(this, EventArgs.Empty);
                }
            }

        }

        
        public void GetShipSelection(PlayerShip[] shipList)
        {

            ships = shipList;

            this.Dispatcher.BeginInvoke(new Action(UserShipSelect));
         

        }

        public event EventHandler ConnectRequested;



        public static readonly DependencyProperty RedAlertEnabledProperty =
          DependencyProperty.Register("RedAlertEnabled", typeof(bool),
              typeof(MainWindow));

        public bool RedAlertEnabled
        {
            get
            {
                return (bool)this.UIThreadGetValue(RedAlertEnabledProperty);

            }
            set
            {
                this.UIThreadSetValue(RedAlertEnabledProperty, value);

            }
        }

        public static readonly DependencyProperty ShieldsRaisedProperty =
          DependencyProperty.Register("ShieldsRaised", typeof(bool),
              typeof(MainWindow));

        public bool ShieldsRaised
        {
            get
            {
                return (bool)this.UIThreadGetValue(ShieldsRaisedProperty);

            }
            set
            {
                this.UIThreadSetValue(ShieldsRaisedProperty, value);

            }
        }



        public static readonly DependencyProperty ConnectionStartedProperty =
          DependencyProperty.Register("ConnectionStarted", typeof(bool),
              typeof(MainWindow));

        public bool ConnectionStarted
        {
            get
            {
                return (bool)this.UIThreadGetValue(ConnectionStartedProperty);

            }
            set
            {
                this.UIThreadSetValue(ConnectionStartedProperty, value);

            }
        }


        public static readonly DependencyProperty GameRunningProperty =
          DependencyProperty.Register("GameRunning", typeof(bool),
              typeof(MainWindow));

        public bool GameRunning
        {
            get
            {
                return (bool)this.UIThreadGetValue(GameRunningProperty);

            }
            set
            {
                this.UIThreadSetValue(GameRunningProperty, value);

            }
        }


        public static readonly DependencyProperty SimulationRunningProperty =
          DependencyProperty.Register("SimulationRunning", typeof(bool),
              typeof(MainWindow));

        public bool SimulationRunning
        {
            get
            {
                return (bool)this.UIThreadGetValue(SimulationRunningProperty);

            }
            set
            {
                this.UIThreadSetValue(SimulationRunningProperty, value);

            }
        }


        public static readonly DependencyProperty SelfDestructRunningProperty =
          DependencyProperty.Register("SelfDestructRunning", typeof(bool),
              typeof(MainWindow));

        public bool SelfDestructRunning
        {
            get
            {
                return (bool)this.UIThreadGetValue(SelfDestructRunningProperty);

            }
            set
            {
                this.UIThreadSetValue(SelfDestructRunningProperty, value);

            }
        }
        public event EventHandler StartSelfDestruct;

        public event EventHandler CancelSelfDestruct;

        public event EventHandler DisconnectRequested;


        public void ConnectionFailed()
        {
            if (this.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
            {
                this.Dispatcher.Invoke(new Action(ConnectionFailed));
            }
            else
            {
                GameRunning = false;
                SimulationRunning = false;
                SelfDestructRunning = false;
                plyr.Stop();
                MessageBox.Show("Connection to server lost.", "Big Red Button of Death!", MessageBoxButton.OK, MessageBoxImage.Hand);

                ConnectionStarted = false;
             
            }
        }

        public void GameStarted()
        {
            GameRunning = true;
            SimulationRunning = true;
        }

        public void GameEnded()
        {
            GameRunning = false;
            SimulationRunning = false;
            SelfDestructRunning = false;
            plyr.Stop();
        }
        public void SimulationEnded()
        {
            SimulationRunning = false;
            SelfDestructRunning = false;
            plyr.Stop();
        }
        private void OnConnect(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.LastPort = Port;
            Properties.Settings.Default.LastHost = Host;
            Properties.Settings.Default.Save();
            if (ConnectRequested != null)
            {
                
                ConnectionStarted = true;
                ConnectRequested(this, EventArgs.Empty);

            }
        }

        private void OnSelfDestruct(object sender, RoutedEventArgs e)
        {
            if (StartSelfDestruct != null)
            {
                //plyr.PlayLooping();
                SelfDestructRunning = true;
                StartSelfDestruct(this, EventArgs.Empty);
               
            }
        }

        private void OnResetSelfDestruct(object sender, RoutedEventArgs e)
        {
            if (CancelSelfDestruct != null)
            {
                plyr.Stop();
                CancelSelfDestruct(this, EventArgs.Empty);
                SelfDestructRunning = false;
            }

        }

        private void OnClosed(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            Dispose();
        }



        bool isDisposed = false;
   
        protected virtual void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    if (DisposeRequested != null)
                    {
                        DisposeRequested(this, EventArgs.Empty);
                    }
                    if (plyr != null)
                    {
                        plyr.Stop();
                        plyr.Dispose();
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


        public event EventHandler DisposeRequested;

        public event EventHandler ShipSelected;

        public int SelectedShip { get; private set; }

        public void UnableToConnect()
        {
            if (this.Dispatcher != System.Windows.Threading.Dispatcher.CurrentDispatcher)
            {
                this.Dispatcher.Invoke(new Action(UnableToConnect));
            }
            else
            {
                MessageBox.Show("Unable to connect to server", "The Big Red Button of Death!", MessageBoxButton.OK, MessageBoxImage.Hand);
                ConnectionStarted = false;
                GameRunning = false;
                SimulationRunning = false;
                SelfDestructRunning = false;
            }
        }

        

        private void OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            //Properties.Settings.Default.Save();
            this.Close();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SimulationRunning)
            {
                FrameworkElement img = sender as FrameworkElement;
                if (img != null)
                {
                    Properties.Settings.Default.Size = img.ActualWidth;
                }
                Properties.Settings.Default.Save();
            }
        }

        private void OnChangeSelfDestruct(object sender, RoutedEventArgs e)
        {
            KeyValuePair<ImageSource, string> item = SelectImage("Select self-destruct image", Properties.Settings.Default.SelfDestructPath);
            if (item.Key != null)
            {
                Properties.Settings.Default.SelfDestructPath = item.Value;
                SelfDestructImage = item.Key;
                Properties.Settings.Default.Save();
            }
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        static KeyValuePair<ImageSource, string> SelectImage(string title, string baseFile)
        {
            string target = null;
            ImageSource img = null;
            string file = baseFile;
            OpenFileDialog diag = new OpenFileDialog();
            diag.Title = title;
            diag.FileName = file;
            diag.CheckFileExists = true;
            diag.Filter = "Image Files|*.jpg;*.png;*.gif;*.bmp;*.tif|All files|*.*";
            if (diag.ShowDialog() == true)
            {
                img = GetImage(diag.FileName);
                file = diag.FileName;
                target = System.IO.Path.Combine(App.AppDataPath, new FileInfo(file).Name);
                try
                {
                    File.Copy(file, target, true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error copying to data directory:\r\n\r\n" + ex.Message, "Big Red Button of Death", MessageBoxButton.OK, MessageBoxImage.Error);
                    target = null;
                }
            }
            return new KeyValuePair<ImageSource, string>(img, target);
        }
        private void OnChangeAbortSelfDestruct(object sender, RoutedEventArgs e)
        {
            KeyValuePair<ImageSource, string> item = SelectImage("Select cancel self-destruct image", Properties.Settings.Default.AbortSelfDestructPath);
            if (item.Key != null)
            {
                Properties.Settings.Default.AbortSelfDestructPath = item.Value;
                AbortSelfDestructImage = item.Key;
                Properties.Settings.Default.Save();
            }
        }

        private void OnReset(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.AbortSelfDestructPath = string.Empty;

            Properties.Settings.Default.SelfDestructPath = string.Empty;
            
            Properties.Settings.Default.Save();
            SetSelfDestructImage();
            SetAbortSelfDestruct();
        }

    }
}
