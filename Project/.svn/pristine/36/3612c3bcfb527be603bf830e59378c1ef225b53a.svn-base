using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Library.Partners.SFExpress.Entity
{
    /// <summary>
    /// 路由推送响应
    /// </summary>
    public class RoutePushServiceResponse : EntityBase
    {
        public IList<string> SuccessID { get; set; }
        public IList<string> ErrID { get; set; }

        public RoutePushServiceResponse()
            : base("RouteService")
        {

        }

        public RoutePushServiceResponse(IList<string> iSuccessID, IList<string> iErrID)
            : this()
        {
            this.SuccessID = iSuccessID;
            this.ErrID = iErrID;
        }

        public RoutePushServiceResponse(int iErrCode, string iErrMessage)
            : this()
        {
            this.ErrCode = iErrCode;
            this.ErrMessage = iErrMessage;
        }

        public override void CreateBody(XmlElement body)
        {
            //var bill = XmlDoc.CreateElement("WaybillRouteResponse");

            //bill.SetAttribute("id", string.Join(",", SuccessID.ToArray()));

            //if (ErrID.Count > 0)
            //{
            //    bill.SetAttribute("id_error", string.Join(",", ErrID.ToArray()));
            //}

            //body.AppendChild(bill);
        }
    }
}
