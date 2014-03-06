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

    public class text : Itext
    {
        [Dependency]
        public IOutPut ii { get; set; }

        public void say()
        {
            ii.Output();
            Console.WriteLine();
            ii.put();
        }
    }

    public interface Itext
    {
        void say();
    }


    class Program
    {

        static void Main(string[] args)
        {

            var bb = MyUnityContainer.Instance.Resolve<text>();
            bb.say();

            Console.ReadKey();
        }
    }




    public interface IOutPut
    {
        void Output();
        void put();
    }

    [LoadInit]
    [First]
    [After]
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

    public class LoadInitAttribute : InitializationAttribute
    {
        public override void OnInit(IMethodInvocation input)
        {
            Console.WriteLine("Init");
            //MyUnityContainer.Instance.Resolve();
        }

        public override void OnRelease(IMethodInvocation input)
        {
            Console.WriteLine("Release");
        }
    }















}
