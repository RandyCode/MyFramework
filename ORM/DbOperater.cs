
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
        private string _connStr = null;
        private SqlConnection _conn;
        private SqlCommand _cmd;

        public DbOperater()
        {
            //_connStr = System.Configuration.ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;
            _connStr = "server=.;database=ebusiness;uid=sa;pwd=12345678;";
        }

        private void OpenConnection(Action action)
        {
            using (_conn = new SqlConnection(_connStr))
            {
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
            }
        }


        public int ExecuteNonQuery<T>(T t)
            where T : DBObject, new()
        {
            IDictionary<string, SqlTypeValue> dic = MappingAttributte<T>(t);
            //試試不賦值會不會為空。
            if (t.DbActionType == null)
                throw new Exception("this model has not DbActionType,plz check it !");
            string sql = "";
            switch (t.DbActionType)
            {
                case CURDActionEnum.Create:
                    sql = GetCreateSQL(dic);
                    break;
                case CURDActionEnum.Delete:
                    sql = GetDeleteSQL(dic);
                    break;
                case CURDActionEnum.Update:
                    sql = GetUpdateSQL(dic);
                    break;
                default: throw new Exception("cannot handler this DbActionType");
            }
            return ExecuteNonQuery(sql);
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

        public List<T> ExecuteScalar<T>(string sql)
            where T:DBObject,new()
        {
            List<T> obj = null;
            OpenConnection(() =>
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, _conn);
                DataSet dataset = new DataSet();
                adapter.Fill(dataset);
               var result = dataset.Tables[0];
                obj=  GetEntities<T>(result);
            });
            return obj;
        }

        /// <summary>
        ///  mapping the datatable to dictionary
        /// </summary>
        private IDictionary<string, SqlTypeValue> MappingAttributte<T>(T t)
            where T : DBObject, new()
        {
            if (t == null)
                throw new Exception("client post a null entity,plz check it again.");
            IDictionary<string, SqlTypeValue> dic = new Dictionary<string, SqlTypeValue>();
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<DataTableAttribute>().TableName;
            dic.Add("tablename", new SqlTypeValue() { Value = tableName, Type = "datatable" });
            string fieldName, value, valuetype;
            foreach (PropertyInfo pro in type.GetProperties())
            {
                if (pro.GetValue(t) != null)
                {
                    value = pro.GetValue(t).ToString();
                    valuetype = pro.GetValue(t).GetType().ToString();
                    //排除DbTypeAction
                    if (valuetype == "ORM.CURDActionEnum")
                        break;
                    fieldName = pro.GetCustomAttribute<DataFieldAttribute>().DataFieldName;
                    //已添加了具有相同建的項
                    var p = type.GetProperties();
                    if (!dic.ContainsKey(fieldName))
                        dic.Add(fieldName, new SqlTypeValue() { Value = value, Type = valuetype });
                }
            }
            return dic;
        }

        public object GetList<T>(Expression expwhere, Expression expsort, bool desc, int rowCount, int pageIndex)
            where T :DBObject,new()
        {
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<DataTableAttribute>().TableName;

            StringBuilder sb = new StringBuilder();
            bool ispaging = (rowCount != 0 && pageIndex != 0);
            //paging
            if (ispaging)
            {
                sb.Append("select top * " + rowCount + " from ( select ROW_NUMBER() OVER(ORDER BY id) AS rownumber,* from " + tableName + " ) temp ");
            }
            else
            {
                sb.Append("select * from " + tableName);
            }

            if (ispaging)
                sb.Append("where temp.rownumber > " + (rowCount * (pageIndex - 1)));
            //where
            if (expwhere != null)
            {
                ConditionBuilder builder = new ConditionBuilder();
                builder.Build(expwhere);
                string sqlwhere = string.Format(builder.Condition, builder.Arguments); //reutrn where remark = {dd}
                if (builder.Arguments.Length > 0)
                {
                    if (ispaging)
                    {
                        sb.Append(" and " + sqlwhere);
                    }
                    else
                    {
                        sb.Append(" where " + sqlwhere);
                    }

                }
            }
            //order by
            string isdesc = desc ? " desc" : "";
            if (expsort != null)
            {
                ConditionBuilder sortBuilder = new ConditionBuilder();
                sortBuilder.Build(expsort);
                if (sortBuilder.Arguments.Length > 0)
                {
                    string sqlsort = GetSortField(sortBuilder.Condition);
                    sb.Append(" order by " + sqlsort + isdesc);
                }
            }

            return (List<T>)ExecuteScalar<T>(sb.ToString());
        }
        private string GetSortField(string str)
        {
            int startIndex = str.IndexOf("[");
            int last = str.IndexOf("]");
            return str.Substring(startIndex, last - startIndex + 1);
        }

        public T GetModel<T>(Expression expwhere) 
            where T:DBObject,new()

        {
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<DataTableAttribute>().TableName;
            StringBuilder sb = new StringBuilder();
            sb.Append("select top 1 * from " + tableName);
            if (expwhere != null)
            {
                ConditionBuilder builder = new ConditionBuilder();
                builder.Build(expwhere);
                string sqlwhere = string.Format(builder.Condition, builder.Arguments); //reutrn where remark = {dd}
                if (builder.Arguments.Length > 0)
                {
                    sb.Append(" where " + sqlwhere);
                }
            }
            return (T)(ExecuteScalar<T>(sb.ToString()).FirstOrDefault());
        }

        public void Remove<T>(Expression expwhere)
        {
            Type type = typeof(T);
            string tableName = type.GetCustomAttribute<DataTableAttribute>().TableName;
            if (expwhere != null)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("delete from " + tableName);
                ConditionBuilder builder = new ConditionBuilder();
                builder.Build(expwhere);
                string sqlwhere = string.Format(builder.Condition, builder.Arguments); //reutrn where remark = {dd}
                if (builder.Arguments.Length > 0)
                {
                    sb.Append(" where " + sqlwhere);
                }
                ExecuteNonQuery(sb.ToString());
            }
            else
            {
                throw new InvalidExpressionException("Invalid Expression,Expression tree is null ");
            }
        }

        public string GetDeleteSQL(IDictionary<string, SqlTypeValue> dic)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from " + dic["tablename"].Value);
            sb.Append(" where ");
          foreach (var item in dic)
            {
                    if (item.Key.ToLower()=="id")
                    {
                        sb.Append(" " + item.Key + "=" + JudgeSqlTypeAndCreate(item.Value) );
                    }                
            }
            return sb.ToString();
        }

        private string GetCreateSQL(IDictionary<string, SqlTypeValue> dic)
        {
            if (dic == null)
                throw new Exception("dictionary is null ..check the parameter");
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into " + dic["tablename"].Value);
            sb.Append(" (");
            foreach (var item in dic)
            {
                if (item.Key.ToLower() != "tablename")
                {
                    sb.Append(" " + item.Key + ",");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(") values (");
            foreach (var item in dic)
            {
                if (item.Key.ToLower() != "tablename")
                {
                    sb.Append("" + JudgeSqlTypeAndCreate(item.Value) + ",");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            return sb.ToString();
        }

        public string GetUpdateSQL(IDictionary<string, SqlTypeValue> dic)
        {
            //condition: only search column name contain id and modify it.  filter uses 'or'
            StringBuilder sb = new StringBuilder();
            sb.Append("update " + dic["tablename"].Value);
            sb.Append(" set ");
            foreach (var item in dic)
            {
                if (item.Key.ToLower() != "tablename")
                {
                    sb.Append(item.Key + "=" + JudgeSqlTypeAndCreate(item.Value) + " ,");
                }
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" where ");
            foreach (var item in dic)
            {
                    if (item.Key.ToLower()=="id")
                    {
                        sb.Append(" " + item.Key + "=" + JudgeSqlTypeAndCreate(item.Value) );
                    }                
            }
            //sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private string JudgeSqlTypeAndCreate(SqlTypeValue item)
        {
            var type = item.Type;
            if (type.Contains("int") || type.Contains("double") || type.Contains("float"))
            {
                return item.Value;
            }
            else if (type.Contains("nvarchar"))
            {
                return "N'" + item.Value + "'";
            }
            else   //contains bool  datetime  
            {
                return "'" + item.Value + "'";
            }
        }

        private List<T> GetEntities<T>(DataTable datatable) where T : DBObject, new()
        {
            List<T> list = new List<T>();
            foreach (DataRow row in datatable.Rows)
            {
                list.Add((T)Convert2Object(row, typeof(T)));
            }
            return list;
        }

        /// <summary>
        /// 轉換實體
        /// </summary>
        /// <param name="p">DataRow</param>
        /// <param name="type">轉換的目標類型</param>
        /// <returns></returns>
        private object Convert2Object(DataRow row, Type type)
        {
            var instance = type.Assembly.CreateInstance(type.FullName.ToString());
            string colunmName;
            foreach (PropertyInfo pro in type.GetProperties())
            {
                if (pro.Name == "DbActionType")
                    break;
                if (pro.GetCustomAttribute<DataFieldAttribute>().DataFieldName != null)
                {
                    colunmName = pro.GetCustomAttribute<DataFieldAttribute>().DataFieldName;
                    var s = row[colunmName];
                    pro.SetValue(instance, row[colunmName]);
                }

            }

            return instance;
        }

    }
    public class SqlTypeValue
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
