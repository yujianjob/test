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
    public class Luxury_ClassDC : EntityBase
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
        /// 上级ID
        /// </summary>
        [Display(Name = "上级ID")]
        [DataMember]
        public int ParentID { set; get; }

        /// <summary>
        /// 代码
        /// </summary>
        [Display(Name = "代码")]
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [DataMember]
        public int Sort { set; get; }

        /// <summary>
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        [DataMember]
        public int Enable { set; get; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        [DataMember]
        public string PictureL { set; get; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        [DataMember]
        public string PictureM { set; get; }

        /// <summary>
        /// 图片
        /// </summary>
        [Display(Name = "图片")]
        [DataMember]
        public string PictureS { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Luxury_ClassDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Luxury_ClassDC GetEntity(IDataReader reader)
        {
            Luxury_ClassDC entity = null;

            entity = new Luxury_ClassDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
