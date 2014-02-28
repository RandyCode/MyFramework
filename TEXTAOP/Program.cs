using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspect;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;

namespace TEXTAOP
{
    class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer c = new UnityContainer();
            c.AddNewExtension<Interception>()
                .Configure<Interception>()
            .SetInterceptorFor<IOutPut>(new InterfaceInterceptor()); // 泛型要动态注入 用IUnityContainer

            c.RegisterType<IOutPut, Output>();
            IOutPut ii = c.Resolve<IOutPut>();
            ii.Output();
            Console.WriteLine();


            ii.put();

            Console.ReadKey();
        }
    }


    public interface IOutPut
    {
        void Output();
        void put();
    }


    [Before]
    [After] 
    [Call]
    public class Output : IOutPut
    {
       [First]
        void IOutPut.Output()
        {
            Console.WriteLine("11111");
        }

        void IOutPut.put()
        {
            Console.WriteLine("22222");
        }
    }


    public class FirstAttribute : AuthenticationAttribute
    {
        public override void CheckAuthentication(IMethodInvocation input)
        {
            Console.WriteLine("authentication");
        }
    }

    public class CallAttribute : TransactionAttribute
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("Transaction");
            return getNext()(input, getNext);
        }
    }

    public class BeforeAttribute : BeforeCallAttribute
    {
        public override void BeforeCall(IMethodInvocation input)
        {
            Console.WriteLine("before");
        }
    }

    public class AfterAttribute : AfterCallAttribute
    {
        public override void AfterCall(IMethodInvocation input)
        {
            Console.WriteLine("after");
        }
    }














}
