using log4net;
using System;
using System.Linq;
using System.Threading;

namespace CommonHelper
{
    public class LogWrap : ILogWrap
    {
        public ILog ErrorLog { get; set; }

        public void Write(string message)
        {
            Write(message, null);
        }

        public void Write(string message, LogMediaEnum[] mediaArray)
        {
            Write(message, mediaArray, null);
        }

        public void Write(string message, LogMediaEnum[] mediaArray, object obj)
        {
            ThreadPool.QueueUserWorkItem((a) => { DoWrite(message, mediaArray, obj); });
        }

        private void DoWrite(string message, LogMediaEnum[] mediaArray, object obj)
        {
            if (mediaArray == null || mediaArray.Count() == 0)
                mediaArray = new LogMediaEnum[] { LogMediaEnum.FILE };
            try
            {
                foreach (var type in mediaArray)
                {
                    var logger = LoggerFactory.GetLogger(type);
                    if (logger == null) continue;
                    ErrorLog = logger.Log;
                    logger.Write(message, obj);
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
//调用
//ILogWrap log = new LogWrap();
//log.Write("Randy Log Email", new LogMediaEnum[] { LogMediaEnum.EMAIL }, new { Email = "361703739@qq.com" });
//log.Write("Randy Log DB", new LogMediaEnum[] { LogMediaEnum.DB }, new { ConnectionStr = "..." });
//log.Write("file");
