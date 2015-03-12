using Aspect;
using Microsoft.Practices.Unity;
using System;
using System.Web;
using System.Web.Compilation;
using System.Web.UI;

namespace Aspect.AspNet
{
    public class AspectHttpHandlerFactory : PageHandlerFactory, IHttpHandlerFactory
    {
        public AspectHttpHandlerFactory()
        {
        }

        public override IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            if (!MyUnityContainer.Instance.IsRegistered<IHttpHandler>(url))
            {
                Type compiledType = BuildManager.GetCompiledType(url);
                MyUnityContainer.Instance.RegisterType(typeof(IHttpHandler), compiledType, url, new InjectionMember[0]);
            }
            IHttpHandler httpHandler = MyUnityContainer.Instance.Resolve<IHttpHandler>(url, new ResolverOverride[0]);
            return httpHandler;
        }
    }
}