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
    public class UserService :  IUserService
    {
        [Dependency]
        IUserManager UserManager { set; get; }

      
        public Model.BusinessUser GetUserInfo(string id)
        {
            return UserManager.GetUserInfo(id);
        }


        public Model.BusinessUser CreateUser(Model.BusinessUser user)
        {
            return UserManager.CreateUser(user);
        }

 
        public Model.BusinessUser UpdateUser(Model.BusinessUser user)
        {

            return UserManager.UpdateUser(user);

        }

  
        public Model.BusinessUser RemoveUser(Model.BusinessUser user)
        {
            return UserManager.RemoveUser(user);
        }

    
        public Model.BusinessUser RemoveUser(System.Linq.Expressions.Expression<Func<Model.BusinessUser, bool>> where)
        {

            return UserManager.RemoveUser(where);
        }
    
        public List<Model.BusinessUser> GetUserList(System.Linq.Expressions.Expression<Func<Model.BusinessUser, bool>> where = null, System.Linq.Expressions.Expression<Func<Model.BusinessUser, object>> sortField = null, bool desc = false, int rowCount = 0, int pageIndex = 0)
        {
            return UserManager.GetUserList(where, sortField, desc, rowCount, pageIndex);
        }


    }
}


