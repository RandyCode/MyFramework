using Aspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Aop
{
    public class InitAttribute :InitializationAttribute
    {
        public override void OnInit(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input)
        {
           
        }

        public override void OnRelease(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input)
        {
           
        }
    }
}
