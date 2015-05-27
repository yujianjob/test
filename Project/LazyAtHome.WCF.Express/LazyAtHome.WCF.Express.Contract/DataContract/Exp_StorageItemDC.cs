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
    public class Exp_StorageItemDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        [DataMember]
        public string Number { set; get; }

        /// <summary>
        /// 附属编号
        /// </summary>
        [Display(Name = "附属编号")]
        [DataMember]
        public string OtherNumber { set; get; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        [Display(Name = "仓库ID")]
        [DataMember]
        public int StorageID { set; get; }

        /// <summary>
        /// 物品类型
        /// </summary>
        [Display(Name = "物品类型")]
        [DataMember]
        public int ItemType { set; get; }

        /// <summary>
        /// 物品名称
        /// </summary>
        [Display(Name = "物品名称")]
        [DataMember]
        public string ItemName { set; get; }

        /// <summary>
        /// 物品详情
        /// </summary>
        [Display(Name = "物品详情")]
        [DataMember]
        public string ItemDetail { set; get; }

        /// <summary>
        /// 站点ID
        /// </summary>
        [Display(Name = "站点ID")]
        [DataMember]
        public int NodeID { set; get; }

        /// <summary>
        /// 负责人ID
        /// </summary>
        [Display(Name = "负责人ID")]
        [DataMember]
        public int ManagerID { set; get; }

        /// <summary>
        /// 目标库类型
        /// </summary>
        [Display(Name = "目标库类型")]
        [DataMember]
        public int TargetType { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "状态")]
        [DataMember]
        public int Status { set; get; }

        #endregion Model
        
        /// <summary>
        /// 负责人名
        /// </summary>
        [Display(Name = "负责人名")]
        [DataMember]
        public string ManagerName { set; get; }

        /// <summary>
        /// 仓库名
        /// </summary>
        [Display(Name = "仓库名")]
        [DataMember]
        public string StorageName { set; get; }

        /// <summary>
        /// 仓库类型
        /// </summary>
        [Display(Name = "仓库类型")]
        [DataMember]
        public int StorageType { set; get; }


        [DataMember]
        public DateTime? TargetTime1 { set; get; }
        [DataMember]
        public DateTime? TargetTime2 { set; get; }
        [DataMember]
        public DateTime? TargetTime3 { set; get; }
        [DataMember]
        public DateTime? TargetTime4 { set; get; }
        [DataMember]
        public DateTime? TargetTime5 { set; get; }
        [DataMember]
        public DateTime? TargetTime6 { set; get; }
        [DataMember]
        public DateTime? TargetTime7 { set; get; }
        [DataMember]
        public DateTime? TargetTime8 { set; get; }


        /// <summary>
        /// 根据Reader生成Exp_StorageItemDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Exp_StorageItemDC GetEntity(IDataReader reader)
        {
            Exp_StorageItemDC entity = null;

            entity = new Exp_StorageItemDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
