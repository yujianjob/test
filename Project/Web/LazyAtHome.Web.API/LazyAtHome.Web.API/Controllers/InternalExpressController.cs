using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.Web.API.Models.InternalExpressModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.API.Controllers
{
    public class InternalExpressController : Controller
    {
        [ValidateInput(false)]
        //[HttpGet]
        public ActionResult RoutePush(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                App_Code.UtilityFunc.Add("内部物流推送回调解析错误:Content is null");

                return Content("ERROR");
            }

            try
            {
                //记录日志
                App_Code.UtilityFunc.Add(content);

                RoutePushModel p = null;

                try
                {
                    p = Newtonsoft.Json.JsonConvert.DeserializeObject<RoutePushModel>(content);
                }
                catch (Exception e)
                {
                    App_Code.UtilityFunc.Add("内部物流推送回调解析错误" + e.Message);
                }

                //解析协议
                if (p == null)
                {
                    App_Code.UtilityFunc.Add("内部物流推送回调解析错误" + content);

                    return Content("ERROR");
                }

                var list = new List<RoutePushDataDC>();

                //调用订单系统
                foreach (var data in p.list)
                {
                    list.Add(new RoutePushDataDC()
                    {
                        ID = data.ID.ToString(),
                        RouteInfo = data.RouteInfo,
                        AcceptTime = DateTime.ParseExact(data.Obj_Cdate, "yyyy-MM-dd HH:mm:ss", null),
                        ExpressNumber = data.ExpNumber,
                        OpCode = data.OpCode,
                        OrderNumber = data.OutNumber,
                        Remark = data.Remark,
                    });
                }

                WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<bool>>(client => client.Proxy.Internal_ExpressRoutePush(list));

                App_Code.UtilityFunc.Add("SUCCESS");

                return Content("SUCCESS");

            }
            catch (Exception ex)
            {
                App_Code.UtilityFunc.Add("内部物流回调处理异常:" + ex);
                return Content("ERROR");
            }
        }

    }
}