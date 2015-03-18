using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonHelper
{
    public class UtilsHepler
    {
        #region DTO  Converter

        public static object ToEntity(Type targetType, object sourceModel)
        {
            //match property name and Assignment
            var result = Convert2Target(targetType, sourceModel);
            return result;
        }

        public static object ToModel(Type targetType, object sourceEntity)
        {
            var result = Convert2Target(targetType, sourceEntity);
            return result;
        }

        private static object Convert2Target(Type targetType, object source)
        {
            var result = Activator.CreateInstance(targetType);
            Type sourceType = source.GetType();
            var properties = sourceType.GetProperties();
            var targerProperties = targetType.GetProperties();
            foreach (var item in properties)
            {
                foreach (var property in targerProperties)
                {
                    if (property.Name == item.Name
                        && property.PropertyType == item.PropertyType)
                    {
                        var value = item.GetValue(source);
                        property.SetValue(result, value);
                        break;
                    }
                }
            }
            return result;
        }
        #endregion
    }
}
