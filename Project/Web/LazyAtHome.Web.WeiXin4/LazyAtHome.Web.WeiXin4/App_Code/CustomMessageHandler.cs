using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web;

using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Entities.Request;

namespace LazyAtHome.Web.WeiXin4
{
    public class CustomMessageHandler : MessageHandler<MessageContext<IRequestMessageBase, IResponseMessageBase>>
    {
        public CustomMessageHandler(Stream inputStream, PostModel postModel, int maxRecordCount = 0)
            : base(inputStream, postModel, maxRecordCount)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            /* 所有没有被处理的消息会默认返回这里的结果，
             * 因此，如果想把整个微信请求委托出去（例如需要使用分布式或从其他服务器获取请求），
             * 只需要在这里统一发出委托请求，如：
             * var responseMessage = MessageAgent.RequestResponseMessage(agentUrl, agentToken, RequestDocument.ToString());
             * return responseMessage;
             */

            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            //responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("OnTextRequest", "debug");
            return this.CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();//转到多客服平台
        }

        //public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        //{
        //    string location = requestMessage.Location_X + "," + requestMessage.Location_Y;//百度、QQ
        //    //string location = requestMessage.Location_Y + "," + requestMessage.Location_X;//Sogou
        //    App_Code.UtilityFunc.AddToFile("location:" + location, "debug");
        //    HttpContext.Current.Cache.Insert(AppConfig.Cache_Location + requestMessage.FromUserName, location, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), System.Web.Caching.CacheItemPriority.Default, null);

        //    return base.OnLocationRequest(requestMessage);
        //}

        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            string location = requestMessage.Latitude + "," + requestMessage.Longitude;//百度、QQ
            //string location = requestMessage.Longitude + "," + requestMessage.Latitude;//Sogou
            App_Code.UtilityFunc.AddToFile("location:" + location, "debug");
            HttpContext.Current.Cache.Insert(AppConfig.Cache_Location + requestMessage.FromUserName, location, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), System.Web.Caching.CacheItemPriority.Default, null);
            return base.OnEvent_LocationRequest(requestMessage);
        }

        public override IResponseMessageBase OnEvent_ScanRequest(RequestMessageEvent_Scan requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("scan:" + requestMessage.FromUserName, "test");
            var responseMessage = CreateResponseMessage<ResponseMessageNews>();
            Article article = new Article();
            article.Title = "懒到家欢迎您！";
            article.Description = "欢迎关注懒到家，小懒在此恭候多时。干洗服务，应有尽有，一键下单，轻松搞定。全场满18元免运费，数十款衣物洗涤仅需9.9元，更多特惠活动，赶紧猛戳下方菜单试试吧~~\n\n温馨提示：如有疑问可通过微信平台咨询也可拨打客服服务热线：4008-762-799电话咨询，我们的客服服务时间为周一至周日的9：00~21：30，感谢您的支持与信赖。";
            article.PicUrl = AppConfig.BaseUrl + "/img/activity_banner.jpg";
            article.Url = AppConfig.BaseUrl + "/Home/Activites";
            responseMessage.Articles.Add(article);
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("OnEvent_SubscribeRequest", "debug");
            App_Code.Proxy.UserProxycs.User_WeixinAttention_ADD(requestMessage.FromUserName, requestMessage.CreateTime, requestMessage.EventKey);

            if (requestMessage.EventKey != "")
            {
                App_Code.UtilityFunc.AddToFile("scan:" + requestMessage.FromUserName, "test");
            }
            //return base.OnEvent_SubscribeRequest(requestMessage);
            var responseMessage = CreateResponseMessage<ResponseMessageNews>();
            Article article = new Article();
            article.Title = "懒到家欢迎您！";
            article.Description = "欢迎关注懒到家，小懒在此恭候多时。干洗服务，应有尽有，一键下单，轻松搞定。全场满18元免运费，数十款衣物洗涤仅需9.9元，更多特惠活动，赶紧猛戳下方菜单试试吧~~\n\n温馨提示：如有疑问可通过微信平台咨询也可拨打客服服务热线：4008-762-799电话咨询，我们的客服服务时间为周一至周日的9：00~21：30，感谢您的支持与信赖。";
            article.PicUrl = AppConfig.BaseUrl + "/img/activity_banner.jpg";
            article.Url = AppConfig.BaseUrl + "/Home/Activites";
            responseMessage.Articles.Add(article);
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("OnEvent_UnsubscribeRequest", "debug");
            App_Code.Proxy.UserProxycs.User_WeixinAttention_Remove(requestMessage.FromUserName);
            return base.OnEvent_UnsubscribeRequest(requestMessage);
        }
    }
}