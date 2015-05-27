using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;

namespace LazyAtHome.Core.Infrastructure.WCF
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class DBAttribute : Attribute
    {
        public DBAttribute()
        {
            IsCommon = false;
            IsIdentity = false;
        }

        public DBAttribute(string columnName, DbType columnType)
            : this()
        {
            ColumnName = columnName;
            ColumnType = columnType;
        }

        public string TableName { get; set; }
        public bool IsPrimeKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsCommon { get; set; }

        public string ColumnName { get; set; }
        public DbType ColumnType { get; set; }
    }

    public class ColumnMetadata
    {
        public bool IsPrimeKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsCommon { get; set; }
        public DbType DbType { get; set; }
        public PropertyInfo Property { get; set; }
    }

    public class DBMetadata
    {
        public DBMetadata()
        {
            Columns = new Dictionary<string, ColumnMetadata>();
            PColumns = new Dictionary<string, string>();
        }

        public void Add(string key, ColumnMetadata value)
        {
            if (value.IsPrimeKey) PrimeKey = key;
            Columns.Add(key, value);
            PColumns.Add(value.Property.Name, key);
        }

        public string TableName { get; set; }
        public string PrimeKey { get; set; }
        public IDictionary<string, ColumnMetadata> Columns { get; set; }
        public IDictionary<string, string> PColumns { get; set; }
    }
}
