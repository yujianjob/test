using LazyAtHome.Web.API.Models.JsonResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.Models.ExpressMobileResultModels
{
    public class LoginResult : BaseResult
    {
        public UserModel user;

        public class UserModel
        {
            /// <summary>
            /// 用户ID
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 用户名
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 用户类型	1:快递 2:跟车
            /// </summary>
            public int type { get; set; }
            /// <summary>
            /// 用户手机
            /// </summary>
            public string mpno { get; set; }
            /// <summary>
            /// 站点ID
            /// </summary>
            public int nodeid { get; set; }
            /// <summary>
            /// 站点名称
            /// </summary>
            public string nodename { get; set; }
            /// <summary>
            /// 站点编号
            /// </summary>
            public string nodeno { get; set; }
            /// <summary>
            /// 推荐码
            /// </summary>
            public string reccode { get; set; }
            /// <summary>
            /// 地址
            /// </summary>
            public string addr { get; set; }
            /// <summary>
            /// 站点列表
            /// </summary>
            public IList<SiteModel> sitelist { get; set; }
        }

        /// <summary>
        /// 5.3	站点实体类
        /// </summary>
        public class SiteModel
        {
            public int id { get; set; }
            /// <summary>
            /// 站点名
            /// </summary>
            public string name { get; set; }
        }
    }
}