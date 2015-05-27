using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace LazyAtHome.WCF.Common.Contract.DataContract.PR
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class OperatorLogDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 操作人
        /// </summary>
        [Display(Name = "操作人")]
        [DataMember]
        public int OperatorID { set; get; }

        /// <summary>
        /// 操作人名字
        /// </summary>
        [Display(Name = "操作人名字")]
        [DataMember]
        public string OperatorName { set; get; }

        /// <summary>
        /// 日志类型
        /// </summary>
        [Display(Name = "日志类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 日志内容
        /// </summary>
        [Display(Name = "日志内容")]
        [DataMember]
        public string LogContent { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成OperatorLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static OperatorLogDC GetEntity(IDataReader reader)
        {
            OperatorLogDC entity = null;

            entity = new OperatorLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
