using Aspect;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BusinessService.Aop
{
    public class WCFTransactionAttribute : TransactionAttribute
    {

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            throw new NotImplementedException();
        }
    }
}
