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

        public FileLogger()
        {
            base.Log = Log4netBuilder.GetLog(LogMediaEnum.FILE);
        }
        public override void Write(string message)
        {
            Log.Info(message);
        }
    }
}
