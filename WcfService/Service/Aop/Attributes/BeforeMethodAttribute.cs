using Aspect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Aop
{
    public class BeforeMethodAttribute : BeforeCallAttribute
    {
        public override void BeforeCall(Microsoft.Practices.Unity.InterceptionExtension.IMethodInvocation input)
        {
            Console.WriteLine("BeforeMethod");
        }
    }
}
