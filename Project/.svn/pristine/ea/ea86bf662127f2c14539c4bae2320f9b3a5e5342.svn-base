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
    /// 网站用分类(小类)
    /// </summary>
    [DataContract]
    [Serializable]
    public class web_Wash_ClassDC : EntityBase
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
        /// 
        /// </summary>
        [DataMember]
        public IList<web_Wash_CategoryDC> CategoryList { set; get; }

        /// <summary>
        /// 上级ID
        /// </summary>
        [Display(Name = "上级ID")]
        [DataMember]
        public int ParentID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "图片")]
        [DataMember]
        public string Picture { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Wash_ClassDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static web_Wash_ClassDC GetEntity(IDataReader reader)
        {
            web_Wash_ClassDC entity = null;

            entity = new web_Wash_ClassDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.Picture))
            {
                entity.Picture = System.Configuration.ConfigurationManager.AppSettings["Class_Picture"] + entity.Picture;
            }

            return entity;
        }
    }
}
