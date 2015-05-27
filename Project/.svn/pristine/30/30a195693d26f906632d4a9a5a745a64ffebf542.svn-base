using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.CodeSource.Common
{
    public class Func
    {
        public static string FormatOrderClass(int OrderClass)
        {
            string rtn = string.Empty;
            switch (OrderClass)
            {
                case 1:
                    rtn = "个人普通";
                    break;
                case 2:
                    rtn = "一键下单";
                    break;
                case 3:
                    rtn = "合作门店";
                    break;
                case 4:
                    rtn = "奢侈品";
                    break;
                case 5:
                    rtn = "商城";
                    break;
                default:
                    rtn = "未知：" + OrderClass;
                    break;
            }
            return rtn;

        }


        public static string FormatPayStatus(int PayStatus)
        {
            string rtn = string.Empty;
            switch (PayStatus)
            {
                case 0:
                    rtn = "未支付";
                    break;
                case 1:
                    rtn = "已支付";
                    break;
                default:
                    rtn = "未知：" + PayStatus;
                    break;
            }
            return rtn;

        }

        public static string FormatOrderStatus(int OrderStatus)
        {
            string rtn = string.Empty;
            switch (OrderStatus)
            {
                case 0:
                    rtn = "未审核订单";
                    break;
                case 1:
                    rtn = "创建订单";
                    break;
                case 2:
                    rtn = "完成订单";
                    break;
                case 6:
                    rtn = "已退单";
                    break;
                case 7:
                    rtn = "已拆单";
                    break;
                case 16:
                    rtn = "用户撤销";
                    break;
                case 32:
                    rtn = "系统撤销";
                    break;
                default:
                    rtn = "未知：" + OrderStatus;
                    break;
            }
            return rtn;

        }


        public static string FormatRegistSource(int RegistSource)
        {
            string rtn = string.Empty;
            switch (RegistSource)
            {
                case -1:
                    rtn = "客服";
                    break;
                case 1:
                    rtn = "网站";
                    break;
                case 2:
                    rtn = "APP";
                    break;
                case 3:
                    rtn = "新浪微博";
                    break;
                case 4:
                    rtn = "微信";
                    break;
                case 6:
                    rtn = "短信";
                    break;
                case 90:
                    rtn = "合作门店";
                    break;
                case 901:
                    rtn = "若海";
                    break;

                default:
                    rtn = "未知：" + RegistSource;
                    break;
            }
            return rtn;

        }


        public static string FormatOrderStep(int? OrderStep)
        {
            string rtn = string.Empty;
            switch (OrderStep)
            {
                case 1:
                    rtn = "下单";
                    break;
                case 2:
                    rtn = "取件中";
                    break;
                case 3:
                    rtn = "送洗中";
                    break;
                case 4:
                    rtn = "洗涤中";
                    break;
                case 5:
                    rtn = "出库中";
                    break;
                case 6:
                    rtn = "送件中";
                    break;
                case 7:
                    rtn = "完成";
                    break;
                default:
                    rtn = "未知：" + OrderStep;
                    break;
            }
            return rtn;
        }

    }
}