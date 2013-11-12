
using ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DbOperater
    {
        private string _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;


        /// <summary>
        /// modle特性映射數據表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public Dictionary<string,string> MappingAttributte<T>(T t)
        {
            if (t == null)
               throw new Exception("client post a null entity,plz check it again.");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<DataTableAttribute>().TableName;
            dic.Add("tablename", tableName);
            string fieldName,value;
            foreach (PropertyInfo pro in type.GetProperties())
            {
                fieldName = pro.GetCustomAttribute<DataFieldAttribute>().DataFieldName;
                value = pro.GetValue(t).ToString();
                dic.Add(fieldName, value);
            }
            return dic;
        }


    }
}
