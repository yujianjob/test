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
    [DataContract]
    [Serializable]
    public class Wash_StoreLoginInfoDC : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "门店ID")]
        [DataMember]
        public Guid StoreID { set; get; }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        [DataMember]
        public string StoreCode { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [DataMember]
        public string StoreName { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 联系人
        /// </summary>
        [Display(Name = "联系人")]
        [DataMember]
        public string LinkMan { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        public string MPNo { set; get; }

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

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "操作员ID")]
        [DataMember]
        public int OperatorID { set; get; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录名")]
        [DataMember]
        public string LoginName { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "登录姓名")]
        [DataMember]
        public string OperatorName { set; get; }

        /// <summary>
        /// 是否最高权限
        /// </summary>
        [Display(Name = "是否最高权限")]
        [DataMember]
        public int IsAdmin { set; get; }

        /// <summary>
        /// 行政区全名
        /// </summary>
        [Display(Name = "行政区全名")]
        [DataMember]
        public string DistrictName { get; set; }

        /// <summary>
        /// 城市名
        /// </summary>
        [Display(Name = "城市名")]
        [DataMember]
        public string CityName { get; set; }

        /// <summary>
        /// 省名
        /// </summary>
        [Display(Name = "省名")]
        [DataMember]
        public string ProvinceName { get; set; }

        /// <summary>
        /// 根据Reader生成Wash_StoreDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_StoreLoginInfoDC GetEntity(IDataReader reader)
        {
            Wash_StoreLoginInfoDC entity = null;

            entity = new Wash_StoreLoginInfoDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
