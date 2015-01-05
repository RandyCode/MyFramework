using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public abstract class BeforeCallAttribute : BaseInterceptorAttribute
    {
        public BeforeCallAttribute()
        {
            base.Order = AopOrderEnum.BeforeCall.GetHashCode();
        }

        public abstract void BeforeCall(IMethodInvocation input);

        public override IMethodReturn Invoke(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input, Microsoft.Practices.Unity.InterceptionExtension.GetNextHandlerDelegate getNext)
        {
            this.BeforeCall(input);
            return getNext()(input,getNext);
        }
    }
}
