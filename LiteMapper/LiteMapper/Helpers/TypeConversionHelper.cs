using LiteMapper.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LiteMapper.Helpers
{
    public static class TypeConversionHelper
    {
        public static bool TryConvertValue(object input, Type targetType, out object result)
        {
            try
            {
                if (targetType.IsEnum && input is string str)
                {
                    result = Enum.Parse(targetType, str);
                    return true;
                }

                result = Convert.ChangeType(input, targetType);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public static bool IsComplexType(Type type)
        {
            return !(type.IsPrimitive || type.IsEnum || type == typeof(string) || type == typeof(DateTime));
        }

        public static bool IsEnumerable(Type type, out Type elementType)
        {
            elementType = null;

            if (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type))
            {
                elementType = type.GetGenericArguments()[0];
                return true;
            }

            return false;
        }

        public static object MapCollection(object sourceCollection, Type sourceItemType, Type destItemType)
        {
            var listType = typeof(List<>).MakeGenericType(destItemType);
            var list = (IList)Activator.CreateInstance(listType);

            //var mapMethod = typeof(ObjectMapper.Extensions.ObjectMapper)
            //    .GetMethod(nameof(ObjectMapper.Extensions.ObjectMapper.Map))
            //    ?.MakeGenericMethod(sourceItemType, destItemType);
            var mapMethod = typeof(ObjectMapper)
                .GetMethod(nameof(ObjectMapper.Map))
                ?.MakeGenericMethod(sourceItemType, destItemType);

            foreach (var item in (IEnumerable)sourceCollection)
            {
                var mappedItem = mapMethod.Invoke(null, new object[] { item });
                list.Add(mappedItem);
            }

            return list;
        }
    }
}
