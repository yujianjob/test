using LazyAtHome.Core.Infrastructure.WCF.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace LazyAtHome.Service.SMS
{
    public class JZServiceClient : ClientProxyBase<JianZhouService.BusinessService>
    {
        public JZServiceClient()
            : base("BusinessServicePort")
        {
        }

        /// <summary>
        /// 批量发送短信
        /// </summary>
        /// <param name="iPhone">多个手机号码用;分割，建议一次提交1000左右的号码，多次提交</param>
        /// <param name="iContent">短消息内容</param>
        /// <returns></returns>
        public int SendBatchMessage(string iPhone, string iContent)
        {
            JianZhouService.sendBatchMessageBody sbmb = new JianZhouService.sendBatchMessageBody();
            sbmb.account = JZService.Account;
            sbmb.password = JZService.Password;
            sbmb.destmobile = iPhone;
            sbmb.msgText = iContent;
            JianZhouService.sendBatchMessage req = new JianZhouService.sendBatchMessage(sbmb);
            JianZhouService.sendBatchMessageResponse resp = this.Proxy.sendBatchMessage(req);
            JianZhouService.sendBatchMessageResponseBody body = resp.Body;
            return body.sendBatchMessageReturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPhone"></param>
        /// <param name="iContent"></param>
        /// <returns></returns>
        public int SendMessage(string iPhone, string iContent)
        {
            JianZhouService.sendMessageBody sbmb = new JianZhouService.sendMessageBody();
            sbmb.account = JZService.Account;
            sbmb.password = JZService.Password;
            sbmb.destmobile = iPhone;
            sbmb.msgText = iContent;
            JianZhouService.sendMessage req = new JianZhouService.sendMessage(sbmb);
            JianZhouService.sendMessageResponse resp = this.Proxy.sendMessage(req);
            JianZhouService.sendMessageResponseBody body = resp.Body;
            return body.sendMessageReturn;
        }

        /// <summary>
        /// 批量发送个性化短信
        /// </summary>
        /// <param name="iPhone">多个手机号码用||分割</param>
        /// <param name="iContent">短消息内容 多个内容用||分隔,号码和内容||分隔数量必须相等</param>
        /// <returns></returns>
        public int SendPersonalMessages(string iPhone, string iContent)
        {
            JianZhouService.sendPersonalMessagesBody spmb = new JianZhouService.sendPersonalMessagesBody();
            spmb.account = JZService.Account;
            spmb.password = JZService.Password;
            spmb.destMobiles = iPhone;
            spmb.msgContents = iContent;
            JianZhouService.sendPersonalMessages req = new JianZhouService.sendPersonalMessages(spmb);
            JianZhouService.sendPersonalMessagesResponse resp = this.Proxy.sendPersonalMessages(req);
            JianZhouService.sendPersonalMessagesResponseBody body = resp.Body;
            return body.sendPersonalMessagesReturn;
        }

        /// <summary>
        /// 获取余额
        /// </summary>
        /// <returns></returns>
        public int GetUserBalance()
        {
            JianZhouService.getUserInfoBody guib = new JianZhouService.getUserInfoBody();
            guib.account = JZService.Account;
            guib.password = JZService.Password;
            JianZhouService.getUserInfo req = new JianZhouService.getUserInfo(guib);
            JianZhouService.getUserInfoResponse resp = this.Proxy.getUserInfo(req);
            JianZhouService.getUserInfoResponseBody body = resp.Body;
            var xmlString = body.getUserInfoReturn;

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlString);
            var Balance = xmldoc.SelectSingleNode("/userinfo/remainFee").InnerText;
            return Convert.ToInt32(Balance);
        }

        /// <summary>
        /// 验证用户名和密码
        /// </summary>
        /// <returns></returns>
        public bool ValidateUser()
        {
            JianZhouService.validateUserBody vub = new JianZhouService.validateUserBody();
            vub.account = JZService.Account;
            vub.password = JZService.Password;
            JianZhouService.validateUser req = new JianZhouService.validateUser(vub);
            JianZhouService.validateUserResponse resp = this.Proxy.validateUser(req);
            JianZhouService.validateUserResponseBody body = resp.Body;
            if (body.validateUserReturn == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
