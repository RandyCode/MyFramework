using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Configuration;
using CommonHelper;
using System.Threading;
using System.Collections.Concurrent;

///for cycle job
namespace GenericServiceHost
{
    class Program
    {

        internal delegate void Maindelegate();

        internal static event Maindelegate MainHandleEvent;

        //public ManualResetEvent
       
        //队列 -》事件 ->订阅-> 监视 xinhaoliang  -> 管理线程 delete 要把处理线程也杀死 
        static void Main(string[] args)
        {

            var files = HostTool.FindFiles(ConfigurationManager.AppSettings["dllPath"], "*.dll");

            HostTool.InvokeStatrupMethod(files);

            while (true)
            {
 
            }


            Console.ReadKey();
        }



        private ConcurrentQueue<FileInfo> safeQueue;
         void WatchDirectory(string path,string filter)
        {
            FileSystemWatcher fsWatch = new FileSystemWatcher();
            fsWatch.Path = path;
            fsWatch.Filter = filter;

            fsWatch.Changed += new FileSystemEventHandler(OnChanged);
            fsWatch.Created += new FileSystemEventHandler(OnChanged);
            fsWatch.Deleted += new FileSystemEventHandler(OnChanged);
            fsWatch.Renamed += new RenamedEventHandler(OnRenamed);

            fsWatch.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName  | NotifyFilters.CreationTime;
            fsWatch.InternalBufferSize = 8192 * 8;
            fsWatch.EnableRaisingEvents = true;
        }

        void OnChanged(object source, FileSystemEventArgs e)
        {
            if (!this.IsFile(e.FullPath) && e.ChangeType == WatcherChangeTypes.Changed) return;

            FileSystemWatcher watcher = source as FileSystemWatcher;
            SyncAction syncItem = new SyncAction();
            syncItem.FullPath = e.FullPath;
            syncItem.ChangedTime = DateTime.Now;
            syncItem.Name = e.Name;
            syncItem.ChangedType = e.ChangeType;
            syncItem.SyncPath = watcher.Path;

            string logicFullPath = string.Empty;
            //path 目标路径
            foreach (string path in watchList)
            {
                if (syncItem.FullPath.StartsWith(path)) continue;
                logicFullPath = syncItem.FullPath.Replace(syncItem.SyncPath, "");
                string fullPath = path + logicFullPath;
                switch (syncItem.ChangedType)
                {
                    case WatcherChangeTypes.Created:
                        if (File.Exists(fullPath) || Directory.Exists(fullPath))
                        {
                            return; //已存在 不加入队列
                        }
                        break;
                    case WatcherChangeTypes.Changed:
                        if (IsFile(syncItem.FullPath) && IsFile(fullPath))
                        {
                            FileInfo srcFile = new FileInfo(syncItem.FullPath);
                            FileInfo dstFile = new FileInfo(fullPath);
                            if (srcFile.LastWriteTime <= dstFile.LastWriteTime)
                                return;

                        }
                        break;
                    default: break;
                }
            }
            //同步实体入队列
            if (syncItem.FullPath.ToLower().EndsWith(".tmp")) return;
            this.safeQueue.Enqueue(syncItem);
        }


        void OnRenamed(object source, RenamedEventArgs e)
        {
            FileSystemWatcher watcher = source as FileSystemWatcher;
            SyncAction syncItem = new SyncAction();
            syncItem.OldFullPath = e.OldFullPath;
            syncItem.FullPath = e.FullPath;
            syncItem.ChangedTime = DateTime.Now;
            syncItem.Name = e.Name;
            syncItem.OldName = e.OldName;
            syncItem.ChangedType = e.ChangeType;
            syncItem.SyncPath = watcher.Path;
            if (syncItem.FullPath.ToLower().EndsWith(".tmp")) return;
            this.safeQueue.Enqueue(syncItem);  //入队列
        }

    }
}
