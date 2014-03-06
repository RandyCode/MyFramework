using Aspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BusinessService.Aop
{
    public class AfterMethodAttribute : AfterCallAttribute
    {
        public override void AfterCall(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input)
        {
            
        }
    }
}
