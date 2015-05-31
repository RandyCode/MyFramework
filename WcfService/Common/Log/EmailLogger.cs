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
            base.Log = Log4netBuilder.GetLog(LogMediaEnum.EMAIL);
        }
        public override void Write(string message, object arguments)
        {
            Log4netBuilder._email = GetArgumentsValue(arguments, "Email").ToString();
            Log.Info(message);
        }
    }
}
