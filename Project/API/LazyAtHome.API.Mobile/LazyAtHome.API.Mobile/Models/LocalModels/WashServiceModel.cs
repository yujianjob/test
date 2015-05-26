using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.3	服务实体类
    /// </summary>
    public class WashServiceModel
    {
        /// <summary>
        /// 服务ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 5.4 类别列表
        /// </summary>
        public IList<WashClassModel> classlist { get; set; }
    }
}