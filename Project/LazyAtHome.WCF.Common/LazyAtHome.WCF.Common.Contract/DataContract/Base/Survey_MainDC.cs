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
    public class Survey_MainDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

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
        /// 有效期开始
        /// </summary>
        [Display(Name = "有效期开始")]
        [DataMember]
        public DateTime BeginDate { set; get; }

        /// <summary>
        /// 有效期结束
        /// </summary>
        [Display(Name = "有效期结束")]
        [DataMember]
        public DateTime EndDate { set; get; }

        /// <summary>
        /// 参与人数
        /// </summary>
        [Display(Name = "参与人数")]
        [DataMember]
        public int UserCount { set; get; }

        /// <summary>
        /// 确认状态
        /// </summary>
        [Display(Name = "确认状态")]
        [DataMember]
        public int CommitStatus { set; get; }

        #endregion Model

        /// <summary>
        /// 问题列表
        /// </summary>
        [DataMember]
        public IList<Survey_QuestionDC> QuestionList { get; set; }

        /// <summary>
        /// 根据Reader生成Survey_MainDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Survey_MainDC GetEntity(IDataReader reader)
        {
            Survey_MainDC entity = null;

            entity = new Survey_MainDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
