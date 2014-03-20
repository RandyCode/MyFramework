using Aspect;
using Service;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Service
{
    public class UnityContainerRegister : IUnityContanierRegister
    {
        public void Register(IUnityContainer container)
        {
            _container = container;
            //RegistNSetInterceptor<IOutPut, Output>();
            RegistNSetInterceptor<IUserManager,UserManager>();
           
        }

        #region private method helper
        private IUnityContainer _container;

        /// <summary>
        /// RegisterType
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
