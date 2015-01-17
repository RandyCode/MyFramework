using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class DatabaseRepository : IDatabaseRepository
    {
        // QUESTION ???
        //1. DbContext不适合创建成单例模式，例如A对象正在编辑，B对象编辑完了提交，导致正在编辑的A对象也被提交了，但是A的改可能要取消的，但是最终都被提交到数据库中了。
        //2. 如果DbContext创建过多的实例，就要控制好并发的问题，因为不同实例的DbContext可能会对同一条记录进行修改。
        //3. DbContext线程安全问题，同一实例的DbContext被不同线程调用会引发第一条场景的情况。不同线程使用不同实例的DbContext时又会引发第二种场景的情况。

        //这类 应该是提供增删查改方法。 如何选择需要那个数据库？ 优先  分库 ：垂直切分 水平切分？

        private DbContext _dbContext;

        public DatabaseRepository()
        {
            _dbContext = new AopDbEntity();   //can extension -> reflection
        }

        public int Add<T>(T model)
            where T : class
        {
            _dbContext.Set<T>().Add(model);
            var result = _dbContext.SaveChanges(); 
            return result;
        }

        public int Delete<T>(Expression<Func<T, bool>> whereLambda = null, string activeProperty = "Active")
                   where T : class
        {
            var model = GetModel(whereLambda);
            DbEntityEntry entry = _dbContext.Entry<T>(model);
            entry.State = System.Data.Entity.EntityState.Unchanged;
            entry.Property(activeProperty).IsModified = true;
            return _dbContext.SaveChanges();
        }

        public int RealDelete<T>(Expression<Func<T, bool>> whereLambda = null)
           where T : class
        {
            var model = GetModel(whereLambda);
            //DbEntityEntry entry = _dbContext.Entry<T>(model);
            _dbContext.Set<T>().Remove(model);
            return _dbContext.SaveChanges();
        }

        public int Update<T>(T model)
           where T : class
        {
            DbEntityEntry entry = _dbContext.Entry<T>(model);
            entry.State = System.Data.Entity.EntityState.Modified;
            return _dbContext.SaveChanges();
        }


        public List<T> GetPagedList<T, TKey>(int pageIndex, int pageSize, Expression<Func<T, bool>> whereLambda = null, Expression<Func<T, TKey>> orderBy = null)
                 where T : class
        {
            return _dbContext.Set<T>().Where(whereLambda).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }


        public int ExcuteSql(string strSql, params object[] paras)
        {
            return _dbContext.Database.ExecuteSqlCommand(strSql, paras);
        }


        public T GetModel<T>(Expression<Func<T, bool>> lambda) where T : class
        {
            return _dbContext.Set<T>().Where(lambda).FirstOrDefault();
        }
    }
}
