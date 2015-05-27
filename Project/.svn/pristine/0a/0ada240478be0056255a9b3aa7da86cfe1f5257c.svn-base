using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LazyAtHome.WCF.Common.Contract.Enumerate
{
    /// <summary>
    /// 事件日志级别分类
    /// </summary>
    [DataContract]
    public enum NotifyLevel
    {
        /// <summary>
        /// 2 ( 信息 ): 可提供关于系统操作一般信息的消息。 
        /// </summary>
        [EnumMember]
        Information2 = 2,

        /// <summary>
        /// 3 (通知 ): 关于常规事件 ( 包括由 admin 发起的配置更改 ) 的消息。
        /// </summary>
        [EnumMember]
        Notification3 = 3,

        /// <summary>
        /// 4 ( 警告 ):关于可能影响设备功能的情况(例如连接到电子邮件服务器失败或认证失败、超时和成功)的消息。
        /// </summary>
        [EnumMember]
        Warning4 = 4,

        /// <summary>
        /// 5 (错误): 关于可能影响设备功能的错误情况 (例如反病毒扫描失败或与 SSH 服务器通信失败) 的消息。 
        /// </summary>
        [EnumMember]
        Error5 = 5,

        /// <summary>
        /// 6 (关键 ): 关于可能影响设备功能的情况 [例如高可用性 (HA) 状态更改 ]的消息。
        /// </summary>
        [EnumMember]
        Critical6 = 6,

        /// <summary>
        /// 7 ( 警示 ): 关于需要立即引起注意的情况 ( 例如防火墙攻击和许可密钥到期 ) 的消息。
        /// </summary>
        [EnumMember]
        Alert7 = 7,

        /// <summary>
        /// 8 ( 紧急 ): 关于 SYN 攻击、Tear Drop 攻击及 Ping of Death 攻击的消息。
        /// </summary>
        Emergency8  = 8,
    }
}
