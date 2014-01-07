using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public interface IPackage : IDisposable
    {
        OriginType GetValidOrigin();

        
        MemoryStream GetRawData();

        /// <summary>
        /// Gets the errors.  These would be Exceptions as a result of processing the incoming byte array.
        /// </summary>
        /// <returns></returns>
        ReadOnlyCollection<Exception> GetErrors();
    }
}
