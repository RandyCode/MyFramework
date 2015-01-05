using System;
using Microsoft.Practices.Unity;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Aspect;

namespace Aspect.ForWCF
{
    public class AspectServiceHostFactory : ServiceHostFactory
    {
        protected virtual IUnityContainer UnityContainer
        {
            get { return MyUnityContainer.Instance; }
        }

        protected override ServiceHost CreateServiceHost(System.Type serviceType, System.Uri[] baseAddresses)
        {
            return new AspectServiceHost(this.UnityContainer,serviceType, baseAddresses);
        }
    }
}
