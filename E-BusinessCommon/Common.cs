using ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BusinessCommon
{
    public class Common
    {
        private static IDbContext _dbcontext;
        public static IDbContext DbContext
        {
            get 
            {
                if (_dbcontext == null)
                {
                    _dbcontext = new DbContext();
                }
                return _dbcontext;
            }
            set { _dbcontext = value; }
        }


    }
}
