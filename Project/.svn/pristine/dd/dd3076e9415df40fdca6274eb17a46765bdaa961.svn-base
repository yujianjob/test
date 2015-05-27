using LazyAtHome.Core.Infrastructure.WCF.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.SMS
{
    class YMWebServiceClient : ClientProxyBase<YiMeiService.SDKClient>
    {
        public YMWebServiceClient()
            : base("SDKService")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key">用户自定义key值， 长度不超过15个字符的字符串(可包含数字和字母)</param>
        /// <param name="serialpass">软件序列号密码，密码（6位）</param>
        /// <returns></returns>
        public int Regist(string softwareSerialNo, string key, string serialpass)
        {
            return this.Proxy.registEx(
                   new YiMeiService.registExRequest()
                   {
                       arg0 = softwareSerialNo,
                       arg1 = key,
                       arg2 = serialpass,
                   }).@return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key">关键字key，必须和软件序列号注册的key一致</param>
        /// <param name="eName">企业名称(最多60字节)</param>
        /// <param name="linkMan">联系人姓名(最多20字节)，必须输入</param>
        /// <param name="phoneNum">联系电话(最多20字节)，必须输入</param>
        /// <param name="mobile">联系手机(最多15字节)，必须输入</param>
        /// <param name="email">电子邮件(最多60字节)，必须输入</param>
        /// <param name="fax">联系传真(最多20字节)，必须输入</param>
        /// <param name="address">公司地址(最多60字节)，必须输入</param>
        /// <param name="postcode">邮政编码(最多6字节)，必须输入</param>
        /// <returns></returns>
        public int RegistDetail(String softwareSerialNo, String key, String eName, String linkMan,
            String phoneNum, String mobile, String email, String fax, String address, String postcode)
        {
            return this.Proxy.registDetailInfo(
                   new YiMeiService.registDetailInfoRequest()
                   {
                       arg0 = softwareSerialNo,
                       arg1 = key,
                       arg2 = eName,
                       arg3 = linkMan,
                       arg4 = phoneNum,
                       arg5 = mobile,
                       arg6 = email,
                       arg7 = fax,
                       arg8 = address,
                       arg9 = postcode,
                   }).@return;
        }

        /// <summary>
        /// 3.7	发送短信
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key">用户自定义key值， 长度不超过15个字符字，和软件序列号注册时的关键字保持一致</param>
        /// <param name="sendTime">定时短信的定时时间，格式为:年年年年月月日日时时分分秒秒</param>
        /// <param name="mobiles">手机号码(字符串数组,最多为200个手机号码)</param>
        /// <param name="smsContent">短信内容(最多500个汉字或1000个纯英文</param>
        /// <param name="addSerial">扩展号码 (长度小于15的字符串) 用户可通过附加码自定义短信类别 扩展号码的功能，需另外申请，当未申请扩展号码功能时，该参数默认为空值即可。</param>
        /// <param name="smsPriority">短信等级，范围1~5，数值越高优先级越高</param>
        /// <param name="id">短信ID，自定义唯一的消息ID，数字位数最大19位，与状态报告ID一一对应，需用户自定义ID规则确保ID的唯一性。如果smsID为0将获取不到相应的状态报告信息。</param>
        /// <returns></returns>
        public int SendMessage(string softwareSerialNo, string key, string sendTime, string mobiles, string smsContent
            , string addSerial, int smsPriority, int id)
        {

            return this.Proxy.sendSMS(
                  new YiMeiService.sendSMSRequest()
                  {
                      arg0 = softwareSerialNo.ToString(),
                      arg1 = key.ToString(),
                      arg2 = sendTime.ToString(),
                      arg3 = mobiles.Trim().ToString().Split(new char[] { ',' }),
                      arg4 = smsContent.Trim().ToString(),
                      arg5 = addSerial.Trim().ToString(),
                      arg6 = "GBK",
                      arg7 = smsPriority,
                      arg8 = id,

                  }
              ).@return;
        }

        /// <summary>
        /// 3.5	查询余额
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key">关键字,必须和软件注册时的关键字保持一致</param>
        /// <returns></returns>
        public double GetBalance(String softwareSerialNo, String key)
        {
            return this.Proxy.getBalance(
                 new YiMeiService.getBalanceRequest()
                 {
                     arg0 = softwareSerialNo,
                     arg1 = key,
                 }).@return;
        }
    }
}
