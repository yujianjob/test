using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.RegisterSourceConfig
{
    /// <summary>
    /// 推广对账列表页绑定实体
    /// </summary>
    [Serializable]
    public class AccountListModel
    {
        public AccountSearchInfo SearchInfo;

        /// <summary>
        /// 1：表示兼职 2：表示顺丰
        /// </summary>
        public int RegisterSourceType { get; set; }

        /// <summary>
        /// 兼职对账
        /// </summary>
        public AccountStatJob AccountJob { get; set; }

        /// <summary>
        /// 兼职对账详情
        /// </summary>
        public IList<AccountStatJob> AccountJobDetailList { get; set; }



        /// <summary>
        /// 顺丰对账
        /// </summary>
        public AccountStatSF AccountSF { get; set; }

        /// <summary>
        /// 顺丰对账详情
        /// </summary>
        public IList<AccountStatSF> AccountSFDetailList { get; set; }

    }


    /// <summary>
    /// 推广对账基础实体类
    /// </summary>
    public class AccountStatBase
    {
        /// <summary>
        /// 推广编号
        /// </summary>
        public string InternalKey { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime DateFrom { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime DateTo { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 时间段
        /// </summary>
        public string DateRange
        {
            get 
            {
                return DateFrom.ToShortDateString() + "至" + DateTo.ToShortDateString();
            }
        }

        /// <summary>
        /// 关注数
        /// </summary>
        public int AttentionCount { get; set; }

        /// <summary>
        /// 取消关注数
        /// </summary>
        public int RemoveAttentionCount { get; set; }

        /// <summary>
        /// 实际关注数
        /// </summary>
        public int ActualAttentionCount
        {
            get
            {
                return this.AttentionCount - this.RemoveAttentionCount;
            }
        }

        /// <summary>
        /// 注册数
        /// </summary>
        public int RegisterCount { get; set; }

        /// <summary>
        /// 转化率
        /// </summary>
        public decimal Rate
        {
            get 
            {
                if (this.ActualAttentionCount > 0)
                {
                    return Math.Round(Convert.ToDecimal(this.RegisterCount / 1.0 / this.ActualAttentionCount),4);
                }
                else
                {
                    return 0;
                }
            }
        }

        

        //public int RegisterSourceType { get; set; }
        
    }

    /// <summary>
    /// 兼职推广对账实体类
    /// </summary>
    public class AccountStatJob : AccountStatBase
    {
        /// <summary>
        /// 工作日数量
        /// </summary>
        public int WorkDayCount { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsFinish 
        {
            get
            {
                if (this.WorkDayCount > 0 && this.ActualAttentionCount >= (this.ActualAttentionCountLine * this.WorkDayCount) && this.RegisterCount >= (this.RegisterCountLine * this.WorkDayCount))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 佣金
        /// </summary>
        public decimal Reward 
        {
            get
            {
                if (IsFinish)
                {
                    return this.BasePay * this.WorkDayCount;

                }
                else
                {
                    if (this.WorkDayCount > 0)
                    {
                        return this.RegisterCount * this.UnFinishRegisterPay + this.ActualAttentionCount * this.UnFinishAttentionPay;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }


        /// <summary>
        /// 超额佣金
        /// </summary>
        public decimal RewardAbove
        {
            get
            {
                if (IsFinish)
                {
                    return (this.ActualAttentionCount - (this.ActualAttentionCountLine * this.WorkDayCount)) * this.FinishAttentionPay +
                                                    (this.RegisterCount - (this.RegisterCountLine * this.WorkDayCount)) * this.FinishRegisterPay;

                }
                else
                {
                    return 0;
                }
            }
        }


        /// <summary>
        /// 底薪
        /// </summary>
        public decimal BasePay = 150;

        /// <summary>
        /// 关注要求数
        /// </summary>
        public int ActualAttentionCountLine = 150;

        /// <summary>
        /// 注册要求数
        /// </summary>
        public int RegisterCountLine = 20;



        /// <summary>
        /// 完成任务 超出按照3元/注册数
        /// </summary>
        public decimal FinishRegisterPay = 3;


        /// <summary>
        /// 完成任务 超出按照0.5元/关注数
        /// </summary>
        public decimal FinishAttentionPay = 0.5m;

        /// <summary>
        /// 未完成任务 按照3元/注册数
        /// </summary>
        public decimal UnFinishRegisterPay = 3;


        /// <summary>
        /// 未完成任务 按照0.4元/关注数
        /// </summary>
        public decimal UnFinishAttentionPay = 0.4m;
    }


    public class AccountStatSF : AccountStatBase
    {
        /// <summary>
        /// 结算单价
        /// </summary>
        public decimal Price = 0.5m;


        /// <summary>
        /// 结算总计
        /// </summary>
        public decimal TotalAmount 
        {
            get 
            {
                return Price * this.ActualAttentionCount;
            }
        
        }
    }
}