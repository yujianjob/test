using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.Web.API.Models.JsonResultModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.Models.InternalExpressModels
{
    public class RoutePushModel : BaseResult
    {
        public IList<RoutePushDC> list { get; set; }

        public class RoutePushDC
        {
            /// <summary>
            /// 主键
            /// </summary>
            public int ID { set; get; }

            /// <summary>
            /// 物流单号
            /// </summary>
            public string ExpNumber { set; get; }

            /// <summary>
            /// 外部单号
            /// </summary>
            public string OutNumber { set; get; }

            /// <summary>
            /// 操作代码
            /// </summary>
            public string OpCode { set; get; }

            /// <summary>
            /// 流转信息
            /// </summary>
            public string RouteInfo { set; get; }

            /// <summary>
            /// 备注
            /// </summary>
            public string Remark { set; get; }

            public string Obj_Cdate { set; get; }
        }

    }
}