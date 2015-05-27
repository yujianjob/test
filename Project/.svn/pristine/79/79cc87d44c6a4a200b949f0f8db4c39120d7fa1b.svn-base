using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.API.Controllers
{
    public class SFController : Controller
    {
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult RoutePush(string content, FormCollection xlist)
        {
            //App_Code.UtilityFunc.Add("Calling...");
            //if (string.IsNullOrWhiteSpace(content))
            //{
            //    App_Code.UtilityFunc.Add("Content is null");
            //    return Content("<Response service=\"RoutePushService\"> <Head>ERR</Head> <ERROR code=\"8000\">数据格式错误</ERROR></Response>");
            //}

            //var rtn = System.Net.WebUtility.UrlDecode(content);
            //App_Code.UtilityFunc.Add("Content:" + rtn);
            //App_Code.UtilityFunc.Add("-------------------------------------------");
            //return Content("<Response service=\"RoutePushService\"><Head>OK</Head></Response>");


            if (string.IsNullOrWhiteSpace(content))
            {
                App_Code.UtilityFunc.Add("顺丰回调解析错误:Content is null");

                return Content("<Response service=\"RoutePushService\"> <Head>ERR</Head> <ERROR code=\"8000\">数据格式错误</ERROR></Response>");
            }

            try
            {
                //记录日志
                App_Code.UtilityFunc.Add(content);

                LazyAtHome.Library.Partners.SFExpress.Entity.RoutePushServiceBody pushBody = new Library.Partners.SFExpress.Entity.RoutePushServiceBody(content);

                //解析协议
                if (!pushBody.Deserialize(false))
                {
                    App_Code.UtilityFunc.Add("顺丰回调解析错误" + content);
                    return Content("<Response service=\"RoutePushService\"> <Head>ERR</Head> <ERROR code=\"8000\">数据格式错误</ERROR></Response>");
                }

                var list = new List<RoutePushDataDC>();

                //调用订单系统
                foreach (var data in pushBody.DataList)
                {
                    list.Add(new RoutePushDataDC()
                    {
                        ID = data.id,
                        RouteInfo = data.acceptAddress,
                        AcceptTime = data.acceptTime,
                        ExpressNumber = data.mailno,
                        OpCode = data.opCode,
                        OrderNumber = data.orderid,
                        Remark = data.remark,
                    });
                }

                WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(client => client.Proxy.SF_Express_RoutePush(list));

                //调用订单,回复成功

                //var response = pushBody.GetResponse(list.Select(p => p.ID).ToList(), new List<string>());

                //var response = pushBody.GetResponse();

                var response = "<Response service=\"RouteService\"><Head>OK</Head></Response>";

                App_Code.UtilityFunc.Add(response);

                return Content(response);

            }
            catch (Exception ex)
            {
                App_Code.UtilityFunc.Add("顺丰回调处理异常:" + ex);
                return Content("<Response service=\"RoutePushService\"> <Head>ERR</Head> <ERROR code=\"4001\">系统发生错误</ERROR></Response>");
            }
        }

        public ActionResult Test()
        {
            App_Code.UtilityFunc.Add("调用test");
            return Content("Test Page");
        }
    }
}