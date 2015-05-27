using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Base
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Survey_AnswerDetailDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 答题表ID
        /// </summary>
        [Display(Name = "问卷表ID")]
        [DataMember]
        public int AnsID { set; get; }

        /// <summary>
        /// 问题表ID
        /// </summary>
        [Display(Name = "问题表ID")]
        [DataMember]
        public int QuID { set; get; }

        /// <summary>
        /// 答案值
        /// </summary>
        [Display(Name = "答案值")]
        [DataMember]
        public int AnsValue { set; get; }

        /// <summary>
        /// 回答内容
        /// </summary>
        [Display(Name = "回答内容")]
        [DataMember]
        public string AnsContent { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Survey_AnswerDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Survey_AnswerDetailDC GetEntity(IDataReader reader)
        {
            Survey_AnswerDetailDC entity = null;

            entity = new Survey_AnswerDetailDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
