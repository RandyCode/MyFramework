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

        public static ILog GetLog(LogMediaEnum type)
        {
            XmlDocument xmldoc = new XmlDocument();
            string xml = string.Empty;
            switch (type)
            {
                case LogMediaEnum.FILE:
                    xml = "<?xml version='1.0' encoding='utf-8' ?> <log4net>  <logger name='Logger'> <appender-ref ref='FileAppender'/>  <level value='info'/> </logger>";
                    xml += "<appender name='FileAppender' type='log4net.Appender.RollingFileAppender'> <file value='logs/log' /> <appendToFile value='true' /> <rollingStyle value='Date' /> <datePattern value='_yyyy-MM-dd&quot;.log&quot;' /> <layout type='log4net.Layout.PatternLayout'>   <conversionPattern value='%newline记录时间：%date  %newline 线程ID:[%thread] %newline 日志级别：%-5level %newline 出错类：%logger property:[%property{NDC}]  错误描述：%message%newline ' /> </layout> </appender>";
                    xml += "</log4net>";
                    break;
                case LogMediaEnum.EMAIL:
                    xml = "<?xml version='1.0' encoding='utf-8' ?> <log4net>  <logger name='Logger'> <appender-ref ref='123'/>  <level value='info'/> </logger>";
                    xml += "<appender name='123' type='log4net.Appender.RollingFileAppender'> <file value='logs/log' /> <appendToFile value='true' /> <rollingStyle value='Date' /> <datePattern value='_yyyy-MM-dd&quot;.log&quot;' /> <layout type='log4net.Layout.PatternLayout'>   <conversionPattern value='%newline记录时间：%date  %newline 线程ID:[%thread] %newline 日志级别：%-5level %newline 出错类：%logger property:[%property{NDC}]  错误描述：%message%newline ' /> </layout> </appender>";
                    xml += "</log4net>";
                    break;
                case LogMediaEnum.DB:
                    xml = "<?xml version='1.0' encoding='utf-8' ?> <log4net>  <logger name='Logger'> <appender-ref ref='123'/>  <level value='info'/> </logger>";
                    xml += "<appender name='123' type='log4net.Appender.RollingFileAppender'> <file value='logs/log' /> <appendToFile value='true' /> <rollingStyle value='Date' /> <datePattern value='_yyyy-MM-dd&quot;.log&quot;' /> <layout type='log4net.Layout.PatternLayout'>   <conversionPattern value='%newline记录时间：%date  %newline 线程ID:[%thread] %newline 日志级别：%-5level %newline 出错类：%logger property:[%property{NDC}]  错误描述：%message%newline ' /> </layout> </appender>";
                    xml += "</log4net>";
                    break;
                default: break;
            }
            xmldoc.LoadXml(xml);
            XmlConfigurator.Configure(xmldoc.DocumentElement);
            return LogManager.GetLogger("Logger");
        }

        //    <log4net>
        //  <!-- 定义日志的输出媒介 -->
        //  <logger name="Logger">
        //    <appender-ref ref="FILE"/>
        //     <appender-ref ref="DB"/>
        //    <appender-ref ref="EMAIL"/>
        //    <level value="ALL"/>
        //  </logger>

        //  <!-- 定义输出到日志文件 -->
        //  <appender name="FILE" type="log4net.Appender.RollingFileAppender">
        //    <!--日志文件名开头-->
        //    <file value="logs/log" />
        //    <appendToFile value="true" />
        //    <rollingStyle value="Date" />
        //    <datePattern value="_yyyy-MM-dd&quot;.log&quot;" />
        //    <layout type="log4net.Layout.PatternLayout">
        //      <conversionPattern value="%newline记录时间：%date  %newline 线程ID:[%thread] %newline 日志级别：%-5level %newline 出错类：%logger property:[%property{NDC}]  错误描述：%message%newline " />
        //    </layout>
        //  </appender>


        //  <!-- 发送邮件-->
        //  <!--<appender name="EMAIL" type="log4net.Appender.SmtpAppender">
        //    <authentication value="Basic" />
        //    <to value="fengsheng@cnpcag.com" />
        //    <from value="fengsheng@cnpcag.com" />
        //    <username value="Feng Sheng" />
        //    <password value="******" />
        //    <subject value="test logging message" />
        //    <smtpHost value="mail.cnpcag.com" />
        //    <bufferSize value="512" />
        //    <lossy value="true" />
        //    <evaluator type="log4net.Core.LevelEvaluator">
        //      <threshold value="ALL"/>
        //    </evaluator>
        //    <layout type="log4net.Layout.PatternLayout">
        //      <conversionPattern value="%newline%date [%thread] %-5level %logger [%property{NDC}] - %message%newline%newline%newline" />
        //    </layout>
        //  </appender>-->

        //</log4net>
    }
}
