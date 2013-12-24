using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class ExceptionEventArgs : ConnectionEventArgs
    {
        public ExceptionEventArgs(Exception ex, Guid connectionID) : base(connectionID)
        {
            
            CapturedException = ex;
        }
        
        public Exception CapturedException { get; private set; }

    }
}
