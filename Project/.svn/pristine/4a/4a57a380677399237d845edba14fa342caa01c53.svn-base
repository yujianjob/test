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
    public class Luxury_ProductDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
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
        /// 类别ID
        /// </summary>
        [Display(Name = "类别ID")]
        [DataMember]
        public int ClassID { set; get; }

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
        /// 服务费率
        /// </summary>
        [Display(Name = "服务费率")]
        [DataMember]
        public decimal ServiceFeeRate { set; get; }

        /// <summary>
        /// 最低价
        /// </summary>
        [Display(Name = "最低价")]
        [DataMember]
        public int MinPrice { set; get; }

        /// <summary>
        /// 最高价
        /// </summary>
        [Display(Name = "最高价")]
        [DataMember]
        public int? MaxPrice { set; get; }

        /// <summary>
        /// 价格区间
        /// </summary>
        [Display(Name = "价格区间")]
        [DataMember]
        public int? IntervalPrice { set; get; }

        /// <summary>
        /// 确认状态
        /// </summary>
        [Display(Name = "确认状态")]
        [DataMember]
        public int CommitStatus { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Luxury_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Luxury_ProductDC GetEntity(IDataReader reader)
        {
            Luxury_ProductDC entity = null;

            entity = new Luxury_ProductDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
