using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.Express.Contract.DataContract;
using LazyAtHome.WCF.Express.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Business
{
    public class ApiExpress
    {
        private LazyAtHome.WCF.Express.Interface.IDAL.IExpressDAL expressDAL;

        public ApiExpress()
        {
            if (expressDAL == null)
                expressDAL = new LazyAtHome.WCF.Express.DAL.ExpressDAL();
        }

        static ApiExpress _ApiExpress;

        public static ApiExpress Instance
        {
            get
            {
                if (_ApiExpress == null)
                {
                    _ApiExpress = new ApiExpress();
                }
                return _ApiExpress;
            }
        }

        /// <summary>
        /// 下单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iGetDistrictID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iGetContacts"></param>
        /// <param name="iGetMpno"></param>
        /// <param name="iGetExpTime"></param>
        /// <param name="iSendDistrictID"></param>
        /// <param name="iSendAddress"></param>
        /// <param name="iSendContacts"></param>
        /// <param name="iSendMpno"></param>
        /// <param name="iSendExpTime"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iUserRemark"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Create(string iOutNumber,
            int iGetDistrictID, string iGetAddress, string iGetContacts, string iGetMpno, DateTime iGetExpTime,
            int iSendDistrictID, string iSendAddress, string iSendContacts, string iSendMpno, DateTime iSendExpTime,
            string iPackageInfo, int iPackageCount, decimal iChargeFee, string iUserRemark, string iInviteCode
         )
        {
            var order = expressDAL.Exp_Order_SELECT_Entity(iOutNumber, 1);
            if (order != null)
            {
                return new ReturnEntity<bool>(-1, "物流订单已生成");
            }

            var getOrder = new Exp_OrderDC()
            {
                OutNumber = iOutNumber,
                TransportType = 1,
                DistrictID = iGetDistrictID,
                Address = iGetAddress,
                Contacts = iGetContacts,
                Mpno = iGetMpno,
                ExpTime = iGetExpTime,
                PackageInfo = iPackageInfo,
                PackageCount = iPackageCount,
                Step = 0,
                UserRemark = iUserRemark,
                ChargeFee = 0,
                InviteCode = iInviteCode,
            };
            var sendOrder = new Exp_OrderDC()
            {
                OutNumber = iOutNumber,
                TransportType = 2,
                DistrictID = iSendDistrictID,
                Address = iSendAddress,
                Contacts = iSendContacts,
                Mpno = iSendMpno,
                ExpTime = iGetExpTime,
                PackageInfo = iPackageInfo,
                PackageCount = iPackageCount,
                Step = 0,
                UserRemark = iUserRemark,
                ChargeFee = iChargeFee,
            };

            int getID = expressDAL.Exp_Order_ADD(getOrder);
            int sendID = expressDAL.Exp_Order_ADD(sendOrder);

            #region 默认万海 2014-10-11

            //ManageExpress.Instance.Exp_Order_Allocation(getID, 12, 1, -1);

            //ManageExpress.Instance.Exp_Order_Allocation(sendID, null, 1, -1);

            #endregion


            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 物流更新地址 
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iTransportType"></param>
        /// <param name="iGetDistrictID"></param>
        /// <param name="iGetAddress"></param>
        /// <param name="iGetContacts"></param>
        /// <param name="iGetMpno"></param>
        /// <param name="iGetExpTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Address(string iOutNumber, int iTransportType,
              int iDistrictID, string iAddress, string iContacts, string iMpno)
        {
            var rtn = expressDAL.Exp_Order_UPDATE_Address(iOutNumber, iTransportType, iDistrictID, iAddress, iContacts, iMpno);

            return new ReturnEntity<bool>(rtn);
        }


        //static Random rd = new Random(new object().GetHashCode());

        /// <summary>
        /// 工厂创建物流送货单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public ReturnEntity<Exp_OrderDC> Exp_Order_Create_SendNumber(string iOutNumber)
        {
            var order = expressDAL.Exp_Order_SELECT_Entity(iOutNumber, 2);
            if (order == null)
            {
                return new ReturnEntity<Exp_OrderDC>(-1, "物流单不存在");
            }
            if (string.IsNullOrWhiteSpace(order.ExpNumber))
            {
                //生成物流单号
                order.ExpNumber = order.Obj_Cdate.Value.ToString("yyMMddHH") + (order.ID % 10000).ToString().PadLeft(4, '0');

                expressDAL.Exp_Order_UPDATE_ExpNumber(order.ID, order.ExpNumber);
            }

            //修改物流进程
            expressDAL.Exp_Order_SetStep(order.ID, 1, null, -1);
            //返回

            if (order.NodeID.HasValue)
            {
                var node = expressDAL.Exp_Node_SELECT_Entity(order.NodeID.Value);
                if (node != null && node.ParentID != 0)
                {
                    order.LineID = node.ParentID;
                    order.LineNo = node.ParentNo;
                    order.LineName = node.ParentName;
                }
            }
            return new ReturnEntity<Exp_OrderDC>(order);
        }


        /// <summary>
        /// 更新代收货款
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee)
        {
            var rtn = expressDAL.Exp_Order_UPDATE_Send_ChargeFee(iOutNumber, iChargeFee);

            return new ReturnEntity<bool>(rtn);
        }


        /// <summary>
        /// 更新代收货款
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee, string iPackageInfo, int iPackageCount)
        {
            var rtn = expressDAL.Exp_Order_UPDATE_Send_ChargeFee(iOutNumber, iChargeFee, iPackageInfo, iPackageCount);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 物流订单更新成完成
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Get_Finish(string iOutNumber, string iExpNumber)
        {
            var rtn = expressDAL.Exp_Order_UPDATE_Get_Finish(iOutNumber, iExpNumber);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 更改取件时间
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Get_ExpectTime_Change(string iOutNumber, DateTime iExpectTime)
        {
            var order = expressDAL.Exp_Order_SELECT_Entity(iOutNumber, 1);
            if (order == null)
            {
                return new ReturnEntity<bool>(-1, "订单不存在");
            }

            var rtn = expressDAL.Exp_Order_Get_ExpectTime_Change(order.ID, iExpectTime);

            return new ReturnEntity<bool>(rtn);
        }

        /// <summary>
        /// 取消物流单
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_Cancel(string iOutNumber)
        {
            expressDAL.Exp_Order_Cancel(iOutNumber);

            return Exp_Storage_OrderChargeBack(iOutNumber);
        }

        /// <summary>
        /// 物流站点切换
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iNodeType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_NodeChange(string iOutNumber, int iNodeType)
        {
            switch (iNodeType)
            {
                case 1://赛奥迪
                    expressDAL.Exp_Order_NodeChange(iOutNumber, 5, 22);
                    return new ReturnEntity<bool>(true);
                case 2://顺丰取赛奥迪送
                    expressDAL.Exp_Order_NodeChange(iOutNumber, 11, 33, 1);
                    expressDAL.Exp_Order_NodeChange(iOutNumber, 5, 22, 2);
                    return new ReturnEntity<bool>(true);
                default:
                    return new ReturnEntity<bool>(-1, "不支持的类型");
            }
        }

        /// <summary>
        /// 订单分拣拆包入库
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iStorageItemList"></param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_StorageItem_BreakPackage(string iOutNumber,
            IList<Exp_StorageItemDC> iStorageItemList, int iOperatorID)
        {
            if (iStorageItemList == null || iStorageItemList.Count == 0)
            {
                return new ReturnEntity<bool>(-1, "订单产品详情错误");
            }
            var oper = expressDAL.PR_Operator_SELECT_BYID(iOperatorID);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "操作员不存在");
            }
            //if (!oper.NodeID.HasValue)
            //{
            //    return new ReturnEntity<bool>(-1, "操作员无站点");
            //}
            //var node = expressDAL.Exp_Node_SELECT_Entity(oper.NodeID.Value);
            //if (node == null)
            //{
            //    return new ReturnEntity<bool>(-1, "操作员无站点");
            //}

            //var order = expressDAL.Exp_Order_SELECT_Entity(iOutNumber, 1);
            //if (order == null)
            //{
            //    return new ReturnEntity<bool>(-1, "订单不存在");
            //}

            var storageList = expressDAL.Exp_StorageItem_SELECT_List(null, iOutNumber, null, null, null, null, null, null, 1, 2);
            if (storageList == null || storageList.ReturnList == null || storageList.ReturnList.Count == 0)
            {
                return new ReturnEntity<bool>(-1, "库存不存在");
            }
            else if (storageList.ReturnList.Count > 1)
            {
                return new ReturnEntity<bool>(-1, "库存错误");
            }
            var storageItem = storageList.ReturnList.First();
            if (storageItem == null)
            {
                return new ReturnEntity<bool>(-1, "库存错误");
            }
            if (storageItem.ItemType != 1)
            {
                return new ReturnEntity<bool>(-1, "库存产品非包裹");
            }
            if (storageItem.Status != 1)
            {
                return new ReturnEntity<bool>(-1, "库存产品挂起中");
            }

            if (storageItem.StorageID != oper.StorageID)
            {
                //物品不在工厂,直接分拣到工厂里了
                Console.WriteLine("!!!!!!!!!!!!!!!!物品不在工厂,直接分拣到工厂里了!!!!!!!!!!!!!!!");
            }

            //订单出库
            var outRtn = expressDAL.Exp_StorageItem_IO(storageItem.ID, storageItem.StorageID,
                (int)SystemStorage.CheckComplete, (int)StorageTargetType.None, false, iOperatorID);
            if (outRtn <= 0)
            {
                return new ReturnEntity<bool>(-1, "订单包裹出库错误");
            }
            //产品入库
            foreach (var item in iStorageItemList)
            {
                var inRtn = expressDAL.Exp_StorageItem_In(item.Number, item.OtherNumber,
                    oper.StorageID, (int)StorageItemType.Clothing, item.ItemName, item.ItemDetail,
                    storageItem.ManagerID, (int)StorageTargetType.ToWash, iOperatorID, "分拣拆包");
                if (outRtn <= 0)
                {
                    return new ReturnEntity<bool>(-1, "订单产品入库错误");
                }
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 工厂打包出库
        /// </summary>
        /// <param name="iNumber"></param>
        /// <param name="iOtherNumber"></param>
        /// <param name="iOutType">1:一起出库 2:单件出库</param>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_StorageItem_FactoryPreOut(string iNumber,
            string iOtherNumber, int iOutType, int iOperatorID)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iOperatorID);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "操作员不存在");
            }
            //if (!oper.NodeID.HasValue)
            //{
            //    return new ReturnEntity<bool>(-1, "操作员无站点");
            //}
            //var node = expressDAL.Exp_Node_SELECT_Entity(oper.NodeID.Value);
            //if (node == null)
            //{
            //    return new ReturnEntity<bool>(-1, "操作员无站点");
            //}

            var storageList = expressDAL.Exp_StorageItem_SELECT_List(null, iNumber, iOtherNumber, null, null, null, null, null, 1, 2);
            if (storageList == null || storageList.ReturnList == null || storageList.ReturnList.Count == 0)
            {
                return new ReturnEntity<bool>(-1, "库存不存在");
            }
            //else if (storageList.ReturnList.Count > 1)
            //{
            //    return new ReturnEntity<bool>(-1, "库存错误");
            //}

            var storageItem = storageList.ReturnList.First();
            if (storageItem == null)
            {
                return new ReturnEntity<bool>(-1, "库存错误");
            }

            //判断类型
            if (storageItem.ItemType != 2)
            {
                return new ReturnEntity<bool>(-1, "库存产品非衣物");
            }
            if (storageItem.Status != 1)
            {
                return new ReturnEntity<bool>(-1, "库存产品挂起中");
            }

            if (storageItem.StorageType != (int)StorageType.Factory)
            {
                //return new ReturnEntity<bool>(-1, "库存产品不在工厂");
                Console.WriteLine("!!!!!!!!!!!!!!!!库存产品不在工厂,直接工厂打面单了!!!!!!!!!!!!!!!");
            }

            //直接出库
            if (iOutType == 2)
            {
                var ioRtn = expressDAL.Exp_StorageItem_IO(storageItem.ID, storageItem.StorageID, oper.StorageID,
                 (int)StorageTargetType.FactoryOut, false, iOperatorID);
                if (ioRtn <= 0)
                {
                    return new ReturnEntity<bool>(-1, "订单产品入库错误");
                }
                //完成提交
                return new ReturnEntity<bool>(true);
            }
            //全部齐再出库
            else if (iOutType == 1)
            {
                //先出库到预出库
                if (storageItem.TargetType != (int)StorageTargetType.FactoryPick)
                {
                    if (storageItem.TargetType == (int)StorageTargetType.FactoryPick && storageItem.StorageID == oper.StorageID)
                    {
                        //相同库,相同去向,不再处理
                    }
                    else
                    {
                        var ioRtn = expressDAL.Exp_StorageItem_IO(storageItem.ID, storageItem.StorageID, oper.StorageID,
                      (int)StorageTargetType.FactoryPick, false, iOperatorID);
                        if (ioRtn <= 0)
                        {
                            return new ReturnEntity<bool>(-1, "订单产品入库错误");
                        }
                    }

                }

                //重新查询订单内的所有物品是否都在
                storageList = expressDAL.Exp_StorageItem_SELECT_List(null, iNumber, null, (int)StorageItemType.Clothing,
                    null, null, null, null, 1, int.MaxValue);
                if (storageList == null || storageList.ReturnList == null || storageList.ReturnList.Count == 0)
                {
                    return new ReturnEntity<bool>(-1, "库存不存在");
                }

                //检查库存中所有物品
                foreach (var item in storageList.ReturnList)
                {
                    if (item.StorageID != oper.StorageID)
                    {
                        //有一件衣服不在这个仓库里
                        return new ReturnEntity<bool>(true);
                    }
                    if (item.TargetType != (int)StorageTargetType.FactoryPick && item.TargetType != (int)StorageTargetType.FactoryOut)
                    {
                        //有一件衣服不是等待打包或者等待出库
                        return new ReturnEntity<bool>(true);
                    }
                }

                //出库到出库仓
                foreach (var item in storageList.ReturnList)
                {
                    if (item.TargetType == (int)StorageTargetType.FactoryOut)
                    {
                        continue;
                    }
                    //出库到出库仓
                    var ioRtn = expressDAL.Exp_StorageItem_IO(item.ID, item.StorageID, item.StorageID,
                            (int)StorageTargetType.FactoryOut, false, iOperatorID);
                    if (ioRtn <= 0)
                    {
                        return new ReturnEntity<bool>(-1, "订单产品入库错误");
                    }
                }
                return new ReturnEntity<bool>(true);
            }
            else
            {
                return new ReturnEntity<bool>(-1, "未配置的出库类型");
            }
        }

        //工厂入库条码扫不出
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_NotFound(string iExpressNumber, int iMuser)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iMuser);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "用户错误");
            }

            //客服
            CommonExpress.NotifySend(null, RoleDC.Role_CustomerService, 0, 0, "工厂入库错误,物流号找不到订单",
             "物流条码:" + iExpressNumber + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
             (int)NotifyLevel.Warning4, false, false, 0);

            return new ReturnEntity<bool>(true);
        }

        //工厂入库产品不对
        public ReturnEntity<bool> Order_Order_InFactory_FailNotify_ItemError(string iOrderNumber, string iContent, int iMuser)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iMuser);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "用户错误");
            }

            //客服
            CommonExpress.NotifySend(iOrderNumber, RoleDC.Role_CustomerService, 0, 0, "工厂入库错误,订单内容错误",
             "错误信息:" + iContent + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
             (int)NotifyLevel.Warning4, false, false, 0);

            return new ReturnEntity<bool>(true);
        }

        //工厂出库条码扫不出
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_NotFound(string iCode, int iMuser)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iMuser);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "用户错误");
            }

            //客服
            CommonExpress.NotifySend(null, RoleDC.Role_CustomerService, 0, 0, "工厂出库错误,订单内容错误",
             "衣物条码:" + iCode + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
             (int)NotifyLevel.Warning4, false, false, 0);

            return new ReturnEntity<bool>(true);
        }

        //工厂出库产品不对
        public ReturnEntity<bool> Order_Order_OutFactory_FailNotify_ItemBad(string iOrderNumber, string iContent, int iMuser)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iMuser);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "用户错误");
            }

            //客服
            CommonExpress.NotifySend(iOrderNumber, RoleDC.Role_CustomerService, 0, 0, "工厂出库错误,订单内容错误",
             "错误信息:" + iContent + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
             (int)NotifyLevel.Warning4, false, false, 0);

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 订单刷新(修改产品内容)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_OrderReNew(string iOrderNumber, int iMuser)
        {
            //查找所有该订单号所在库
            var storageItemList = expressDAL.Exp_StorageItem_SELECT_List(null, iOrderNumber, null, null, null, null, null, null, 1, int.MaxValue);
            if (storageItemList == null || storageItemList.RecordCount == 0 || storageItemList.ReturnList == null)
            {
                return new ReturnEntity<bool>(-1, "库存不存在");
            }

            //循环
            foreach (var storageItem in storageItemList.ReturnList)
            {
                //如果是包裹
                if (storageItem.ItemType == (int)StorageItemType.Package)
                {
                    //如果在分拣完成库
                    if (storageItem.StorageID == (int)SystemStorage.CheckComplete)
                    {
                        //找到出入库日志
                        var storageItemLogList = expressDAL.Exp_StorageLog_SELECT_List(null, 1, iOrderNumber, null, (int)StorageItemType.Package, null, null, 1, 10);
                        if (storageItemLogList == null || storageItemLogList.ReturnList == null || storageItemLogList.ReturnList.Count == 0)
                        {
                            return new ReturnEntity<bool>(-1, "包裹出入库日志不存在,无法退回");
                        }
                        else
                        {
                            //出回工厂 
                            var storageItemLog = storageItemLogList.ReturnList.First();

                            var ioRtn = expressDAL.Exp_StorageItem_IO(storageItem.ID, storageItemLog.TargetStorageID,
                                storageItemLog.SourceStorageID, (int)StorageTargetType.ToCheck, false, iMuser);
                            if (ioRtn <= 0)
                            {
                                return new ReturnEntity<bool>(-1, "包裹退回工厂库错误");
                            }
                        }
                    }
                    //如果不再分拣完成库
                    else
                    {
                        //判断方向是否为待分拣4
                        if (storageItem.TargetType != (int)StorageTargetType.ToCheck
                            && storageItem.TargetType != (int)StorageTargetType.ToFactory
                            && storageItem.TargetType != (int)StorageTargetType.ToLine
                            )
                        {
                            //否:报错
                            return new ReturnEntity<bool>(-1, "订单所在库错误");
                        }
                    }
                }
                //如果是产品
                else
                {
                    //出到废品库
                    var ioRtn = expressDAL.Exp_StorageItem_IO(storageItem.ID, storageItem.StorageID, (int)SystemStorage.Damage,
                        (int)StorageTargetType.None, false, iMuser);
                    if (ioRtn <= 0)
                    {
                        return new ReturnEntity<bool>(-1, "产品报废错误");
                    }
                    //删除
                    expressDAL.Exp_StorageItem_DELETE(storageItem.ID, iMuser);
                }
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 订单退单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_OrderChargeBack(string iOrderNumber)
        {
            //查找所有该订单号所在库
            var storageItemList = expressDAL.Exp_StorageItem_SELECT_List(null, iOrderNumber, null, null, null, null, null, null, 1, int.MaxValue);
            if (storageItemList == null || storageItemList.RecordCount == 0 || storageItemList.ReturnList == null)
            {
                return new ReturnEntity<bool>(true);
            }
            //循环
            foreach (var storageItem in storageItemList.ReturnList)
            {
                //如果在分拣完成库
                if (storageItem.StorageID != (int)SystemStorage.Damage)
                {
                    var ioRtn = expressDAL.Exp_StorageItem_IO(storageItem.ID, storageItem.StorageID,
                              (int)SystemStorage.Damage, (int)StorageTargetType.None, false, -1);
                    if (ioRtn <= 0)
                    {
                        return new ReturnEntity<bool>(-1, "库存清理错误");
                    }
                }
            }
            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 工厂揽干线完成
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_Complete_Line(int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iMuser);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "用户错误");
            }
            if (oper.NodeType != (int)NodeType.Factory)
            {
                return new ReturnEntity<bool>(-1, "用户非工厂帐号");
            }

            //获取工厂
            var factory = expressDAL.Exp_Node_SELECT_Entity(oper.NodeID);
            if (factory == null)
            {
                return new ReturnEntity<bool>(-1, "工厂不存在");
            }

            //获取干线出库数量
            var storageList = expressDAL.Exp_StorageItem_SELECT_List(iStorageID,
                null, null, (int)StorageItemType.Package, null, null, (int)StorageTargetType.ToFactory, 1, 1, int.MaxValue);
            if (storageList == null)
            {
                return new ReturnEntity<bool>(-1, "获取数量错误");
            }

            //第一次循环,检查传入的订单号是否都在库存里
            foreach (var item in iOrderNumberList)
            {
                if (storageList.ReturnList.Count(p => p.Number == item) == 0)
                {
                    //没找到
                    return new ReturnEntity<bool>(-1, "未找到订单号:" + item);
                }
            }

            foreach (var item in storageList.ReturnList)
            {
                if (!iOrderNumberList.Contains(item.Number))
                {
                    //不在此次提交列表内
                    continue;
                }

                var ioRtn = expressDAL.Exp_StorageItem_IO(item.ID, item.StorageID, factory.StorageID,
                    (int)StorageTargetType.ToCheck, false, iMuser);
                if (ioRtn <= 0)
                {
                    return new ReturnEntity<bool>(-1, "包裹入工厂错误");
                }
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 工厂出库产品不对
        /// </summary>
        /// <param name="iOrderNumberList"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_FailNotify_ErrorNumber(int iStorageID, IList<string> iOrderNumberList, int iMuser)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iMuser);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "用户错误");
            }
            if (oper.NodeType != (int)NodeType.Factory)
            {
                return new ReturnEntity<bool>(-1, "用户非工厂帐号");
            }

            //获取工厂
            var factory = expressDAL.Exp_Node_SELECT_Entity(oper.NodeID);
            if (factory == null)
            {
                return new ReturnEntity<bool>(-1, "工厂不存在");
            }
            var manager = expressDAL.PR_Operator_SELECT_BYID(factory.ManagerID);

            foreach (var item in iOrderNumberList)
            {
                if (manager == null)
                {
                    //客服
                    CommonExpress.NotifySend(null, RoleDC.Role_CustomerService, 0, 0, "工厂入库错误,物流条码找不到订单",
                     "物流条码:" + item + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
                     (int)NotifyLevel.Notification3, false, false, 0);
                }
                else
                {
                    //客服
                    CommonExpress.NotifySend(null, manager.RoleID, manager.ID, 0, "工厂入库错误,物流条码找不到订单",
                     "物流条码:" + item + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
                     (int)NotifyLevel.Notification3, false, false, 0);
                }
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 工厂出库产品不对
        /// </summary>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Storage_FactoryTakeLine_FailNotify_ErrorCount(int iStorageID, int iMuser)
        {
            var oper = expressDAL.PR_Operator_SELECT_BYID(iMuser);
            if (oper == null)
            {
                return new ReturnEntity<bool>(-1, "用户错误");
            }
            if (oper.NodeType != (int)NodeType.Factory)
            {
                return new ReturnEntity<bool>(-1, "用户非工厂帐号");
            }

            //获取工厂
            var factory = expressDAL.Exp_Node_SELECT_Entity(oper.NodeID);
            if (factory == null)
            {
                return new ReturnEntity<bool>(-1, "工厂不存在");
            }

            //获取站点出库数量
            var storageList = expressDAL.Exp_StorageItem_SELECT_List(iStorageID,
                null, null, (int)StorageItemType.Package, null, null, (int)StorageTargetType.ToFactory, 1, 1, int.MaxValue);
            if (storageList == null)
            {
                return new ReturnEntity<bool>(-1, "获取数量错误");
            }
            if (storageList.RecordCount == 0)
            {
                return new ReturnEntity<bool>(true);
            }

            var manager = expressDAL.PR_Operator_SELECT_BYID(factory.ManagerID);
            if (manager == null)
            {
                //客服
                CommonExpress.NotifySend(null, RoleDC.Role_CustomerService, 0, 0, "工厂入库错误,有未入库订单",
                 "数量:" + storageList.RecordCount + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
                 (int)NotifyLevel.Notification3, false, false, 0);
            }
            else
            {
                //客服
                CommonExpress.NotifySend(null, manager.RoleID, manager.ID, 0, "工厂入库错误,有未入库订单",
                 "数量:" + storageList.RecordCount + " 工厂:" + oper.NodeName + " 操作员:" + oper.Name,
                 (int)NotifyLevel.Notification3, false, false, 0);
            }
            return new ReturnEntity<bool>(true);
        }
    }
}
