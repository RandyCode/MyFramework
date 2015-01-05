using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class DBLogger : BaseLogger
    {

        public override void Write(string message, object arguments)
        {
            Log4netBuilder._connectionStr = GetArgumentsValue(arguments, "ConnectionStr").ToString(); //ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();
            Log = Log4netBuilder.GetLog(LogMediaEnum.DB);
            Log.Info(message);
        }
    }
}
