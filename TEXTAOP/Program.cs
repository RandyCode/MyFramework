using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspect;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.Practices.Unity;
using System.Configuration;

namespace TEXTAOP
{
    class Program
    {
        [Dependency]
        static IOutPut ii { get; set; }

        static void Main(string[] args)
        {
            IUnityContainer c = new UnityContainer();
            c.RegisterType<IOutPut, Output>();
            c.AddNewExtension<Interception>()
              .Configure<Interception>()
            .SetInterceptorFor<IOutPut>(new InterfaceInterceptor()); // 泛型要动态注入 用IUnityContainer   自己包一层 ?


            UnityContainerRegister uu = new UnityContainerRegister();
            uu.Register(new UnityContainer());

            var b = c.Resolve<IOutPut>();

            b.Output();
            Console.WriteLine();
            b.put();




            //Console.WriteLine(typeof(sd).ToString());

            Console.ReadKey();
        }
    }


    public interface IOutPut
    {
        void Output();
        void put();
    }


    //[Before]
    //[After] 
    //[Call]  
    [First]
    public class Output : IOutPut
    {
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

    public class LoadInfoAttribute : InitializationAttribute
    {
        public override void OnInit(IMethodInvocation input)
        {
            throw new NotImplementedException();
        }

        public override void OnRelease(IMethodInvocation input)
        {
            throw new NotImplementedException();
        }
    }















}
