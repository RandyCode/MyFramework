using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CommonHelper
{
    public class Log4netBuilder
    {
        private static StringBuilder _xml = new StringBuilder();
        public static string _email = string.Empty;
        public static string _connectionStr = string.Empty;

        public static ILog GetLog(LogMediaEnum type)
        {
            XmlDocument xmldoc = new XmlDocument();
            switch (type)
            {
                case LogMediaEnum.FILE:
                    _xml.Append("<?xml version='1.0' encoding='utf-8' ?> <log4net>  <logger name='Logger'> <appender-ref ref='FileAppender'/>  <level value='info'/> </logger>");
                    _xml.Append("<appender name='FileAppender' type='log4net.Appender.RollingFileAppender'> <file value='logs/log' /> <appendToFile value='true' /> <rollingStyle value='Date' /> <datePattern value='_yyyy-MM-dd&quot;.log&quot;' /> <layout type='log4net.Layout.PatternLayout'>   <conversionPattern value='%newline记录时间：%date   %newline 日志级别：%-5level %newline 出错类：%logger property:[%property{NDC}]  错误描述：%message%newline ' /> </layout> </appender>");
                    _xml.Append("</log4net>");
                    break;
                case LogMediaEnum.EMAIL:
                    _xml.Append("<?xml version='1.0' encoding='utf-8' ?> <log4net> <logger name='Logger'> <appender-ref ref='SmtpAppender'/>  <level value='info'/> </logger>");
                    _xml.Append("<appender name='SmtpAppender' type='log4net.Appender.SmtpAppender'> <authentication value='Basic' /> <smtpHost value='smtp.163.com' /> <from value='lr6522626@163.com' /> <to value='361703739@qq.com' />  <username value='lr6522626@163.com' /><password value='like6522626' /> <bufferSize value='512' /> <subject value='Trace Logging' /><lossy value='true' /><evaluator type='log4net.Core.LevelEvaluator'><threshold value='ALL'/> </evaluator><layout type='log4net.Layout.PatternLayout'>   <conversionPattern value='%newline记录时间：%date   %newline 日志级别：%-5level %newline 出错类：%logger property:[%property{NDC}]  错误描述：%message%newline ' /> </layout> </appender>");
                    _xml.Append("</log4net>");
                    if (_email != string.Empty) { _xml.Replace("to value='361703739@qq.com' ", string.Format("to value='{0}'", _email)); }
                    break;
                case LogMediaEnum.DB:
                    //  <!-- DB-->  http://www.poluoluo.com/jzxy/201311/249970.html
                    break;
                default: break;
            }
            xmldoc.LoadXml(_xml.ToString());
            XmlConfigurator.Configure(xmldoc.DocumentElement);
            return LogManager.GetLogger("Logger");
        }




    }
}
