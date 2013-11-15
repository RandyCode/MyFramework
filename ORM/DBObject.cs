using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    /// <summary>
    /// A1:all the model must inherit this class
    /// A2:create data must use guid
    /// </summary>
   public class DBObject
    {
        private CURDActionEnum _dbActionType;

        public CURDActionEnum DbActionType
        {
            get { return _dbActionType; }
            set { _dbActionType = value; }
        }
    }
}
