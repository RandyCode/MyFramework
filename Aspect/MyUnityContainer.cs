using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    static class InitContainer
    {
        static UnityContainer _singel;

        public static UnityContainer Single
        {
            get
            {
                if (_singel == null)
                    _singel = new UnityContainer();

                return _singel;
            }
        }

        static InitContainer()
        {
            MyUnityContainer.Init();            
        }

    }

    /// <summary>
    /// 實例化UnityContainer
    /// </summary>
    public class MyUnityContainer
    {

        public static IUnityContainer Instance
        {
            get { return InitContainer.Single; }
        }

        private static IUnityContanierRegister GetUnityContainerRegister()
        {
            string str = ConfigurationManager.AppSettings["UnityContainerRegister"];
            if (!string.IsNullOrEmpty(str))
            {
                try { return (Activator.CreateInstance(Type.GetType(str)) as IUnityContanierRegister); }
                catch (Exception) { }
            }
           
            return null;
        }

        public static void Init()
        {
            IUnityContanierRegister register = GetUnityContainerRegister();
            if (register != null)
            {
                register.Register(Instance);
            }
        }
    }
}
