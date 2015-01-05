using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class LoggerFactory
    {
        public static BaseLogger GetLogger(LogMediaEnum media)
        {
            switch (media)
            {
                case LogMediaEnum.FILE:
                    return new FileLogger();

                case LogMediaEnum.EMAIL:
                    return new EmailLogger();

                case LogMediaEnum.DB:
                    return new DBLogger();

                default: return null;
            }
        }

    }
}
