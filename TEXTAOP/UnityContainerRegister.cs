using Aspect;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEXTAOP
{
    public class UnityContainerRegister : IUnityContanierRegister
    {
        public void Register(IUnityContainer container)
        {
            _container = container;
            RegistNSetInterceptor<IOutPut, Output>();
            RegistNSetInterceptor<Itext, text>();
            _container.RegisterType<IUserManager, UserManager>();
            RegistNSetInterceptor<IUserService, UserService>();
        }

        #region private method helper
        private IUnityContainer _container;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="I">Interface</typeparam>
        /// <typeparam name="T">Instance Class</typeparam>
        private void RegistNSetInterceptor<I, T>()
                where T : I
        {
            _container.RegisterType<I, T>(new HierarchicalLifetimeManager())
                        .AddNewExtension<Interception>()
                        .Configure<Interception>()
                        .SetInterceptorFor<I>(new InterfaceInterceptor());
        } 
        #endregion
    }
}
