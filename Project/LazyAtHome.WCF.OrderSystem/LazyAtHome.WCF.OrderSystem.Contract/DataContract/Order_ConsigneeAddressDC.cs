using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.OrderSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Order_ConsigneeAddressDC : EntityBase
    {
        #region Model

        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 订单表ID
        /// </summary>
        [Display(Name = "订单表ID")]
        [DataMember]
        public int OID { set; get; }

        /// <summary>
        /// 地址类型
        /// </summary>
        [Display(Name = "地址类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Display(Name = "收货人")]
        [DataMember]
        public string Consignee { set; get; }

        /// <summary>
        /// 省市区
        /// </summary>
        [Display(Name = "省市区")]
        [DataMember]
        public int DistrictID { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        public string Mpno { set; get; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        [DataMember]
        public string Phone { set; get; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Display(Name = "邮编")]
        [DataMember]
        public string ZipCode { set; get; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [DataMember]
        public string Email { set; get; }

        #endregion Model

        /// <summary>
        /// 省市区
        /// </summary>
        [Display(Name = "省市区")]
        [DataMember]
        public string DistrictName { set; get; }

        /// <summary>
        /// 城市名
        /// </summary>
        [Display(Name = "城市名")]
        [DataMember]
        public string CityName { set; get; }

        /// <summary>
        /// 省名
        /// </summary>
        [Display(Name = "省名")]
        [DataMember]
        public string ProvinceName { set; get; }

        /// <summary>
        /// 根据Reader生成Order_ConsigneeAddressDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_ConsigneeAddressDC GetEntity(IDataReader reader)
        {
            Order_ConsigneeAddressDC entity = null;

            entity = new Order_ConsigneeAddressDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
