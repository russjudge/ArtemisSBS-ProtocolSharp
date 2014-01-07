using ArtemisComm;
using BigRedButtonOfDeath.Library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BigRedButtonOfDeath.Forms
{
    public partial class Form1 : Form, IView
    {
        public Form1()
        {
            InitializeComponent();
            SetForConnection();
            this.grpShip.Dock = DockStyle.Fill;
            this.grpSimulationEnded.Dock = DockStyle.Fill;
            grpStandby.Dock = DockStyle.Fill;
            connectBox.Dock = DockStyle.Fill;
            btnSelfDestruct.Dock = DockStyle.Fill;
            new Controller(this);
        }
        void SetForConnection()
        {
            SetVisible(false, false, false, false, true);
            this.AcceptButton = btnConnect;
        }
        public string Host
        {
            get { return txtHost.Text; }
           
        }

        public int Port
        {
            get 
            {
                int retVal = 0;
                if (!int.TryParse(txtPort.Text, out retVal))
                {
                    retVal = 2010;
                }
                return retVal;
            }
        }

        public void AlertAboutArtemisVersionConflict(string message)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action<string>(AlertAboutArtemisVersionConflict), message);
            }
            else
            {
                MessageBox.Show(message, "Big Red Button of Death!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        public void PromptShips(Collection<PlayerShip> ships)
        {
            if (ships != null)
            {
                lstShips.Items.Clear();
                foreach (PlayerShip ship in ships)
                {
                    lstShips.Items.Add(ship.Name);
                }
            }
            this.AcceptButton = btnShip;
        }
        public void GetShipSelection(ArtemisComm.PlayerShip[] shipList)
        {
            SetVisible(false, true, false, false, false);
            Collection<PlayerShip> shps = new Collection<PlayerShip>(shipList);
            if (this.InvokeRequired)
            {

                this.BeginInvoke(new Action<Collection<PlayerShip>>(PromptShips), shps);
            }
            else
            {

                PromptShips(shps);
               
            }
           
        }
        public event EventHandler ShipSelected;
        public int SelectedShip { get; set; }
        public event EventHandler ConnectRequested;

        public bool RedAlertEnabled { get; set; }

        public bool ShieldsRaised { get; set; }

        public event EventHandler StartSelfDestruct;

        public event EventHandler CancelSelfDestruct;

        public event EventHandler DisconnectRequested;

        
        public void UnableToConnect()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(UnableToConnect));
            }
            else
            {
                this.TopMost = false;
                MessageBox.Show("Unable to connect to server.", "The Big Red Button of Death!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                SetForConnection();
                this.TopMost = true;
            }
        }
      

        public void ConnectionFailed()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(ConnectionFailed));
            }
            else
            {
                this.TopMost = false;
                MessageBox.Show("Connection to server was lost.", "The Big Red Button of Death!", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                SetForConnection();
                this.TopMost = true;
               
            }
        }

        public void GameStarted()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(GameStarted));
            }
            else
            {
                SetVisible(false, false, true, false, false);
                this.AcceptButton = btnSelfDestruct;
                btnSelfDestruct.Text = "Push for Self-Destruct";
            }
        }

        public void GameEnded()
        {
            plyr.Stop();
        }
        void SetVisible(bool standBy, bool chooseShip, bool SelfDestruct, bool simulationEnded, bool connect)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool, bool, bool, bool, bool>(SetVisible), standBy, chooseShip, SelfDestruct, simulationEnded, connect);
            }
            else
            {
                grpStandby.Visible = standBy;
                grpShip.Visible = chooseShip;
                grpSimulationEnded.Visible = simulationEnded;
                btnSelfDestruct.Visible = SelfDestruct;
                connectBox.Visible = connect;
            }
        }
        public void SimulationEnded()
        {
            plyr.Stop();
            SetVisible(false, false, false, true, false);
            
        }
        bool selfDestructStarted = false;
        private void OnSelfDestruct(object sender, EventArgs e)
        {

            if (selfDestructStarted)
            {
                plyr.Stop();
                selfDestructStarted = false;
                btnSelfDestruct.Text = "Push for Self-Destruct";
                btnSelfDestruct.BackColor = Color.Red;
                if (CancelSelfDestruct != null)
                {
                    CancelSelfDestruct(this, e);
                }
            }
            else
            {
                plyr.PlayLooping();
                if (StartSelfDestruct != null)
                {
                    StartSelfDestruct(this, e);
                }
                btnSelfDestruct.Text = "Push to abort self-destruct";
                btnSelfDestruct.BackColor = Color.LightGreen;
                selfDestructStarted = true;

            }
        }

        private void OnConnect(object sender, EventArgs e)
        {
            if (ConnectRequested != null)
            {
                SetVisible(true, false, false, false, false);
                ConnectRequested(this, e);
                
                
                
            }
        }

        private void OnSelectShip(object sender, EventArgs e)
        {
            SelectedShip = lstShips.SelectedIndex;
            if (ShipSelected != null)
            {
                SetVisible(true, false, false, false, false);
                ShipSelected(this, e);
            }
        }
  
        System.Media.SoundPlayer plyr = new System.Media.SoundPlayer(Properties.Resources.alert);
       
        public event EventHandler DisposeRequested;
        private void OnClosed(object sender, FormClosedEventArgs e)
        {
            if (DisposeRequested != null)
            {
                DisposeRequested(this, EventArgs.Empty);
            }
        }

      
    }
}
