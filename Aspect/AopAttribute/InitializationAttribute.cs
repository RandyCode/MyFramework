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

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result;
            try
            {
                this.OnInit(input);
                result = getNext()(input, getNext);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                this.OnRelease(input);
            }
            return result;

        }
    }
}
