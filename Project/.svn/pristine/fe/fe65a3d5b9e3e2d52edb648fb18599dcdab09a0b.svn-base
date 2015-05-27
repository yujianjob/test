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
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Exp_OrderDC : EntityBase
    {
        #region Model
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
        /// 运输类型
        /// </summary>
        [Display(Name = "运输类型")]
        [DataMember]
        public int TransportType { set; get; }

        /// <summary>
        /// 加急类型
        /// </summary>
        [Display(Name = "加急类型")]
        [DataMember]
        public int QuickType { set; get; }

        /// <summary>
        /// 行政区
        /// </summary>
        [Display(Name = "行政区")]
        [DataMember]
        public int DistrictID { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [DataMember]
        public string Contacts { set; get; }

        /// <summary>
        /// 联系手机
        /// </summary>
        [Display(Name = "联系手机")]
        [DataMember]
        public string Mpno { set; get; }

        /// <summary>
        /// 预计时间
        /// </summary>
        [Display(Name = "预计时间")]
        [DataMember]
        public DateTime ExpTime { set; get; }

        /// <summary>
        /// 联系用户状态
        /// </summary>
        [Display(Name = "联系用户状态")]
        [DataMember]
        public int CallUserStatus { set; get; }

        /// <summary>
        /// 联系用户时间
        /// </summary>
        [Display(Name = "联系用户时间")]
        [DataMember]
        public DateTime? CallUserTime { set; get; }

        /// <summary>
        /// 联系用户时长
        /// </summary>
        [Display(Name = "联系用户时长")]
        [DataMember]
        public int CallUserSecond { set; get; }

        /// <summary>
        /// 揽件人
        /// </summary>
        [Display(Name = "揽件人")]
        [DataMember]
        public int? OperatorID { set; get; }

        /// <summary>
        /// 站点ID
        /// </summary>
        [Display(Name = "站点ID")]
        [DataMember]
        public int? NodeID { set; get; }

        /// <summary>
        /// 接单时间
        /// </summary>
        [Display(Name = "接单时间")]
        [DataMember]
        public DateTime? TakeTime { set; get; }

        /// <summary>
        /// 揽件时间
        /// </summary>
        [Display(Name = "揽件时间")]
        [DataMember]
        public DateTime? OperatorTime { set; get; }

        /// <summary>
        /// 完成时间
        /// </summary>
        [Display(Name = "完成时间")]
        [DataMember]
        public DateTime? CompleteTime { set; get; }

        /// <summary>
        /// 分配状态
        /// </summary>
        [Display(Name = "分配状态")]
        [DataMember]
        public int AllotStatus { set; get; }

        /// <summary>
        /// 分配时间
        /// </summary>
        [Display(Name = "分配时间")]
        [DataMember]
        public DateTime? AllotTime { set; get; }

        /// <summary>
        /// 纬度
        /// </summary>
        [DataMember]
        public decimal? Latitude { set; get; }

        /// <summary>
        /// 经度
        /// </summary>
        [DataMember]
        public decimal? Longitude { set; get; }

        /// <summary>
        /// 货物内容
        /// </summary>
        [Display(Name = "货物内容")]
        [DataMember]
        public string PackageInfo { set; get; }

        /// <summary>
        /// 货物数量
        /// </summary>
        [Display(Name = "货物数量")]
        [DataMember]
        public int PackageCount { set; get; }

        /// <summary>
        /// 代收货款
        /// </summary>
        [Display(Name = "代收货款")]
        [DataMember]
        public decimal ChargeFee { set; get; }

        /// <summary>
        /// 步骤
        /// </summary>
        [Display(Name = "步骤")]
        [DataMember]
        public int Step { set; get; }

        /// <summary>
        /// 步骤说明
        /// </summary>
        [Display(Name = "步骤说明")]
        [DataMember]
        public string StepRemark { set; get; }

        /// <summary>
        /// 用户备注
        /// </summary>
        [Display(Name = "用户备注")]
        [DataMember]
        public string UserRemark { set; get; }

        /// <summary>
        /// 客服备注
        /// </summary>
        [Display(Name = "客服备注")]
        [DataMember]
        public string CSRemark { set; get; }

        /// <summary>
        /// 系统备注
        /// </summary>
        [Display(Name = "系统备注")]
        [DataMember]
        public string SystemRemark { set; get; }

        /// <summary>
        /// 邀请码
        /// </summary>
        [Display(Name = "邀请码")]
        [DataMember]
        public string InviteCode { set; get; }

        /// <summary>
        /// 是否报警
        /// </summary>
        [Display(Name = "是否报警")]
        [DataMember]
        public int Alarm { set; get; }

        /// <summary>
        /// 待处理
        /// </summary>
        [Display(Name = "待处理")]
        [DataMember]
        public int WaitProcess { set; get; }

        #endregion Model

        /// <summary>
        /// 行政区
        /// </summary>
        [Display(Name = "行政区")]
        [DataMember]
        public string DistrictName { set; get; }

        /// <summary>
        /// 站点编号
        /// </summary>
        [DataMember]
        public string NodeNo { set; get; }

        /// <summary>
        /// 站点名称
        /// </summary>
        [DataMember]
        public string NodeName { set; get; }

        /// <summary>
        /// 干线
        /// </summary>
        [DataMember]
        public int LineID { set; get; }

        /// <summary>
        /// 干线
        /// </summary>
        [DataMember]
        public string LineNo { set; get; }

        /// <summary>
        /// 干线
        /// </summary>
        [DataMember]
        public string LineName { set; get; }

        /// <summary>
        /// 站点地址
        /// </summary>
        [Display(Name = "站点地址")]
        [DataMember]
        public string NodeAddress { set; get; }

        /// <summary>
        /// 揽件人名称
        /// </summary>
        [DataMember]
        public string OperatorName { set; get; }

        /// <summary>
        /// 送货数量
        /// </summary>
        [Display(Name = "送货数量")]
        [DataMember]
        public int SendCount { set; get; }

        /// <summary>
        /// 库存信息
        /// </summary>
        [DataMember]
        public IList<Exp_StorageItemDC> StorageItemList { set; get; }

        /// <summary>
        /// 根据Reader生成Exp_OrderDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Exp_OrderDC GetEntity(IDataReader reader)
        {
            Exp_OrderDC entity = null;

            entity = new Exp_OrderDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
