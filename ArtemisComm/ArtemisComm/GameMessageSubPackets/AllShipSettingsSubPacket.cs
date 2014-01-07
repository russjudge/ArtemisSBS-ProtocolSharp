using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm.GameMessageSubPackets
{
    public class AllShipSettingsSubPacket : IPackage
    {
        //**CONFIRMED

     

        public AllShipSettingsSubPacket(Stream stream, int index)
        {

            RawData = stream.GetMemoryStream(index);
            LoadData(RawData, 0);

        }
        

        void LoadData(Stream stream, int index)
        {
            if (stream != null)
            {
                try
                {
                    List<PlayerShip> ships = new List<PlayerShip>();
                    using (MemoryStream dataStream = stream.GetMemoryStream(index))
                    {
                        int position = 0;
                        do
                        {
                            PlayerShip p = new PlayerShip(dataStream, position);
                            ships.Add(p);
                            position += p.Length;

                        } while (position < dataStream.Length);
                    }
                    Ships = new ReadOnlyCollection<PlayerShip>(ships);
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }
        }
        //public byte[] GetBytes()
        //{

        //    List<byte> retVal = new List<byte>();


        //    foreach (PlayerShip p in Ships)
        //    {
        //        retVal.AddRange(p.GetBytes());

        //    }

        //    return retVal.ToArray();
        //}



        public ReadOnlyCollection<PlayerShip> Ships { get; private set; }
        //A list of the eight available player ships. Each ship is structured as follows:

        //Drive type (int)
        //Ship type (int)
        //Unknown (int): So far, the only value that has been observed here is 1. This field is new as of Artemis 2.0.
        //Name (string): Name of the ship
        //See Enumerations for drive and ship type values.

        public OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        List<Exception> errors = new List<Exception>();
        public ReadOnlyCollection<Exception> GetErrors()
        {
            return new ReadOnlyCollection<Exception>(errors);
        }
        #region Dispose

        bool _isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!_isDisposed)
                {

                    this.DisposeProperties();
                   
                    _isDisposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion

        [ArtemisExcluded]
        private MemoryStream RawData = null;

        public MemoryStream GetRawData()
        {
            return RawData;
        }
    }
}
