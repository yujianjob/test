using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Express.Contract.DataContract
{
    /// <summary>
    /// 物流推送
    /// </summary>
    [DataContract]
    [Serializable]
    public class Exp_RoutePushDC
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 物流单号
        /// </summary>
        [Display(Name = "物流单号")]
        [DataMember]
        public string ExpNumber { set; get; }

        /// <summary>
        /// 外部单号
        /// </summary>
        [Display(Name = "外部单号")]
        [DataMember]
        public string OutNumber { set; get; }

        /// <summary>
        /// 操作代码
        /// </summary>
        [Display(Name = "操作代码")]
        [DataMember]
        public string OpCode { set; get; }

        /// <summary>
        /// 流转信息
        /// </summary>
        [Display(Name = "流转信息")]
        [DataMember]
        public string RouteInfo { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Display(Name = "备注")]
        [DataMember]
        public string Remark { set; get; }

        [DataMember]
        public string Obj_Cdate { set; get; }
    }
}
