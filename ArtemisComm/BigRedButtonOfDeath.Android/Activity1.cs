using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using BigRedButtonOfDeath.Library;

namespace BigRedButtonOfDeath.Android
{
    [Activity(Label = "BigRedButtonOfDeath.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity, IView
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            
            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.MyButton);
            button.Click += HostSelect_Click;
            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            button = FindViewById<Button>(Resource.Id.btnAbout);
            button.Click += OnAbout;
           

          

            new Controller(this);
        }

        private void OnCancelSelfDestruct(object sender, EventArgs e)
        {
            if (CancelSelfDestruct != null)
            {
                CancelSelfDestruct(this, EventArgs.Empty);
            }
            SetContentView(Resource.Layout.selfdestruct);
            Button button = FindViewById<Button>(Resource.Id.btnSelfDestruct);
            button.Click += OnSelfDestruct;
        }

        private void OnSelfDestruct(object sender, EventArgs e)
        {
            if (StartSelfDestruct != null)
            {
                StartSelfDestruct(this, EventArgs.Empty);
            }
            
            SetContentView(Resource.Layout.cancelselfdestruct);
            Button button = FindViewById<Button>(Resource.Id.btnCancelSelfDestruct);
            button.Click += OnCancelSelfDestruct;
            
        }

        void OnAbout(object sender, EventArgs e)
        {
            Toast.MakeText(this, "The Big Red Button of Death! by Russ Judge.  Please consider making a Paypal donation to russjudge<at>gmail.com", ToastLength.Long);
        }
        
        void HostSelect_Click(object sender, EventArgs e)
        {

            EditText txt = FindViewById<EditText>(Resource.Id.Port);
            int port = 2010;
            if (!int.TryParse(txt.Text, out port))
            {
                port = 2010;
            }
          

            txt = FindViewById<EditText>(Resource.Id.Host);
            Host = txt.Text;
            if (string.IsNullOrEmpty(Host))
            {
                Host = "127.0.0.1";
            }
            Port = port;
            if (ConnectRequested != null)
            {
                ConnectRequested(this, EventArgs.Empty);
            }
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public void AlertAboutArtemisVersionConflict(string message)
        {
            Toast.MakeText(this, "Warning--Artemis server version is different than expected--be aware that this app's behavior may not be correct.", ToastLength.Short);
        }

        public void GetShipSelection(ArtemisComm.PlayerShip[] shipList)
        {
            
            RadioGroup grp = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            grp.RemoveAllViews();
            int id = 1000;
            foreach (ArtemisComm.PlayerShip ship in shipList)
            {
                RadioButton rad = new RadioButton(this);
                rad.Text = ship.Name.Value;
                rad.Id = id++;
                grp.AddView(rad);   
            }
            SetContentView(Resource.Layout.ShipSelect);
            Button button = FindViewById<Button>(Resource.Id.btnSelectShip);
            button.Click += Select_Click;

        }

        void Select_Click(object sender, EventArgs e)
        {
            RadioGroup grp = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            RadioButton radioButton = FindViewById<RadioButton>(grp.CheckedRadioButtonId);
            this.SelectedShip = radioButton.Id - 1000;
            
            if (ShipSelected != null)
            {
                ShipSelected(this, EventArgs.Empty);
            }
            SetContentView(Resource.Layout.standby);
        }

        public event EventHandler ConnectRequested;
        bool _redAlert;
        public bool RedAlertEnabled
        {
            get
            {
                return _redAlert;
            }
            set
            {
                _redAlert = value;
                TextView txt = FindViewById<TextView>(Resource.Id.txtRedAlert);
                if (value)
                {
                    txt.Visibility = ViewStates.Visible;
                }
                else
                {
                    txt.Visibility = ViewStates.Gone;
                    
                }
            }
        }
        bool _shieldsRaised;
        public bool ShieldsRaised
        {
            get
            {
                return _shieldsRaised;
            }
            set
            {
                _shieldsRaised = value;
                TextView txt = FindViewById<TextView>(Resource.Id.txtShields);
                if (value)
                {
                    txt.Visibility = ViewStates.Visible;
                }
                else
                {
                    txt.Visibility = ViewStates.Gone;
                }
            }
        }

        public event EventHandler StartSelfDestruct;

        public event EventHandler CancelSelfDestruct;

        public event EventHandler DisconnectRequested;

        public event EventHandler DisposeRequested;

        public event EventHandler ShipSelected;

        public int SelectedShip { get; set; }

        public void ConnectionFailed()
        {
            Toast.MakeText(this, "Connection has failed.", ToastLength.Short);
            SetContentView(Resource.Layout.Main);
        }

        public void UnableToConnect()
        {
            Toast.MakeText(this, "Unable to connect to server", ToastLength.Short);
        }

        public void GameStarted()
        {
            SetContentView(Resource.Layout.selfdestruct); 
            Button button = FindViewById<Button>(Resource.Id.btnSelfDestruct);
            button.Click += OnSelfDestruct;
        }

        public void GameEnded()
        {
            SetContentView(Resource.Layout.standby);
        }

        public void SimulationEnded()
        {
            SetContentView(Resource.Layout.SimulationEnd);
        }
    }
}

