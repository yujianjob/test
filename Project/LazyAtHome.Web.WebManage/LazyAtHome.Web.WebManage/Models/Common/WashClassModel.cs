using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Common
{
    /// <summary>
    /// 分类 品类 二级联动下拉框控件页实体
    /// </summary>
    [Serializable]
    public class WashClassModel
    {
        /// <summary>
        /// 产品分类
        /// </summary>
        public IList<Wash_ClassDC> WashClassL1 { set; get; }

        /// <summary>
        /// 产品品类
        /// </summary>
        public IList<Wash_ClassDC> WashClassL2 { set; get; }

        /// <summary>
        /// 产品分类ID
        /// </summary>
        public int PID { set; get; }

        /// <summary>
        /// 产品品类ID
        /// </summary>
        public int ID { set; get; }
    }
}