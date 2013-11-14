
using ORM.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Threading;

namespace ORM
{
    public class DbOperater
    {
        private string _connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
        private SqlConnection _conn;
        private SqlCommand _cmd;

        private void OpenConnection(Action action)
        {
            //using (_conn = new SqlConnection(_connStr))
            //{
                //把鏈接放進當前線程中，一次請求只建立一次鏈接。 
                _conn.Open();
                using (_cmd = new SqlCommand())
                {
                    _cmd.Connection = _conn;
                    _cmd.CommandType = CommandType.Text;
                    try
                    {
                        action();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                 
                }
            //當前線程結束前才close鏈接
                _conn.Close();
            //}
        }


        public int ExecuteNonQuery<T>(T t)
            where T:DBObject,new()
        {
            IDictionary<string,string> dic = MappingAttributte<T>(t);
            if (t.DbActionType == null)
                throw new Exception("this model has not DbActionType,plz check it !");
            switch (t.DbActionType)
            {
                case CURDActionEnum.Create:
                    break;
                case CURDActionEnum.Delete: 
                    break;
                case CURDActionEnum.Update: 
                    break;
                default: throw new Exception("cannot handler this DbActionType");
            }
            return 0;
        }

        public int ExecuteNonQuery(string sql)
        {
            int result = 0;
            OpenConnection(() =>
            {
                _cmd.CommandText = sql;
                result = _cmd.ExecuteNonQuery();
            });
            return result;
        }

        public object ExecuteScalar(string sql)
        {
            object result=null ;
            OpenConnection(() =>
            {
                _cmd.CommandText = sql;
                result = _cmd.ExecuteScalar();
            });
            return result;
        }

        /// <summary>
        ///  mapping the datatable to dictionary
        /// </summary>
        public IDictionary<string, TypeValue> MappingAttributte<T>(T t)
            where T : DBObject, new()
        {
            if (t == null)
                throw new Exception("client post a null entity,plz check it again.");
            IDictionary<string, TypeValue> dic = new Dictionary<string, TypeValue>();
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<DataTableAttribute>().TableName;
            dic.Add("tablename", new TypeValue() { Value = tableName });
            string fieldName, value,valuetype;
            foreach (PropertyInfo pro in type.GetProperties())
            {
                fieldName = pro.GetCustomAttribute<DataFieldAttribute>().DataFieldName;
                value = pro.GetValue(t).ToString();
                valuetype = pro.GetValue(t).GetType().ToString();
                dic.Add(fieldName, new TypeValue() {  Value=value, Type=valuetype});
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
        //randy
        private string GetCreateSQL(IDictionary<string ,string> dic)
        {
            if (dic == null) 
                throw new Exception("dictionary is null ..check the parameter");
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into " + dic["tablename"]);
            foreach (var item in dic)
            {
                if (!string.IsNullOrEmpty(item.Value)&&item.Key!="tablename")
                {

                }
            }
        }
        //public string GetDeleteSQL()
        //{

        //}



    }
    internal class TypeValue
    {
        private string _type;

        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string value;

        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
    
}
