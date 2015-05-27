using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using LazyAtHome.Core.Infrastructure.WCF;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Base_CouponProductDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "ID")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 优惠券ID
        /// </summary>
        [Display(Name = "优惠券ID")]
        [DataMember]
        public int CouponID { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        [DataMember]
        public int ProductID { set; get; }

        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "产品名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "产品金额")]
        [DataMember]
        public decimal SalesPrice { set; get; }

        /// <summary>
        /// 根据Reader生成Base_CouponProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_CouponProductDC GetEntity(IDataReader reader)
        {
            Base_CouponProductDC entity = null;

            entity = new Base_CouponProductDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
