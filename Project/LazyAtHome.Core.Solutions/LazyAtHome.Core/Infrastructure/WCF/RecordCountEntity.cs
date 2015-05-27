using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LazyAtHome.Core.Infrastructure.WCF
{
    /// <summary>
    /// 通用返回记录数对象
    /// </summary>
    [DataContract]
    [Serializable]
    public class RecordCountEntity<T> where T : class
    {
        /// <summary>
        /// 记录数
        /// </summary>
        [DataMember]
        public int RecordCount { get; set; }

        /// <summary>
        /// 返回对象
        /// </summary>
        [DataMember]
        public IList<T> ReturnList { get; set; }
    }
}