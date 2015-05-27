using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.SFExpress.Entity
{
    /// <summary>
    /// 路由推送
    /// </summary>
    public class RoutePushServiceBody : EntityBase
    {
        public class BodyData
        {
            /// <summary>
            /// 路由编号
            /// </summary>
            public string id { get; set; }

            /// <summary>
            /// 运单号
            /// </summary>
            public string mailno { get; set; }

            /// <summary>
            /// 订单号
            /// </summary>
            public string orderid { get; set; }

            /// <summary>
            /// 路由发生时间
            /// </summary>
            public DateTime acceptTime { get; set; }

            /// <summary>
            /// 路由发生城市
            /// </summary>
            public string acceptAddress { get; set; }

            /// <summary>
            /// 路由说明
            /// </summary>
            public string remark { get; set; }

            /// <summary>
            /// 操作码
            /// </summary>
            public string opCode { get; set; }

        }

        public IList<BodyData> DataList;

        public RoutePushServiceBody(string xml)
            : base(null, xml)
        {

        }

        public override bool AnalyzeBody()
        {
            var nodes = XmlDoc.SelectNodes("/Request/Body/WaybillRoute");

            DataList = new List<BodyData>();

            foreach (System.Xml.XmlNode node in nodes)
            {
                var entity = new BodyData();

                //路由编号
                entity.id = node.Attributes["id"].Value;
                //运单号
                entity.mailno = node.Attributes["mailno"].Value;
                //订单号
                if (node.Attributes["orderid"] != null)
                {
                    entity.orderid = node.Attributes["orderid"].Value;
                }
                //路由产生时间
                entity.acceptTime = DateTime.ParseExact(node.Attributes["acceptTime"].Value, "yyyy-MM-dd HH:mm:ss", null);
                //路由发生城市
                entity.acceptAddress = node.Attributes["acceptAddress"].Value;
                //路由说明
                entity.remark = node.Attributes["remark"].Value;
                //操作码
                if (node.Attributes["opCode"] != null)
                {
                    entity.opCode = node.Attributes["opCode"].Value;
                }

                DataList.Add(entity);
            }
            return true;
        }

        public string GetResponse()
        {
            RoutePushServiceResponse res = new RoutePushServiceResponse();

            return res.Serializer(false);
        }

        public string GetResponse(IList<string> iSuccessID, IList<string> iErrID)
        {
            RoutePushServiceResponse res = new RoutePushServiceResponse(iSuccessID, iErrID);

            return res.Serializer(false);
        }

        public string GetResponse(int iErrCode, string iErrMessage)
        {
            RoutePushServiceResponse res = new RoutePushServiceResponse(iErrCode, iErrMessage);

            return res.Serializer(false);
        }
    }
}
