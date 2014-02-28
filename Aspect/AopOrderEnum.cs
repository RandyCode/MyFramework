using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public enum AopOrderEnum
    {
        Exception = 1,
        Init = 2,
        Log = 3,
        Authentication = 4,
        BeforeCall = 5,
        Transaction = 6,
        AfterCall = 7
    }
}

