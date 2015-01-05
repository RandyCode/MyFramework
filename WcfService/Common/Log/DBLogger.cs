using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class DBLogger : BaseLogger
    {
 
        public DBLogger()
        {
            Log = Log4netBuilder.GetLog(LogMediaEnum.DB);
        }

        public  override void Write(string message)
        {
            Log.Info(message);
        }
    }
}
