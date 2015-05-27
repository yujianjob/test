using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Business.Business
{
    public partial class Order
    {
        ///// <summary>
        ///// 1 订单创建
        ///// </summary>
        ///// <param name="iOrder"></param>
        //public void Order_Message_FinshOrder(Order_OrderDC iOrder)
        //{

        //}

        /// <summary>
        /// 2 订单完成
        /// </summary>
        /// <param name="iOrder"></param>
        public void Order_Message_GetPackage(Order_OrderDC iOrder)
        {
            //若海推送
            if (iOrder.Channel == (int)Channel.Partner_Ruohai)
            {
                Partner_Ruohai_Order_ChangeStatus(iOrder.OrderNumber, 1);
            }
            else
            {
                orderDAL.Order_Finish_Message(iOrder.ID);
            }
        }

        /// <summary>
        /// 3 送洗中 送往工厂(生成物流单号,物流已扫码取件)
        /// </summary>
        /// <param name="iOrder"></param>
        public void Order_Message_SendFactory(Order_OrderDC iOrder)
        {
            //若海推送
            if (iOrder.Channel == (int)Channel.Partner_Ruohai)
            {
                Partner_Ruohai_Order_ChangeStatus(iOrder.OrderNumber, 2);
            }
        }

        /// <summary>
        /// 4 洗涤中 工厂入库(打印水洗条码)
        /// </summary>
        /// <param name="iOrder"></param>
        public void Order_Message_Wash(Order_OrderDC iOrder)
        {
            //推送短信
            orderDAL.Order_Onekey_InFactory_Message(iOrder.ID);

            //若海推送
            if (iOrder.Channel == (int)Channel.Partner_Ruohai)
            {
                Partner_Ruohai_Order_ChangeStatus(iOrder.OrderNumber, 3);
            }
        }

        /// <summary>
        /// 5 工厂出库(打印出库面单)
        /// </summary>
        /// <param name="iOrder"></param>
        public void Order_Message_OutFactory(Order_OrderDC iOrder)
        {
            //工厂出库消息推送
            orderDAL.Order_OutFactory_Message(iOrder.ID);
        }

        /// <summary>
        /// 6 送件中(收到物流数据)
        /// </summary>
        /// <param name="iOrder"></param>
        public void Order_Message_SendPackage(Order_OrderDC iOrder)
        {

        }

        /// <summary>
        /// 7 全部完成
        /// </summary>
        /// <param name="iOrder"></param>
        public void Order_Message_AllFinish(Order_OrderDC iOrder)
        {
            //发送完成短信
            orderDAL.Order_AllFinish_Message(iOrder.ID);

            //若海推送
            if (iOrder.Channel == (int)Channel.Partner_Ruohai)
            {
                Partner_Ruohai_Order_ChangeStatus(iOrder.OrderNumber, 4);
            }
        }


    }
}
