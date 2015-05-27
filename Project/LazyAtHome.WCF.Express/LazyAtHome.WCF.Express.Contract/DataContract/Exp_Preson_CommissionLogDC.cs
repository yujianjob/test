using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Express.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Exp_Preson_CommissionLogDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int UserID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ChangeType { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal BeforeValue { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal ChangeValue { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal AfterValue { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Exp_Preson_CommissionLogDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Exp_Preson_CommissionLogDC GetEntity(IDataReader reader)
        {
            Exp_Preson_CommissionLogDC entity = null;

            entity = new Exp_Preson_CommissionLogDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
