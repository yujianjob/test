using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.SMS.DataContract
{
    /// <summary>
    /// 消息内容
    /// </summary>
    public class Base_SmsSendDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { set; get; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string Content { set; get; }

        /// <summary>
        /// 发送状态 0:未发送1:已发送 其他:错误内容
        /// </summary>
        public int SendStatus { set; get; }

        /// <summary>
        /// 实际发送时间
        /// </summary>
        public DateTime? SendTime { set; get; }

        /// <summary>
        /// 定时发送时间
        /// </summary>
        public DateTime RunTime { set; get; }

        /// <summary>
        /// 重试次数
        /// </summary>
        public int ReTry { set; get; }

        /// <summary>
        /// 该用户已发送次数
        /// </summary>
        public int Count { set; get; }

        /// <summary>
        /// 优先级 1-5 5最快
        /// </summary>
        public int Priority { set; get; }

        /// <summary>
        /// 短信类型(验证码,普通,广告)
        /// </summary>
        public int Type { set; get; }

        /// <summary>
        /// 发送渠道
        /// </summary>
        public int Channel { set; get; }

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
