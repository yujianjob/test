using System;
using System.Runtime.Serialization;

namespace LazyAtHome.Core.Infrastructure.WCF
{
    [DataContract]
    [Serializable]
    [KnownTypeAttribute(typeof(DBNull))]
    public class UpdateParamEntity
    {
        private string _Name;
        private object _OldValue;
        private object _NewValue;
        private System.Data.DbType _DbType;
        private bool _IsPrimaryKey;

        [DataMember]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [DataMember]
        public object OldValue
        {
            get { return _OldValue; }
            set { _OldValue = value; }
        }

        [DataMember]
        public object NewValue
        {
            get { return _NewValue; }
            set { _NewValue = value; }
        }

        [DataMember]
        public System.Data.DbType DbType
        {
            get { return _DbType; }
            set { _DbType = value; }
        }

        [DataMember]
        public bool IsPrimaryKey
        {
            get { return _IsPrimaryKey; }
            set { _IsPrimaryKey = value; }
        }
    }
}
