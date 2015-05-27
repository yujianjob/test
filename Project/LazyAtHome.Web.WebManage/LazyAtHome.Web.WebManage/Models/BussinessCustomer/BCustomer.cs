using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.BussinessCustomer
{
    /// <summary>
    /// 企业客户
    /// </summary>
    public class BCustomer
    {
        public int BCType { set; get; }

        /// <summary>
        /// 客户ID
        /// </summary>
        public Guid BCID 
        {
            get
            {

                if (this.BCType == 1)
                {
                    return new Guid("6802FC98-D51B-48D7-9892-BC2C4946A8B8");
                }
                else
                {
                    return Guid.Empty;
                }
            }
        }

        /// <summary>
        /// 地址
        /// </summary>
        public string BCAddress { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string BCConsignee { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string BCMpno { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string BCProductName { get; set; }
     
        /// <summary>
        /// 单价
        /// </summary>
        public decimal BCPrice { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int BCCount { get; set; }

        /// <summary>
        /// 取件时间
        /// </summary>
        public DateTime? BCExpectTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BCRemark { get; set; }

        /// <summary>
        /// 行政区ID
        /// </summary>
        public int BCDistrictID = 0;



    }
}