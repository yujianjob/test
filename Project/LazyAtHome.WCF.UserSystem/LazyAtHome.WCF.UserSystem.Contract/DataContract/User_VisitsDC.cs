using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_VisitsDC : EntityBase
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
        /// 来访用户ID
        /// </summary>
        [DataMember]
        public Guid VisitsUserID { set; get; }

        /// <summary>
        /// 来访用户ID
        /// </summary>
        [DataMember]
        public int VisitsSeedID { set; get; }

        /// <summary>
        /// 来访昵称
        /// </summary>
        [DataMember]
        public string VisitsNickName { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_VisitsDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_VisitsDC GetEntity(IDataReader reader)
        {
            User_VisitsDC entity = null;

            entity = new User_VisitsDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
