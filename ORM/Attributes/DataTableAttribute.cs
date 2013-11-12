using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DataTableAttribute : Attribute
    {
        private string _tableName="";

        public string TableName
        {
            get { return _tableName; }
        }
        public DataTableAttribute(string tableName)
        {
            this._tableName = tableName;
        }
    }
}
