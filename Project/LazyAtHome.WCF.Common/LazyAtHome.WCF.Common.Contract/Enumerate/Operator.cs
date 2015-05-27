using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.Enumerate
{
    /// <summary>
    /// 登录类型
    /// </summary>
    [DataContract]
    public enum OperatorType
    {
        /// <summary>
        /// 全部
        /// </summary>
        [EnumMember]
        ALL = -1,

        /// <summary>
        /// 后台管理工具
        /// </summary>
        [EnumMember]
        Manage = 1,

        /// <summary>
        /// 工厂
        /// </summary>
        [EnumMember]
        Factory = 2,

        /// <summary>
        ///  3:站点
        /// </summary>
        [EnumMember]
        Site = 3,

        /// <summary>
        ///  4:干线
        /// </summary>
        [EnumMember]
        Line = 4,

    }
}
