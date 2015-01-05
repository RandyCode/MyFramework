using Aspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Aop
{
    public class AfterMethodAttribute : AfterCallAttribute
    {
        public override void AfterCall(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input)
        {
            Console.WriteLine("AfterMethod");
        }
    }
}
