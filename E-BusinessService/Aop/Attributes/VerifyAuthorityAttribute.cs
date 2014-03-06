using Aspect;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BusinessService.Aop
{
    public class VerifyAuthorityAttribute : AuthenticationAttribute
    {

        //private IAuthenticationManager AuthenticationManager { get { return WcfContext.CurrentContainer.Resolve<IAuthenticationManager>(); } }

        public override void CheckAuthentication(IMethodInvocation input)
        {
           
        }
    }
}
