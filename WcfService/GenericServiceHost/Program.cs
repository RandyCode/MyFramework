using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Configuration;
using CommonHelper;

namespace GenericServiceHost
{
    class Program
    {

        internal delegate void Maindelegate();

        internal static event Maindelegate MainHandleEvent;

        //订阅，监视
        static void Main(string[] args)
        {

            var files = HostTool.FindFiles(ConfigurationManager.AppSettings["dllPath"], "*.dll");

            HostTool.InvokeStatrupMethod(files);


            Console.ReadKey();
        }




    }
}
