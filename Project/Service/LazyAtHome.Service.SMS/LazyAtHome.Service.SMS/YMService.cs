using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.SMS
{
    internal class YMService : IService
    {
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

            //即时发送      这里是软件序列号    手机号       短信内容     优先级
            // int result = SendSMS(sn, iMobile, Global.Sign + iContent, iPriority.ToString());
            int result = SendSMS(sn, iMobile, iContent, iPriority.ToString());

            return result;

            //if (result == 1)
            //    throw new Exception("发送成功");
            //else if (result == 101)
            //    throw new Exception("网络故障");
            //else if (result == 110)
            //    throw new Exception("短信内容为空或超长");
            //else if (result == 998)
            //    throw new Exception("超时(长时间没有得到响应消息)");
            //else if (result == -1)
            //    throw new Exception("系统异常");
            //else if (result == -104)
            //    throw new Exception("请求超过限制");
            //else if (result == -117)
            //    throw new Exception("发送短信失败");
            //else if (result == -9001)
            //    throw new Exception("序列号格式错误");
            //else if (result == -9016)
            //    throw new Exception("发送短信包大小超出范围");
            //else if (result == -9020)
            //    throw new Exception("发送短信手机号格式错误");
            //else if (result == -9019)
            //    throw new Exception("发送短信优先级格式错误");
            //else if (result == -9022)
            //    throw new Exception("发送短信唯一序列值错误");
            //else if (result == -9017)
            //    throw new Exception("发送短信内容格式错误");
            //else
            //    throw new Exception("其他故障值：" + result.ToString());
        }

        public string Service_Balance()
        {
            System.Text.StringBuilder balance = new System.Text.StringBuilder(0, 20);

            //得到余额            注册号         余额
            int result = GetBalance(sn, balance);

            //string mybalance = balance.ToString(0, balance.Length - 1);
            if (result == 1)
                return balance + "元";
            else if (result == 101)
                throw new Exception("网络故障");
            else if (result == 105)
                throw new Exception("参数balance指针为空");
            else if (result == 125)
                throw new Exception("初始化通讯SOCKET失败");
            else if (result == -1)
                throw new Exception("系统异常");
            else if (result == -124)
                throw new Exception("查询余额失败");
            else if (result == -2)
                throw new Exception("客户端异常");
            else if (result == -9002)
                throw new Exception("密码格式错误");
            else if (result == -1100)
                throw new Exception("序列号错误,序列号不存在内存中,或尝试攻击的用户");
            else
                throw new Exception("其他故障值:" + result.ToString());
        }

        //软件序列号
        const string sn = "6SDK-EMY-6688-KEYPQ";
        //密码（6位）
        const string pwd = "032322";
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
            int result = Register(sn, pwd, EntName, LinkMan, Phone, Mobile, Email, Fax, sAddress, Postcode);
            if (result == 1)
                return true;
            //("注册成功");
            else if (result == 101)
                throw new Exception("网络故障");
            else if (result == 999)
                throw new Exception("调用频率过快");
            else if (result == -1)
                throw new Exception("系统异常");
            else if (result == -2)
                throw new Exception("客户端异常");
            else if (result == -101)
                throw new Exception("命令不被支持");
            else if (result == -110)
                throw new Exception("号码注册激活失败");
            else if (result == -1100)
                throw new Exception("序列号错误，序列号不存在内存中，或尝试攻击的用户");
            else if (result == -1103)
                throw new Exception("序列号Key错误");
            else if (result == -1102)
                throw new Exception("序列号密码错误");
            else
            {
                throw new Exception("其他故障值：" + result.ToString());
            }
        }

        public bool Service_UnRegister()
        {
            //注销序列号            注册号
            int result = UnRegister(sn);
            if (result == 1)
                return true;
            else if (result == 101)
                throw new Exception("网络故障");
            else if (result == 113)
                throw new Exception("调用接口失败");
            else if (result == 999)
                throw new Exception("调用频率过快");
            else if (result == -1)
                throw new Exception("系统异常");
            else if (result == -2)
                throw new Exception("客户端异常");
            else if (result == -122)
                throw new Exception("号码注销激活失败");
            else if (result == -1100)
                throw new Exception("序列号错误，序列号不存在内存中，或尝试攻击户的用");
            else
                throw new Exception("其他故障值：" + result.ToString());
        }

        #region [注册DLL]
        [DllImport("EUCPComm.dll", EntryPoint = "SendSMS")]  //即时发送
        public static extern int SendSMS(string sn, string mn, string ct, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "SendSMSEx")]  //即时发送(扩展)
        public static extern int SendSMSEx(string sn, string mn, string ct, string addi, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "SendScheSMS")]  // 定时发送
        public static extern int SendScheSMS(string sn, string mn, string ct, string ti, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "SendScheSMSEx")]  // 定时发送(扩展)
        public static extern int SendScheSMSEx(string sn, string mn, string ct, string ti, string addi, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "ReceiveSMS")]  // 接收短信
        public static extern int ReceiveSMS(string sn, deleSQF mySmsContent);

        [DllImport("EUCPComm.dll", EntryPoint = "ReceiveSMSEx")]  // 接收短信
        public static extern int ReceiveSMSEx(string sn, deleSQF mySmsContent);

        [DllImport("EUCPComm.dll", EntryPoint = "ReceiveStatusReport")]  // 接收短信报告
        public static extern int ReceiveStatusReport(string sn, delegSMSReport mySmsReport);

        [DllImport("EUCPComm.dll", EntryPoint = "ReceiveStatusReportEx")]  // 接收短信报告(带批量ID)
        public static extern int ReceiveStatusReportEx(string sn, delegSMSReportEx mySmsReportEx);

        [DllImport("EUCPComm.dll", EntryPoint = "Register")]   // 注册 
        public static extern int Register(string sn, string pwd, string EntName, string LinkMan, string Phone, string Mobile, string Email, string Fax, string sAddress, string Postcode);

        [DllImport("EUCPComm.dll", EntryPoint = "GetBalance", CallingConvention = CallingConvention.Winapi)] // 余额 
        public static extern int GetBalance(string m, System.Text.StringBuilder balance);


        [DllImport("EUCPComm.dll", EntryPoint = "ChargeUp")]  // 存值
        public static extern int ChargeUp(string sn, string acco, string pass);

        [DllImport("EUCPComm.dll", EntryPoint = "GetPrice")]  // 价格
        public static extern int GetPrice(string m, System.Text.StringBuilder balance);

        [DllImport("EUCPComm.dll", EntryPoint = "RegistryTransfer")]  //申请转接
        public static extern int RegistryTransfer(string sn, string mn);

        [DllImport("EUCPComm.dll", EntryPoint = "CancelTransfer")]  // 注销转接
        public static extern int CancelTransfer(string sn);

        [DllImport("EUCPComm.dll", EntryPoint = "UnRegister")]  // 注销
        public static extern int UnRegister(string sn);

        [DllImport("EUCPComm.dll", EntryPoint = "SetProxy")]  // 设置代理服务器 
        public static extern int SetProxy(string IP, string Port, string UserName, string PWD);

        [DllImport("EUCPComm.dll", EntryPoint = "RegistryPwdUpd")]  // 修改序列号密码
        public static extern int RegistryPwdUpd(string sn, string oldPWD, string newPWD);
        #endregion

        //回调(接收短信)
        /*回调函数参数说明(收到上行短信的各参数值)
		    mobile：手机号码（当falg=1时有内容）
		    senderaddi：发送者附加号码（当falg=1时有内容），无此项
		    recvaddi：接收者附加号码（当falg=1时有内容），无此项
		    ct：短信内容（当falg=1时有内容）
		    sd：接收时间（当falg=1时有内容，格式：yyyymmddhhnnss）
		    flag：1表示有短信，0表示无短信（不用在处理信息了）
         */
        static void getSMSContent(string mobile, string senderaddi, string recvaddi, string ct, string sd, ref int flag)
        {
            string mob = mobile;
            string content = ct;
            int myflag = flag;
        }

        //声明委托，对回调函数进行封装。
        public delegate void deleSQF(string mobile, string senderaddi, string recvaddi, string ct, string sd, ref int flag);
        deleSQF mySmsContent = new deleSQF(getSMSContent);

        //回调(接收状态报告)
        static void getSMSReport(string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag)
        {
            string mob = mobile;
            int myflag = flag;
        }
        public delegate void delegSMSReport(string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag);
        delegSMSReport mySmsReport = new delegSMSReport(getSMSReport);

        //回调(接收状态报告)带批量ID
        static void getSMSReportEx(ref long seq, string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag)
        {
            string mob = mobile;
            int myflag = flag;
        }

        public delegate void delegSMSReportEx(ref long seq, string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag);
        delegSMSReportEx mySmsReportEx = new delegSMSReportEx(getSMSReportEx);


    }
}
