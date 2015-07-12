using System;
using CommonHelper;
using TestConsole.ServiceReference1;
using System.ServiceModel;
using System.ComponentModel;
using TestConsole.ServiceReference2;


namespace TestConsole
{

    class Program
    {

        static void Main(string[] args)
        {



            ITestService tServ = new TestServiceClient();
            tServ.GetData();


            IUtilsService uServ = new UtilsServiceClient();
            uServ.DoWork();



            Console.ReadLine();
        }


    }



}
