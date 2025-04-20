using System;
using System.Collections.Generic;
using System.Text;

namespace LiteMapper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class MapFromAttribute : Attribute
    {
        public string SourceProperty { get; }
        public MapFromAttribute(string sourceProperty)
        {
            SourceProperty = sourceProperty;
        }
    }
}
