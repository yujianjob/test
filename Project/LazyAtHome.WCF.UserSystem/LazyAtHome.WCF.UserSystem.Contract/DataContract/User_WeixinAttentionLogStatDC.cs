using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;
using System.Runtime.Serialization;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_WeixinAttentionLogStatDC
    {
        /// <summary>
        /// 统计日期
        /// </summary>
        [DataMember]
        public DateTime StatDate { get; set; }

        /// <summary>
        /// 注册来源类型
        /// </summary>
        [DataMember]
        public int RegisterSourceType { get; set; }

        /// <summary>
        /// 关注数
        /// </summary>
        [DataMember]
        public int AttentionCount { get; set; }

        /// <summary>
        /// 取消关注数
        /// </summary>
        [DataMember]
        public int RemoveAttentionCount { get; set; }

        /// <summary>
        /// 注册会员数
        /// </summary>
        [DataMember]
        public int RegisterCount { get; set; }
    }
}
