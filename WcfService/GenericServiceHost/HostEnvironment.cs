using CommonHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericServiceHost
{
    public class HostEnvironment
    {


        public event Action SignalHandler;
        /// <summary>
        /// fullName,Thread
        /// </summary>
        public Dictionary<string, Thread> ExistFiles { get; set; }
        public Queue<FileInfo> FileQueue { get; set; }

        public HostEnvironment(string path, string searchPattern = null)
        {
            if (ExistFiles == null)
                ExistFiles = new Dictionary<string, Thread>();

            EnqueueFiles(path, searchPattern);
        }


        public void EnqueueFiles(string path, string searchPattern = null)
        {
            FileInfo[] infos = null;
            DirectoryInfo dir = new DirectoryInfo(path);

            if (!dir.Exists)
            {
                dir.Create();
                return;
            }

            if (string.IsNullOrWhiteSpace(searchPattern))
                infos = dir.GetFiles();
            else
                infos = dir.GetFiles(searchPattern);

            foreach (var file in infos)
            {
                if (ExistFiles.Keys.Contains(file.FullName))
                    continue;
                FileQueue.Enqueue(file);
            }
            SignalHandler();

        }

        public void DequeueInvoke()
        {
            while (FileQueue.Count > 1)
            {
                var fileInfo = FileQueue.Dequeue();
                InvokeStatrupMethod(fileInfo);
            }
        }

        private void InvokeStatrupMethod(FileInfo file)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(InvokeStart));
            if (!ExistFiles.Keys.Contains(file.FullName))
                ExistFiles.Add(file.FullName, thread);
            else
            {
                ExistFiles[file.FullName] = thread;
            }
            thread.IsBackground = true;
            thread.Start(file);
        }


        private void InvokeStart(object file)
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
