using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public
    class web_Wash_CategoryDC : EntityBase
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

        ///// <summary>
        ///// 描述
        ///// </summary>
        //[Display(Name = "描述")]
        //[DataMember]
        //public string Content { set; get; }

        /// <summary>
        /// 中图
        /// </summary>
        [Display(Name = "中图")]
        [DataMember]
        public string PictureM { set; get; }

        /// <summary>
        /// 大图
        /// </summary>
        [Display(Name = "大图")]
        [DataMember]
        public string PictureL { set; get; }

        /// <summary>
        /// 图片描述
        /// </summary>
        [Display(Name = "图片描述")]
        [DataMember]
        public string PictureAlt { set; get; }

        /// <summary>
        /// 销售价
        /// </summary>
        [Display(Name = "销售价")]
        [DataMember]
        public decimal SalesPrice { set; get; }

        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ClassID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ClassName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ParentClassID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ParentClassName { get; set; }

        /// <summary>
        /// 同类推荐
        /// </summary>
        [DataMember]
        public IList<web_Wash_ProductDC> GroupList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SalesPriceInterval
        {
            get
            {
                if (ProductList != null && ProductList.Count > 0)
                {
                    var min = ProductList.Min(p => p.SalesPrice);
                    var max = ProductList.Max(p => p.SalesPrice);
                    if (min == max)
                    {
                        return min.ToString("0.00");
                    }
                    else
                    {
                        return min.ToString("0.00") + "-" + max.ToString("0.00");
                    }
                }
                else
                {
                    return "-";
                }
            }
            set { }
        }

        [DataMember]
        public IList<web_Wash_ProductDC> ProductList { get; set; }

        /// <summary>
        /// 根据Reader生成Wash_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static web_Wash_CategoryDC GetEntity(System.Data.IDataReader reader)
        {
            web_Wash_CategoryDC entity = null;

            entity = new web_Wash_CategoryDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.PictureM))
            {
                entity.PictureM = System.Configuration.ConfigurationManager.AppSettings["Category_Picture"] + entity.PictureM;
            }

            if (!string.IsNullOrWhiteSpace(entity.PictureL))
            {
                entity.PictureL = System.Configuration.ConfigurationManager.AppSettings["Category_Picture"] + entity.PictureL;
            }

            return entity;
        }

    }
}
