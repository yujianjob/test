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
    /// 网站用产品运价
    /// </summary>
    [DataContract]
    [Serializable]
    public class web_Wash_ProductDC : EntityBase
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
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [DataMember]
        public string WebName { set; get; }
        
        /// <summary>
        /// 分类ID
        /// </summary>
        [Display(Name = "分类ID")]
        [DataMember]
        public int CategoryID { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [DataMember]
        public string Content { set; get; }

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
        /// 
        /// </summary>
        [Display(Name = "分组")]
        [DataMember]
        public int Group { set; get; }

        #endregion Model

        /// <summary>
        /// 中图
        /// </summary>
        [Display(Name = "中图")]
        [DataMember]
        public string PictureM { set; get; }
        
        /// <summary>
        /// 根据Reader生成Wash_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static web_Wash_ProductDC GetEntity(System.Data.IDataReader reader)
        {
            web_Wash_ProductDC entity = null;

            entity = new web_Wash_ProductDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.PictureM))
            {
                entity.PictureM = System.Configuration.ConfigurationManager.AppSettings["Category_Picture"] + entity.PictureM;
            }

            return entity;
        }
    }
}
