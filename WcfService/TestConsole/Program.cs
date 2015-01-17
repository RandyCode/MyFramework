using System;
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
