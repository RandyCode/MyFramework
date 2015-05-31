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

        public DBLogger()
        {
            base.Log = Log4netBuilder.GetLog(LogMediaEnum.DB);
        }

        public override void Write(string message, object arguments)
        {
            Log4netBuilder._connectionStr = GetArgumentsValue(arguments, "ConnectionStr").ToString(); //ConfigurationManager.ConnectionStrings["ConnectionStr"].ToString();   
            Log.Info(message);
        }
    }
}


//create table _log
//(
//    id int identity(1,1) primary key not null,
//    date datetime null,
//    thread int null,
//    level varchar(10) null,
//    logger varchar(20) null,
//    Message varchar(100) null,
//    Exception varchar(100) null
//)
