using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public abstract class ExceptionAttribute : BaseInterceptorAttribute
    {

        public ExceptionAttribute()
        {
            base.Order = AopOrderEnum.Exception.GetHashCode();
        }

        public abstract Exception HandlerException(IMethodInvocation input, Exception exception);

        public override IMethodReturn Invoke(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input, Microsoft.Practices.Unity.InterceptionExtension.GetNextHandlerDelegate getNext)
        {
            IMethodReturn return2 = null;
            Exception exception = null;
            try
            {
                return2 = getNext()(input, getNext);
                exception = return2.Exception;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            if (exception != null)
            { throw this.HandlerException(input, exception); }
            return return2;
        }
    }
}
