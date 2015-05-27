using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.DataContract.weixin
{
    /// <summary>
    /// 微信用产品运价
    /// </summary>
    [DataContract]
    [Serializable]
    public class wx_Wash_ProductDC : EntityBase
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

        #endregion Model

        /// <summary>
        /// 小图
        /// </summary>
        [Display(Name = "小图")]
        [DataMember]
        public string PictureS { set; get; }

        /// <summary>
        /// 根据Reader生成Wash_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static wx_Wash_ProductDC GetEntity(System.Data.IDataReader reader)
        {
            wx_Wash_ProductDC entity = null;

            entity = new wx_Wash_ProductDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.PictureS))
            {
                entity.PictureS = System.Configuration.ConfigurationManager.AppSettings["Category_Picture"] + entity.PictureS;
            }

            return entity;
        }
    }
}
