using Common;
using Microsoft.Practices.Unity;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserManager : BaseManager,IUserManager 
    {
        public Model.BusinessUser GetUserInfo(string id)
        {
          return  Helper.DbContext.GetModel<BusinessUser>(x => x.Id == id);
        }

        [Aop.WCFTransaction]
        public Model.BusinessUser CreateUser(Model.BusinessUser user)
        {
            return Helper.DbContext.Create<BusinessUser>(user);
        }

        public Model.BusinessUser UpdateUser(Model.BusinessUser user)
        {
            return Helper.DbContext.Update<BusinessUser>(user);
        }

        public Model.BusinessUser RemoveUser(Model.BusinessUser user)
        {
            return Helper.DbContext.Remove<BusinessUser>(user);
        }

        public Model.BusinessUser RemoveUser(System.Linq.Expressions.Expression<Func<Model.BusinessUser, bool>> where)
        {
            return Helper.DbContext.Remove<BusinessUser>(where);
        }

        public List<Model.BusinessUser> GetUserList(System.Linq.Expressions.Expression<Func<Model.BusinessUser, bool>> where = null, System.Linq.Expressions.Expression<Func<Model.BusinessUser, object>> sortField = null, bool desc = false, int rowCount = 0, int pageIndex = 0)
        {
            return Helper.DbContext.GetList<BusinessUser>(where, sortField, desc, rowCount, pageIndex);
        }
    }
}
