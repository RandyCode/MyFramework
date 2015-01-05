using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace Aspect
{
    [ConfigurationElementType(typeof(CustomCallHandlerData))]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface)]
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
