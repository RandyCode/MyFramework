using Microsoft.Practices.Unity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Service
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    [Aop.VerifyAuthority]
    public class UtilsService :  IUtilsService
    {

        [Dependency]
        public IDatabaseRepository DatabaseRepository { get; set; }

        public string DoWork(string cipher)
        {
            return "randy";
        }
    }
}
