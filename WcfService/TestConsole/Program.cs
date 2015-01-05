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
            //log.Write("Randy Log Email", new LogMediaEnum[] { LogMediaEnum.EMAIL }, new { Email = "361703739@qq.com" });
            //log.Write("file");


   



            Console.ReadLine();
        }


    }



}
