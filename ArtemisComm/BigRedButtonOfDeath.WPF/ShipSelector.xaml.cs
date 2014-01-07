using ArtemisComm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BigRedButtonOfDeath.WPF
{
    /// <summary>
    /// Interaction logic for ShipSelector.xaml
    /// </summary>
    public partial class ShipSelector : Window
    {
        public ShipSelector(PlayerShip[] ships)
        {
            if (ships != null)
            {
                Ships = new ObservableCollection<string>();
                foreach (PlayerShip ship in ships)
                {
                    Ships.Add(ship.Name.Value);
                }
            }
            InitializeComponent();
            if (lstShips.Items.Count > 0)
            {
                lstShips.SelectedIndex = 0;
            }
        }


        public static readonly DependencyProperty ShipsProperty =
          DependencyProperty.Register("Ships", typeof(ObservableCollection<string>),
              typeof(ShipSelector));

        public ObservableCollection<string> Ships
        {
            get
            {
                return (ObservableCollection<string>)this.GetValue(ShipsProperty);

            }
            set
            {
                this.SetValue(ShipsProperty, value);

            }
        }


        public static readonly DependencyProperty SelectedShipProperty =
         DependencyProperty.Register("SelectedShip", typeof(int),
             typeof(ShipSelector));

        public int SelectedShip
        {
            get
            {
                return (int)this.GetValue(SelectedShipProperty);

            }
            set
            {
                this.SetValue(SelectedShipProperty, value);

            }
        }
        private void OnSelect(object sender, RoutedEventArgs e)
        {
            SelectedShip = lstShips.SelectedIndex;
            DialogResult = true;
            this.Close();
        }

        private void lstShips_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SelectedShip = lstShips.SelectedIndex;
            DialogResult = true;
            this.Close();
        }



    }
}
