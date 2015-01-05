using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Aspect.ForWCF
{
    public class AspectServiceHost:ServiceHost
    {
        public Type ContractType
        {
            get;
            private set;
        }

        public IUnityContainer UnityContainer
        {
            get;
            private set;
        }

        public AspectServiceHost(IUnityContainer unityContainer, Type serviceType, params Uri[] baseAddresses)
            : base(serviceType, baseAddresses)
        {
            this.UnityContainer = unityContainer;
        }

        protected override void OnOpening()
        {
            base.OnOpening();
            if (base.Description.Behaviors.Find<AspectServiceBehaviorAttribute>() == null)
            {
                base.Description.Behaviors.Add(new AspectServiceBehaviorAttribute(this.UnityContainer));
            }
        }
    }
}
