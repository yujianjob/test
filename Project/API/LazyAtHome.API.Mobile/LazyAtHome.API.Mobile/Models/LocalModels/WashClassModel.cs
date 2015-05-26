using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.4	分类实体类
    /// </summary>
    public class WashClassModel
    {
        /// <summary>
        /// 类别ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 类别名	
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 5.5 产品列表
        /// </summary>
        public IList<WashProductModel> productlist { get; set; }
    }
}