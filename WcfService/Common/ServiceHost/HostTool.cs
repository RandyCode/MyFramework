using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public static class HostTool
    {
        public static FileInfo[] FindFiles(string path, string searchPattern=null)
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


    }
}
