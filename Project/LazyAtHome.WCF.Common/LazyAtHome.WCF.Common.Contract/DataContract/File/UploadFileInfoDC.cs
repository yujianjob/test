using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.File
{
    /// <summary>
    /// 文件上传信息(默认覆盖同名文件)
    /// </summary>
    [DataContract]
    [Serializable]
    public class UploadInfoDC : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        public UploadInfoDC()
        {
            //edit by guominjie 2013-03-27
            Overwrite = true;
        }

        /// <summary>
        /// 项目类型
        /// </summary>
        [DataMember]
        public int ProjectType { get; set; }

        /// <summary>
        /// 用户自定文件夹
        /// </summary>
        [DataMember]
        public string CustomPath { get; set; }

        /// <summary>
        /// 文件内容
        /// </summary>
        [DataMember]
        public byte[] PostArray { get; set; }

        /// <summary>
        /// 是否覆盖已有文件
        /// </summary>
        [DataMember]
        public bool Overwrite { get; set; }

        /// <summary>
        /// 文件名
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// 是否由系统自动生成文件名
        /// </summary>
        [DataMember]
        public bool NewFileName { get; set; }

        /// <summary>
        /// 用户自定义标识
        /// </summary>
        [DataMember]
        public string CustomKey { get; set; }

        public override string ToString()
        {
            return string.Format(@"ProjectType:{0} CustomPath:{1} PostArray:{2}
                                     Overwrite:{3} FileName:{4} NewFileName:{5} CustomKey:{6}",
                                ProjectType, CustomPath, PostArray == null ? 0 : PostArray.Length,
                                Overwrite, FileName, NewFileName, CustomKey
                );
        }
    }
}
