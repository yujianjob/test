using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Wash_CategoryDC : EntityBase
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
        /// 代码
        /// </summary>
        [Display(Name = "代码")]
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 类别表ID
        /// </summary>
        [Display(Name = "类别表ID")]
        [DataMember]
        public int ClassID { set; get; }

        /// <summary>
        /// 单位
        /// </summary>
        [Display(Name = "单位")]
        [DataMember]
        public string Unit { set; get; }

        /// <summary>
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        [DataMember]
        public int Enable { set; get; }

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
        /// 图片描述
        /// </summary>
        [Display(Name = "图片描述")]
        [DataMember]
        public string PictureAlt { set; get; }

        #endregion Model

        /// <summary>
        /// 类别名称
        /// </summary>
        [Display(Name = "类别名称")]
        [DataMember]
        public string ClassName { set; get; }

        /// <summary>
        /// 类别表ID
        /// </summary>
        [Display(Name = "大类ID")]
        [DataMember]
        public int ParentClassID { set; get; }

        /// <summary>
        /// 类别表ID
        /// </summary>
        [Display(Name = "大类名称")]
        [DataMember]
        public string ParentClassName { set; get; }

        /// <summary>
        /// 根据Reader生成Wash_CategoryDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_CategoryDC GetEntity(IDataReader reader)
        {
            Wash_CategoryDC entity = null;

            entity = new Wash_CategoryDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
