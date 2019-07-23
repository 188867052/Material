﻿namespace MaterialUI.Dapper
{
    using System;
    using System.Linq;
    using System.Reflection;
    using global::Dapper;

    /// <summary>
    /// Uses the Name value of the ColumnAttribute specified, otherwise maps as usual.
    /// </summary>
    public class ColumnAttributeTypeMapper : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper(Type type)
            : base(new SqlMapper.ITypeMap[] { new CustomPropertyTypeMap(type, PropertySelector), new DefaultTypeMap(type), })
        {
        }

        private static PropertyInfo PropertySelector(Type type, string columnName)
        {
            return type.GetProperties()
                    .FirstOrDefault(prop => prop.GetCustomAttributes(false)
                    .OfType<ColumnMappingAttribute>()
                    .Any(attr => attr.Name == columnName));
        }

        [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
        public class ColumnMappingAttribute : Attribute
        {
            public string Name { get; set; }
        }
    }
}