using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace LazyAtHome.Web.Wash2.App_Code
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

            Article article = new Article();
            article.Title = "懒到家欢迎您！";
            article.Description = "欢迎关注懒到家，小懒在此恭候多时。\n干洗服务，应有尽有，一键下单，轻松搞定。全场免运费，还有更多特惠活动，赶紧猛戳下方菜单试试吧~~\n\n****温馨提示*****\n如需服务咨询、操作帮助，请拔打客服电话4008-762-799，客服工作时间周一至周日9:00~18:00。";
            article.PicUrl = AppConfig.WeiXin_BaseUrl + "/Images/welcome.jpg";
            article.Url = AppConfig.WeiXin_BaseUrl + "/weixin/welcome";

            var responseMessage = CreateResponseMessage<ResponseMessageNews>();
            responseMessage.Articles.Add(article);
            return responseMessage;
        }

        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>(); //ResponseMessageText也可以是News等其他类型
            responseMessage.Content = "感谢您使用懒到家。如您需要帮助，请联系我们的客服：400-876-2799";
            AppConfig.WeiXin_UserOpenID = requestMessage.FromUserName;
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            App_Code.Proxy.UserProxycs.User_WeixinAttention_ADD(requestMessage.FromUserName, requestMessage.CreateTime, requestMessage.EventKey);
            return base.OnEvent_SubscribeRequest(requestMessage);
        }

        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            App_Code.Proxy.UserProxycs.User_WeixinAttention_Remove(requestMessage.FromUserName);
            return base.OnEvent_UnsubscribeRequest(requestMessage);
        }

        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {

            return base.OnEvent_ClickRequest(requestMessage);
        }
    }
}