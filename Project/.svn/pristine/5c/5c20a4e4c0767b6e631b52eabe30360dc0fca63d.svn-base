using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.File
{
    /// <summary>
    /// 上传文件日志
    /// </summary>
    [DataContract]
    [Serializable]
    public class UpLoadLogDC : EntityBase
    {
        /// <summary>
        /// 主键
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        [DataMember]
        public int ProjectType { get; set; }

        /// <summary>
        /// 文件标识
        /// </summary>
        [DataMember]
        public Guid FileSignKey { get; set; }

        /// <summary>
        /// 自定义标识
        /// </summary>
        [DataMember]
        public string FileCustomKey { get; set; }

        /// <summary>
        /// 保存路径
        /// </summary>
        [DataMember]
        public string SaveFilePath { get; set; }

        /// <summary>
        /// MD5
        /// </summary>
        [DataMember]
        public string FileMD5 { get; set; }

        /// <summary>
        /// 根据Reader生成UpLoadFileLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static UpLoadLogDC GetEntity(IDataReader reader)
        {
            UpLoadLogDC entity = null;

            entity = new UpLoadLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
