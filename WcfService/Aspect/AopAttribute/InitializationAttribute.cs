using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public abstract class InitializationAttribute : BaseInterceptorAttribute
    {
        public InitializationAttribute()
        {
            base.Order = AopOrderEnum.Init.GetHashCode();
        }

        public abstract void OnInit(IMethodInvocation input);
        public abstract void OnRelease(IMethodInvocation input);

        public abstract Exception OnException(IMethodInvocation input, Exception ex);

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn return2 = null;
            Exception exception = null;
            try
            {
                this.OnInit(input);
                return2 = getNext()(input, getNext);
                exception = return2.Exception;
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                this.OnRelease(input);
            }
            if (exception != null)
            { throw this.OnException(input, exception); }
            return return2;

        }

    }
}
