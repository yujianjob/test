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
        //private void Order_Step_CreateOrder(Order_OrderDC iOrder)
        //{
        //    //步骤在取件中
        //    if (iOrder.Step != (int)WashStepType.CreateOrder)
        //    {
        //        //步骤添加
        //        orderDAL.Order_Step_ADD(iOrder.ID, WashStepType.CreateOrder, "下单");

        //        Order_Message_FinshOrder(iOrder);
        //    }
        //}

        /// <summary>
        /// 2 订单完成
        /// </summary>
        private void Order_Step_GetPackage(Order_OrderDC iOrder)
        {
            //步骤在取件中
            if (iOrder.Step != (int)WashStepType.GetPackage)
            {
                //步骤添加
                orderDAL.Order_Step_ADD(iOrder.ID, WashStepType.GetPackage, "取件中");

                Order_Message_GetPackage(iOrder);
            }
        }

        /// <summary>
        /// 3 送洗中 送往工厂(生成物流单号,物流已扫码取件)
        /// </summary>
        private void Order_Step_SendFactory(Order_OrderDC iOrder, string iExpressNumber, bool iSendMessage)
        {
            if (!string.IsNullOrWhiteSpace(iExpressNumber))
            {
                //更新物流号
                orderDAL.Order_Order_UPDATE_ExpressNumber(iOrder.ID, iExpressNumber, ExpressType.Get);
            }

            //步骤在取件中
            if (iOrder.Step != (int)WashStepType.SendFactory)
            {
                //步骤添加
                orderDAL.Order_Step_ADD(iOrder.ID, WashStepType.SendFactory, "送洗中");

                if (iSendMessage)
                {
                    Order_Message_SendFactory(iOrder);
                }
            }
        }

        /// <summary>
        /// 4 洗涤中 工厂入库(打印水洗条码)
        /// </summary>
        /// <param name="iOrder"></param>
        private void Order_Step_Wash(Order_OrderDC iOrder)
        {
            if (iOrder.Step != (int)WashStepType.Wash)
            {
                //更新订单状态
                orderDAL.Order_Step_ADD(iOrder.ID, WashStepType.Wash, "洗涤中");

                Order_Message_Wash(iOrder);
            }
        }

        /// <summary>
        /// 5 工厂出库(打印出库面单)
        /// </summary>
        /// <param name="iOrder"></param>
        private void Order_Step_OutFactory(Order_OrderDC iOrder)
        {
            //更新订单状态
            if (iOrder.Step != (int)WashStepType.Delivery)
            {
                //步骤添加
                orderDAL.Order_Step_ADD(iOrder.ID, WashStepType.Delivery, "出库中");

                Order_Message_OutFactory(iOrder);
            }
        }

        /// <summary>
        /// 6 送件中(收到物流数据)
        /// </summary>
        /// <param name="iOrder"></param>
        private void Order_Step_SendPackage(Order_OrderDC iOrder)
        {
            //更新订单状态
            if (iOrder.Step != (int)WashStepType.SendPackage)
            {
                //步骤添加
                orderDAL.Order_Step_ADD(iOrder.ID, WashStepType.SendPackage, "送件中");

                Order_Message_SendPackage(iOrder);
            }
        }

        /// <summary>
        /// 7 全部完成
        /// </summary>
        /// <param name="iOrder"></param>
        private void Order_Step_AllFinish(Order_OrderDC iOrder, DateTime iFinishTime, bool iSendMessage)
        {
            //更新订单状态
            if (iOrder.Step != (int)WashStepType.Finish)
            {
                //还有未支付的金额,用现金支付
                if (iOrder.TotalAmount - iOrder.PayAmount > 0)
                {
                    Order_Order_Pay(iOrder.OrderNumber, iOrder.TotalAmount - iOrder.PayAmount, PayMoneyType.None, PayMoneyChannel.Null, DateTime.Now, null);
                }

                //步骤添加
                orderDAL.Order_Step_ADD(iOrder.ID, WashStepType.Finish, "完成");

                //更新全部完成时间
                orderDAL.Order_Order_UPDATE_AllFinish(iOrder.ID, iFinishTime, iOrder.UserID);

                if (iSendMessage)
                {
                    Order_Message_AllFinish(iOrder);
                }
            }
        }

    }
}
