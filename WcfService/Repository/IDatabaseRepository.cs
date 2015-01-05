using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IDatabaseRepository
    {
        int Add<T>(T model) where T : class;

        int Delete<T>(Expression<Func<T, bool>> whereLambda = null, string activeProperty = "Active") where T : class;

        int RealDelete<T>(Expression<Func<T, bool>> whereLambda = null) where T : class;

        int Update<T>(T model) where T : class;

        T GetModel<T>(Expression<Func<T, bool>> lambda) where T : class;

        List<T> GetPagedList<T, TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda = null, Expression<Func<T, TKey>> orderBy = null) where T : class;

        int ExcuteSql(string strSql, params object[] paras);
    }
}
