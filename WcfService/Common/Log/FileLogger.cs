using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class FileLogger : BaseLogger
    {
        public override void Write(string message,object arguments)
        {
            Log = Log4netBuilder.GetLog(LogMediaEnum.FILE);
            Log.Info(message);
        }
    }
}
