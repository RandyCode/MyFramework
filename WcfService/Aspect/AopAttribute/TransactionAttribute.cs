using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Aspect
{
    public abstract class TransactionAttribute : BaseInterceptorAttribute
    {
        protected bool isDistributed = true;

        public TransactionAttribute()
        {
            base.Order = AopOrderEnum.Transaction.GetHashCode();
        }

        public override IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            IMethodReturn result = null;

            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    result = getNext()(input, getNext);
                    if (result.Exception == null)
                        scope.Complete();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return result;
            }
        }
    }

    public enum TransactionAttributeOption
    {
        /// <summary>
        /// 本地事务
        /// </summary>
        LTM,
        /// <summary>
        /// 分布式事务
        /// </summary>
        DTC
    }
}
