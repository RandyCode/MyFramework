using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
   public class DbContext:IDbContext
    {
       private DbOperater _operater=null;

       public DbContext()
       {
           _operater = new DbOperater();
         
       }

        /// <summary>
        /// 排序查找list
        /// </summary>
        public List<T> GetList<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null, System.Linq.Expressions.Expression<Func<T, object>> sortField = null, bool desc = true) where T : DBObject, new()
        {
         
            throw new NotImplementedException();
        }

        /// <summary>
        /// 分頁查找list
        /// </summary>
        public List<T> GetList<T>(System.Linq.Expressions.Expression<Func<T, bool>> where, System.Linq.Expressions.Expression<Func<T, object>> sortField, bool desc, int rowCount, int pageIndex) where T : DBObject, new()
        {
            throw new NotImplementedException();
        }

        public T GetModel<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null) where T : DBObject, new()
        {
            throw new NotImplementedException();
        }

        public T Create<T>(T model) where T : DBObject, new()
        {
            throw new NotImplementedException();
        }

        public T Update<T>(T model) where T : DBObject, new()
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(T model) where T : DBObject, new()
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null) where T : DBObject, new()
        {
            throw new NotImplementedException();
        }

        public int GetCount<T>(System.Linq.Expressions.Expression<Func<T, bool>> where = null) where T : DBObject, new()
        {
            throw new NotImplementedException();
        }

        public int ExecuteNonQuery(string sql)
        {
            throw new NotImplementedException();
        }

        public object ExecuteScalar(string sql)
        {
            throw new NotImplementedException();
        }
    }
}
