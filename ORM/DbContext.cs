using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    //Flow: DbContext-> lambdaToSql(just for select sql sentence) -> Dboperater ->DB
    public class DbContext : IDbContext
    {
        
        private DbOperater _operater = null;

        public DbContext()
        {
            _operater = new DbOperater();

        }

        /// <summary>
        /// 排序查找list
        /// </summary>
        public List<T> GetList<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null, System.Linq.Expressions.Expression<Func<T, object>> sortField = null, bool desc = false) where T : DBObject, new()
        {
            return this.GetList<T>(where, sortField, desc, 0, 0);        
        }

        /// <summary>
        /// 分頁查找list
        /// </summary>
        public List<T> GetList<T>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, object>> sortField, bool desc, int rowCount=0, int pageIndex=0) where T : DBObject, new()
        {
            return (List<T>)_operater.GetList<T>(where.Body, sortField.Body, desc, rowCount, pageIndex);
        }

        public T GetModel<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null) where T : DBObject, new()
        {
            return (T)_operater.GetModel<T>(where.Body);
        }

        public void Remove<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null) where T : DBObject, new()
        {
            _operater.Remove<T>(where.Body);
        }

        public int Create<T>(T model) where T : DBObject, new()
        {
            model.DbActionType = CURDActionEnum.Create;
           return _operater.ExecuteNonQuery<T>(model);
        }

        public int Update<T>(T model) where T : DBObject, new()
        {
            model.DbActionType = CURDActionEnum.Update;
            return _operater.ExecuteNonQuery<T>(model);
        }

        public int Remove<T>(T model) where T : DBObject, new()
        {
            model.DbActionType = CURDActionEnum.Delete;
            return _operater.ExecuteNonQuery<T>(model);
        }

        public int ExecuteNonQuery(string sql)
        {
            return _operater.ExecuteNonQuery(sql);
        }

        public object ExecuteScalar(string sql)
        {
            return _operater.ExecuteScalar(sql);
        }
    }
}
