using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Integration.Mvc;
using TestConsole.ServiceReference1;
using System.Web.Mvc;
using Aspect;
using Microsoft.Practices.Unity;
using System.Threading;
using System.Xml;
using log4net.Config;
using log4net;
using CommonHelper;


namespace TestConsole
{

    class Program
    {

        static bool done;
        static object locker = new object(); // ！！

        static void Main(string[] args)
        {
            //IService1 svic = new Service1Client();

            //var ss = svic.GetData();

            ILogWrap log = new LogWrap();
            log.Write("Randy Log");


            //XmlDocument xmldoc = new XmlDocument();
            //string xml = "<?xml version='1.0' encoding='utf-8' ?> <log4net>  <logger name='Logger'> <appender-ref ref='FileAppender'/>  <level value='info'/> </logger>";
            //xml += "<appender name='FileAppender' type='log4net.Appender.RollingFileAppender'> <file value='../logs/log' /> <appendToFile value='true' /> <rollingStyle value='Date' /> <datePattern value='_yyyy-MM-dd&quot;.log&quot;' /> <layout type='log4net.Layout.PatternLayout'>   <conversionPattern value='%newline记录时间：%date  %newline 线程ID:[%thread] %newline 日志级别：%-5level %newline 出错类：%logger property:[%property{NDC}]  错误描述：%message%newline ' /> </layout> </appender>";
            //xml += "</log4net>";

            //xmldoc.LoadXml(xml);
            //XmlConfigurator.Configure(xmldoc.DocumentElement);
            //var log1 = LogManager.GetLogger("Logger");  //LogFileAppender
            //log1.Info("------------inininininininRandy!!!!!!!!!!!-----------");



            Console.ReadLine();
        }



        static void Go()
        {
            lock (locker) {
                if (!done)
                {
                    Thread.Sleep(1000); // 模拟耗时操作
                    Console.WriteLine("Done");
                    done = true;
                }
            }
  
        }



    }



}
