using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspect
{
    public interface IUnityContanierRegister
    {
        void Register(IUnityContainer container);
    }
}
