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
    public class Exp_AreaDC : EntityBase
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
        public string Name { set; get; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        [Display(Name = "负责人ID")]
        [DataMember]
        public int ManagerID { set; get; }

        #endregion Model

        /// <summary>
        /// 负责人名
        /// </summary>
        [Display(Name = "负责人名")]
        [DataMember]
        public string ManagerName { set; get; }

        /// <summary>
        /// 根据Reader生成Exp_AreaDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Exp_AreaDC GetEntity(IDataReader reader)
        {
            Exp_AreaDC entity = null;

            entity = new Exp_AreaDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
