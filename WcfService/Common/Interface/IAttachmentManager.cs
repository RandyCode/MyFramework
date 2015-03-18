using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public interface IAttachmentManager
    {
        FileInfo[] GetAllFilesInfo();

        void UploadFile(Stream stream, bool privateFile=false);

        void DownLoadFile(string fullpath);

    }
}
