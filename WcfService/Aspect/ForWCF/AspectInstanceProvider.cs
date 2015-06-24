using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;

namespace Aspect.ForWCF
{
    public class AspectInstanceProvider : Attribute, IInstanceProvider
    {
        public Type ContractType
        {
            get;
            private set;
        }

        public Type ServiceType
        {
            get;
            private set;
        }

        public IUnityContainer UnityContainer
        {
            get;
            private set;
        }

        public AspectInstanceProvider(IUnityContainer unityContainer, Type contractType, Type serviceType)
        {
            this.UnityContainer = unityContainer;
            this.ContractType = contractType;
            this.ServiceType = serviceType;
        }

        public object GetInstance(InstanceContext istanceContext, Message message)
        {      
            object obj;
            try
            {
                object obj1 = null;
                if (!this.UnityContainer.IsRegistered(this.ContractType))
                {
                    obj1 = Activator.CreateInstance(this.ServiceType);
                }
                else
                {
                    IUnityContainer unityContainer = this.UnityContainer.CreateChildContainer();
                    InitContainer.Single = unityContainer;
                    var aa= unityContainer.IsRegistered(this.ContractType);

                    obj1 = this.UnityContainer.Resolve(this.ContractType, new ResolverOverride[0]);  //bug
                    //obj1 = PolicyInjection.Wrap(this.ContractType, obj1);
                }
                obj = obj1;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return obj;
        }

        public object GetInstance(InstanceContext istanceContext)
        {
            return GetInstance(istanceContext, null);
        }

        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            try
            {
                IUnityContainer currentContainer = InitContainer.Single;
                if (currentContainer != null)
                {
                    currentContainer.Dispose();
                    InitContainer.Single = null;
                }
            }
            catch
            {
            }
            IDisposable disposable = instance as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

      
    }
}
