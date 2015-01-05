using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public interface ILogWrap
    {
        void Write(string message, LogMediaEnum[] mediaArray=null);
    }
}
