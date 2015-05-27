using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace LazyAtHome.Web.WeiXin3.App_Code
{
    public class CustomMessageHandler : MessageHandler<MessageContext>
    {
        public CustomMessageHandler(Stream inputStream)
            : base(inputStream)
        {
            
        }

        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            //var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            //responseMessage.Content = "欢迎您光临干洗客！目前微信服务号正在开发中，如有问题请拨打4008-762-799咨询。";
            //CustomerConfig.UserOpenID = requestMessage.FromUserName;

            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            //App_Code.UtilityFunc.AddToFile("DefaultResponseMessage " + requestMessage.MsgType, "debug");
            //responseMessage.Content = "感谢您使用懒到家。如您需要帮助，请联系我们的客服：400-876-2799";
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("OnTextRequest", "debug");
            //var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            //responseMessage.Content = "感谢您使用懒到家。如您需要帮助，请联系我们的客服：400-876-2799";
            //AppConfig.WeiXin_UserOpenID = requestMessage.FromUserName;
            //return responseMessage;
            return CreateResponseMessage<ResponseMessageTransfer_Customer_Service>();
        }

        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("OnEvent_SubscribeRequest", "debug");
            App_Code.Proxy.UserProxycs.User_WeixinAttention_ADD(requestMessage.FromUserName, requestMessage.CreateTime, requestMessage.EventKey);
            //return base.OnEvent_SubscribeRequest(requestMessage);
            var responseMessage = CreateResponseMessage<ResponseMessageNews>();
            Article article = new Article();
            article.Title = "懒到家欢迎您！";
            article.Description = "欢迎关注懒到家，小懒在此恭候多时。干洗服务，应有尽有，一键下单，轻松搞定。全场满18元免运费，数十款衣物洗涤仅需9.9元，更多特惠活动，赶紧猛戳下方菜单试试吧~~\n\n温馨提示：如有疑问可通过微信平台咨询也可拨打客服服务热线：4008-762-799电话咨询，我们的客服服务时间为周一至周日的9：00~21：30，感谢您的支持与信赖。";
            article.PicUrl = AppConfig.BaseUrl + "/img/activity_banner.jpg";
            article.Url = AppConfig.BaseUrl + "/Activites";
            responseMessage.Articles.Add(article);
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("OnEvent_UnsubscribeRequest", "debug");
            App_Code.Proxy.UserProxycs.User_WeixinAttention_Remove(requestMessage.FromUserName);
            return base.OnEvent_UnsubscribeRequest(requestMessage);
        }

        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            string location = requestMessage.Latitude + "," + requestMessage.Longitude;//百度、QQ
            //string location = requestMessage.Longitude + "," + requestMessage.Latitude;//Sogou
            App_Code.UtilityFunc.AddToFile("location:" + location, "debug");
            //HttpContext.Current.Cache[AppConfig.Cache_Location + requestMessage.FromUserName] = location;
            AppConfig.WeiXin_UserOpenID = requestMessage.FromUserName;
            HttpContext.Current.Cache.Insert(AppConfig.Cache_Location + requestMessage.FromUserName, location, null, System.Web.Caching.Cache.NoAbsoluteExpiration, new TimeSpan(1, 0, 0), System.Web.Caching.CacheItemPriority.Default, null);
            return base.OnEvent_LocationRequest(requestMessage);
        }

        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            App_Code.UtilityFunc.AddToFile("OnEvent_ClickRequest", "debug");
            return base.OnEvent_ClickRequest(requestMessage);
        }

        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            App_Code.UtilityFunc.AddToFile("OnVoiceRequest " + requestMessage.MsgType + " Content: " + requestMessage.Recognition, "debug");
            responseMessage.Content = requestMessage.Recognition + " b";
            return responseMessage;
        }
    }
}