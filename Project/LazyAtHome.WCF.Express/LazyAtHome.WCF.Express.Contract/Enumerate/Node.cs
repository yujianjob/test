using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LazyAtHome.WCF.Express.Contract.Enumerate
{

    /// <summary>
    /// 站点类型
    /// </summary>
    [DataContract]
    public enum NodeType
    {
        /// <summary>
        ///  1:站点
        /// </summary>
        [EnumMember]
        Site = 1,

        /// <summary>
        ///  2:干线
        /// </summary>
        [EnumMember]
        Line = 2,

        /// <summary>
        ///  3:工厂
        /// </summary>
        [EnumMember]
        Factory = 3,
    }
}
