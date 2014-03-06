using System;
using System.Collections.Generic;
using Model;
using Microsoft.Practices.Unity;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Common;

namespace Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“DBService”。
    public class UserService : BaseService, IUserService
    {
        [Dependency]
        IUserManager UserManager { set; get; }

        [Aop.WCFTransaction]
        public Model.BusinessUser GetUserInfo(string id)
        {
            return Helper.DbContext.GetModel<BusinessUser>(x => x.Id == id);
        }

        [Aop.WCFTransaction]
        public Model.BusinessUser CreateUser(Model.BusinessUser user)
        {
            return Helper.DbContext.Create<BusinessUser>(user);
        }

        [Aop.WCFTransaction]
        public Model.BusinessUser UpdateUser(Model.BusinessUser user)
        {
            UserManager.CheckUserStatus();
            return Helper.DbContext.Update<BusinessUser>(user);
        }

        [Aop.WCFTransaction]
        public Model.BusinessUser RemoveUser(Model.BusinessUser user)
        {
            UserManager.CheckUserStatus();
            return Helper.DbContext.Remove<BusinessUser>(user);
        }

        [Aop.WCFTransaction]
        public Model.BusinessUser RemoveUser(System.Linq.Expressions.Expression<Func<Model.BusinessUser, bool>> where)
        {
            UserManager.CheckUserStatus();
            return Helper.DbContext.Remove<BusinessUser>(where);
        }

        [Aop.WCFTransaction]
        public List<Model.BusinessUser> GetUserList(System.Linq.Expressions.Expression<Func<Model.BusinessUser, bool>> where = null, System.Linq.Expressions.Expression<Func<Model.BusinessUser, object>> sortField = null, bool desc = false, int rowCount = 0, int pageIndex = 0)
        {
            return Helper.DbContext.GetList<BusinessUser>(where, sortField, desc, rowCount, pageIndex);
        }


    }
}


