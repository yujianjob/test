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
    public class Wash_AttributeDC : EntityBase, IEquatable<Wash_AttributeDC>
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
        /// 启用
        /// </summary>
        [Display(Name = "启用")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [Display(Name = "属性类型")]
        [DataMember]
        public int SelectType { set; get; }

        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public IList<Wash_AttributeDC> ChildAttributeList { set; get; }

        public bool Equals(Wash_AttributeDC other)
        {
            if (this.ID == other.ID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据Reader生成Wash_AttributeDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_AttributeDC GetEntity(IDataReader reader)
        {
            Wash_AttributeDC entity = null;

            entity = new Wash_AttributeDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
