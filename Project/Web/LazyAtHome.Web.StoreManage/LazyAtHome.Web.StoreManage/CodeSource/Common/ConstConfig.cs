using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.StoreManage.CodeSource.Common
{
    public class ConstConfig
    {
        /// <summary>
        /// 错误日志地址
        /// </summary>
        public static string WRONG_LOG_PATH = System.Configuration.ConfigurationManager.AppSettings["WrongLogPath"].ToString();


        /// <summary>
        /// 图片路径前缀
        /// </summary>
        public static string IMAGE_PATH = System.Configuration.ConfigurationManager.AppSettings["ImagePath"];

        ///// <summary>
        ///// 运价图片目录
        ///// </summary>
        //public static string PRODUCT_IMG_PATH = System.Configuration.ConfigurationManager.AppSettings["ProductImgPath"];

        ///// <summary>
        ///// 小类图片目录
        ///// </summary>
        //public static string Class_IMG_PATH = System.Configuration.ConfigurationManager.AppSettings["ClassImgPath"];


        /// <summary>
        /// 品类目录
        /// </summary>
        public static string CATEGORY_IMG_PATH = System.Configuration.ConfigurationManager.AppSettings["CategoryImgPath"];

        public static string WRONG_SearchMessage = "查询数据发生错误，请联系管理员！";
        public static string INFO_SearchMessage = "未查到您要的数据！";
        public static string WRONG_SessionTimeoutMessage = "会话超时，请重新登录！";



        public static Order_ConsigneeAddressDC GetConsigneeAddress(Wash_StoreLoginInfoDC item)
        {
            //地址信息
            Order_ConsigneeAddressDC address = new Order_ConsigneeAddressDC();
            address.Address = item.DistrictName + item.Address;
            address.ProvinceName = item.ProvinceName;
            address.CityName = item.CityName;
            address.Mpno = item.MPNo;
            address.Phone = item.Phone;
            address.ZipCode = item.ZipCode;
            address.Consignee = item.LinkMan;

            return address;
        }
    }
}