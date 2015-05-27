using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.Winform.FactoryPortal.PRService;

namespace LazyAtHome.Winform.FactoryPortal.Common
{
    public class ConstConfig
    {
        /// <summary>
        /// 当前操作员
        /// </summary>
        public static OperatorDC currOperator = null;

        public static string currPwd = null;


        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public static LazyAtHome.Winform.FactoryPortal.WashOrderService.LoginCredentials GetWashOrderLogin()
        {
            LazyAtHome.Winform.FactoryPortal.WashOrderService.LoginCredentials login = new WashOrderService.LoginCredentials();
            login.LoginName = currOperator.LoginName;
            login.Password = currPwd;//currOperator.LoginPwd;
            login.OperatorType = WashOrderService.OperatorType.Factory;

            return login;
        }


        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public static LazyAtHome.Winform.FactoryPortal.SFExpressService.LoginCredentials GetSFExpressLogin()
        {
            LazyAtHome.Winform.FactoryPortal.SFExpressService.LoginCredentials login = new SFExpressService.LoginCredentials();
            login.LoginName = currOperator.LoginName;
            login.Password = currPwd;//currOperator.LoginPwd;
            login.OperatorType = SFExpressService.OperatorType.Factory;

            return login;
        }

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public static LazyAtHome.Winform.FactoryPortal.WashProductService.LoginCredentials GetWashProductLogin()
        {
            LazyAtHome.Winform.FactoryPortal.WashProductService.LoginCredentials login = new WashProductService.LoginCredentials();
            login.LoginName = currOperator.LoginName;
            login.Password = currPwd;//currOperator.LoginPwd;
            login.OperatorType = WashProductService.OperatorType.Factory;

            return login;
        }


        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public static LazyAtHome.Winform.FactoryPortal.QFExpressService.LoginCredentials GetQFExpressLogin()
        {
            LazyAtHome.Winform.FactoryPortal.QFExpressService.LoginCredentials login = new QFExpressService.LoginCredentials();
            login.LoginName = currOperator.LoginName;
            login.Password = currPwd;//currOperator.LoginPwd;
            login.OperatorType = QFExpressService.OperatorType.Factory;

            return login;
        }



        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <returns></returns>
        public static LazyAtHome.Winform.FactoryPortal.InternalExpressService.LoginCredentials GetInternalExpressLogin()
        {
            LazyAtHome.Winform.FactoryPortal.InternalExpressService.LoginCredentials login = new InternalExpressService.LoginCredentials();
            login.LoginName = currOperator.LoginName;
            login.Password = currPwd;//currOperator.LoginPwd;
            login.OperatorType = InternalExpressService.OperatorType.Factory;

            return login;
        }

        //public static string GetDateTimeCodeHHmmss()
        //{
        //    return System.DateTime.Now.ToString("HHmmss");
        //}

        /// <summary>
        /// 错误日志地址
        /// </summary>
        public static string WRONG_LOG_PATH = System.Configuration.ConfigurationManager.AppSettings["WrongLogPath"].ToString();



        /// <summary>
        /// 工厂名
        /// </summary>
        public static string S_Company = System.Configuration.ConfigurationManager.AppSettings["Company"].ToString();

        /// <summary>
        /// 工厂联系人
        /// </summary>
        public static string S_Contact = System.Configuration.ConfigurationManager.AppSettings["Contact"].ToString();

        /// <summary>
        /// 工厂联系电话
        /// </summary>
        public static string S_Tel = System.Configuration.ConfigurationManager.AppSettings["Tel"].ToString();

        /// <summary>
        /// 工厂联系电话
        /// </summary>
        public static string S_Phone = System.Configuration.ConfigurationManager.AppSettings["Phone"].ToString();

        /// <summary>
        /// 工厂联系地址
        /// </summary>
        public static string S_Address = System.Configuration.ConfigurationManager.AppSettings["Address"].ToString();

        /// <summary>
        /// 工厂所在城市
        /// </summary>
        public static string S_City = System.Configuration.ConfigurationManager.AppSettings["City"].ToString();

        /// <summary>
        /// 工厂所在省市
        /// </summary>
        public static string S_Province = System.Configuration.ConfigurationManager.AppSettings["Province"].ToString();

        /// <summary>
        /// 工厂邮编
        /// </summary>
        public static string S_ZipCode = System.Configuration.ConfigurationManager.AppSettings["ZipCode"].ToString();

        /// <summary>
        /// 全峰快递单打印机名称
        /// </summary>
        public static string QFPrintName = System.Configuration.ConfigurationManager.AppSettings["QFPrintName"].ToString();

        /// <summary>
        /// 顺丰快递单打印机名称
        /// </summary>
        public static string SFPrintName = System.Configuration.ConfigurationManager.AppSettings["SFPrintName"].ToString();


        /// <summary>
        /// 面单打印机名称
        /// </summary>
        public static string PrintName = System.Configuration.ConfigurationManager.AppSettings["PrintName"].ToString();

        /// <summary>
        /// 条码打印机名称
        /// </summary>
        public static string ClothesCodeBarPrintName = System.Configuration.ConfigurationManager.AppSettings["ClothesCodeBarPrintName"].ToString();

        /// <summary>
        /// 洗涤条码渠道
        /// 1：自己打条码  2：威士打条码
        /// </summary>
        public static string CodeBarChannel = System.Configuration.ConfigurationManager.AppSettings["CodeBarChannel"].ToString();


        /// <summary>
        /// 顺丰快递下单订单后缀
        /// </summary>
        public static string SFOrderPostfix = "0000";

        /// <summary>
        /// 顺丰月结卡号
        /// </summary>
        public static string SFMonthCardNo = "0210481559";
    }
}
