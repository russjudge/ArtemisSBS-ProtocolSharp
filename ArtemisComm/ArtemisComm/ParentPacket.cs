using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ArtemisComm
{
    //This class is intended to be the base class for all packages, so that properties are loaded with Reflection with the LoadProperties method.
    //  This class is not complete.
    public class ParentPacket : IPackage
    {
        public ParentPacket()
        {

        }
        public ParentPacket(Stream stream, int index)
        {
            LoadProperties(stream, index);
        }


        [ArtemisExcluded]
        public int Length { get; private set; }


        #region Errors
        protected void AddError(Exception ex)
        {
            errors.Add(ex);
        }
        Collection<Exception> errors = new Collection<Exception>();
        public System.Collections.ObjectModel.ReadOnlyCollection<Exception> GetErrors()
        {
            return new System.Collections.ObjectModel.ReadOnlyCollection<Exception>(errors);
        }
        #endregion


        #region Load

        private void LoadProperties(Stream stream, int index)
        {

            _rawData = stream.GetMemoryStream(index);

            Length = Utility.LoadProperties(this, _rawData, 0, errors);
        
        }

        #endregion

        public virtual OriginType GetValidOrigin()
        {
            return OriginType.Indeterminate;
        }
        

        #region RawData
        MemoryStream _rawData =null;

        public MemoryStream GetRawData()
        {
            if (_rawData == null)
            {
                _rawData = this.SetRawData();
            }
            return _rawData;
        }
        
        #endregion


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
