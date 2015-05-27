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
    /// 投诉
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_ComplaintsDC : EntityBase
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
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        [DataMember]
        public string No { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        public string MPNo { set; get; }

        /// <summary>
        /// 投诉人
        /// </summary>
        [Display(Name = "投诉人")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 处理状态(0：未处理  1：已处理)
        /// </summary>
        [Display(Name = "处理状态")]
        [DataMember]
        public int HandleStatus { set; get; }

        /// <summary>
        /// 处理人ID
        /// </summary>
        [Display(Name = "处理人ID")]
        [DataMember]
        public int? HandleEmployeeID { set; get; }

        /// <summary>
        /// 处理时间
        /// </summary>
        [Display(Name = "处理时间")]
        [DataMember]
        public DateTime? HandleDate { set; get; }

        /// <summary>
        /// 处理内容
        /// </summary>
        [Display(Name = "处理内容")]
        [DataMember]
        public string HandleContent { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_ComplaintsDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_ComplaintsDC GetEntity(IDataReader reader)
        {
            User_ComplaintsDC entity = null;

            entity = new User_ComplaintsDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
