using LiteMapper.Attributes;
using LiteMapper.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace LiteMapper.Extensions
{
    public static class ObjectMapper
    {
        public static TDestination Map<TSource, TDestination>(TSource source)
            where TDestination : new()
        {
            if (source == null) return default;

            var destination = new TDestination();
            var sourceType = typeof(TSource);
            var destType = typeof(TDestination);

            foreach (var destProp in destType.GetProperties())
            {
                if (!destProp.CanWrite) continue;

                string sourcePropName = destProp.GetCustomAttribute<MapFromAttribute>()?.SourceProperty ?? destProp.Name;

                var sourceProp = sourceType.GetProperty(sourcePropName);
                if (sourceProp == null || !sourceProp.CanRead) continue;

                var sourceValue = sourceProp.GetValue(source);
                if (sourceValue == null) continue;

                // Direct match
                if (destProp.PropertyType == sourceProp.PropertyType)
                {
                    destProp.SetValue(destination, sourceValue);
                }
                // Collection
                else if (TypeConversionHelper.IsEnumerable(destProp.PropertyType, out Type destItemType) &&
                         TypeConversionHelper.IsEnumerable(sourceProp.PropertyType, out Type sourceItemType))
                {
                    var mappedCollection = TypeConversionHelper.MapCollection(sourceValue, sourceItemType, destItemType);
                    destProp.SetValue(destination, mappedCollection);
                }
                // Type conversion
                else if (TypeConversionHelper.TryConvertValue(sourceValue, destProp.PropertyType, out object converted))
                {
                    destProp.SetValue(destination, converted);
                }
                // Nested mapping
                else if (TypeConversionHelper.IsComplexType(destProp.PropertyType) &&
                         TypeConversionHelper.IsComplexType(sourceProp.PropertyType))
                {
                    var mapMethod = typeof(ObjectMapper)
                        .GetMethod(nameof(Map), BindingFlags.Public | BindingFlags.Static)
                        ?.MakeGenericMethod(sourceProp.PropertyType, destProp.PropertyType);

                    var nested = mapMethod?.Invoke(null, new object[] { sourceValue });
                    destProp.SetValue(destination, nested);
                }
            }

            return destination;
        }
    }

}
