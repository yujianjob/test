using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.SMS
{
    internal class YMWebService : IService
    {
        public YMWebService()
        {
            
        }

        public int Service_SendSMS(string iMobile, string iContent, int iPriority, int iType)
        {
            if (iType == 1)
            {
                iPriority = 5;
            }
            if (iPriority < 1 || iPriority > 5)
            {
                iPriority = 3;
            }

            var result = WCFInvokeHelper<YMWebServiceClient>.Invoke<int>(client =>
              client.SendMessage(sn, key, string.Empty, iMobile, iContent, string.Empty, iPriority, 0));
            if (result >= 0)
            {
                return 1;
            }
            else
            {
                return result;
            }
        }

        public string Service_Balance()
        {
            var result = WCFInvokeHelper<YMWebServiceClient>.Invoke<double>(client =>
                            client.GetBalance(sn, key));

            return result + "元";
        }

        //软件序列号
        const string sn = "9SDK-EMY-0999-JDZTN";
        //密码（6位）
        const string pwd = "154301";
        //自定义key
        const string key = "5EEA7B06";
        //企业名称(最多60字节)
        const string EntName = "上海懒到佳信息技术有限公司";
        //联系人姓名(最多20字节)
        const string LinkMan = "陈英辉";
        //联系电话(最多20字节)
        const string Phone = "86-21-6601 6229";
        //联系手机(最多15字节)
        const string Mobile = "18601717193";
        //电子邮件(最多60字节)
        const string Email = "admin@landaojia.com";
        //联系传真(最多20字节)
        const string Fax = "86-21-6601 6230";
        //公司地址(最多60字节)
        const string sAddress = "上海市宝山区泰和路2038号D楼203室";
        //邮政编码(最多6字节)
        const string Postcode = "201901";

        /*
        * 注册企业信息Register（软件序列号,密码（6位）,企业名称(最多60字节)，联系人姓名(最多20字节),
        * 联系电话(最多20字节)，联系手机(最多15字节)，电子邮件(最多60字节)，联系传真(最多20字节)，
        * 公司地址(最多60字节)，邮政编码(最多6字节)）
        * 
        * 参数说明：
        * 注册需要的序列号,请通过亿美销售人员获取
        * 注册需要的密码,请通过亿美销售人员获取
        * 
        * 在注册序列号时，需注意不需要每次发送短信时都注册一遍，  一个序列号在一台机器上只需要注册一次就够了
        * 即便是关闭应用程序或重启电脑也不需要重新注册，除非重装电脑或注销序列号之后才需要重新注册。
        */
        public bool Service_Register()
        {
            Console.WriteLine("注册序列号...");
            var result = WCFInvokeHelper<YMWebServiceClient>.Invoke<int>(client =>
                          client.Regist(sn, key, pwd));
            if (result != 0)
                throw new Exception("注册失败：" + result.ToString());

            Console.WriteLine("注册企业信息...");
            result = WCFInvokeHelper<YMWebServiceClient>.Invoke<int>(client =>
                        client.RegistDetail(sn, key, EntName, LinkMan, Phone, Mobile, Email, Fax, sAddress, Postcode));
            if (result == 0)
            {
                Console.WriteLine("成功.");
                return true;
            }
            else
                throw new Exception("注册详情失败：" + result.ToString());
        }

        /// <summary>
        /// 3.3	注销序列号
        /// </summary>
        /// <param name="softwareSerialNo"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Service_UnRegister()
        {
            //注销序列号            注册号
            var result = WCFInvokeHelper<YMWebServiceClient>.Invoke<int>(client =>
                          client.Proxy.logout(
                          new YiMeiService.logoutRequest()
                          {
                              arg0 = sn,
                              arg1 = key,
                          }
                          ).@return);
            if (result == 0)
                return true;
            else
                throw new Exception("注册详情失败：" + result.ToString());
        }
    }
}
