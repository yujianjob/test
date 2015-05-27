using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.DataContract.web
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class web_Mall_ProductDC : EntityBase
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
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        [DataMember]
        public string Picture { set; get; }

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
        public static web_Mall_ProductDC GetEntity(IDataReader reader)
        {
            web_Mall_ProductDC entity = null;

            entity = new web_Mall_ProductDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.Picture))
            {
                entity.Picture = System.Configuration.ConfigurationManager.AppSettings["MallProduct_Picture"] + entity.Picture;
            }
            return entity;
        }
    }
}
