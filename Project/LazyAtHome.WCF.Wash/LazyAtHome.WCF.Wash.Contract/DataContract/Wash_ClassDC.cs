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
    public class Wash_ClassDC : EntityBase
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
        /// 预定义不可修改
        /// </summary>
        [Display(Name = "预定义不可修改")]
        [DataMember]
        public int IsDefault { set; get; }

        /// <summary>
        /// 
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

        #endregion Model

        /// <summary>
        /// 根据Reader生成Wash_ClassDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_ClassDC GetEntity(IDataReader reader)
        {
            Wash_ClassDC entity = null;

            entity = new Wash_ClassDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
