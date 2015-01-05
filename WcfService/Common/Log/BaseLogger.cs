using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public abstract class BaseLogger  
    {
        protected ILog Log { get; set; }

        public abstract void Write(string message);

    }
}
