using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Express.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Express
{
    /// <summary>
    /// 库存物品页面展示实体类
    /// </summary>
    [Serializable]
    public class ExpStorageItemModel
    {
        /// <summary>
        /// 存衣点信息
        /// </summary>
        public Exp_StorageItemDC StorageItemInfo { get; set; }
    }
}