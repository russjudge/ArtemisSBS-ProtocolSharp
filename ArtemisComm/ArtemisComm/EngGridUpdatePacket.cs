using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class EngGridUpdatePacket : IPackage
    {


        public EngGridUpdatePacket(Stream stream, int index)
        {
            Initialize(stream, index);
        }
        public void Initialize(Stream stream, int index)
        {
            Systems = SystemNode.GetNodes(stream, index);
            
            foreach (SystemNode node in Systems)
            {
                index += node.DataLength;
            }
            index++;
            DamageControlTeams = DamComStatus.GetDamComTeams(stream, index);

        }
        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    foreach (SystemNode node in Systems)
        //    {
        //        retVal.AddRange(node.GetBytes());
        //    }
        //    retVal.Add(0xff);

        //    foreach (DamComStatus stat in DamageControlTeams)
        //    {
        //        retVal.AddRange(stat.GetBytes());
        //    }

        //    retVal.Add(0xfe);

        //    return retVal.ToArray();
        //}
        public ReadOnlyCollection<SystemNode> Systems { get; private set; }

        public ReadOnlyCollection<DamComStatus> DamageControlTeams { get; private set; }
        
        
        //System grid status (array)

        //This contains a list of system nodes, terminated with 0xff. Each system node is formatted as follows:

        //X coordinate (byte)
        //Y coordinate (byte)
        //Z coordinate (byte)
        //Damage (float)
        //DAMCON team status (array)

        //This contains a list of DAMCON teams, terminated with 0xfe. Each DAMCON team is formatted as follows:

        //Team number (byte, this value minus 0x0a)
        //Goal X coordinate (int)
        //Current X coordinate (int)
        //Goal Y coordinate (int)
        //Current Y coordinate (int)
        //Goal Z coordinate (int)
        //Current Z coordinate (int)
        //Progress (float)
        //Number of team members (int)

        public OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        List<Exception> errors = new List<Exception>();
        public ReadOnlyCollection<Exception> GetErrors()
        {
            return new ReadOnlyCollection<Exception>(errors);
        }

        MemoryStream _rawData = null;
        public MemoryStream GetRawData()
        {
            if (_rawData == null)
            {
                _rawData = this.SetRawData();
            }
            return _rawData;
        }

        #region Dispose

        bool _isDisposed = false;
        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                if (!_isDisposed)
                {
                    if (_rawData != null)
                    {
                        _rawData.Dispose();
                    }
                    //foreach (IDisposable item in Systems)
                    //{
                    //    item.Dispose();
                    //}
                    foreach (IDisposable item in DamageControlTeams)
                    {
                        item.Dispose();
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
        #endregion
    }
}
