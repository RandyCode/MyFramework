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

    //class Program
    //{
    //    //若要将初始状态设置为终止，则为 true；若要将初始状态设置为非终止，则为 false。
    //    static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

    //    static void Main(string[] args)
    //    {
    //        Thread t1 = new Thread(() =>
    //        {
    //            while (true)
    //            {
    //                Console.WriteLine("客户1在排队等待付钱...");

    //                //客户1调用manualResetEvent上的WaitOne来等待付钱通知
    //                manualResetEvent.WaitOne();
    //                Console.WriteLine("已经有人请客，客户1不用付钱");
    //            }
    //        });
    //        t1.IsBackground = true;
    //        t1.Start();

    //        Pay();//发送通知

    //        Console.ReadKey();
    //    }

    //    static void Pay()
    //    {
    //        string tip = Console.ReadLine();
    //        if (tip == "next")
    //        {
    //            manualResetEvent.Set();//收银员发送付钱通知，通过调用Set来通知客户付钱

    //            //Timer timer = new Timer(StopPay, null, 0, 5000);//5秒钟后收银员下班了，线程要重新等待了
    //        }
    //    }

    //    static void StopPay(object s)
    //    {
    //        manualResetEvent.Reset();
    //        Console.WriteLine("收银员下班, 后面的客户要继续等待");
    //    }
    //}
}
