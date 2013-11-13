
using ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DbOperater
    {
        private string _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;


        /// <summary>
        ///  mapping the datatable to dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public IDictionary<string, string> MappingAttributte<T>(T t)
            where T : DBObject, new()
        {
            if (t == null)
                throw new Exception("client post a null entity,plz check it again.");
            IDictionary<string, string> dic = new Dictionary<string, string>();
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<DataTableAttribute>().TableName;
            dic.Add("tablename", tableName);
            string fieldName, value;
            foreach (PropertyInfo pro in type.GetProperties())
            {
                fieldName = pro.GetCustomAttribute<DataFieldAttribute>().DataFieldName;
                value = pro.GetValue(t).ToString();
                dic.Add(fieldName, value);
            }
            return dic;
        }

        //public string GetSelectSQL(IDictionary<string, string> dic)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append("select ");
        //    foreach (var item in dic)
        //    {
        //        if (!string.IsNullOrEmpty(item.Value))
        //        {

        //        }
        //    }
        //}

        //public string GetUpdateSQL()
        //{

        //}
        //public string GetCreateSQL()
        //{

        //}
        //public string GetDeleteSQL()
        //{

        //}

        public string LambdaToSql(Expression exp)
        {
            
            return null;
        }


    }
}
