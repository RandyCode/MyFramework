using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ORM;
using E_BusinessCommon;
using E_BusinessModel;
using Microsoft.Practices.Unity;

namespace E_BusinessService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“DBService”。
    public class UserService : BaseService, IUserService
    {
        [Dependency]
        IUserManager UserManager { set; get; }

        [Aop.WCFTransaction]
        public E_BusinessModel.BusinessUser GetUserInfo(string id)
        {
            return Common.DbContext.GetModel<BusinessUser>(x => x.Id == id);
        }

        [Aop.WCFTransaction]
        public E_BusinessModel.BusinessUser CreateUser(E_BusinessModel.BusinessUser user)
        {
            return Common.DbContext.Create<BusinessUser>(user);
        }

        [Aop.WCFTransaction]
        public E_BusinessModel.BusinessUser UpdateUser(E_BusinessModel.BusinessUser user)
        {
            UserManager.CheckUserStatus();
            return Common.DbContext.Update<BusinessUser>(user);
        }

        [Aop.WCFTransaction]
        public E_BusinessModel.BusinessUser RemoveUser(E_BusinessModel.BusinessUser user)
        {
            UserManager.CheckUserStatus();
            return Common.DbContext.Remove<BusinessUser>(user);
        }

        [Aop.WCFTransaction]
        public E_BusinessModel.BusinessUser RemoveUser(System.Linq.Expressions.Expression<Func<E_BusinessModel.BusinessUser, bool>> where)
        {
            UserManager.CheckUserStatus();
            return Common.DbContext.Remove<BusinessUser>(where);
        }

        [Aop.WCFTransaction]
        public List<E_BusinessModel.BusinessUser> GetUserList(System.Linq.Expressions.Expression<Func<E_BusinessModel.BusinessUser, bool>> where = null, System.Linq.Expressions.Expression<Func<E_BusinessModel.BusinessUser, object>> sortField = null, bool desc = false, int rowCount = 0, int pageIndex = 0)
        {
            return Common.DbContext.GetList<BusinessUser>(where, sortField, desc, rowCount, pageIndex);
        }


    }
}
