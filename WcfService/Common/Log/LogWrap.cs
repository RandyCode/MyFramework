using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class LogWrap: ILogWrap
    {
        private ManualResetEvent _signal;  //  异步？
        private Queue<Action> _queue;

        public ILog ErrorLog { get; set; }

        public  void Write(string message, LogMediaEnum[] mediaArray=null)
        {
            if (mediaArray == null) 
                mediaArray = new LogMediaEnum[] { LogMediaEnum.FILE };

            try
            {
                foreach (var type in mediaArray)
                {
                    var logger = LoggerFactory.GetLogger(type);
                    logger.Write(message);             
                }

            }
            catch (Exception ex)
            {
                if (ErrorLog != null)
                {
                    this.ErrorLog.Error(string.Format("{0} inserts access log failed.", this.GetType().Name), ex);
                }
            }
        }

    }
}
