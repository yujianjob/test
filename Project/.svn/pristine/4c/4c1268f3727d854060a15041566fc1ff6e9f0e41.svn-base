using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.File
{
    [DataContract]
    [Serializable]
    public class UploadResultDC : EntityBase
    {
        /// <summary>
        /// -1: 系统错误
        /// -2: 路径不存在
        /// -3: 文件名错误
        /// -4: 文件已存在
        /// -5: 文件为只读,不可覆盖
        /// </summary>
        [DataMember]
        public int Result { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 文件唯一标识,下载用
        /// </summary>
        [DataMember]
        public Guid SignKey { get; set; }
    }

}
