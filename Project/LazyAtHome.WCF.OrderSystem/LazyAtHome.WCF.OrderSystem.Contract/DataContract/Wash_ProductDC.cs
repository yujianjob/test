using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Contract.DataContract
{    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Wash_ProductDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 网站名称
        /// </summary>
        [Display(Name = "网站名称")]
        [DataMember]
        public string WebName { set; get; }

        /// <summary>
        /// 代码
        /// </summary>
        [Display(Name = "代码")]
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        [DataMember]
        public int CategoryID { set; get; }

        ///// <summary>
        ///// 销售状态
        ///// </summary>
        //[Display(Name = "销售状态")]
        //[DataMember]
        //public int SaleStatus { set; get; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        [Display(Name = "搜索关键字")]
        [DataMember]
        public string Keyword { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 市场价
        /// </summary>
        [Display(Name = "市场价")]
        [DataMember]
        public decimal MarketPrice { set; get; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Display(Name = "销售价")]
        [DataMember]
        public decimal SalesPrice { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        [Display(Name = "站点")]
        [DataMember]
        public int Site { set; get; }

        /// <summary>
        /// 审核状态
        /// </summary>
        [Display(Name = "审核状态")]
        [DataMember]
        public int VerifyStatus { set; get; }

        /// <summary>
        /// 审核提交人ID
        /// </summary>
        [Display(Name = "审核提交人ID")]
        [DataMember]
        public int? SubmitOperatorID { set; get; }

        /// <summary>
        /// 提交审核时间
        /// </summary>
        [Display(Name = "提交审核时间")]
        [DataMember]
        public DateTime? SubmitDate { set; get; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        [Display(Name = "审核人ID")]
        [DataMember]
        public int? AduitOperatorID { set; get; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [Display(Name = "审核时间")]
        [DataMember]
        public DateTime? AduitDate { set; get; }

        /// <summary>
        /// 审核人意见
        /// </summary>
        [Display(Name = "审核人意见")]
        [DataMember]
        public string Comment { set; get; }

        /// <summary>
        /// 确认状态
        /// </summary>
        [Display(Name = "确认状态")]
        [DataMember]
        public int CommitStatus { set; get; }

        /// <summary>
        /// 上线人ID
        /// </summary>
        [Display(Name = "上线人ID")]
        [DataMember]
        public int? OnOperatorID { set; get; }

        /// <summary>
        /// 上线时间
        /// </summary>
        [Display(Name = "上线时间")]
        [DataMember]
        public DateTime? OnDate { set; get; }

        /// <summary>
        /// 下线人ID
        /// </summary>
        [Display(Name = "下线人ID")]
        [DataMember]
        public int? OffOperatorID { set; get; }

        /// <summary>
        /// 下线时间
        /// </summary>
        [Display(Name = "下线时间")]
        [DataMember]
        public DateTime? OffDate { set; get; }

        #endregion Model

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        [DataMember]
        public string CategoryName { set; get; }

        ///// <summary>
        ///// 图片列表
        ///// </summary>
        //[DataMember]
        //public IList<Wash_ProductPictureDC> PictureList { get; set; }

        /// <summary>
        /// 根据Reader生成Wash_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_ProductDC GetEntity(System.Data.IDataReader reader)
        {
            Wash_ProductDC entity = null;

            entity = new Wash_ProductDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
