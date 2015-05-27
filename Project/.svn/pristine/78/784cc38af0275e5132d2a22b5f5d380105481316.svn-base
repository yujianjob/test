using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LazyAtHome.Core.Infrastructure.WCF;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_Message_PrivateDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 源用户ID(用户PM)
        /// </summary>
        [Display(Name = "源用户ID(用户PM)")]
        [DataMember]
        public Guid? SendUserID { set; get; }

        /// <summary>
        /// 消息类型
        /// </summary>
        [Display(Name = "消息类型")]
        [DataMember]
        public int MessageType { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        [Display(Name = "内容")]
        [DataMember]
        public string Message { set; get; }

        /// <summary>
        /// 读取状态(0:未读 1:已读)
        /// </summary>
        [Display(Name = "读取状态(0:未读 1:已读)")]
        [DataMember]
        public int ReadStatus { set; get; }

        /// <summary>
        /// 公告ID
        /// </summary>
        [Display(Name = "公告ID")]
        [DataMember]
        public int? PublicID { set; get; }

        #endregion Model


        /// <summary>
        /// 根据Reader生成User_Message_PrivateDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_Message_PrivateDC GetEntity(IDataReader reader)
        {
            User_Message_PrivateDC entity = null;

            entity = new User_Message_PrivateDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
