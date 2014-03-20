using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public interface IDbContext
    {
        DbOperater Operater { get; set; }

        List<T> GetList<T>(Expression<Func<T, bool>> where = null, Expression<Func<T, object>> sortField = null, bool desc = true, int rowCount=0, int pageIndex=0)
            where T : DBObject, new();

        T GetModel<T>(Expression<Func<T, bool>> where = null)
           where T : DBObject, new();

        T Create<T>(T model)
         where T : DBObject, new();

        T Update<T>(T model)
           where T : DBObject, new();

        T Remove<T>(T model)
               where T : DBObject, new();

        T Remove<T>(Expression<Func<T, bool>> where = null)
          where T : DBObject, new();


    }
}
