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
    public class User_ShareDC : EntityBase
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
        public string Content { set; get; }

        /// <summary>
        /// 标签
        /// </summary>
        [Display(Name = "标签")]
        [DataMember]
        public string Tag { set; get; }

        /// <summary>
        /// 评价(顶)数
        /// </summary>
        [Display(Name = "评价(顶)数")]
        [DataMember]
        public int Comment_UP { set; get; }

        /// <summary>
        /// 评价(赞)数
        /// </summary>
        [Display(Name = "评价(赞)数")]
        [DataMember]
        public int Comment_Like { set; get; }

        /// <summary>
        /// 评价(平)数
        /// </summary>
        [Display(Name = "评价(平)数")]
        [DataMember]
        public int Comment_Common { set; get; }

        /// <summary>
        /// 评价(踩)数
        /// </summary>
        [Display(Name = "评价(踩)数")]
        [DataMember]
        public int Comment_Down { set; get; }

        /// <summary>
        /// 评论数
        /// </summary>
        [Display(Name = "评论数")]
        [DataMember]
        public int CommentCount { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        [DataMember]
        public int ItemID { set; get; }

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
        /// 根据Reader生成User_ShareDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_ShareDC GetEntity(IDataReader reader)
        {
            User_ShareDC entity = null;

            entity = new User_ShareDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
