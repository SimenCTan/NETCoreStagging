using DapperExtensions.Mapper;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace KYExpress.Dapper.Expressions
{
    /// <summary>
    /// 重写Dapper映射
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KYExpressPluralizedAutoClassMapper<T> : PluralizedAutoClassMapper<T> where T : class
    {
        public KYExpressPluralizedAutoClassMapper()
        {
            Type type = typeof(T);
            var tableAttribute = type.GetCustomAttribute<TableAttribute>();

            string tableName = type.Name;
            if (tableAttribute != null)
            {
                tableName = tableAttribute.Name;
            }
            Table(tableName);
        }

        protected override void AutoMap()
        {
            base.AutoMap();
            foreach (PropertyMap propertyMap in Properties)
            {
                var attributes = propertyMap.PropertyInfo.GetCustomAttributes();
                foreach (var attribute in attributes)
                {
                    if(attribute is NotMappedAttribute)
                    {
                        propertyMap.Ignore();
                    }
                    if (attribute is ColumnAttribute)
                    {
                        var columnAttribute = attribute as ColumnAttribute;
                        propertyMap.Column(columnAttribute.Name);
                    }
                    if (attribute is KeyAttribute)
                    {
                        propertyMap.Key(KeyType.Identity);
                    }
                }
            }
        }

        public override void Table(string tableName)
        {
            TableName = tableName;
        }
    }
}
