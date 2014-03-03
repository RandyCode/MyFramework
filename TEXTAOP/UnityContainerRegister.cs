using Aspect;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEXTAOP
{
    public class UnityContainerRegister : IUnityContanierRegister
    {
        public void Register(Microsoft.Practices.Unity.IUnityContainer container)
        {
            container.RegisterType<IOutPut, Output>(new HierarchicalLifetimeManager());
        }
    }
}
