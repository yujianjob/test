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
    public class User_Message_PublicDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

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
        /// 发布时间
        /// </summary>
        [Display(Name = "发布时间")]
        [DataMember]
        public DateTime PublishDate { set; get; }

        /// <summary>
        /// 发布状态(0:未发布 1:已发布 2:下线)
        /// </summary>
        [Display(Name = "发布状态(0:未发布 1:已发布 2:下线)")]
        [DataMember]
        public int PublishStatus { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_Message_PublicDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_Message_PublicDC GetEntity(IDataReader reader)
        {
            User_Message_PublicDC entity = null;

            entity = new User_Message_PublicDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
