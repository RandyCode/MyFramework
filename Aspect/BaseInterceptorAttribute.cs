using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
   public abstract class BaseInterceptorAttribute:HandlerAttribute,ICallHandler
    {
       protected BaseInterceptorAttribute() { }

       protected IUnityContainer Container { get; set; }

       int ICallHandler.Order 
       {
           get { return base.Order; }
           set { base.Order = value; }
       }

       public override ICallHandler CreateHandler(IUnityContainer container)
       {
           this.Container = container;
           return this;
       }

       public abstract IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext);
    }
}
