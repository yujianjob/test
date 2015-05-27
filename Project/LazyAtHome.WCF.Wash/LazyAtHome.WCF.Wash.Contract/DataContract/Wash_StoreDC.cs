using LazyAtHome.Core.Infrastructure.WCF;
using System.Runtime.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.Wash.Contract.DataContract
{
    [DataContract]
    [Serializable]
    public class Wash_StoreDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public Guid StoreID { set; get; }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Display(Name = "名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 省市区
        /// </summary>
        [Display(Name = "省市区")]
        [DataMember]
        public int DistrictID { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        [Display(Name = "站点")]
        [DataMember]
        public int Site { set; get; }

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

        #endregion Model

        /// <summary>
        /// 省市区
        /// </summary>
        [Display(Name = "省市区")]
        [DataMember]
        public string DistrictName { set; get; }

        /// <summary>
        /// 根据Reader生成Wash_StoreDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_StoreDC GetEntity(IDataReader reader)
        {
            Wash_StoreDC entity = null;

            entity = new Wash_StoreDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}

