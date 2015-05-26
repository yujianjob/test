using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    public class DistrictModel<TD>
    {
        /// <summary>
        /// 行政区ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 行政区名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 行政区列表	递归市区
        /// </summary>
        public List<TD> dislist { get; set; }
    }
}