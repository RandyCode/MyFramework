using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace CommonHelper
{
    public abstract class BaseLogger  
    {
        internal ILog Log { get; set; }

        public abstract void Write(string message,object arguments);

        protected object GetArgumentsValue(object args,string PropertyName)
        {
            if (args == null)
                return null;

            Type type = args.GetType();
            var properties = type.GetProperties();
            foreach (var pro in properties)
            {
                if (pro.Name.ToUpper() == PropertyName.Trim().ToUpper())
                {
                    var result = pro.GetValue(args);
                    return result;
                }
            }

            return null;
        }

    }
}
