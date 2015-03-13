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
    public class ThreadWatcher
    {

        public event Action ActionEvent;
        public ManualResetEvent ThreadSignal { get; set; }

        public HostEnvironment Host { get; set; }

        public ThreadWatcher()
        {
            ThreadSignal = new ManualResetEvent(true);
        }


        public void WatchDirectory(string path, string filter)
        {

            FileSystemWatcher fsWatch = new FileSystemWatcher();
            fsWatch.Path = path;
            fsWatch.Filter = filter;

            fsWatch.Changed += new FileSystemEventHandler(OnChanged);
            fsWatch.Created += new FileSystemEventHandler(OnChanged);
            fsWatch.Deleted += new FileSystemEventHandler(OnChanged);
            fsWatch.Renamed += new RenamedEventHandler(OnRenamed);

            fsWatch.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.CreationTime;
            fsWatch.InternalBufferSize = 8192 * 8;
            fsWatch.EnableRaisingEvents = true;

            Host = new HostEnvironment(path, filter);
            Host.SignalHandler += () => { ThreadSignal.Set(); };

            Thread HandlerFileChange = new Thread(() =>
            {
                while (true)
                {
                    if (Host.FileQueue.Count > 0)
                        Host.DequeueInvoke();

                    //ThreadSignal.WaitOne();
                    ThreadSignal.Reset();
                }
            });
            HandlerFileChange.IsBackground = true;
            HandlerFileChange.Start();
        }

        void OnChanged(object source, FileSystemEventArgs e)
        {
            if (!this.IsFile(e.FullPath))
                return;

            FileSystemWatcher watcher = source as FileSystemWatcher;
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    var threadC = Host.ExistFiles[e.FullPath];
                    threadC.Abort();
                    Host.EnqueueFiles(e.FullPath);
                    //e.FullPath
                    break;
                case WatcherChangeTypes.Created:
                    Host.EnqueueFiles(e.FullPath);
                    break;
                case WatcherChangeTypes.Deleted:
                    var threadD = Host.ExistFiles[e.FullPath];
                    threadD.Abort();
                    Host.ExistFiles.Remove(e.FullPath);
                    break;
                default: break;
            }

            ThreadSignal.Set();
        }


        void OnRenamed(object source, RenamedEventArgs e)
        {
            if (!this.IsFile(e.OldFullPath))
                return;

            var thread = Host.ExistFiles[e.OldFullPath];
            thread.Abort();
            Host.ExistFiles.Remove(e.OldFullPath);
            Host.EnqueueFiles(e.FullPath);
            ThreadSignal.Set();
        }

        private bool IsFile(string path)
        {
            if (File.Exists(path))
                return true;
            return false;
        }


    }
}
