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
    public class User_ShareCommentDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 晒单ID
        /// </summary>
        [Display(Name = "晒单ID")]
        [DataMember]
        public int ShareID { set; get; }

        /// <summary>
        /// 晒单人ID
        /// </summary>
        [Display(Name = "晒单人ID")]
        [DataMember]
        public Guid ShareUserID { set; get; }

        /// <summary>
        /// CommentUserID
        /// </summary>
        [Display(Name = "CommentUserID")]
        [DataMember]
        public Guid CommentUserID { set; get; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [Display(Name = "评论内容")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 评价类型(顶,赞,平,踩)
        /// </summary>
        [Display(Name = "评价类型(顶,赞,平,踩)")]
        [DataMember]
        public int? CommentType { set; get; }

        /// <summary>
        /// 上级ID(回复其他人评论)
        /// </summary>
        [Display(Name = "上级ID(回复其他人评论)")]
        [DataMember]
        public int? ParentID { set; get; }

        /// <summary>
        /// 审核状态(1：已提交 2：审核通过 3：审核失败)
        /// </summary>
        [Display(Name = "审核状态(1：已提交 2：审核通过 3：审核失败)")]
        [DataMember]
        public int VerifyStatus { set; get; }

        /// <summary>
        /// 审核人ID
        /// </summary>
        [Display(Name = "审核人ID")]
        [DataMember]
        public int? AduitEmployeeID { set; get; }

        /// <summary>
        /// 审核时间
        /// </summary>
        [Display(Name = "审核时间")]
        [DataMember]
        public DateTime? AduitDate { set; get; }

        /// <summary>
        /// 审核人意见
        /// </summary>
        [Display(Name = "审核人意见")]
        [DataMember]
        public string Comment { set; get; }

        #endregion Model

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public RecordCountEntity<User_ShareCommentDC> ReplyList { get; set; }

        /// <summary>
        /// 根据Reader生成User_ShareCommentDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_ShareCommentDC GetEntity(IDataReader reader)
        {
            User_ShareCommentDC entity = null;

            entity = new User_ShareCommentDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
