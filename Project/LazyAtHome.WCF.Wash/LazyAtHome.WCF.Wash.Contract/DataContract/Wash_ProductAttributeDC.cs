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
    public class Wash_ProductAttributeDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 产品运价ID
        /// </summary>
        [Display(Name = "产品运价ID")]
        [DataMember]
        public int ProductID { set; get; }

        /// <summary>
        /// 属性ID
        /// </summary>
        [Display(Name = "属性ID")]
        [DataMember]
        public int AttributeID { set; get; }

        /// <summary>
        /// 产品运价名称
        /// </summary>
        [Display(Name = "产品运价名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        [Display(Name = "描述")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 加价金额
        /// </summary>
        [Display(Name = "加价金额")]
        [DataMember]
        public decimal Price { set; get; }

        /// <summary>
        /// 强制选中
        /// </summary>
        [Display(Name = "强制选中")]
        [DataMember]
        public int Selected { set; get; }

        /// <summary>
        /// 默认选中
        /// </summary>
        [Display(Name = "默认选中")]
        [DataMember]
        public int Default { set; get; }

        #endregion Model

        /// <summary>
        /// 属性名
        /// </summary>
        [Display(Name = "属性名")]
        [DataMember]
        public string AttributeName { set; get; }

        /// <summary>
        /// 上级属性ID
        /// </summary>
        [Display(Name = "上级属性ID")]
        [DataMember]
        public int ParentAttributeID { set; get; }

        /// <summary>
        /// 根据Reader生成Wash_ProductAttributeDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_ProductAttributeDC GetEntity(IDataReader reader)
        {
            Wash_ProductAttributeDC entity = null;

            entity = new Wash_ProductAttributeDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
