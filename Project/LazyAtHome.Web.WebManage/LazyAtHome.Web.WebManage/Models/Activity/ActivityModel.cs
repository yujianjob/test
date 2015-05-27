using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Activity
{
    /// <summary>
    /// 活动页信息
    /// </summary>
    [Serializable]
    public class ActivityModel
    {
        /// <summary>
        /// 产品基础信息
        /// </summary>
        public Wash_ActivityDC ActivityInfo { get; set; }


        /// <summary>
        /// 图片目录路径
        /// </summary>
        public string ImgRoot = LazyAtHome.Web.WebManage.CodeSource.Common.ConstConfig.IMAGE_PATH + LazyAtHome.Web.WebManage.CodeSource.Common.ConstConfig.Activity_IMG_PATH;
    }
}