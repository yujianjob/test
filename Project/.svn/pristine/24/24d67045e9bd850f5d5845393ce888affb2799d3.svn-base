using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_ScoreLogDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember]
        public Guid UserID { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 内容
        /// </summary>
        [DataMember]
        public string Content { set; get; }

        /// <summary>
        /// 变更前
        /// </summary>
        [DataMember]
        public int BeforeValue { set; get; }

        /// <summary>
        /// 变更积分
        /// </summary>
        [DataMember]
        public int ChangeValue { set; get; }

        /// <summary>
        /// 变更后
        /// </summary>
        [DataMember]
        public int AfterValue { set; get; }

        /// <summary>
        /// 渠道ID(订单ID)
        /// </summary>
        [DataMember]
        public string SourceID { set; get; }

        #endregion Model

        /// <summary>
        /// 手机号
        /// </summary>
        [Display(Name = "手机号")]
        [DataMember]
        public string MPNo { set; get; }

        /// <summary>
        /// 根据Reader生成User_ScoreLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_ScoreLogDC GetEntity(IDataReader reader)
        {
            User_ScoreLogDC entity = null;

            entity = new User_ScoreLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
