using ArtemisComm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BigRedButtonOfDeath.Library
{
    public interface IView : IDisposable
    {
        string Host { get; }
        int Port { get; }


        void AlertAboutArtemisVersionConflict(string message);

        /// <summary>
        /// Gets the ship selection.
        /// </summary>
        /// <param name="shipList">The ship list.  Return index # of array (0-based).</param>
        /// <returns></returns>
        void GetShipSelection(PlayerShip[] shipList);
        event EventHandler ConnectRequested;

        bool RedAlertEnabled { get; set; }
        bool ShieldsRaised { get; set; }

        event EventHandler StartSelfDestruct;
        event EventHandler CancelSelfDestruct;
        event EventHandler DisconnectRequested;
        event EventHandler DisposeRequested;
        event EventHandler ShipSelected;
        int SelectedShip { get; }

        
        void ConnectionFailed();
        void UnableToConnect();

        void GameStarted();
        void GameEnded();
        void SimulationEnded();

    }
}
