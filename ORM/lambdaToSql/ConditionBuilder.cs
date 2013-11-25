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
    /// <summary>
    /// ExpressionVisitor 是 Expression Tree 的遍历器
    /// </summary>
   internal class ConditionBuilder:ExpressionVisitor
    {
       private List<object> _m_arguments;
       private Stack<string> _m_conditionParts;

       public string Condition { get; private set; }
       public object[] Arguments { get; private set; }

       public ConditionBuilder()
       {
           _m_arguments = new List<object>();
           _m_conditionParts = new Stack<string>();
       }

       public void Build(Expression expression)
       {
           PartialEvaluator evaluator = new PartialEvaluator();
           Expression evaluatorExp = evaluator.Eval(expression);

           this.Visit(evaluatorExp);

           this.Arguments = this._m_arguments.ToArray();
           this.Condition = this._m_conditionParts.Count > 0 ? this._m_conditionParts.Pop() : null;
       }

       protected override Expression VisitBinary(BinaryExpression b)
       {
           if (b == null) return b;

           string opr;
           switch (b.NodeType)
           {
               case ExpressionType.Equal:
                   opr = "=";
                   break;
               case ExpressionType.NotEqual:
                   opr = "<>";
                   break;
               case ExpressionType.GreaterThan:
                   opr = ">";
                   break;
               case ExpressionType.GreaterThanOrEqual:
                   opr = ">=";
                   break;
               case ExpressionType.LessThan:
                   opr = "<";
                   break;
               case ExpressionType.LessThanOrEqual:
                   opr = "<=";
                   break;
               case ExpressionType.AndAlso:
                   opr = "AND";
                   break;
               case ExpressionType.OrElse:
                   opr = "OR";
                   break;
               case ExpressionType.Add:
                   opr = "+";
                   break;
               case ExpressionType.Subtract:
                   opr = "-";
                   break;
               case ExpressionType.Multiply:
                   opr = "*";
                   break;
               case ExpressionType.Divide:
                   opr = "/";
                   break;
               default:
                   throw new NotSupportedException(b.NodeType + " is not supported.");
           }

           this.Visit(b.Left);
           this.Visit(b.Right);

           string right = this._m_conditionParts.Pop();
           string left = this._m_conditionParts.Pop();

           string condition = String.Format("({0} {1} {2})", left, opr, right);
           this._m_conditionParts.Push(condition);

           return b;
       }

       //return format value
       protected override Expression VisitConstant(ConstantExpression c)
       {
           if (c == null) return c;

           this._m_arguments.Add("'"+c.Value+"'");
           this._m_conditionParts.Push(String.Format("{{{0}}}", this._m_arguments.Count - 1));

           return c;
       }

       //return format key
       protected override Expression VisitMemberAccess(MemberExpression m)
       {
           if (m == null) return m;

           PropertyInfo propertyInfo = m.Member as PropertyInfo;
           if (propertyInfo == null) return m;
           string fieldName = propertyInfo.GetCustomAttribute<DataFieldAttribute>().DataFieldName;
           this._m_conditionParts.Push(String.Format("[{0}]", fieldName));

           return m;
       }

       protected override Expression VisitMethodCall(MethodCallExpression m)
       {
           if (m == null) return m;

           string format;
           switch (m.Method.Name)
           {
               case "StartsWith":
                   format = "({0} LIKE {1}+'%')";
                   break;

               case "Contains":
                   format = "({0} LIKE '%'+{1}+'%')";
                   break;

               case "EndsWith":
                   format = "({0} LIKE '%'+{1})";
                   break;

               default:
                   throw new NotSupportedException(m.NodeType + " is not supported!");
           }

           this.Visit(m.Object);
           this.Visit(m.Arguments[0]);
           string right = this._m_conditionParts.Pop();
           string left = this._m_conditionParts.Pop();
           this._m_conditionParts.Push(String.Format(format, left, right));

           return m;
       }
    }
}
