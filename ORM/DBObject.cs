using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    /// <summary>
    /// all the model must inherit this class
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
