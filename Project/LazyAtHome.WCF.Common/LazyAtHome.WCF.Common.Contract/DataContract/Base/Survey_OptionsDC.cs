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
    public class Survey_OptionsDC : EntityBase
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
        /// 问题表ID
        /// </summary>
        [Display(Name = "问题表ID")]
        [DataMember]
        public int QuID { set; get; }

        /// <summary>
        /// 选项名
        /// </summary>
        [Display(Name = "选项名")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 选项文字
        /// </summary>
        [Display(Name = "选项文字")]
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 选项类型
        /// </summary>
        [Display(Name = "选项类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 序号
        /// </summary>
        [Display(Name = "序号")]
        [DataMember]
        public int Seq { set; get; }

        /// <summary>
        /// 选择人数
        /// </summary>
        [Display(Name = "选择人数")]
        [DataMember]
        public int SelPct { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Survey_OptionsDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Survey_OptionsDC GetEntity(IDataReader reader)
        {
            Survey_OptionsDC entity = null;

            entity = new Survey_OptionsDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
