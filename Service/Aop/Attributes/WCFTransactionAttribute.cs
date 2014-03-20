using Aspect;
using Common;
using Microsoft.Practices.Unity.InterceptionExtension;
using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Aop
{
    public class WCFTransactionAttribute : TransactionAttribute
    {
        public override IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            Console.WriteLine("transaction attribute");
            Helper.DbContext.Operater.BeginTransaction();
            TransactionContext context = new TransactionContext(Helper.DbContext.Operater.Tran);
            Exception ex = null;
            try
            {
                var result = getNext()(input, getNext);
                ex = result.Exception;
                return result;
            }
            catch (System.Data.DataException dex)
            {
                throw new System.Data.DataException(dex.Message);
            }
            finally
            {
                TryRollBack(context, ex);
            }
        }

        private void TryRollBack(TransactionContext Context, Exception ex)
        {
            try
            {
                if (ex == null)
                    Context.Commit();
                else
                    Context.RollBack();
            }
            catch { }
            finally { Helper.DbContext.Operater.CloseConnection(); }
        }
    }
}
