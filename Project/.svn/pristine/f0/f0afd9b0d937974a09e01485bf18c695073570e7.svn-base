using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.Models.LocalModels
{
    /// <summary>
    /// 5.8	订单实体类
    /// </summary>
    public class OrderModel
    {
        public OrderModel()
        {
            getexp = new OrderExpressModel()
            {
                sdate = "星期三 7月25日",
                stime = "13：00",
                status = "取件",
                pic = "",
                //pic = "http://newsyue.8866.org/img/APP/exp1.jpg",
            };
            sendexp = new OrderExpressModel()
            {
                sdate = "星期四 7月26日",
                stime = "--：--",
                status = "送件",
                pic = "",
                //pic = "http://newsyue.8866.org/img/APP/exp1.jpg",
            };
        }

        /// <summary>
        /// 订单ID
        /// </summary>
        public int orderid { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 订单状态 1.未支付 2.待取件 3.送洗中 4.洗涤中 5.送还中 6.完成
        /// </summary>
        public int status { get; set; }

        /// <summary>
        /// 洗涤状态
        /// </summary>
        public int washstatus { get; set; }
        /// <summary>
        /// 支付按钮
        /// </summary>
        public int paybtn { get; set; }

        /// <summary>
        /// 支付信息内容
        /// </summary>
        public string payresult { get; set; }

        /// <summary>
        /// 取消按钮
        /// </summary>
        public int cancelbtn { get; set; }

        /// <summary>
        /// 订单快递对象
        /// </summary>
        public OrderExpressModel getexp { get; set; }
        /// <summary>
        /// 订单快递对象
        /// </summary>
        public OrderExpressModel sendexp { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public int amount { get; set; }
        /// <summary>
        /// 需支付的金额 单位:分 未支付订单存在
        /// </summary>
        public int payamount { get; set; }
        /// <summary>
        /// 提交订单时间
        /// </summary>
        public string ptime { get; set; }
        /// <summary>
        /// 5.5 产品列表	
        /// </summary>
        public IList<WashProductModel> productlist { get; set; }
        /// <summary>
        /// 5.7 金额变动列表	
        /// </summary>
        public IList<OrderAmountModel> amountlist { get; set; }

        public void SetExpressModel(DateTime dt)
        {
            getexp = new OrderExpressModel()
            {
                sdate = dt.ToString("dddd M月d日"), // "星期三 7月25日",
                stime = "--:--",//dt.ToString("HH：mm"), //"13：00",
                status = "取件",
                pic = "", //"http://newsyue.8866.org/img/APP/exp1.jpg",
            };

            dt = dt.AddDays(3);

            sendexp = new OrderExpressModel()
            {
                sdate = dt.ToString("dddd M月d日"),
                stime = "--:--",//"16：00",
                status = "送件",
                pic = "",
            };
        }
    }
}