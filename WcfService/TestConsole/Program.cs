using System;
using CommonHelper;
using TestConsole.ServiceReference1;
using System.ServiceModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;




namespace TestConsole
{

    class Program
    {

        static bool done;
        static object locker = new object(); // ！！

        static void Main(string[] args)
        {
            //wcf
            //IService1 svic = new Service1Client();


            //remoting

       
            Console.ReadLine();
        }

        static void hello(string name)
        {
            Console.WriteLine("hello" + name);
        }


    }


    public class Hello : MarshalByRefObject
    {

    }



}
