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
    public class User_CollectDC : EntityBase
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
        /// 产品ID
        /// </summary>
        [DataMember]
        public int ItemID { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        [DataMember]
        public int Site { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_CollectDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_CollectDC GetEntity(IDataReader reader)
        {
            User_CollectDC entity = null;

            entity = new User_CollectDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
