using E_BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace E_BusinessService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IDBService”。
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        BusinessUser GetUserInfo(string id);

        [OperationContract]
        BusinessUser CreateUser(BusinessUser user);

        [OperationContract]
        BusinessUser UpdateUser(BusinessUser user);

        [OperationContract]
        BusinessUser RemoveUser(BusinessUser user);

        [OperationContract]
        BusinessUser RemoveUser(Expression<Func<BusinessUser,bool>> where);

        [OperationContract]
        List<BusinessUser> GetUserList(Expression<Func<BusinessUser, bool>> where=null, Expression<Func<BusinessUser, object>> sortField = null, bool desc = true);

        [OperationContract]
        List<BusinessUser> GetUserPagedList(Expression<Func<BusinessUser, bool>> where=null, Expression<Func<BusinessUser, object>> sortField = null, bool desc = true, int rowCount=0, int pageIndex=0);

    }
}
