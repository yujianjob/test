using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_CardDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 卡表ID
        /// </summary>
        [Display(Name = "卡表ID")]
        [DataMember]
        public int CardID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 卡号
        /// </summary>
        [Display(Name = "卡号")]
        [DataMember]
        public string Number { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        [Display(Name = "密码")]
        [DataMember]
        public string Password { set; get; }

        /// <summary>
        /// 面值
        /// </summary>
        [Display(Name = "面值")]
        [DataMember]
        public decimal Money { set; get; }

        /// <summary>
        /// 余额
        /// </summary>
        [Display(Name = "余额")]
        [DataMember]
        public decimal Balance { set; get; }

        /// <summary>
        /// 有效期
        /// </summary>
        [Display(Name = "有效期")]
        [DataMember]
        public DateTime ExpiryDate { set; get; }

        /// <summary>
        /// 城市
        /// </summary>
        [Display(Name = "城市")]
        [DataMember]
        public string CityCode { set; get; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Display(Name = "批次号")]
        [DataMember]
        public string Batch { set; get; }

        /// <summary>
        /// 启用状态
        /// </summary>
        [Display(Name = "启用状态")]
        [DataMember]
        public int Enable { set; get; }

        /// <summary>
        /// 使用状态
        /// </summary>
        [Display(Name = "使用状态")]
        [DataMember]
        public int Used { set; get; }

        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public IList<Base_CardLogDC> CardLogList { get; set; }

        /// <summary>
        /// 根据Reader生成User_CardDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_CardDC GetEntity(IDataReader reader)
        {
            User_CardDC entity = null;

            entity = new User_CardDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}

