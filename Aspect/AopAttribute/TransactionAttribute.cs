using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public abstract class TransactionAttribute : BaseInterceptorAttribute
    {
       public TransactionAttribute()
       {
           base.Order = AopOrderEnum.Transaction.GetHashCode();
       }
    }
}
