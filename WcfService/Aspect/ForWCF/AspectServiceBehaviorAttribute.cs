using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Aspect.ForWCF
{
    public class AspectServiceBehaviorAttribute : IServiceBehavior
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

        public AspectServiceBehaviorAttribute(IUnityContainer unityContainer)
        {
            this.UnityContainer = unityContainer;
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher channelDispatcher in serviceHostBase.ChannelDispatchers)
            {
                foreach (EndpointDispatcher aspectInstanceProvider in channelDispatcher.Endpoints)
                {
                    Type type = (
                        from endpoint in serviceHostBase.Description.Endpoints
                        where (endpoint.Contract.Name != aspectInstanceProvider.ContractName ? false : endpoint.Contract.Namespace == aspectInstanceProvider.ContractNamespace)
                        select endpoint.Contract.ContractType).FirstOrDefault<Type>();
                    if (!(null == type))
                    {
                        if (serviceHostBase.Description.ServiceType.GetInterfaces().Any<Type>((Type t) => t == type))
                        {
                            if (!this.UnityContainer.IsRegistered(type))
                            {
                                this.UnityContainer.RegisterType(type, serviceHostBase.Description.ServiceType, new InjectionMember[0]);
                            }
                        }
                        aspectInstanceProvider.DispatchRuntime.InstanceProvider = new AspectInstanceProvider(this.UnityContainer, type, serviceHostBase.Description.ServiceType);
                    }
                }
            }
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
         
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
           
        }

    }
}
