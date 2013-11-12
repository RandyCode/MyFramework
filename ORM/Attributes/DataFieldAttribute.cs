using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataFieldAttribute : Attribute
    {
        private string _dataFieldName = "";

        public string DataFieldName
        {
            get { return _dataFieldName; }
        }

        public DataFieldAttribute(string fieldName)
        {
            this._dataFieldName = fieldName;
        }
    }
}
