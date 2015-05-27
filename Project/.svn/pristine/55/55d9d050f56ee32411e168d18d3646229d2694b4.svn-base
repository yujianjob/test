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
    public class Exp_Preson_CommissionBillDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [Display(Name = "用户ID")]
        [DataMember]
        public int OperatorID { set; get; }

        /// <summary>
        /// 账单结算周期
        /// </summary>
        [Display(Name = "账单结算周期")]
        [DataMember]
        public string Period { set; get; }

        /// <summary>
        /// 周期开始时间
        /// </summary>
        [Display(Name = "周期开始时间")]
        [DataMember]
        public DateTime StartTime { set; get; }

        /// <summary>
        /// 周期结束时间
        /// </summary>
        [Display(Name = "周期结束时间")]
        [DataMember]
        public DateTime EndTime { set; get; }

        /// <summary>
        /// 佣金
        /// </summary>
        [Display(Name = "佣金")]
        [DataMember]
        public decimal Commission { set; get; }

        /// <summary>
        /// 实际结算佣金
        /// </summary>
        [Display(Name = "实际结算佣金")]
        [DataMember]
        public decimal RealCommission { set; get; }

        /// <summary>
        /// 结算状态
        /// </summary>
        [Display(Name = "结算状态")]
        [DataMember]
        public int BillStatus { set; get; }

        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        [DataMember]
        public string BillRemark { set; get; }

        #endregion Model

        /// <summary>
        /// 本次结算金额
        /// </summary>
        [Display(Name = "说明")]
        [DataMember]
        public decimal CurrentCommission { set; get; }

        /// <summary>
        /// 用户
        /// </summary>
        [Display(Name = "用户")]
        [DataMember]
        public string OperatorName { set; get; }

        /// <summary>
        /// 根据Reader生成Exp_Preson_CommissionBillDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Exp_Preson_CommissionBillDC GetEntity(IDataReader reader)
        {
            Exp_Preson_CommissionBillDC entity = null;

            entity = new Exp_Preson_CommissionBillDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
