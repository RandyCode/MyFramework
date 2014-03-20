using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class TransactionContext
    {
        private List<SqlTransaction> _txnList;

        public TransactionContext(List<SqlTransaction> list)
        {
            _txnList = list;
        }

        public void Commit()
        {
            if (_txnList == null)
                throw new NullReferenceException("null refence");
            else
                _txnList.ForEach(x => x.Commit());
        }

        public void RollBack()
        {
            if (_txnList == null)
                throw new NullReferenceException("null refence");
            else
                _txnList.ForEach(x => x.Rollback());
        }
    }
}
