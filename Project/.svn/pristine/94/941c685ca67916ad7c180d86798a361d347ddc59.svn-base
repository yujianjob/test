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
    public class Exp_StorageDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 仓库名
        /// </summary>
        [Display(Name = "仓库名")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        [Display(Name = "负责人ID")]
        [DataMember]
        public int ManagerID { set; get; }

        /// <summary>
        /// 联系人ID
        /// </summary>
        [Display(Name = "联系人ID")]
        [DataMember]
        public int LinkManID { set; get; }

        #endregion Model

        /// <summary>
        /// 负责人名
        /// </summary>
        [Display(Name = "负责人名")]
        [DataMember]
        public string ManagerName { set; get; }

        /// <summary>
        /// 联系人名
        /// </summary>
        [Display(Name = "联系人名")]
        [DataMember]
        public string LinkManName { set; get; }

        /// <summary>
        /// 根据Reader生成Exp_StorageDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Exp_StorageDC GetEntity(IDataReader reader)
        {
            Exp_StorageDC entity = null;

            entity = new Exp_StorageDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
