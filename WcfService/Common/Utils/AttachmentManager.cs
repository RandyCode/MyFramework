using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class AttachmentManager :IAttachmentManager
    {

        public System.IO.FileInfo[] GetAllFilesInfo()
        {
            throw new NotImplementedException();
        }

        public void UploadFile(System.IO.Stream stream, bool privateFile = false)
        {
            throw new NotImplementedException();
        }

        public void DownLoadFile(string fullpath)
        {
            throw new NotImplementedException();
        }
    }
}
