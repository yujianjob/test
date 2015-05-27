using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Wash.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Mall_ProductDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 类别
        /// </summary>
        [Display(Name = "类别")]
        [DataMember]
        public int Class { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 搜索关键字
        /// </summary>
        [Display(Name = "搜索关键字")]
        [DataMember]
        public string Keyword { set; get; }

        /// <summary>
        /// 类型用数值
        /// </summary>
        [Display(Name = "类型用数值")]
        [DataMember]
        public string TypeValue { set; get; }

        /// <summary>
        /// 总数量
        /// </summary>
        [Display(Name = "总数量")]
        [DataMember]
        public int TotalCount { set; get; }

        /// <summary>
        /// 剩余数量
        /// </summary>
        [Display(Name = "剩余数量")]
        [DataMember]
        public int LeftCount { set; get; }

        /// <summary>
        /// 已销售数量
        /// </summary>
        [Display(Name = "已销售数量")]
        [DataMember]
        public int SaleCount { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        [Display(Name = "站点")]
        [DataMember]
        public int Site { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [DataMember]
        public int Sort { set; get; }

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
        /// 启用状态
        /// </summary>
        [Display(Name = "启用状态")]
        [DataMember]
        public int Enable { set; get; }

        /// <summary>
        /// 大图
        /// </summary>
        [Display(Name = "大图")]
        [DataMember]
        public string PictureL { set; get; }

        /// <summary>
        /// 中图
        /// </summary>
        [Display(Name = "中图")]
        [DataMember]
        public string PictureM { set; get; }

        /// <summary>
        /// 小图
        /// </summary>
        [Display(Name = "小图")]
        [DataMember]
        public string PictureS { set; get; }

        /// <summary>
        /// 销售开始时间
        /// </summary>
        [Display(Name = "销售开始时间")]
        [DataMember]
        public DateTime SellBeginDate { set; get; }

        /// <summary>
        /// 销售结束时间
        /// </summary>
        [Display(Name = "销售结束时间")]
        [DataMember]
        public DateTime SellEndDate { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Mall_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Mall_ProductDC GetEntity(IDataReader reader)
        {
            Mall_ProductDC entity = null;

            entity = new Mall_ProductDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
