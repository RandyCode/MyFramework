using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class AsyncThread
    {
        private static ManualResetEvent _signal;  
        private static Queue<Action> _queue;


        private static void Call(Action action)
        {
            
        }

    }
}
