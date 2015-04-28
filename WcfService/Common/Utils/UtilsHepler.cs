using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

        public static object DeepClone(object obj)
        {

            IFormatter bf = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            bf.Serialize(stream, obj);
            stream.Close();


            Stream stream1 = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            var result = bf.Deserialize(stream1);
            stream1.Close();

            return result;
        }
    }
}
