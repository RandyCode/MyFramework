using CommonHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericServiceHost
{
    public static class HostTool
    {
        static HostTool()
        {
            if (ExistFiles == null)
                ExistFiles = new Dictionary<string, DateTime>();

        }

        /// <summary>
        /// fullName,LastUpdateTime
        /// </summary>
        public static Dictionary<string, DateTime> ExistFiles { get; set; }

        public static FileInfo[] FindFiles(string path, string searchPattern = null)
        {

            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                dir.Create();
                return new FileInfo[0];
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
                return dir.GetFiles();
            else
                return dir.GetFiles(searchPattern);

        }


        public static void InvokeStatrupMethod(FileInfo[] files)
        {
            // one file one thread.
            foreach (var file in files)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(InvokeStart));
                thread.IsBackground = true;
                thread.Start(file);
            }

        }

        private static void InvokeStart(object file)
        {
            var target = (FileInfo)file;
            var assembly = Assembly.LoadFrom(target.FullName);
            Type[] types = assembly.GetTypes(); //loaded exception  referrence dll didn't exist/

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
