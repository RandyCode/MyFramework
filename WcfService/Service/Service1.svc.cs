using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Microsoft.Practices.Unity;
using System.ServiceModel.Activation;
using Repository;
using System.Threading.Tasks;
using Aspect;

namespace Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“Service1”。
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1 : IService1
    {
        [Dependency]
        public IDatabaseRepository DatabaseRepository { get; set; }

        //[Aop.Init]
        [Aop.BeforeMethod]
        [Aop.WCFTransaction]
        public User GetData()
        {
            User aa = new User();
            aa.id = Guid.NewGuid().ToString();
            aa.name = "r2an3dy";
            var re = DatabaseRepository.Add<User>(aa);

            DatabaseRepository.RealDelete<User>(u => u.name == "randy"); //不存在 


            var result = DatabaseRepository.GetModel<User>(u => u.name == "randy");
            return new User();
        }

    }


}
