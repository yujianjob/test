using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.ClientProxy;
using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Business
{
    public partial class ManageExpress
    {
        private LazyAtHome.WCF.Express.Interface.IDAL.IExpressDAL expressDAL;

        public ManageExpress()
        {
            if (expressDAL == null)
                expressDAL = new LazyAtHome.WCF.Express.DAL.ExpressDAL();
        }

        static ManageExpress _ManageExpress;

        public static ManageExpress Instance
        {
            get
            {
                if (_ManageExpress == null)
                {
                    _ManageExpress = new ManageExpress();
                }
                return _ManageExpress;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Exp_Node_ADD(Exp_NodeDC iExp_NodeDC)
        {


            //加仓库
            var storageID = expressDAL.Exp_Storage_ADD(new Exp_StorageDC()
            {
                LinkManID = iExp_NodeDC.LinkManID,
                ManagerID = iExp_NodeDC.ManagerID,
                Name = iExp_NodeDC.Name,
                Type = iExp_NodeDC.Type,
            });
            if (storageID <= 0)
            {
                return new ReturnEntity<int>(-1, "添加站点仓库错误");
            }

            iExp_NodeDC.StorageID = storageID;

            var rtn = expressDAL.Exp_Node_ADD(iExp_NodeDC);

            Allocation.Instance.NodeListNeedRefresh = true;

            return new ReturnEntity<int>(rtn);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iExp_NodeDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Node_UPDATE(Exp_NodeDC iExp_NodeDC)
        {
            var storage = expressDAL.Exp_Storage_SELECT_Entity(iExp_NodeDC.StorageID);
            if (storage != null)
            {
                storage.LinkManID = iExp_NodeDC.LinkManID;
                storage.ManagerID = iExp_NodeDC.ManagerID;
                storage.Name = iExp_NodeDC.Name;
                expressDAL.Exp_Storage_UPDATE(storage);
            }

            var rtn = expressDAL.Exp_Node_UPDATE(iExp_NodeDC);

            Allocation.Instance.NodeListNeedRefresh = true;

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 根据ID查询Exp_Node
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_NodeDC> Exp_Node_SELECT_Entity(int iID)
        {
            var rtn = expressDAL.Exp_Node_SELECT_Entity(iID);

            if (rtn != null && rtn.ParentID > 0)
            {
                var parent = expressDAL.Exp_Node_SELECT_Entity(rtn.ParentID);
                if (parent != null)
                {
                    rtn.ParentName = parent.Name;
                }
            }

            return new ReturnEntity<Exp_NodeDC>(rtn);
        }

        /// <summary>
        /// 查询站点Exp_Node
        /// </summary>
        /// <param name="iDistrictID"></param>
        /// <param name="iName"></param>
        /// <param name="iType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_NodeDC>> Exp_Node_SELECT_List(
            int? iAreaID, string iName,
            int? iType,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize, int? iParentID = null)
        {
            var rtn = expressDAL.Exp_Node_SELECT_List(iAreaID, iName, iType, iParentID, iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_NodeDC>>(rtn);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="iExp_OperatorDC"></param>
        ///// <returns></returns>
        //public ReturnEntity<int> Exp_Operator_ADD(Exp_OperatorDC iExp_OperatorDC)
        //{
        //    var rtn = expressDAL.Exp_Operator_ADD(iExp_OperatorDC);

        //    return new ReturnEntity<int>(rtn);
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="iExp_OperatorDC"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> Exp_Operator_UPDATE(Exp_OperatorDC iExp_OperatorDC)
        //{
        //    var rtn = expressDAL.Exp_Operator_UPDATE(iExp_OperatorDC);

        //    return new ReturnEntity<bool>(rtn);
        //}

        ///// <summary>
        ///// 根据ID查询Exp_Operator
        ///// </summary>
        ///// <param name="iID">主键</param>
        ///// <returns></returns>
        //public ReturnEntity<Exp_OperatorDC> Exp_Operator_SELECT_Entity(int iID)
        //{
        //    var rtn = expressDAL.Exp_Operator_SELECT_Entity(iID);

        //    return new ReturnEntity<Exp_OperatorDC>(rtn);
        //}

        ///// <summary>
        ///// 查询全部
        ///// </summary>
        ///// <returns></returns>
        //public ReturnEntity<RecordCountEntity<Exp_OperatorDC>> Exp_Operator_SELECT_List(
        //    string iLoginName, string iName, string iMpNo,
        //    int? iType,
        //    string iNodeName,
        //    DateTime? iStartDate, DateTime? iEndDate,
        //    int iPageIndex, int iPageSize)
        //{
        //    var rtn = expressDAL.Exp_Operator_SELECT_List(iLoginName, iName, iMpNo,
        //                     iType, iNodeName, iStartDate, iEndDate, iPageIndex, iPageSize);

        //    return new ReturnEntity<RecordCountEntity<Exp_OperatorDC>>(rtn);
        //}

        /// <summary>
        /// 查询全部Exp_Order
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_List(
            string iExpNumber, string iOutNumber,
            int? iTransportType,
            string iAddress, string iContacts, string iMpno,
            int? iStep,
            string iKeyword,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            var list = expressDAL.Exp_Order_SELECT_List(iExpNumber, iOutNumber,
                            iTransportType, iAddress, iContacts, iMpno,
                            iStep, iKeyword, iStartDate, iEndDate, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_OrderDC>>(list);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_OrderDC>> Exp_Order_SELECT_List_UnAllocation(
          string iExpNumber, string iOutNumber,
          int? iTransportType,
          string iAddress, string iContacts, string iMpno,
          int? iStep,
          DateTime? iStartDate, DateTime? iEndDate,
          int iPageIndex, int iPageSize)
        {
            var list = expressDAL.Exp_Order_SELECT_List_UnAllocation(iExpNumber, iOutNumber,
                            iTransportType, iAddress, iContacts, iMpno,
                            iStep, iStartDate, iEndDate, iPageIndex, iPageSize);

            //if (list != null && list.ReturnList != null)
            //{
            //    IDictionary<int, Exp_NodeDC> nodeDi = null;

            //    var nodelist = ExpressClient.Cache_Exp_Node_GetList();
            //    if (nodelist != null)
            //    {
            //        nodeDi = nodelist.ToDictionary(p => p.ID, p => p);
            //    }

            //    foreach (var item in list.ReturnList)
            //    {
            //        if (item.NodeID.HasValue && nodeDi.ContainsKey(item.NodeID.Value))
            //        {
            //            item.NodeName = nodeDi[item.NodeID.Value].Name;
            //        }
            //        if (item.OperatorID.HasValue)
            //        {
            //            var oper = ExpressClient.Cache_Exp_Operator_GetEntity(item.OperatorID.Value);
            //            if (oper != null && oper.OBJ != null)
            //            {
            //                item.OperatorName = oper.OBJ.Name;
            //            }
            //        }
            //    }
            //}

            return new ReturnEntity<RecordCountEntity<Exp_OrderDC>>(list);
        }

        /// <summary>
        /// 更新Exp_Order
        /// </summary>
        /// <param name="iExp_OrderDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE(Exp_OrderDC iExp_OrderDC)
        {
            var rtn = expressDAL.Exp_Order_UPDATE(iExp_OrderDC);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 根据ID查询Exp_Order
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_OrderDC> Exp_Order_SELECT_Entity(int iOrderID)
        {
            var rtn = expressDAL.Exp_Order_SELECT_Entity(iOrderID);

            return new ReturnEntity<Exp_OrderDC>(rtn);
        }

        /// <summary>
        /// 手动分配
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Allocation(int iOrderID, int? iOperatorID, int? iNodeID, int iMuser)
        {
            return new ReturnEntity<bool>(-1, "不再提供支持");

            //var rtn = expressDAL.Exp_Order_Allocation(iOrderID, iOperatorID, iNodeID, iMuser);

            //return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 强制分配
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Allocation_Forced(int iOrderID, int? iOperatorID, int? iNodeID,
            int iMuser, string iCSRemark)
        {
            var order = expressDAL.Exp_Order_SELECT_Entity(iOrderID);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单不存在");
            }

            if (iOperatorID == null && iNodeID == null)
            {
                //清空数据,重新分配
                expressDAL.Exp_Order_Clear(iOrderID, order.OutNumber);
            }
            else if (iOperatorID == null && iNodeID.HasValue)
            {
                //分配到该站点下所有人

                var node = expressDAL.Exp_Node_SELECT_Entity(iNodeID.Value);
                if (node == null)
                {
                    return new ReturnEntity<bool>(-1, "站点不存在");
                }

                if (!expressDAL.Exp_Order_Allocation_Success(iOrderID, iNodeID.Value, node.No, order.Address, node.AlarmType))
                {
                    return new ReturnEntity<bool>(-1, "站点无人在岗");
                }
            }
            else if (iOperatorID.HasValue && iNodeID.HasValue)
            {
                //直接分配到该人手里
                expressDAL.Exp_Order_Allocation_ToOperator(order.ID, order.OutNumber, iNodeID.Value, iOperatorID.Value, order.Address);
            }
            else
            {
                return new ReturnEntity<bool>(-1, "无法处理");
            }

            expressDAL.Exp_Order_UPDATE_CSRemark(iOrderID, iCSRemark);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStep"></param>
        /// <param name="iStepRemark"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_SetStep(int iOrderID, int iStep, string iStepRemark, int iMuser)
        {
            var rtn = expressDAL.Exp_Order_SetStep(iOrderID, iStep, iStepRemark, iMuser);

            return new ReturnEntity<bool>(rtn);
        }


        /// <summary>
        /// 根据ID查询Exp_Preson_Account
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_Preson_AccountDC> Exp_Preson_Account_SELECT_Entity_UserID(int iUserID)
        {
            var rtn = expressDAL.Exp_Preson_Account_SELECT_Entity_UserID(iUserID);

            return new ReturnEntity<Exp_Preson_AccountDC>(rtn);
        }

        /// <summary>
        /// 查询全部Exp_Preson_CommissionLog
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_Preson_CommissionLogDC>> Exp_Preson_CommissionLog_SELECT_List(
            int? iUserID, int iPageIndex, int iPageSize)
        {
            var rtn = expressDAL.Exp_Preson_CommissionLog_SELECT_List(iUserID, iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_Preson_CommissionLogDC>>(rtn);
        }

        /// <summary>
        /// 查询全部Exp_Preson_CommissionBill
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_Preson_CommissionBillDC>> Exp_Preson_CommissionBill_SELECT_List(
             string iPeriod, string iOperatorName, int? iBillStatus,
             int iPageIndex, int iPageSize)
        {
            var rtn = expressDAL.Exp_Preson_CommissionBill_SELECT_List(iPeriod, iOperatorName, iBillStatus,
               iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_Preson_CommissionBillDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_CommissionBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_Preson_CommissionBillDC> Exp_Preson_CommissionBill_SELECT_Entity(int iID)
        {
            var rtn = expressDAL.Exp_Preson_CommissionBill_SELECT_Entity(iID);

            return new ReturnEntity<Exp_Preson_CommissionBillDC>(rtn);
        }

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Preson_CommissionBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iMuser)
        {
            var bill = expressDAL.Exp_Preson_CommissionBill_SELECT_Entity(iBillID);
            if (bill == null)
            {
                return new ReturnEntity<bool>(-1, "账单不存在");
            }
            if (bill.BillStatus == 2)
            {
                return new ReturnEntity<bool>(-1, "账单已结算完成");
            }
            if (bill.RealCommission + iRealMoney > bill.Commission)
            {
                return new ReturnEntity<bool>(-1, "结算金额合计大于账单金额");
            }

            var rtn = expressDAL.Exp_Preson_CommissionBill_UPDATE_Close(iBillID, iRealMoney, (bill.RealCommission + iRealMoney == bill.Commission) ? 2 : 1, iMuser, bill.OperatorID);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 查询全部Exp_Preson_PaymentBill
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Exp_Preson_PaymentBillDC>> Exp_Preson_PaymentBill_SELECT_List(
          string iPeriod, string iOperatorName, int? iBillStatus,
          int iPageIndex, int iPageSize)
        {
            var rtn = expressDAL.Exp_Preson_PaymentBill_SELECT_List(iPeriod, iOperatorName, iBillStatus,
            iPageIndex, iPageSize);

            return new ReturnEntity<RecordCountEntity<Exp_Preson_PaymentBillDC>>(rtn);
        }

        /// <summary>
        /// 根据ID查询Exp_Preson_PaymentBill
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<Exp_Preson_PaymentBillDC> Exp_Preson_PaymentBill_SELECT_Entity(int iID)
        {
            var rtn = expressDAL.Exp_Preson_PaymentBill_SELECT_Entity(iID);

            return new ReturnEntity<Exp_Preson_PaymentBillDC>(rtn);
        }

        /// <summary>
        /// 账单结算
        /// </summary>
        /// <param name="iBillID"></param>
        /// <param name="iRealMoney"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Preson_PaymentBill_UPDATE_Close(int iBillID, decimal iRealMoney, int iMuser)
        {
            var bill = expressDAL.Exp_Preson_PaymentBill_SELECT_Entity(iBillID);
            if (bill == null)
            {
                return new ReturnEntity<bool>(-1, "账单不存在");
            }
            if (bill.BillStatus == 2)
            {
                return new ReturnEntity<bool>(-1, "账单已结算完成");
            }
            if (bill.RealPayment + iRealMoney > bill.Payment)
            {
                return new ReturnEntity<bool>(-1, "结算金额合计大于账单金额");
            }

            var rtn = expressDAL.Exp_Preson_PaymentBill_UPDATE_Close(iBillID, iRealMoney, (bill.RealPayment + iRealMoney == bill.Payment) ? 2 : 1, iMuser, bill.OperatorID);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 站点地图展示
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<string>> Base_Node_Map()
        {
            var nodeList = expressDAL.Exp_Node_SELECT_List(null, null, null, null, null, null, 1, 1000).ReturnList;

            if (nodeList == null)
            {
                return new ReturnEntity<IList<string>>(-1, "站点列表为空");
            }

            var rtn = new List<string>();

            foreach (var node in nodeList)
            {
                //有经纬度
                if (node.Latitude.HasValue && node.Longitude.HasValue)
                {
                    //[121.4536865322,31.3694940429,500,"莲花山路517弄</br>关键字"],
                    rtn.Add(string.Format("{0},{1},{2},{3},{4}"
                        , node.Longitude.Value.ToString("0.0000")
                        , node.Latitude.Value.ToString("0.0000")
                        , node.Radius
                        , node.Name
                        , node.Keyword));
                }
            }

            return new ReturnEntity<IList<string>>(rtn);
        }

        /// <summary>
        /// 新增 Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> Exp_Area_ADD(Exp_AreaDC iExp_AreaDC)
        {
            return new ReturnEntity<int>(expressDAL.Exp_Area_ADD(iExp_AreaDC));
        }

        /// <summary>
        /// 更新Exp_Area
        /// </summary>
        /// <param name="iExp_AreaDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Area_UPDATE(Exp_AreaDC iExp_AreaDC)
        {
            return new ReturnEntity<bool>(expressDAL.Exp_Area_UPDATE(iExp_AreaDC));
        }

        /// <summary>
        /// 查询全部Exp_Area
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Exp_AreaDC>> Exp_Area_SELECT_List()
        {
            return new ReturnEntity<IList<Exp_AreaDC>>(expressDAL.Exp_Area_SELECT_List());
        }

        /// <summary>
        /// 根据ID查询Exp_Area
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_AreaDC> Exp_Area_SELECT_Entity(int iID)
        {
            return new ReturnEntity<Exp_AreaDC>(expressDAL.Exp_Area_SELECT_Entity(iID));
        }

        /// <summary>
        /// 佣金导出
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<IList<string[]>> Exp_Preson_CommissionBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<IList<string[]>>(expressDAL.Exp_Preson_CommissionBill_Export(iPeriod, iPageIndex, iPageSize));
        }

        /// <summary>
        /// 应收货款导出
        /// </summary>
        /// <param name="iPeriod"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<IList<string[]>> Exp_Preson_PaymentBill_Export(DateTime iPeriod, int iPageIndex, int iPageSize)
        {
            return new ReturnEntity<IList<string[]>>(expressDAL.Exp_Preson_PaymentBill_Export(iPeriod, iPageIndex, iPageSize));
        }
    }
}
