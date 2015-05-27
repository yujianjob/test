using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Sms
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Base_PushDC : EntityBase
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
        /// 发送状态(0:未发送,2:已发送)
        /// </summary>
        [Display(Name = "发送状态")]
        [DataMember]
        public int SendStatus { set; get; }

        /// <summary>
        /// 发送时间
        /// </summary>
        [Display(Name = "发送时间")]
        [DataMember]
        public DateTime SendTime { set; get; }

        /// <summary>
        /// 服务轮询到时间
        /// </summary>
        [Display(Name = "服务轮询到时间")]
        [DataMember]
        public DateTime RunTime { set; get; }

        /// <summary>
        /// 推送类型(1:懒到家APP 2:快递APP)
        /// </summary>
        [Display(Name = "推送类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 推送标签
        /// </summary>
        [Display(Name = "推送标签")]
        [DataMember]
        public string Tag { set; get; }
        
        #endregion Model

        /// <summary>
        /// 根据Reader生成Base_PushDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_PushDC GetEntity(IDataReader reader)
        {
            Base_PushDC entity = null;

            entity = new Base_PushDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
