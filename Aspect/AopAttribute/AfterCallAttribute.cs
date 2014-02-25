using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public abstract class AfterCallAttribute : BaseInterceptorAttribute
    {
        public AfterCallAttribute()
        {
            base.Order = AopOrderEnum.AfterCall.GetHashCode();
        }

        public abstract void AfterCall(IMethodInvocation input);

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result = getNext()(input, getNext);
            this.AfterCall(input);
            return result;
        }
    }
}
