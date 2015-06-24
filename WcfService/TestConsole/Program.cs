using System;
using CommonHelper;
using TestConsole.ServiceReference1;
using TestConsole.ServiceReference3;
using System.ServiceModel;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;
using System.Security.Cryptography;




namespace TestConsole
{

    class Program
    {

        static bool done;
        static object locker = new object(); // ！！
        static StreamReader sr = new System.IO.StreamReader("1.txt");
        //static StreamWriter sw = new System.IO.StreamWriter("1.txt", true, Encoding.UTF8);
        static object streamLock = new object();

        static void Main(string[] args)
        {
            //wcf

            IUtilsService iu = new UtilsServiceClient();
            iu.DoWork("a");

            IService1 i1 = new Service1Client();
            i1.DoWork();
            //ITestSrvice


            //CommonHelper.DES des = new CommonHelper.DES();
            //string key = des.GenerateKey();
            //string s0 = "中国软件 - csdn.net";
            //string s1 = des.EncryptString(s0, key);

            //string key1 = des.GenerateKey();

            //string s2 = des.DecryptString(s1, key);

   
            //var result= System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile("12345678", "MD5");

            //var ss = result == "25d55ad283aa400af464c76d713c07ad".ToUpper();

            //ThreadPool.QueueUserWorkItem(new WaitCallback(o => { Write(); }));

            //ThreadPool.QueueUserWorkItem(new WaitCallback(o => { Read(); }));

            //IUtilsService svic = new UtilsServiceClient();
            //svic.DoWork("cipher_by_randy");
            var sss = "qerqwerwqr".GetHashCode();
            var sss1 = "qerqwerwqr".GetHashCode();
            var q1 = "1".GetHashCode();
            Console.ReadLine();
        }

        static string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }


        static void Read()
        {
            Monitor.Enter(sr);
            Thread.Sleep(1000);

            Monitor.Enter(streamLock);
            var aa = sr.ReadToEnd();

            //sr.Close();
            //Monitor.Exit(sr);
        }

        static void Write()
        {

            //StreamWriter sw = new System.IO.StreamWriter("1.txt", true, Encoding.UTF8);
            Thread.Sleep(1000);
            //Monitor.Enter(sr);
            //Monitor.Pulse(sr);
            var aa = sr.ReadToEnd();

            //sr.Close();
            //Monitor.Exit(sr);
            //
            //Monitor.Enter(sw);
            //sw.Write("randy");
            //sw.Write("111");
            //sw.Flush();
            //sw.Close();
            //Monitor.Exit(sw);
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
