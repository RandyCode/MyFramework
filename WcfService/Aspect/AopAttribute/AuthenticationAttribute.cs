using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public abstract class AuthenticationAttribute : BaseInterceptorAttribute
    {

       public AuthenticationAttribute()
       {
           base.Order = AopOrderEnum.Authentication.GetHashCode();
       }

       public abstract void CheckAuthentication(IMethodInvocation input);

        public override Microsoft.Practices.Unity.InterceptionExtension.IMethodReturn Invoke(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input, Microsoft.Practices.Unity.InterceptionExtension.GetNextHandlerDelegate getNext)
        {
            this.CheckAuthentication(input);
            return getNext()(input, getNext);
        }
    }
}
