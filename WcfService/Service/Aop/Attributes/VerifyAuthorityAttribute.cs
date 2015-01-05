using Aspect;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Aop
{
    public class VerifyAuthorityAttribute : AuthenticationAttribute
    {

        //private IAuthenticationManager AuthenticationManager { get { return WcfContext.CurrentContainer.Resolve<IAuthenticationManager>(); } }

        public override void CheckAuthentication(IMethodInvocation input)
        {
            Console.WriteLine("VerifyAuthority");
        }
    }
}
