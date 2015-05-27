using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using LazyAtHome.Core.Infrastructure.WCF;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.Wash.Contract.DataContract.weixin
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class wx_Wash_CategoryDC : EntityBase
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
        /// 小图
        /// </summary>
        [Display(Name = "小图")]
        [DataMember]
        public string PictureS { set; get; }

        #endregion Model

        public string SalesPrice
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
        public IList<wx_Wash_ProductDC> ProductList { get; set; }

        /// <summary>
        /// 根据Reader生成Wash_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static wx_Wash_CategoryDC GetEntity(System.Data.IDataReader reader)
        {
            wx_Wash_CategoryDC entity = null;

            entity = new wx_Wash_CategoryDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.PictureS))
            {
                entity.PictureS = System.Configuration.ConfigurationManager.AppSettings["Category_Picture"] + entity.PictureS;
            }

            return entity;
        }

    }
}
