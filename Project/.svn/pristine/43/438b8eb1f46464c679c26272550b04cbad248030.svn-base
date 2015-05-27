using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Base
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Survey_AnswerDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 问卷表ID
        /// </summary>
        [Display(Name = "问卷表ID")]
        [DataMember]
        public int SurID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public Guid? UserID { set; get; }

        /// <summary>
        /// 用户来源
        /// </summary>
        [Display(Name = "用户来源")]
        [DataMember]
        public string UserSource { set; get; }

        #endregion Model

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [DataMember]
        public string Title { set; get; }
        
        /// <summary>
        /// 答案列表
        /// </summary>
        [Display(Name = "用户来源")]
        [DataMember]
        public IList<Survey_AnswerDetailDC> DetailList { get; set; }

        /// <summary>
        /// 根据Reader生成Survey_AnswerDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Survey_AnswerDC GetEntity(IDataReader reader)
        {
            Survey_AnswerDC entity = null;

            entity = new Survey_AnswerDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
