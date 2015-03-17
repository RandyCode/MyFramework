using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
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

            IMethodReturn methodReturn = null;
            try
            {
                try
                {
                    methodReturn = getNext()(input, getNext);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    methodReturn = input.CreateMethodReturn(null);
                    methodReturn.Exception = exception;
                }
            }
            finally
            {
                if (methodReturn.Exception != null)
                {
                    methodReturn.Exception = this.HandlerException(input, methodReturn.Exception);
                }
            }
            return methodReturn;
        }
    }
}
