using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class EmailLogger : BaseLogger
    {
        public EmailLogger()
        {
            Log = Log4netBuilder.GetLog(LogMediaEnum.EMAIL);
        }
        public override void Write(string message)
        {
            Log.Info(message);
        }
    }
}
