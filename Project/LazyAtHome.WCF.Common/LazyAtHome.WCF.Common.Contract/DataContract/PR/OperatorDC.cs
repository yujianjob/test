using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using LazyAtHome.Core.Infrastructure.WCF;
using System.Reflection;
using System.Collections;

namespace LazyAtHome.WCF.Common.Contract.DataContract.PR
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class OperatorDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 登录名
        /// </summary>
        [DataMember]
        public string LoginName { set; get; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [DataMember]
        public string LoginPwd { set; get; }

        /// <summary>
        ///  登录类型 1:后台 2:工厂
        /// </summary>
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 用户手机
        /// </summary>
        [DataMember]
        public string MpNo { set; get; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        [DataMember]
        public string EMail { set; get; }

        /// <summary>
        /// 是否启用（1启用0不启用）
        /// </summary>
        [DataMember]
        public int Enable { set; get; }

        /// <summary>
        /// 上级ID
        /// </summary>
        [DataMember]
        public int? ParentID { set; get; }

        /// <summary>
        /// 编号
        /// </summary>
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 职位ID
        /// </summary>
        [DataMember]
        public int RoleID { set; get; }

        /// <summary>
        /// 在岗状态
        /// </summary>
        [DataMember]
        public int OnDuty { set; get; }

        /// <summary>
        /// 点位ID
        /// </summary>
        [DataMember]
        public int NodeID { set; get; }

        #endregion Model

        /// <summary>
        /// 点位名称
        /// </summary>
        [DataMember]
        public string NodeName { set; get; }

        /// <summary>
        /// 点位名称
        /// </summary>
        [DataMember]
        public int NodeType { set; get; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        [DataMember]
        public int StorageID { set; get; }

        /// <summary>
        /// 职位名
        /// </summary>
        [DataMember]
        public string RoleName { set; get; }

        /// <summary>
        /// 上级名称
        /// </summary>
        [DataMember]
        public string ParentName { set; get; }

        /// <summary>
        /// 根据Reader生成LoginDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static OperatorDC GetEntity(IDataReader reader)
        {
            OperatorDC entity = null;

            entity = new OperatorDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
