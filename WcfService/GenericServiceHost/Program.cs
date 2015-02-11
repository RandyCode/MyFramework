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


        static void Main(string[] args)
        {

            var files = HostTool.FindFiles(ConfigurationManager.AppSettings["dllPath"], "*.dll");

            InvokeStatrupMethod(files);


            Console.ReadKey();
        }


        static void InvokeStatrupMethod(FileInfo[] files)
        {
            // one file one thread.
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file.FullName);
                Type[] types = assembly.GetTypes(); //loaded exception  referrence dll didn't exist/
                //var intfce = types.Where(i => i.Name == "IHostStartup").FirstOrDefault();
                var intfce = typeof(IHostStartup);
                var startClass = types.Where(t => intfce.IsAssignableFrom(t) == true && t.Name != intfce.Name).ToList();

                if (startClass.Count > 1)
                    throw new Exception("Cann't instantiation 'IHostStartup' interface more than once!");

                Type cls = startClass.FirstOrDefault();
                var instance = Activator.CreateInstance(cls);
                MethodInfo method = cls.GetMethod("Main");
                method.Invoke(instance, null);

            }



        }

    }
}
