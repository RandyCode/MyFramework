using System;
using CommonHelper;
using TestConsole.ServiceReference1;
using System.ServiceModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;


namespace TestConsole
{

    class Program
    {

        static bool done;
        static object locker = new object(); // ！！

        static void Main(string[] args)
        {
            IService1 svic = new Service1Client();

            //try
            //{
            //    svic.ThrowFault();
            //}
            //catch (FaultException ex)
            //{
            //    Console.WriteLine(ex.Message + "  code :" + ex.Code + "Reason:" + ex.Reason);
            //}
            //var ss = svic.GetData();

            //ILogWrap log = new LogWrap();
            //log.Write("Randy Log Email", new LogMediaEnum[] { LogMediaEnum.EMAIL }, new { Email = "361703739@qq.com" });
            //log.Write("file");


            //A a = new A() { Name = "randy" };

            List<A> a = new List<A> { new A { Name = "randy" }, new A { Name = "tom" } };


            IFormatter bf = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            bf.Serialize(stream, a);
            stream.Close();


            Stream stream1 = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            var a1 = (List<A>)bf.Deserialize(stream1);


            //A a1 = a;
            Console.WriteLine(a == a1);





            Console.ReadLine();
        }


    }


    [Serializable]
    class A
    {
        public string Name { get; set; }
    }



}
