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
    public class Survey_QuestionDC : EntityBase
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
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 问题类型
        /// </summary>
        [Display(Name = "问题类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号")]
        [DataMember]
        public int Seq { set; get; }

        #endregion Model

        /// <summary>
        /// 选项列表
        /// </summary>
        [DataMember]
        public IList<Survey_OptionsDC> OptionsList { get; set; }

        /// <summary>
        /// 根据Reader生成Survey_QuestionDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Survey_QuestionDC GetEntity(IDataReader reader)
        {
            Survey_QuestionDC entity = null;

            entity = new Survey_QuestionDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
