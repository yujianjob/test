using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Sms
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Base_SmsSendDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        public string Mobile { set; get; }

        /// <summary>
        /// 短信内容
        /// </summary>
        [Display(Name = "短信内容")]
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 发送状态 0:未发送1:已发送 其他:错误内容
        /// </summary>
        [Display(Name = "发送状态 0:未发送1:已发送 其他:错误内容")]
        [DataMember]
        public int SendStatus { set; get; }

        /// <summary>
        /// 实际发送时间
        /// </summary>
        [Display(Name = "实际发送时间")]
        [DataMember]
        public DateTime? SendTime { set; get; }

        /// <summary>
        /// 定时发送时间
        /// </summary>
        [Display(Name = "定时发送时间")]
        [DataMember]
        public DateTime RunTime { set; get; }

        /// <summary>
        /// 优先级 1-5 5最快
        /// </summary>
        [Display(Name = "优先级 1-5 5最快")]
        [DataMember]
        public int Priority { set; get; }

        /// <summary>
        /// 短信类型(验证码,普通,广告)
        /// </summary>
        [Display(Name = "短信类型(验证码,普通,广告)")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 发送渠道
        /// </summary>
        [Display(Name = "发送渠道")]
        [DataMember]
        public int Channel { set; get; }

        /// <summary>
        /// 来源
        /// </summary>
        [Display(Name = "来源")]
        [DataMember]
        public int Source { set; get; }
        
        /// <summary>
        /// 来源值
        /// </summary>
        [Display(Name = "来源值")]
        [DataMember]
        public string SourceValue { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Base_SmsSendDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_SmsSendDC GetEntity(IDataReader reader)
        {
            Base_SmsSendDC entity = null;

            entity = new Base_SmsSendDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
