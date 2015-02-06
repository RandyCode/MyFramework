using System;
using CommonHelper;
using TestConsole.ServiceReference1;
using System.ServiceModel;


namespace TestConsole
{

    class Program
    {

        static bool done;
        static object locker = new object(); // ！！

        static void Main(string[] args)
        {
            IService1 svic = new Service1Client();

            try
            {
                svic.ThrowFault();
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Message + "  code :" + ex.Code + "Reason:" + ex.Reason);
            }
            //var ss = svic.GetData();

            //ILogWrap log = new LogWrap();
            //log.Write("Randy Log Email", new LogMediaEnum[] { LogMediaEnum.EMAIL }, new { Email = "361703739@qq.com" });
            //log.Write("file");


   



            Console.ReadLine();
        }


    }



}
