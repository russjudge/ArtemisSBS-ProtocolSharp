#if LOG4NET
using log4net;
#endif
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    public class DamComStatus : IPackage
    {

        #region RawData
        MemoryStream _rawData = null;

        public MemoryStream GetRawData()
        {
            if (_rawData == null)
            {
                _rawData = this.SetRawData();
            }
            return _rawData;
        }

        #endregion

        const int PacketLength = 33;
        public static ReadOnlyCollection<DamComStatus> GetDamComTeams(Stream stream, int index)
        {
            List<DamComStatus> retVal = new List<DamComStatus>();
            if (stream != null)
            {
                int position = index;
                stream.Position = position;
                while (position < stream.Length && Convert.ToByte(stream.ReadByte()) != 0xfe)
                {
                    DamComStatus d = new DamComStatus(stream, position);
                    retVal.Add(d);
                    position += PacketLength;
                    stream.Position = position;
                }
            }
            return new ReadOnlyCollection<DamComStatus>(retVal);
        }
        public DamComStatus()
        {

        }
        void Initialize(Stream stream, int index)
        {

            
            if (stream != null && index < stream.Length)
            {
                if (stream.CanSeek)
                {
                    stream.Position = index;
                }
                _rawData = stream.GetMemoryStream(index);
                if (Convert.ToByte(_rawData.ReadByte()) < 0x0a)
                {

                }
                try
                {
                    _rawData.Position = index;

                    TeamNumber = Convert.ToByte(_rawData.ReadByte() - 0x0a);

                 
                    if (_rawData.Position < _rawData.Length - 3)
                    {
                        
                        GoalX = _rawData.ToInt32();
                    }
                    if (_rawData.Position < _rawData.Length - 3)
                    {

                        CurrentX = _rawData.ToInt32();
                    }
                    if (_rawData.Position < _rawData.Length - 3)
                    {

                        GoalY = _rawData.ToInt32();
                    }
                    if (_rawData.Position < _rawData.Length - 3)
                    {
                        CurrentY = _rawData.ToInt32();
                    }
                    if (_rawData.Position < _rawData.Length - 3)
                    {
                        GoalZ = _rawData.ToInt32();
                    }
                    if (_rawData.Position < _rawData.Length - 3)
                    {
                        CurrentZ = _rawData.ToInt32();
                    }
                    if (_rawData.Position < _rawData.Length - 3)
                    {
                        Progress = _rawData.ToSingle();
                    }
                    if (_rawData.Position < _rawData.Length - 3)
                    {

                        NumberOfTeamMembers = _rawData.ToInt32();
                    }
                    _rawData.Position = 0;
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }


            }
        }
        //public DamComStatus(byte[] byteArray)
        //{
        //    Initialize(byteArray, 0);
        //}
        public DamComStatus(Stream stream, int index)
        {
            Initialize(stream, index);
        }
        //DAMCON team status (array)

        //This contains a list of DAMCON teams, terminated with 0xfe. Each DAMCON team is formatted as follows:

        //Team number (byte, this value minus 0x0a)
        public byte TeamNumber { get; set; }

        public int GoalX { get; set; }
        //Goal X coordinate (int)
        public int CurrentX { get; set; }
        //Current X coordinate (int)
        public int GoalY { get; set; }
        //Goal Y coordinate (int)

        public int CurrentY { get; set; }
        //Current Y coordinate (int)

        public int GoalZ { get; set; }
        //Goal Z coordinate (int)
        public int CurrentZ { get; set; }
        //Current Z coordinate (int)
        public float Progress { get; set; }
        //Progress (float)
        public int NumberOfTeamMembers { get; set; }
        //Number of team members (int)
        //public byte[] GetBytes()
        //{
        //    List<byte> retVal = new List<byte>();
        //    retVal.Add(Convert.ToByte(TeamNumber + 0x0a));

        //    retVal.AddRange(BitConverter.GetBytes(GoalX));

        //    retVal.AddRange(BitConverter.GetBytes(CurrentX));

        //    retVal.AddRange(BitConverter.GetBytes(GoalY));

        //    retVal.AddRange(BitConverter.GetBytes(CurrentY));

        //    retVal.AddRange(BitConverter.GetBytes(GoalZ));

        //    retVal.AddRange(BitConverter.GetBytes(CurrentZ));

        //    retVal.AddRange(BitConverter.GetBytes(Progress));
        //    retVal.AddRange(BitConverter.GetBytes(NumberOfTeamMembers));
            
        //    return retVal.ToArray();
        //}

        public OriginType GetValidOrigin()
        {
            return OriginType.Server;
        }
        List<Exception> errors = new List<Exception>();
        public System.Collections.ObjectModel.ReadOnlyCollection<Exception> GetErrors()
        {
            return new System.Collections.ObjectModel.ReadOnlyCollection<Exception>(errors);
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
    }
}
