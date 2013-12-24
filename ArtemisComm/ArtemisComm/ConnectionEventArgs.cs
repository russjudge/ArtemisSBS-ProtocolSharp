using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtemisComm
{
    public class ConnectionEventArgs : EventArgs
    {

        public ConnectionEventArgs(Guid id)
        {
            ID = id;
        }
        public Guid ID { get; private set; }
    }
}
