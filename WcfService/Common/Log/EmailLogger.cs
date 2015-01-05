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
        public override void Write(string message, object arguments)
        {
            Log4netBuilder._email = GetArgumentsValue(arguments, "Email").ToString();
            Log = Log4netBuilder.GetLog(LogMediaEnum.EMAIL);
            Log.Info(message);
        }
    }
}
