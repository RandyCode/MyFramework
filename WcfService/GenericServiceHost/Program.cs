using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Configuration;
using CommonHelper;
using System.Threading;
using System.Collections.Concurrent;

///for cycle job backgroun thread auto;
namespace GenericServiceHost
{
    class Program
    {
        static ManualResetEvent _signal = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            var path = ConfigurationManager.AppSettings["dllPath"];
            ThreadWatcher wathcher = new ThreadWatcher();
            wathcher.WatchDirectory(path, "*.dll");

            Console.ReadKey();

        }


    }

}
