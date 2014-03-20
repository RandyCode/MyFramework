using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public interface IUserManager
    {
        void CheckUserStatus();

        BusinessUser GetUserInfo(string id);


        BusinessUser CreateUser(BusinessUser user);


        BusinessUser UpdateUser(BusinessUser user);


        BusinessUser RemoveUser(BusinessUser user);


        BusinessUser RemoveUser(Expression<Func<BusinessUser, bool>> where);


        List<BusinessUser> GetUserList(Expression<Func<BusinessUser, bool>> where = null, Expression<Func<BusinessUser, object>> sortField = null, bool desc = false, int rowCount = 0, int pageIndex = 0);

    }
}
