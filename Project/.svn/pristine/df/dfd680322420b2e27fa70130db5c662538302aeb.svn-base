using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_ConsigneeAddressDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 收货人
        /// </summary>
        [Display(Name = "收货人")]
        [DataMember]
        [Required(ErrorMessage = "收货人不能为空")]
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
        [Required(ErrorMessage = "收货地址不能为空")]
        public string Address { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        [Required(ErrorMessage = "手机号不能为空")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber)]
        public string MPNo { set; get; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [Display(Name = "电话号码")]
        [DataMember]
        public string Phone { set; get; }

        /// <summary>
        /// 默认地址
        /// </summary>
        [Display(Name = "默认地址")]
        [DataMember]
        public int IsDefault { set; get; }

        /// <summary>
        /// 邮编
        /// </summary>
        [Display(Name = "邮编")]
        [DataMember]
        [DataType(System.ComponentModel.DataAnnotations.DataType.PostalCode)]
        public string ZipCode { set; get; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [Display(Name = "邮箱")]
        [DataMember]
        [DataType(System.ComponentModel.DataAnnotations.DataType.EmailAddress)]
        public string Email { set; get; }

        #endregion Model

        /// <summary>
        /// 省市区
        /// </summary>
        [Display(Name = "省市区")]
        [DataMember]
        public string DistrictName { set; get; }

        /// <summary>
        /// 市名
        /// </summary>
        [Display(Name = "市名")]
        [DataMember]
        public string CityName { set; get; }

        /// <summary>
        /// 省名
        /// </summary>
        [Display(Name = "省名")]
        [DataMember]
        public string ProvinceName { set; get; }

        /// <summary>
        /// 根据Reader生成User_ConsigneeAddressDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_ConsigneeAddressDC GetEntity(IDataReader reader)
        {
            User_ConsigneeAddressDC entity = null;

            entity = new User_ConsigneeAddressDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
