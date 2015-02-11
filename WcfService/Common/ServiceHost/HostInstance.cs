using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class HostInstance:IHostStartup
    {
        public void Main()
        {
            Console.WriteLine("commenetHelper.Main");
        }
    }
}
