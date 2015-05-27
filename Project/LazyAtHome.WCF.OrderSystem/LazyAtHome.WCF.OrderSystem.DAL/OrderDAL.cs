﻿using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.DAL
{
    public class OrderDAL : DALBase, LazyAtHome.WCF.OrderSystem.Interface.IDAL.IOrderDAL
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public OrderDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        /// <summary>
        /// 生成订单号
        /// </summary>
        /// <returns></returns>
        public string Order_OrderNumber_Create()
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_GetNewOrderNumber");

            db.AddOutParameter(cmd, "@ON_OrderNumber", DbType.String, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return db.GetParameterValue(cmd, "@ON_OrderNumber").ToString();

        }

        #region 订单主表

        /// <summary>
        /// 新增订单主表
        /// </summary>
        /// <param name="iOrder_OrderDC"></param>
        /// <returns></returns>
        public int Order_Order_ADD(Order_OrderDC iOrder_OrderDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Order_Order(OrderNumber, OrderClass, OrderType, UserID, Title, SourceAmount, TotalAmount, PayAmount, PayType, UserRemark, ExpectTime, CSRemark, SystemRemark, RelationID, RelationNo, OrderStatus, Site, Channel, InviteCode, SendType, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OrderNumber, @OrderClass, @OrderType, @UserID, @Title, @SourceAmount, @TotalAmount, @PayAmount, @PayType, @UserRemark, @ExpectTime, @CSRemark, @SystemRemark, @RelationID, @RelationNo, @OrderStatus, @Site, @Channel, @InviteCode, @SendType, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //生成订单号
            iOrder_OrderDC.OrderNumber = Order_OrderNumber_Create();
            //订单编号
            db.AddInParameter(cmd, "@OrderNumber", DbType.String, iOrder_OrderDC.OrderNumber);
            //类型
            db.AddInParameter(cmd, "@OrderClass", DbType.Int32, iOrder_OrderDC.OrderClass);
            //订单类型
            db.AddInParameter(cmd, "@OrderType", DbType.Int32, iOrder_OrderDC.OrderType);
            //用户ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iOrder_OrderDC.UserID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iOrder_OrderDC.Title);

            db.AddInParameter(cmd, "@SourceAmount", DbType.Decimal, iOrder_OrderDC.SourceAmount);
            //总金额
            db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, iOrder_OrderDC.TotalAmount);
            //已支付金额
            db.AddInParameter(cmd, "@PayAmount", DbType.Decimal, iOrder_OrderDC.PayAmount);
            //支付类型 1:到付 2:在线支付
            db.AddInParameter(cmd, "@PayType", DbType.Int32, iOrder_OrderDC.PayType);
            //用户备注
            db.AddInParameter(cmd, "@UserRemark", DbType.String, iOrder_OrderDC.UserRemark);
            //
            db.AddInParameter(cmd, "@ExpectTime", DbType.DateTime, iOrder_OrderDC.ExpectTime);
            //客服备注
            db.AddInParameter(cmd, "@CSRemark", DbType.String, iOrder_OrderDC.CSRemark);
            //系统备注
            db.AddInParameter(cmd, "@SystemRemark", DbType.String, iOrder_OrderDC.SystemRemark);
            //关联的主单ID
            db.AddInParameter(cmd, "@RelationID", DbType.Int32, iOrder_OrderDC.RelationID);
            //关联的主单编号
            db.AddInParameter(cmd, "@RelationNo", DbType.String, iOrder_OrderDC.RelationNo);
            //订单状态
            db.AddInParameter(cmd, "@OrderStatus", DbType.Int32, iOrder_OrderDC.OrderStatus);
            //站点
            db.AddInParameter(cmd, "@Site", DbType.Int32, iOrder_OrderDC.Site);
            //渠道
            db.AddInParameter(cmd, "@Channel", DbType.Int32, iOrder_OrderDC.Channel);
            //
            db.AddInParameter(cmd, "@InviteCode", DbType.String, iOrder_OrderDC.InviteCode);

            db.AddInParameter(cmd, "@SendType", DbType.Int32, iOrder_OrderDC.SendType);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOrder_OrderDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            if (iOrder_OrderDC.ProductList != null)
            {
                //产品添加
                foreach (var item in iOrder_OrderDC.ProductList)
                {
                    item.OID = id;
                    Order_Product_ADD(item);
                }
            }

            //金额变化
            if (iOrder_OrderDC.AmountList != null)
            {
                foreach (var item in iOrder_OrderDC.AmountList)
                {
                    item.OID = id;
                    Order_Amount_ADD(item);
                }
            }

            //送货地址
            if (iOrder_OrderDC.GetAddress != null)
            {
                iOrder_OrderDC.GetAddress.OID = id;
                Order_ConsigneeAddress_ADD(iOrder_OrderDC.GetAddress);
            }
            if (iOrder_OrderDC.SendAddress != null)
            {
                iOrder_OrderDC.SendAddress.OID = id;
                Order_ConsigneeAddress_ADD(iOrder_OrderDC.SendAddress);
            }

            //退单无步骤
            if (iOrder_OrderDC.OrderType != 6)
            {
                //步骤更新
                Order_Step_ADD(id, WashStepType.CreateOrder, "下单");
            }

            return id;
        }

        /// <summary>
        /// 修改订单状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_OrderStatus(int iOrderID, OrderStatus iOrderStatus, DateTime? iExpectTime)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Order SET OrderStatus = @OrderStatus ");

            if (iOrderStatus == OrderStatus.Finish)
            {
                sql.Append(" ,CompleteTime = GETDATE() ");
                sql.Append(" ,ExpectTime = @ExpectTime ");
            }

            sql.Append(" WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //订单状态
            db.AddInParameter(cmd, "@OrderStatus", DbType.Int32, (int)iOrderStatus);

            db.AddInParameter(cmd, "@ExpectTime", DbType.DateTime, iExpectTime);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改订单完成时间
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iFinishTime"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_AllFinish(int iOrderID, DateTime iFinishTime, Guid iUserID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Order_Order SET AllFinishTime = @FinishTime WHERE ID = @ID;
                        UPDATE User_Info SET OrderCount = OrderCount + 1 WHERE ID = @UserID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //订单表ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //完成时间
            db.AddInParameter(cmd, "@FinishTime", DbType.DateTime, iFinishTime);

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 根据ID查询订单主表
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Order_OrderDC Order_Order_SELECT_Entity(int iID,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep, bool iGetFeedback = false)
        {
            Order_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Order_Order.*,User_Info.MPNo FROM Order_Order LEFT JOIN User_Info ON Order_Order.UserID = User_Info.ID

            WHERE Order_Order.ID = @ID AND Order_Order.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Order_OrderDC.GetEntity(reader);
                }
            }
            if (entity != null)
            {
                if (iGetProduct)
                    entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                if (iGetAmount)
                    entity.AmountList = Order_Amount_SELECT_List(entity.ID);
                if (iGetConsigneeAddress)
                {
                    entity.GetAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Get);
                    entity.SendAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Send);
                }
                if (iGetExpress)
                    entity.ExpressList = Order_Express_SELECT_List(entity.ID);
                if (iGetPayment)
                    entity.PaymentList = Order_Payment_SELECT_List(entity.ID);
                if (iGetStep)
                    entity.StepList = Order_Step_SELECT_List(entity.ID);
                if (iGetFeedback)
                    entity.Feedback = Order_Feedback_SELECT_Entity(entity.ID);

            }
            return entity;
        }

        /// <summary>
        /// 根据ID查询订单主表
        /// </summary>
        /// <param name="iOrderNumber">主键</param>
        /// <returns></returns>
        public Order_OrderDC Order_Order_SELECT_Entity(string iOrderNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            Order_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Order_Order WHERE OrderNumber = @OrderNumber AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@OrderNumber", DbType.String, iOrderNumber);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Order_OrderDC.GetEntity(reader);
                }
            }
            if (entity != null)
            {
                if (iGetProduct)
                    entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                if (iGetAmount)
                    entity.AmountList = Order_Amount_SELECT_List(entity.ID);
                if (iGetConsigneeAddress)
                {
                    entity.GetAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Get);
                    entity.SendAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Send);
                }
                if (iGetExpress)
                    entity.ExpressList = Order_Express_SELECT_List(entity.ID);
                if (iGetPayment)
                    entity.PaymentList = Order_Payment_SELECT_List(entity.ID);
                if (iGetStep)
                    entity.StepList = Order_Step_SELECT_List(entity.ID);
            }
            return entity;
        }

        /// <summary>
        /// 根据物流订单号查订单
        /// </summary>
        /// <param name="iExpressNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public Order_OrderDC Order_Order_SELECT_Entity_Express(string iExpressNumber,
            bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
            bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            Order_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM Order_Order WHERE Obj_Status = 1
                AND (GetExpressNumber = @ExpressNumber OR SendExpressNumber = @ExpressNumber) order by id desc
            ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ExpressNumber", DbType.String, iExpressNumber);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Order_OrderDC.GetEntity(reader);
                }
            }
            if (entity != null)
            {
                if (iGetProduct)
                    entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                if (iGetAmount)
                    entity.AmountList = Order_Amount_SELECT_List(entity.ID);
                if (iGetConsigneeAddress)
                {
                    entity.GetAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Get);
                    entity.SendAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Send);
                }
                if (iGetExpress)
                    entity.ExpressList = Order_Express_SELECT_List(entity.ID);
                if (iGetPayment)
                    entity.PaymentList = Order_Payment_SELECT_List(entity.ID);
                if (iGetStep)
                    entity.StepList = Order_Step_SELECT_List(entity.ID);
            }
            return entity;
        }

        /// <summary>
        /// 根据工厂条形码查订单
        /// </summary>
        /// <param name="iCodeNumber"></param>
        /// <param name="iGetProduct"></param>
        /// <param name="iGetAmount"></param>
        /// <param name="iGetConsigneeAddress"></param>
        /// <param name="iGetExpress"></param>
        /// <param name="iGetPayment"></param>
        /// <param name="iGetStep"></param>
        /// <returns></returns>
        public Order_OrderDC Order_Order_SELECT_Entity_FactoryCode(string iCodeNumber,
             bool iGetProduct, bool iGetAmount, bool iGetConsigneeAddress,
             bool iGetExpress, bool iGetPayment, bool iGetStep)
        {
            Order_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM Order_Order WHERE Obj_Status = 1 
                AND OrderClass IN (1,2) AND OrderType != 6 AND OrderStatus = 2
                AND DATEDIFF(DAY,Obj_Mdate,GETDATE()) <= 60
                AND EXISTS (SELECT 1 FROM Order_Product WHERE Code = @Code AND Order_Product.OID = Order_Order.ID AND Order_Product.Obj_Status = 1)
            ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@Code", DbType.String, iCodeNumber);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Order_OrderDC.GetEntity(reader);
                }
            }
            if (entity != null)
            {
                if (iGetProduct)
                    entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                if (iGetAmount)
                    entity.AmountList = Order_Amount_SELECT_List(entity.ID);
                if (iGetConsigneeAddress)
                {
                    entity.GetAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Get);
                    entity.SendAddress = Order_ConsigneeAddress_SELECT_Entity(entity.ID, ConsigneeAddressType.Send);
                }
                if (iGetExpress)
                    entity.ExpressList = Order_Express_SELECT_List(entity.ID);
                if (iGetPayment)
                    entity.PaymentList = Order_Payment_SELECT_List(entity.ID);
                if (iGetStep)
                    entity.StepList = Order_Step_SELECT_List(entity.ID);
            }
            return entity;
        }

        #endregion

        #region 产品

        /// <summary>
        /// 新增产品
        /// </summary>
        /// <param name="iOrder_ProductDC"></param>
        /// <returns></returns>
        public int Order_Product_ADD(Order_ProductDC iOrder_ProductDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Order_Product(OID, ProductID, Name, Type, Price, Attribute, Code, UserMPNo, UserName, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OID, @ProductID, @Name, @Type, @Price, @Attribute, @Code, @UserMPNo, @UserName, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrder_ProductDC.OID);
            //产品ID
            db.AddInParameter(cmd, "@ProductID", DbType.Int32, iOrder_ProductDC.ProductID);
            //产品名称
            db.AddInParameter(cmd, "@Name", DbType.String, iOrder_ProductDC.Name);
            //产品类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iOrder_ProductDC.Type);
            //单价
            db.AddInParameter(cmd, "@Price", DbType.Decimal, iOrder_ProductDC.Price);
            //属性
            db.AddInParameter(cmd, "@Attribute", DbType.String, iOrder_ProductDC.Attribute);
            //订单产品识别编号
            db.AddInParameter(cmd, "@Code", DbType.String, iOrder_ProductDC.Code);

            db.AddInParameter(cmd, "@UserMPNo", DbType.String, iOrder_ProductDC.UserMPNo);

            db.AddInParameter(cmd, "@UserName", DbType.String, iOrder_ProductDC.UserName);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOrder_ProductDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 删除产品详情
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public bool Order_Product_DEL(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Product SET Obj_Status = 6 WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除产品详情
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <returns></returns>
        public bool Order_Product_DEL_OrderID(int iOrderID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Product SET Obj_Status = 6 WHERE OID = @OID AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部产品
        /// </summary>
        /// <returns></returns>
        public IList<Order_ProductDC> Order_Product_SELECT_List(int iOrderID, int iOrderClass)
        {
            List<Order_ProductDC> list = new List<Order_ProductDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            switch (iOrderClass)
            {
                case (int)OrderClass.Mall:
                    sql.Append(@"SELECT Order_Product.*,Mall_Product.PictureM,Mall_Product.PictureS
                        FROM Order_Product,Mall_Product
                            WHERE Order_Product.ProductID = Mall_Product.ID
                            --AND Wash_Product.CategoryID = Wash_Category.ID
                            AND Order_Product.OID = @OID 
                            AND Order_Product.Obj_Status = 1
                ");
                    break;
                default:
                    sql.Append(@"SELECT Order_Product.*,Wash_Product.WebName,Wash_Category.ID AS CategoryID
                            ,Wash_Category.Name AS CategoryName,
                            Wash_Category.PictureM,Wash_Category.PictureS
                        FROM Order_Product,Wash_Product,Wash_Category
                            WHERE Order_Product.ProductID = Wash_Product.ID
                            AND Wash_Product.CategoryID = Wash_Category.ID
                            AND Order_Product.OID = @OID 
                            AND Order_Product.Obj_Status = 1
                ");
                    break;
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    if (iOrderClass == (int)OrderClass.Mall)
                    {
                        list.Add(Order_ProductDC.GetEntity_Mall(reader));
                    }
                    else
                    {
                        list.Add(Order_ProductDC.GetEntity(reader));

                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 修改订单返洗状态
        /// </summary>
        /// <param name="iPID"></param>
        /// <returns></returns>
        public bool Order_Product_UPDATE_ReturnStatus(int iPID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Product SET ReturnStatus = 1 WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iPID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        #endregion

        #region 金额变更表

        /// <summary>
        /// 新增金额变更表
        /// </summary>
        /// <param name="iOrder_AmountDC"></param>
        /// <returns></returns>
        public int Order_Amount_ADD(Order_AmountDC iOrder_AmountDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Order_Amount(OID, Money, Type, Content, RelationID, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OID, @Money, @Type, @Content, @RelationID, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrder_AmountDC.OID);
            //金额
            db.AddInParameter(cmd, "@Money", DbType.Decimal, iOrder_AmountDC.Money);
            //类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iOrder_AmountDC.Type);
            //说明
            db.AddInParameter(cmd, "@Content", DbType.String, iOrder_AmountDC.Content);
            //关联ID
            db.AddInParameter(cmd, "@RelationID", DbType.String, iOrder_AmountDC.RelationID);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOrder_AmountDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 删除金额详情
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <returns></returns>
        public bool Order_Amount_DEL(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Amount SET Obj_Status = 6 WHERE ID = @ID AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除金额详情
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <returns></returns>
        public bool Order_Amount_DEL_OrderID(int iOrderID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Amount SET Obj_Status = 6 WHERE OID = @OID AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        /// <summary>
        /// 查询全部金额变更表
        /// </summary>
        /// <returns></returns>
        public IList<Order_AmountDC> Order_Amount_SELECT_List(int iOrderID)
        {
            List<Order_AmountDC> list = new List<Order_AmountDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Order_Amount WHERE OID = @OID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Order_AmountDC.GetEntity(reader));
                }
            }
            return list;
        }

        #endregion

        #region 送货地址表

        /// <summary>
        /// 新增送货地址表
        /// </summary>
        /// <param name="iOrder_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public int Order_ConsigneeAddress_ADD(Order_ConsigneeAddressDC iOrder_ConsigneeAddressDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Order_ConsigneeAddress(OID, Type, Consignee, DistrictID, Address, Mpno, Phone, ZipCode, Email, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OID, @Type, @Consignee, @DistrictID, @Address, @Mpno, @Phone, @ZipCode, @Email, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrder_ConsigneeAddressDC.OID);
            //地址类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iOrder_ConsigneeAddressDC.Type);
            //收货人
            db.AddInParameter(cmd, "@Consignee", DbType.String, iOrder_ConsigneeAddressDC.Consignee);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iOrder_ConsigneeAddressDC.DistrictID);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iOrder_ConsigneeAddressDC.Address);
            //手机号
            db.AddInParameter(cmd, "@Mpno", DbType.String, iOrder_ConsigneeAddressDC.Mpno);
            //电话号码
            db.AddInParameter(cmd, "@Phone", DbType.String, iOrder_ConsigneeAddressDC.Phone);
            //邮编
            db.AddInParameter(cmd, "@ZipCode", DbType.String, iOrder_ConsigneeAddressDC.ZipCode);
            //邮箱
            db.AddInParameter(cmd, "@Email", DbType.String, iOrder_ConsigneeAddressDC.Email);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOrder_ConsigneeAddressDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 修改送货地址表
        /// </summary>
        /// <param name="iOrder_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public bool Order_ConsigneeAddress_UPDATE(Order_ConsigneeAddressDC iOrder_ConsigneeAddressDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_ConsigneeAddress SET Consignee = @Consignee, DistrictID = @DistrictID, 
                Address = @Address, Mpno = @Mpno, Phone = @Phone, ZipCode = @ZipCode, Email = @Email WHERE ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrder_ConsigneeAddressDC.ID);
            //收货人
            db.AddInParameter(cmd, "@Consignee", DbType.String, iOrder_ConsigneeAddressDC.Consignee);
            //省市区
            db.AddInParameter(cmd, "@DistrictID", DbType.Int32, iOrder_ConsigneeAddressDC.DistrictID);
            //地址
            db.AddInParameter(cmd, "@Address", DbType.String, iOrder_ConsigneeAddressDC.Address);
            //手机号
            db.AddInParameter(cmd, "@Mpno", DbType.String, iOrder_ConsigneeAddressDC.Mpno);
            //电话号码
            db.AddInParameter(cmd, "@Phone", DbType.String, iOrder_ConsigneeAddressDC.Phone);
            //邮编
            db.AddInParameter(cmd, "@ZipCode", DbType.String, iOrder_ConsigneeAddressDC.ZipCode);
            //邮箱
            db.AddInParameter(cmd, "@Email", DbType.String, iOrder_ConsigneeAddressDC.Email);

            return db.ExecuteNonQuery(cmd) == 1 ? true : false;
        }

        /// <summary>
        /// 查询全部送货地址表
        /// </summary>
        /// <returns></returns>
        public Order_ConsigneeAddressDC Order_ConsigneeAddress_SELECT_Entity(int iOrderID, ConsigneeAddressType iType)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Order_ConsigneeAddress.*,
                C.FullName AS DistrictName,
                B.Name AS CityName,
                A.Name AS ProvinceName
                FROM Order_ConsigneeAddress
            LEFT JOIN Base_District C ON Order_ConsigneeAddress.DistrictID = C.ID AND C.Level = 3 AND C.Obj_Status = 1
            LEFT JOIN Base_District B ON C.Code1 = B.Code1 AND C.Code2 = B.Code2 AND B.Code3 = '00' AND B.Level = 2 AND B.Obj_Status = 1
            LEFT JOIN Base_District A ON B.Code1 = A.Code1 AND A.Code2 = '00' AND A.Code3 = '00'  AND A.Level = 1 AND A.Obj_Status = 1
            WHERE Order_ConsigneeAddress.Type = @Type AND Order_ConsigneeAddress.OID = @OID AND Order_ConsigneeAddress.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iType);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return Order_ConsigneeAddressDC.GetEntity(reader);
                }
            }
            return null;
        }

        #endregion

        #region 物流

        /// <summary>
        /// 新增物流
        /// </summary>
        /// <param name="iOrder_ExpressDC"></param>
        /// <returns></returns>
        public int Order_Express_ADD(Order_ExpressDC iOrder_ExpressDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Order_Express(OID, Type, Code, Content, RelationID, AcceptTime, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OID, @Type, @Code, @Content, @RelationID, @AcceptTime, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrder_ExpressDC.OID);
            //物流类型
            db.AddInParameter(cmd, "@Type", DbType.Int32, iOrder_ExpressDC.Type);
            //物流编号
            db.AddInParameter(cmd, "@Code", DbType.String, iOrder_ExpressDC.Code);
            //物流数据
            db.AddInParameter(cmd, "@Content", DbType.String, iOrder_ExpressDC.Content);
            //物流第三方ID
            db.AddInParameter(cmd, "@RelationID", DbType.String, iOrder_ExpressDC.RelationID);
            //物流发生时间
            db.AddInParameter(cmd, "@AcceptTime", DbType.DateTime, iOrder_ExpressDC.AcceptTime);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOrder_ExpressDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            //if (iOrder_ExpressDC.Type == (int)ExpressType.Get)
            //{
            //    Order_Order_UPDATE_ExpressNumber(iOrder_ExpressDC.OID, iOrder_ExpressDC.Code, ExpressType.Get);

            //    //2	取件中	完成支付 添加物流信息
            //    Order_Step_ADD(iOrder_ExpressDC.OID, WashStepType.GetPackage, "取件中");
            //}
            //else if (iOrder_ExpressDC.Type == (int)ExpressType.Send)
            //{
            //    Order_Order_UPDATE_ExpressNumber(iOrder_ExpressDC.OID, iOrder_ExpressDC.Code, ExpressType.Send);

            //    //5	出库中	工厂打包
            //    Order_Step_ADD(iOrder_ExpressDC.OID, WashStepType.Delivery, "出库中");
            //}

            return id;
        }

        public void Order_Order_FinishExpress_Get(int iOrderID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Order SET ExpressStatus = 1 WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.ExecuteNonQuery(cmd);
        }


        public void Order_Order_UPDATE_ExpressNumber(int iOrderID, string iExpressNumber, ExpressType iExpressType)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            switch (iExpressType)
            {
                case ExpressType.Get:
                    sql.Append("UPDATE Order_Order SET GetExpressNumber = @ExpressNumber WHERE ID = @ID");
                    break;
                case ExpressType.Send:
                    sql.Append("UPDATE Order_Order SET SendExpressNumber = @ExpressNumber WHERE ID = @ID");
                    break;
                default:
                    return;
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@ExpressNumber", DbType.String, iExpressNumber);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 删除物流
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public bool Order_Express_DELETE(int iID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Order_Express SET Obj_Status = 6, Obj_Mdate = getdate() WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 删除物流
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iType"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Order_Express_DELETE(int iOrderID, int iType, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Express SET Obj_Status = 6, Obj_Mdate = getdate(), Obj_Muser = @Obj_Muser
                WHERE OID = @OID AND Obj_Status = 1 AND Type = @Type");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            db.AddInParameter(cmd, "@Type", DbType.Int32, iType);
            db.AddInParameter(cmd, "@Obj_Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部物流
        /// </summary>
        /// <returns></returns>
        public IList<Order_ExpressDC> Order_Express_SELECT_List(int iOrderID)
        {
            List<Order_ExpressDC> list = new List<Order_ExpressDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Order_Express WHERE OID = @OID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Order_ExpressDC.GetEntity(reader));
                }
            }
            return list;
        }

        #endregion

        #region 支付信息

        /// <summary>
        /// 新增支付信息
        /// </summary>
        /// <param name="iOrder_PaymentDC"></param>
        /// <returns></returns>
        public int Order_Payment_ADD(Order_PaymentDC iOrder_PaymentDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Order_Payment(OID, PayMoney, PayMoneyType, PayChannel, PayDate, RelationID, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OID, @PayMoney, @PayMoneyType, @PayChannel, @PayDate, @RelationID, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrder_PaymentDC.OID);
            //支付金额
            db.AddInParameter(cmd, "@PayMoney", DbType.Decimal, iOrder_PaymentDC.PayMoney);
            //支付类型
            db.AddInParameter(cmd, "@PayMoneyType", DbType.Int32, iOrder_PaymentDC.PayMoneyType);
            //支付渠道
            db.AddInParameter(cmd, "@PayChannel", DbType.Int32, iOrder_PaymentDC.PayChannel);
            //支付时间
            db.AddInParameter(cmd, "@PayDate", DbType.DateTime, iOrder_PaymentDC.PayDate);
            //关联ID
            db.AddInParameter(cmd, "@RelationID", DbType.String, iOrder_PaymentDC.RelationID);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOrder_PaymentDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));

            //修改订单支付金额
            Order_Order_UPDATE_PayAmount(iOrder_PaymentDC.OID, iOrder_PaymentDC.PayMoney);

            return id;
        }

        /// <summary>
        /// 删除金额详情
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <returns></returns>
        public bool Order_Payment_DEL(int iPaymentID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Payment SET Obj_Status = 6 WHERE ID = @ID AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iPaymentID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改订单支付金额
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iPayAmount"></param>
        /// <returns></returns>
        private bool Order_Order_UPDATE_PayAmount(int iOrderID, decimal iPayAmount)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Order_Order
                SET  PayAmount = PayAmount + 
                            (CASE WHEN PayAmount + @PayAmount <= TotalAmount 
                            THEN @PayAmount ELSE TotalAmount - PayAmount END) 
                WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //金额
            db.AddInParameter(cmd, "@PayAmount", DbType.Decimal, iPayAmount);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新订单支付状态
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iPayStatus"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_PayStatus(int iOrderID, PayStatus iPayStatus)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Order_Order SET PayStatus = @PayStatus ");
            if(iPayStatus==PayStatus.Paid)
            {
                sql.Append(", PayAmount= TotalAmount ");
            }
            sql.Append(" WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //金额
            db.AddInParameter(cmd, "@PayStatus", DbType.Int32, (int)iPayStatus);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 查询全部支付信息
        /// </summary>
        /// <returns></returns>
        public IList<Order_PaymentDC> Order_Payment_SELECT_List(int iOrderID)
        {
            List<Order_PaymentDC> list = new List<Order_PaymentDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Order_Payment WHERE OID = @OID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Order_PaymentDC.GetEntity(reader));
                }
            }
            return list;
        }

        #endregion

        #region 步骤

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iOrder_StepDC"></param>
        /// <returns></returns>
        public void Order_Step_ADD(int iOrderID, WashStepType iStep, string iContent)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"INSERT INTO Order_Step(OID, Type, Content, Obj_Cuser, Obj_Muser) VALUES (@OID, @Type, @Content, -1, -1);");

            switch (iStep)
            {
                case WashStepType.CreateOrder:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type, Step1Time = GETDATE() WHERE ID = @OID");
                    break;
                case WashStepType.GetPackage:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type, Step2Time = GETDATE() WHERE ID = @OID");
                    break;
                case WashStepType.SendFactory:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type, Step3Time = GETDATE() WHERE ID = @OID");
                    break;
                case WashStepType.Wash:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type, Step4Time = GETDATE() WHERE ID = @OID");
                    break;
                case WashStepType.Delivery:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type, Step5Time = GETDATE() WHERE ID = @OID");
                    break;
                case WashStepType.SendPackage:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type, Step6Time = GETDATE() WHERE ID = @OID");
                    break;
                case WashStepType.Finish:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type, Step7Time = GETDATE() WHERE ID = @OID");
                    break;
                default:
                    sql.Append(@"UPDATE Order_Order SET Step = @Type WHERE ID = @OID");
                    break;
            }

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            //步骤代码
            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iStep);
            //步骤说明
            db.AddInParameter(cmd, "@Content", DbType.String, iContent);
            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        public IList<Order_StepDC> Order_Step_SELECT_List(int iOrderID)
        {
            List<Order_StepDC> list = new List<Order_StepDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Order_Step WHERE OID = @OID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Order_StepDC.GetEntity(reader));
                }
            }
            return list;
        }
        #endregion

        #region 评论

        /// <summary>
        /// 新增 Order_Feedback
        /// </summary>
        /// <param name="iOrder_FeedbackDC"></param>
        /// <returns></returns>
        public int Order_Feedback_ADD(Order_FeedbackDC iOrder_FeedbackDC)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Order_Feedback(OID, Score1, Score2, Score3, Score4, Content1, ");
            //字段拼接
            AddCommonInsertSql(sql);
            sql.Append(") VALUES (@OID, @Score1, @Score2, @Score3, @Score4, @Content1, ");
            //值拼接
            AddCommonInsertValues(sql);
            sql.Append(");SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //订单表ID
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrder_FeedbackDC.OID);
            //快递响应
            db.AddInParameter(cmd, "@Score1", DbType.Int32, iOrder_FeedbackDC.Score1);
            //客服态度
            db.AddInParameter(cmd, "@Score2", DbType.Int32, iOrder_FeedbackDC.Score2);
            //快递态度
            db.AddInParameter(cmd, "@Score3", DbType.Int32, iOrder_FeedbackDC.Score3);
            //洗涤效果
            db.AddInParameter(cmd, "@Score4", DbType.Int32, iOrder_FeedbackDC.Score4);
            //评论内容
            db.AddInParameter(cmd, "@Content1", DbType.String, iOrder_FeedbackDC.Content1);
            //添加共通参数
            AddCommonInsertParameter(db, cmd, iOrder_FeedbackDC);
            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 查询评论
        /// </summary>
        /// <returns></returns>
        public Order_FeedbackDC Order_Feedback_SELECT_Entity(int iOrderID)
        {
            Order_FeedbackDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Order_Feedback WHERE OID = @OID AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Order_FeedbackDC.GetEntity(reader);
                }
            }
            return entity;
        }

        #endregion

        static Regex R_Number = new Regex(@"^\d*$");
        static Regex R_Number6 = new Regex(@"^\d{6}$");
        static Regex R_Number8 = new Regex(@"^\d{8}$");
        static Regex R_Number9 = new Regex(@"^\d{9}$");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iOrderClass"></param>
        /// <param name="iOrderType"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iSite"></param>
        /// <param name="iChannel"></param>
        /// <param name="iTotalAmountMax"></param>
        /// <param name="iTotalAmountMin"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_OrderDC> Order_Order_SELECT_List(
            string iOrderNumber,
            Guid? iUserID, string iMPNo, string iLoginName,
            OrderClass? iOrderClass,
            OrderType? iOrderType, OrderStatus? iOrderStatus,
            int? iSite, int? iChannel,
            decimal? iTotalAmountMax, decimal? iTotalAmountMin,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize,
            string iConsignee,
            int iSortType,
            DateTime? iExpStartDate, DateTime? iExpEndDate,
            int? iStep, int? iGetExpressType)
        {
            List<Order_OrderDC> list = new List<Order_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.*,Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Consignee,Order_ConsigneeAddress.DistrictID,Order_ConsigneeAddress.Address");
            sqlwhere.Append(@" Order_Order.Obj_Status = 1 ");

            if (iSortType == 2)
            {
                sqlorder.Append(@" ISNULL(Order_Order.ExpectTime,Order_Order.CompleteTime) DESC ");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(iConsignee))
                {
                    sqlorder.Append(@" Order_Order.Obj_Cdate DESC ");
                }
            }

            //订单编号
            if (!string.IsNullOrWhiteSpace(iOrderNumber))
                sqlwhere.Append(" AND Order_Order.OrderNumber = '" + iOrderNumber + "' ");
            //用户ID
            if (iUserID.HasValue)
                sqlwhere.Append(" AND Order_Order.UserID = '" + iUserID + "' ");
            //用户手机
            if (!string.IsNullOrWhiteSpace(iMPNo))
                sqlwhere.Append(" AND Order_ConsigneeAddress.MPNo = '" + iMPNo + "' ");
            //用户登录名
            if (!string.IsNullOrWhiteSpace(iLoginName))
                sqlwhere.Append(" AND (User_Info.LoginName = '" + iLoginName + "' OR User_Info.MPNo = '" + iLoginName + "'OR User_Info.Email = '" + iLoginName + "') ");
            //分类
            if (iOrderClass.HasValue)
                sqlwhere.Append(" AND Order_Order.OrderClass = '" + (int)iOrderClass + "' ");
            //订单类型
            if (iOrderType.HasValue)
            {
                switch (iOrderType.Value)
                {
                    case OrderType.NoBack:
                        sqlwhere.Append(" AND Order_Order.OrderType != 6");
                        break;
                    default:
                        sqlwhere.Append(" AND Order_Order.OrderType = '" + (int)iOrderType + "' ");
                        break;
                }
            }
            //订单状态
            if (iOrderStatus.HasValue)
            {
                switch (iOrderStatus.Value)
                {
                    case OrderStatus.Web:
                        sqlwhere.Append(" AND Order_Order.OrderStatus IN (0,1,2) ");
                        break;
                    case OrderStatus.AllFinish:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = 2 AND Step = 7");
                        break;
                    case OrderStatus.UnFinish:
                        sqlwhere.Append(" AND Order_Order.OrderStatus IN (0,1,2) AND Step != 7");
                        break;
                    case OrderStatus.WaitExpress:
                        sqlwhere.Append(@" AND Order_Order.OrderStatus IN (0,2) AND Step != 7 
                                    AND (OrderClass != 2 OR (OrderClass = 2 AND TotalAmount > 0))");
                        //                        sqlwhere.Append(@"AND Order_Order.OrderStatus IN (0,2) AND Step != 7 
                        //                                       ");
                        break;

                    case OrderStatus.UnPay:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = 1 AND PayStatus = 0 AND PayAmount < TotalAmount");
                        break;
                    case OrderStatus.CSProcess:   //客服需处理
                        //一键未审核,普通订单未生成快递单号
                        sqlwhere.Append(@" AND Order_Order.OrderType IN (1,2,3)
                                AND(
                                (Order_Order.OrderClass = 2 AND Order_Order.OrderStatus = 0)
                                OR (Order_Order.OrderClass IN(1,2,4) AND Order_Order.OrderStatus = 2 AND Order_Order.ExpressStatus = 0)
                                )");
                        break;
                    case OrderStatus.CSDef:
                        sqlwhere.Append(" AND Order_Order.OrderType IN (1,2,3) AND Order_Order.OrderStatus NOT IN (6,16,32)");
                        break;
                    default:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = '" + (int)iOrderStatus + "' ");
                        break;
                }
            }

            //站点
            if (iSite.HasValue)
                sqlwhere.Append(" AND Order_Order.Site = '" + iSite + "' ");
            //下单渠道
            if (iChannel.HasValue)
                sqlwhere.Append(" AND Order_Order.Channel = '" + iChannel + "' ");

            if (iGetExpressType.HasValue)
                sqlwhere.Append(" AND Order_Order.GetExpressType = '" + iGetExpressType + "' ");

            //总金额
            if (iTotalAmountMax.HasValue)
                sqlwhere.Append(" AND Order_Order.TotalAmount <= '" + iTotalAmountMax + "' ");
            //总金额
            if (iTotalAmountMin.HasValue)
                sqlwhere.Append(" AND Order_Order.TotalAmount >= '" + iTotalAmountMin + "' ");

            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Order_Order.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Order_Order.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            //取件人
            if (!string.IsNullOrWhiteSpace(iConsignee))
            {
                if (R_Number6.IsMatch(iConsignee))
                {
                    sqlwhere.Append(@" AND EXISTS(
SELECT 1 FROM Order_Product WHERE OID = Order_Order.ID AND Code LIKE '" + iConsignee.Substring(0, 3) + "%" + iConsignee.Substring(3, 3) + "%')");
                }
                else if (R_Number8.IsMatch(iConsignee))
                {
                    sqlwhere.Append(@" AND EXISTS(
SELECT 1 FROM Order_Product WHERE OID = Order_Order.ID AND OtherCode = '" + iConsignee + "')");
                }
                else if (R_Number9.IsMatch(iConsignee))
                {
                    sqlwhere.Append(@" AND EXISTS(
SELECT 1 FROM Order_Product WHERE OID = Order_Order.ID AND Code = '" + iConsignee + "')");
                }
                else if (iConsignee.Contains("node_"))
                {
                    var tmp = iConsignee.Split('_');
                    if (tmp.Length == 2)
                    {
                        sqlwhere.Append(@" AND EXISTS(
SELECT 1 FROM Exp_Order,Exp_Node WHERE Exp_Order.OutNumber = Order_Order.OrderNumber AND Exp_Order.TransportType = 1
AND Exp_Order.NodeID = Exp_Node.ID AND Exp_Order.Obj_Status = 1 AND Exp_Node.Name = '" + tmp[1] + "')");
                    }
                }
                else if (iConsignee.Contains("oper_"))
                {
                    var tmp = iConsignee.Split('_');
                    if (tmp.Length == 2)
                    {
                        sqlwhere.Append(@" AND EXISTS(
SELECT 1 FROM Exp_Order,PR_Operator WHERE Exp_Order.OutNumber = Order_Order.OrderNumber AND Exp_Order.TransportType = 1
AND Exp_Order.OperatorID = PR_Operator.ID AND Exp_Order.Obj_Status = 1 AND PR_Operator.Name = '" + tmp[1] + "')");
                    }
                }
                else
                {
                    sqlwhere.Append(@" AND (Order_ConsigneeAddress.Consignee LIKE '%" + iConsignee + @"%'
                                OR Order_ConsigneeAddress.Address LIKE '%" + iConsignee + @"%')");
                }
            }

            //预计时间开始
            if (iExpStartDate.HasValue)
                sqlwhere.Append(" AND ISNULL(Order_Order.ExpectTime,Order_Order.CompleteTime) >= '" + iExpStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //预计时间结束
            if (iExpEndDate.HasValue)
                sqlwhere.Append(" AND ISNULL(Order_Order.ExpectTime,Order_Order.CompleteTime) <= '" + iExpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            if (iStep.HasValue)
                sqlwhere.Append(" AND Order_Order.Step = '" + iStep + "' ");

            var rtn = new RecordCountEntity<Order_OrderDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex(
                @"Order_Order LEFT JOIN Order_ConsigneeAddress ON Order_ConsigneeAddress.OID = Order_Order.ID AND Order_ConsigneeAddress.Type = 1 AND Order_ConsigneeAddress.Obj_Status = 1"
                , "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex(
                @"Order_Order LEFT JOIN Order_ConsigneeAddress ON Order_ConsigneeAddress.OID = Order_Order.ID AND Order_ConsigneeAddress.Type = 1 AND Order_ConsigneeAddress.Obj_Status = 1"
                , "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_OrderDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_OrderDC> Order_Order_SELECT_List(
            Guid iUserID, OrderStatus? iOrderStatus,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Order_OrderDC> list = new List<Order_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.*,Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Consignee,Order_ConsigneeAddress.DistrictID,Order_ConsigneeAddress.Address");
            sqlwhere.Append(@" Order_Order.Obj_Status = 1 ");
            sqlorder.Append(@" Order_Order.Obj_Cdate DESC ");

            //用户ID
            sqlwhere.Append(" AND Order_Order.UserID = '" + iUserID + "' ");

            //订单状态
            if (iOrderStatus.HasValue)
            {
                switch (iOrderStatus.Value)
                {
                    case OrderStatus.Web:
                        sqlwhere.Append(" AND Order_Order.OrderStatus IN (0,1,2) ");
                        break;
                    case OrderStatus.AllFinish:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = 2 AND Step = 7");
                        break;
                    case OrderStatus.UnFinish:
                        sqlwhere.Append(" AND Order_Order.OrderStatus IN (0,1,2) AND Step != 7");
                        break;
                    case OrderStatus.WaitExpress:
                        sqlwhere.Append(@" AND Order_Order.OrderStatus IN (0,2) AND Step != 7 
                                    AND (OrderClass != 2 OR (OrderClass = 2 AND TotalAmount > 0))");
                        break;
                    case OrderStatus.UnPay:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = 1 AND PayStatus = 0 AND PayAmount < TotalAmount");
                        break;
                    case OrderStatus.CSProcess:   //客服需处理
                        //一键未审核,普通订单未生成快递单号
                        sqlwhere.Append(@" AND Order_Order.OrderType IN (1,2,3)
                                AND(
                                (Order_Order.OrderClass = 2 AND Order_Order.OrderStatus = 0)
                                OR (Order_Order.OrderClass IN(1,2,4) AND Order_Order.OrderStatus = 2 AND Order_Order.ExpressStatus = 0)
                                )");
                        break;
                    default:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = '" + (int)iOrderStatus + "' ");
                        break;
                }
            }

            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Order_Order.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Order_Order.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Order_OrderDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order LEFT JOIN Order_ConsigneeAddress ON Order_ConsigneeAddress.OID = Order_Order.ID AND Order_ConsigneeAddress.Type = 1 AND Order_ConsigneeAddress.Obj_Status = 1", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order LEFT JOIN Order_ConsigneeAddress ON Order_ConsigneeAddress.OID = Order_Order.ID AND Order_ConsigneeAddress.Type = 1 AND Order_ConsigneeAddress.Obj_Status = 1", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_OrderDC.GetEntity(reader);

                    if (entity != null)
                    {
                        entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_OrderDC> wx_Order_Order_SELECT_List(
         Guid iUserID, OrderStatus? iOrderStatus,
         int iPageIndex, int iPageSize)
        {
            List<Order_OrderDC> list = new List<Order_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.*,User_Info.MPNo ");
            sqlwhere.Append(@"Order_Order.Obj_Status = 1 ");
            //sqlwhere.Append(@" AND Order_Order.UserID = User_Info.ID ");
            sqlorder.Append(@" Order_Order.ID DESC ");


            //用户ID
            sqlwhere.Append(" AND Order_Order.UserID = '" + iUserID + "' ");
            //分类
            sqlwhere.Append(" AND Order_Order.OrderClass IN (1,2)");
            //订单类型
            sqlwhere.Append(" AND Order_Order.OrderType != 6");

            //订单状态
            if (iOrderStatus.HasValue)
            {
                switch (iOrderStatus.Value)
                {
                    case OrderStatus.AllFinish:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = 2 AND Step = 7");
                        break;
                    case OrderStatus.WaitExpress:
                        sqlwhere.Append(@" AND Order_Order.OrderStatus IN (0,2) AND Step != 7 
                                    AND (OrderClass = 1 OR (OrderClass = 2 AND TotalAmount > 0))");
                        break;
                    case OrderStatus.UnPay:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = 1 AND PayStatus = 0 AND PayAmount < TotalAmount");
                        break;
                    default:
                        sqlwhere.Append(" AND Order_Order.OrderStatus = '" + (int)iOrderStatus + "' ");
                        break;
                }
            }

            var rtn = new RecordCountEntity<Order_OrderDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order LEFT JOIN User_Info ON Order_Order.UserID = User_Info.ID", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order LEFT JOIN User_Info ON Order_Order.UserID = User_Info.ID", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_OrderDC.GetEntity(reader);
                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOrderStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_OrderDC> app_Order_Order_SELECT_List(
         Guid iUserID, OrderStatus? iOrderStatus,
         int iPageIndex, int iPageSize)
        {
            List<Order_OrderDC> list = new List<Order_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            //StringBuilder sqlfields = new StringBuilder();
            //StringBuilder sqlorder = new StringBuilder();
            //sqlfields.Append(" TOP 5 * ");

            sql.Append("SELECT TOP 5 * FROM Order_Order ");

            sql.Append(@" WHERE Obj_Status = 1 ");

            //用户ID
            sql.Append(" AND UserID = '" + iUserID + "' ");
            //分类
            sql.Append(" AND OrderClass IN (1,2)");
            //订单类型
            sql.Append(" AND OrderType != 6");
            //订单状态
            sql.Append(" AND OrderStatus IN (0,1,2)");

            sql.Append(" AND NOT EXISTS (SELECT 1 FROM Order_Feedback WHERE OID = Order_Order.ID)");

            sql.Append(@" ORDER BY Order_Order.ID DESC ");

            var rtn = new RecordCountEntity<Order_OrderDC>();

            ////取得总数量
            //rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order", "ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            ////取得功能权限信息列表
            //using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order", "ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            //{
            //    while (reader.Read())
            //    {
            //        var entity = Order_OrderDC.GetEntity(reader);
            //        if (entity != null)
            //        {
            //            entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
            //            entity.AmountList = Order_Amount_SELECT_List(entity.ID);
            //            list.Add(entity);
            //        }
            //    }
            //}

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    var entity = Order_OrderDC.GetEntity(reader);
                    if (entity != null)
                    {
                        entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                        entity.AmountList = Order_Amount_SELECT_List(entity.ID);
                        entity.PaymentList = Order_Payment_SELECT_List(entity.ID);
                        list.Add(entity);
                    }
                }
            }

            rtn.RecordCount = list.Count;

            rtn.ReturnList = list;

            return rtn;
        }

        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iOrderStatus"></param>
        /// <returns></returns>
        public int Order_Order_Cancel(string iOrderNumber, OrderStatus iOrderStatus)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_Cancel");

            db.AddInParameter(cmd, "@OrderNumber", DbType.String, iOrderNumber);
            db.AddInParameter(cmd, "@Status", DbType.Int32, (int)iOrderStatus);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));
        }

        /// <summary>
        /// 退单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iChannel"></param>
        /// <returns></returns>
        public int Order_Order_ChargeBack(string iOrderNumber, Channel iChannel)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_ChargeBack");

            db.AddInParameter(cmd, "@OrderNumber", DbType.String, iOrderNumber);
            db.AddInParameter(cmd, "@Channel", DbType.Int32, (int)iChannel);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));
        }

        /// <summary>
        /// 用户余额使用
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUseMoney"></param>
        /// <param name="iContent"></param>
        /// <param name="iSourceID"></param>
        /// <returns></returns>
        public int User_Amount_Use(Guid iUserID, decimal iUseMoney, string iContent, string iSourceID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Amount_Use");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@LogType", DbType.Int32, 2);
            db.AddInParameter(cmd, "@UseMoney", DbType.Decimal, iUseMoney);
            db.AddInParameter(cmd, "@Content", DbType.String, iContent);
            db.AddInParameter(cmd, "@SourceID", DbType.String, iSourceID);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));

        }

        /// <summary>
        /// 用户余额退还
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUseMoney"></param>
        /// <param name="iContent"></param>
        /// <param name="iSourceID"></param>
        /// <returns></returns>
        public int User_Amount_Back(Guid iUserID, decimal iUseMoney, string iContent, string iSourceID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Amount_Back");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@LogType", DbType.Int32, 2);
            db.AddInParameter(cmd, "@UseMoney", DbType.Decimal, iUseMoney);
            db.AddInParameter(cmd, "@Content", DbType.String, iContent);
            db.AddInParameter(cmd, "@SourceID", DbType.String, iSourceID);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));

        }

        /// <summary>
        /// 用户卡余额使用
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardID"></param>
        /// <param name="iUseMoney"></param>
        /// <param name="iContent"></param>
        /// <param name="iSourceID"></param>
        /// <returns></returns>
        public int User_Card_Use(Guid iUserID, int iUserCardID, decimal iUseMoney, string iContent, string iSourceID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Card_Use");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@LogType", DbType.Int32, 3);
            db.AddInParameter(cmd, "@UserCardID", DbType.Int32, iUserCardID);
            db.AddInParameter(cmd, "@UseMoney", DbType.Decimal, iUseMoney);
            db.AddInParameter(cmd, "@Content", DbType.String, iContent);
            db.AddInParameter(cmd, "@SourceID", DbType.String, iSourceID);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));
        }

        /// <summary>
        /// 门店订单删除产品
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <returns></returns>
        public bool Order_Product_DELETE_Store(int iOrderID, int iOrderProductID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Product SET Obj_Status = 6, Obj_Mdate = GETDATE() WHERE ID = @ID
                AND EXISTS(SELECT 1 FROM Order_Order WHERE Order_Product.OID = Order_Order.ID 
                        AND Order_Order.ID = @OID AND OrderClass = 3 AND OrderType = 1
                        AND OrderStatus = 1 AND Order_Order.Obj_Status = 1)");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            //标题
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderProductID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 价目单个
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public Wash_ProductDC Wash_Product_SELECT_Entity(int iID)
        {
            Wash_ProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Wash_Product.*,Wash_Category.ClassID,Wash_Category.Name AS CategoryName
                            FROM Wash_Product,Wash_Category
                            WHERE Wash_Product.ID = @ID
                            AND Wash_Product.CategoryID = Wash_Category.ID
                            AND Wash_Product.Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Wash_ProductDC.GetEntity(reader);
                }
            }

            return entity;
        }

        /// <summary>
        /// 更新订单金额(一键下单)
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iTitle"></param>
        /// <param name="iTotalAmount"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_Amount_OneKey(int iOrderID, string iTitle, decimal iTotalAmount,
            decimal iSourceAmount, int iSendType)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET Title = @Title, TotalAmount = @TotalAmount, 
                SourceAmount = @SourceAmount, SendType = @SendType WHERE ID = @ID AND OrderClass = 2");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iTitle);
            //金额
            db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, iTotalAmount);

            db.AddInParameter(cmd, "@SourceAmount", DbType.Decimal, iSourceAmount);

            db.AddInParameter(cmd, "@SendType", DbType.Int32, iSendType);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 更新订单金额
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iTitle"></param>
        /// <param name="iTotalAmount"></param>
        /// <param name="iSourceAmount"></param>
        /// <param name="iPayAmount"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_Amount_EditProduct(int iOrderID, string iTitle,
            decimal iTotalAmount, decimal iSourceAmount, decimal iPayAmount)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET Title = @Title, TotalAmount = @TotalAmount, SourceAmount = @SourceAmount, PayAmount = @PayAmount, PayStatus = 0 WHERE ID = @ID;");
            sql.Append(
@"IF((SELECT 1 FROM Order_Order WHERE TotalAmount <= PayAmount AND ID = @ID) > 0)
BEGIN
    UPDATE Order_Order SET PayStatus = 1 WHERE ID = @ID                     
END");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iTitle);
            //金额
            db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, iTotalAmount);

            db.AddInParameter(cmd, "@SourceAmount", DbType.Decimal, iSourceAmount);

            db.AddInParameter(cmd, "@PayAmount", DbType.Decimal, iPayAmount);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iCSRemark"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_CSRemark(int iOrderID, string iCSRemark)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET CSRemark = @CSRemark WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //客服备注
            db.AddInParameter(cmd, "@CSRemark", DbType.String, iCSRemark);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        /// <summary>
        /// 订单审核
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iAuditStatus"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Order_Order_Audit(int iOrderID, bool iAuditStatus, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET OrderStatus = @OrderStatus, Obj_Muser = @Obj_Muser
                WHERE ID = @ID AND OrderClass = 2 AND OrderStatus = 0 AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            if (iAuditStatus)
            {
                //订单状态
                db.AddInParameter(cmd, "@OrderStatus", DbType.Int32, 1);
            }
            else
            {
                db.AddInParameter(cmd, "@OrderStatus", DbType.Int32, 32);

            }
            //操作人ID
            db.AddInParameter(cmd, "@Obj_Muser", DbType.Int32, iMuser);

            return db.ExecuteNonQuery(cmd) == 1 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public int User_Coupon_Back(Guid iUserID, int iUserCouponID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Coupon_Back");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            db.AddInParameter(cmd, "@UserCouponID", DbType.Int32, iUserCouponID);

            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            var rtn = Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));

            return rtn;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public User_InfoDC User_Info_SELECT_Entity(Guid iUserID)
        {
            User_InfoDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM User_Info WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_InfoDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        public User_InfoDC User_Info_SELECT_Entity(string iOpenid, OAuthType iOAuthType)
        {
            User_InfoDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM User_Info 
                WHERE ID = (SELECT UserID 
                                FROM User_OAuth WHERE OpenID = @OpenID AND Type = @Type AND Obj_Status = 1)");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iOAuthType);

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_InfoDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardID"></param>
        /// <param name="iUseMoney"></param>
        /// <returns></returns>
        public bool User_Card_CheckBalance(Guid iUserID, int iUserCardID, decimal iUseMoney)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT COUNT(0) 
                FROM Base_Card,User_Card
                WHERE Base_Card.ID = User_Card.CardID
                AND User_Card.ID = @ID
                AND User_Card.UserID = @UserID
                AND Base_Card.Balance >= @UseMoney
                AND Base_Card.Obj_Status = 1
                AND User_Card.Obj_Status = 1
                AND Base_Card.Enable = 1
                AND User_Card.RelationType = 1
                ");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //ID
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@ID", DbType.String, iUserCardID);
            db.AddInParameter(cmd, "@UseMoney", DbType.Decimal, iUseMoney);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 根据门店ID查询订单主表
        /// </summary>
        /// <param name="iStoreID">门店ID</param>
        /// <returns></returns>
        public Order_OrderDC Order_Order_SELECT_Entity(Guid iStoreID)
        {
            Order_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM Order_Order WHERE UserID = @UserID 
                AND OrderStatus = 1
                AND OrderClass = 3 AND OrderType = 1 AND Obj_Status = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iStoreID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Order_OrderDC.GetEntity(reader);
                }
            }
            if (entity != null)
            {
                entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
            }
            return entity;
        }

        /// <summary>
        /// 微信ID获取用户ID
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public Guid? User_Weixin_SELECT_UserID(string iOpenid, OAuthType iOAuthType)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT ID FROM User_Info WHERE ID = (SELECT UserID 
                                FROM User_OAuth WHERE OpenID = @OpenID AND Type = @Type AND Obj_Status = 1)");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Type", DbType.Int32, (int)iOAuthType);

            db.AddInParameter(cmd, "@OpenID", DbType.String, iOpenid);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    return reader.GetGuid(0);
                }
            }
            return null;
        }

        /// <summary>
        /// 门店完成订单
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iTitle"></param>
        /// <param name="iTotalAmount"></param>
        /// <param name="iExpressNumber"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_StoreFinish(int iOrderID, string iTitle, decimal iTotalAmount, string iExpressNumber)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order 
                SET Title = @Title, TotalAmount = @TotalAmount, OrderStatus = 2,GetExpressNumber = @GetExpressNumber 
                WHERE ID = @ID AND OrderClass = 3 AND OrderType = 1 AND OrderStatus = 1");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //标题
            db.AddInParameter(cmd, "@Title", DbType.String, iTitle);
            //金额
            db.AddInParameter(cmd, "@TotalAmount", DbType.Decimal, iTotalAmount);

            db.AddInParameter(cmd, "@GetExpressNumber", DbType.String, iExpressNumber);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 门店查询用户未签收订单产品
        /// </summary>
        /// <param name="iStoreID"></param>
        /// <param name="iUserMPNo"></param>
        /// <param name="iUserName"></param>
        /// <param name="iUserSignType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <returns></returns>
        public IList<Order_ProductDC> Order_Product_SELECT_List_Store(Guid iStoreID, string iUserMPNo, string iUserName,
            UserSignType? iUserSignType, DateTime iStartDate, DateTime iEndDate)
        {
            List<Order_ProductDC> list = new List<Order_ProductDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Order_Product.*,
                Wash_Product.WebName,Wash_Category.ID AS CategoryID,Wash_Category.Name AS CategoryName
                FROM Order_Order,Order_Product,Wash_Product,Wash_Category
                    WHERE Order_Order.ID =  Order_Product.OID
                    AND Order_Order.OrderClass = 3
                    AND Order_Order.OrderStatus = 2
                    AND Order_Product.ProductID = Wash_Product.ID
                    AND Wash_Product.CategoryID = Wash_Category.ID
                    AND Order_Product.Obj_Status = 1
                ");

            sql.Append(@" AND Order_Order.UserID = '" + iStoreID + "' ");

            if (iUserSignType.HasValue)
            {
                sql.Append(@" AND Order_Product.UserSignType = '" + (int)iUserSignType + "' ");
            }

            if (!string.IsNullOrWhiteSpace(iUserMPNo))
            {
                sql.Append(@" AND Order_Product.UserMPNo = '" + iUserMPNo + "' ");
            }
            if (!string.IsNullOrWhiteSpace(iUserName))
            {
                sql.Append(@" AND Order_Product.UserName LIKE '%" + iUserName + "%' ");
            }

            //时间开始
            sql.Append(" AND Order_Product.Obj_Cdate >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束
            sql.Append(" AND Order_Product.Obj_Cdate <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            sql.Append(" ORDER BY Order_Product.ID DESC ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    list.Add(Order_ProductDC.GetEntity(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 门店签收用户订单产品
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iUserName"></param>
        /// <param name="iUserSignType"></param>
        /// <returns></returns>
        public bool Order_Product_UPDATE_UserSignType(int iOrderProductID, UserSignType iUserSignType)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Product 
                SET UserSignType = @UserSignType,UserSignTime = GETDATE()
                WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderProductID);
            //签收状态
            db.AddInParameter(cmd, "@UserSignType", DbType.Int32, (int)iUserSignType);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 完成订单消息推送
        /// </summary>
        /// <returns></returns>
        public void Order_Finish_Message(int iOrderID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_Finish_Message");

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 订单出工厂消息推送
        /// </summary>
        /// <returns></returns>
        public void Order_OutFactory_Message(int iOrderID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_OutFactory_Message");

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 订单签收消息推送
        /// </summary>
        /// <returns></returns>
        public void Order_AllFinish_Message(int iOrderID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_AllFinish_Message");

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 订单到工厂消息推送
        /// </summary>
        /// <returns></returns>
        public void Order_Onekey_InFactory_Message(int iOrderID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_Onekey_InFactory_Message");

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.ExecuteNonQuery(cmd);
        }

        /// <summary>
        /// 根据ID查询商场产品
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public Mall_ProductDC Mall_Product_SELECT_Entity(int iID)
        {
            Mall_ProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT * FROM Mall_Product WHERE ID = @ID AND Obj_Status = 1 AND Enable = 1 AND SellBeginDate <= GETDATE() AND SellEndDate >= GETDATE()");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);
            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = Mall_ProductDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 更新商城产品剩余数量
        /// </summary>
        /// <param name="iProductID"></param>
        /// <param name="iBuyCount"></param>
        public bool Mall_Product_UPDATE_LeftCount(int iProductID, int iBuyCount)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Mall_Product SET LeftCount = LeftCount - @BuyCount,SaleCount = SaleCount + @BuyCount 
                            WHERE ID = @ID 
                            AND Enable = 1 AND Obj_Status = 1 
                            AND LeftCount >= @BuyCount
                            AND LeftCount != -1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iProductID);

            db.AddInParameter(cmd, "@BuyCount", DbType.Int32, iBuyCount);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 获取电子懒人卡的最后ID
        /// </summary>
        /// <returns></returns>
        public int Base_Card_GetLastID_E(string iPrefix)
        {
            string number = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT MAX(NUMBER) FROM Base_Card WHERE Number LIKE '" + iPrefix + "%' ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    number = reader.GetString(0);
                }
            }
            if (string.IsNullOrWhiteSpace(number))
            {
                return 0;
            }
            else
            {
                return int.Parse(number.Remove(0, iPrefix.Length));
            }
        }

        /// <summary>
        /// 检查密码是否重复
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool Base_Card_Password_Exists(string iPassword)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append("SELECT COUNT(0) FROM Base_Card WHERE Password = @Password ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@Password", DbType.String, iPassword);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 创建懒人卡
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iCardID"></param>
        /// <param name="iPassword"></param>
        /// <param name="iFaceValue"></param>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public int Base_Card_Create(int iType, string iCardID, string iPassword, decimal iFaceValue, string iOrderNumber)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("INSERT INTO Base_Card(Type, Number, Password, Money, Balance, ExpiryDate, CityCode, Batch, Enable, Used, Obj_Remark, Obj_Cuser, Obj_Muser)");
            sql.Append("VALUES (@Type, @Number, @Password, @Balance, @Money, @ExpiryDate, @CityCode,@Batch, @Enable, @Used, @Obj_Remark, @Obj_Cuser, @Obj_Muser)");
            sql.Append(" ;SELECT @@IDENTITY;");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@Type", DbType.Int32, iType);
            db.AddInParameter(cmd, "@Number", DbType.String, iCardID);
            db.AddInParameter(cmd, "@Password", DbType.String, iPassword);
            db.AddInParameter(cmd, "@Money", DbType.Decimal, iFaceValue);
            db.AddInParameter(cmd, "@Balance", DbType.Decimal, iFaceValue);
            db.AddInParameter(cmd, "@ExpiryDate", DbType.DateTime, DateTime.Parse("2020-1-1"));
            db.AddInParameter(cmd, "@CityCode", DbType.String, "021");
            db.AddInParameter(cmd, "@Batch", DbType.String, "20140604");
            db.AddInParameter(cmd, "@Enable", DbType.Int32, 1);
            db.AddInParameter(cmd, "@Used", DbType.Int32, 0);
            db.AddInParameter(cmd, "@Obj_Remark", DbType.String, "订单:" + iOrderNumber);
            db.AddInParameter(cmd, "@Obj_Cuser", DbType.Int32, -1);
            db.AddInParameter(cmd, "@Obj_Muser", DbType.Int32, -1);

            int id = Convert.ToInt32(db.ExecuteScalar(cmd));
            return id;
        }

        /// <summary>
        /// 订单更新卡密
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iCode"></param>
        /// <returns></returns>
        public bool Order_Product_UPDATE_Code(int iID, string iCode, string iAttribute)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Order_Product SET Code = @Code,Attribute = @Attribute WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            db.AddInParameter(cmd, "@Code", DbType.String, iCode);

            db.AddInParameter(cmd, "@Attribute", DbType.String, iAttribute);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 修改工厂洗涤状态
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iFactoryWash"></param>
        /// <returns></returns>
        public bool Order_Product_UPDATE_FactoryWash(int iID, int iFactoryWash)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Order_Product SET FactoryWash = @FactoryWash WHERE ID = @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iID);

            db.AddInParameter(cmd, "@FactoryWash", DbType.Int32, iFactoryWash);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 合并订单
        /// </summary>
        /// <param name="iPrimaryOrderID"></param>
        /// <param name="iSlaveOrderID"></param>
        /// <returns></returns>
        public bool Order_Order_Merger(int iPrimaryOrderID, int iSlaveOrderID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE PrimaryOrder SET 
                            TotalAmount = PrimaryOrder.TotalAmount + SlaveOrder.TotalAmount,
                            PayAmount = PrimaryOrder.PayAmount + SlaveOrder.PayAmount,
                            Title = PrimaryOrder.Title + '(合单)',SystemRemark = '(合单)'
                        FROM Order_Order PrimaryOrder, Order_Order SlaveOrder
                            WHERE PrimaryOrder.ID = @PrimaryOrderID
                            AND SlaveOrder.ID = @SlaveOrderID
                            AND PrimaryOrder.OrderClass = 1
                            AND PrimaryOrder.OrderType IN (1,2,3)
                            AND PrimaryOrder.OrderClass = SlaveOrder.OrderClass
                            AND PrimaryOrder.OrderType = SlaveOrder.OrderType
                            AND PrimaryOrder.OrderStatus = 2
                            AND SlaveOrder.OrderStatus = 2
                            AND PrimaryOrder.Obj_Status = 1
                            AND SlaveOrder.Obj_Status = 1
");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@PrimaryOrderID", DbType.Int32, iPrimaryOrderID);

            db.AddInParameter(cmd, "@SlaveOrderID", DbType.Int32, iSlaveOrderID);

            var rtn = db.ExecuteNonQuery(cmd);
            if (rtn == 1)
            {
                sql.Clear();

                sql.Append(@"
UPDATE Order_Order SET Obj_Status = 6,Obj_Mdate = GETDATE(),SystemRemark = '已合单' WHERE ID = @SlaveOrderID
UPDATE Order_Product SET OID = @PrimaryOrderID WHERE OID = @SlaveOrderID AND Obj_Status = 1
UPDATE Order_Amount SET OID = @PrimaryOrderID WHERE OID = @SlaveOrderID AND Obj_Status = 1
UPDATE Order_Payment SET OID = @PrimaryOrderID WHERE OID = @SlaveOrderID AND Obj_Status = 1
");

                cmd = db.GetSqlStringCommand(sql.ToString());

                db.AddInParameter(cmd, "@PrimaryOrderID", DbType.Int32, iPrimaryOrderID);

                db.AddInParameter(cmd, "@SlaveOrderID", DbType.Int32, iSlaveOrderID);

                db.ExecuteNonQuery(cmd);

                return true;
            }
            return false;
        }


        public RecordCountEntity<Partner_Order_ExpressDC> Partner_Order_Express_SELECT_List(
          string iUserName, string iMPNo, int? iExpressStatus,
          int iPageIndex, int iPageSize)
        {
            List<Partner_Order_ExpressDC> list = new List<Partner_Order_ExpressDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();

            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_ConsigneeAddress.Consignee UserName");
            sqlfields.Append(@" ,Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Address");
            sqlfields.Append(@" ,Order_Order.ExpectTime,Order_Order.CompleteTime");
            sqlfields.Append(@" ,Order_Order.GetExpressNumber,Order_Order.ExpressStatus ");

            sqlwhere.Append(@" Order_Order.ID = Order_ConsigneeAddress.OID");
            sqlwhere.Append(@" AND Order_ConsigneeAddress.Type = 1");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 AND Order_Order.OrderClass IN(1,2) ");
            sqlwhere.Append(@" AND Order_Order.OrderType IN(1,2,3) ");
            sqlwhere.Append(@" AND Order_Order.OrderStatus = 2");
            sqlwhere.Append(@" AND Order_Order.GetExpressNumber IS NULL");

            sqlorder.Append(@" Order_Order.ID DESC ");

            //用户名
            if (!string.IsNullOrWhiteSpace(iUserName))
            {
                if (!DBHelper.CheckParms(iUserName))
                {
                    return new RecordCountEntity<Partner_Order_ExpressDC>() { RecordCount = 0, ReturnList = list };
                }
                sqlwhere.Append(" AND Order_ConsigneeAddress.Consignee LIKE '%" + iUserName + "%' ");
            }
            //用户手机
            if (!string.IsNullOrWhiteSpace(iMPNo))
                sqlwhere.Append(" AND Order_ConsigneeAddress.MPNo = '" + iMPNo + "' ");

            //分类
            if (iExpressStatus.HasValue)
                sqlwhere.Append(" AND Order_Order.ExpressStatus = '" + iExpressStatus + "' ");

            var rtn = new RecordCountEntity<Partner_Order_ExpressDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Partner_Order_ExpressDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }


        public RecordCountEntity<Partner_Order_ExpressDC> Partner_Order_Express_SELECT_List(
            string iUserName, string iMPNo, string iGetExpressNumber,
            DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Partner_Order_ExpressDC> list = new List<Partner_Order_ExpressDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();

            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_ConsigneeAddress.Consignee UserName");
            sqlfields.Append(@" ,Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Address");
            sqlfields.Append(@" ,Order_Order.ExpectTime,Order_Order.CompleteTime");
            sqlfields.Append(@" ,Order_Order.GetExpressNumber,Order_Order.ExpressStatus ");

            sqlwhere.Append(@" Order_Order.ID = Order_ConsigneeAddress.OID");
            sqlwhere.Append(@" AND Order_ConsigneeAddress.Type = 1");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 AND Order_Order.OrderClass IN(1,2) ");
            sqlwhere.Append(@" AND Order_Order.OrderType IN(1,2,3) ");
            sqlwhere.Append(@" AND Order_Order.OrderStatus = 2");

            sqlorder.Append(@" Order_Order.ID DESC ");

            //用户名
            if (!string.IsNullOrWhiteSpace(iUserName))
            {
                if (!DBHelper.CheckParms(iUserName))
                {
                    return new RecordCountEntity<Partner_Order_ExpressDC>() { RecordCount = 0, ReturnList = list };
                }
                sqlwhere.Append(" AND Order_ConsigneeAddress.Consignee LIKE '%" + iUserName + "%' ");
            }

            //用户手机
            if (!string.IsNullOrWhiteSpace(iMPNo))
                sqlwhere.Append(" AND Order_ConsigneeAddress.MPNo = '" + iMPNo + "' ");
            //取件快递号
            if (!string.IsNullOrWhiteSpace(iGetExpressNumber))
                sqlwhere.Append(" AND Order_Order.GetExpressNumber = '" + iGetExpressNumber + "' ");


            //创建时间开始
            if (iStartDate.HasValue)
                sqlwhere.Append(" AND Order_Order.Obj_Cdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束
            if (iEndDate.HasValue)
                sqlwhere.Append(" AND Order_Order.Obj_Cdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Partner_Order_ExpressDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Partner_Order_ExpressDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        public bool Partner_Order_Express_UPDATE_ExpressStatus(int iOrderID, int iExpressStatus)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET ExpressStatus = @ExpressStatus");
            if (iExpressStatus == 1)
            {
                sql.Append(@",PushExpressTime = GETDATE()");
            }
            sql.Append(@" WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //状态
            db.AddInParameter(cmd, "@ExpressStatus", DbType.Int32, iExpressStatus);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }



        /// <summary>
        /// 查询优惠券列表
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <param name="iCouponName"></param>
        /// <param name="iUseClass"></param>
        /// <param name="iUseType"></param>
        /// <param name="iCommitStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public User_CouponDC User_Coupon_SELECT_Entity(Guid iUserID, int iUserCouponID)
        {
            User_CouponDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT User_Coupon.*,Base_Coupon.Name,Base_Coupon.UseClass,Base_Coupon.UseType,
                    Base_Coupon.FaceValue,Base_Coupon.MinMoney,Base_Coupon.MaxMoney,Base_Coupon.TotalCount,
                    Base_Coupon.SendCount,Base_Coupon.ValidDay
                FROM Base_Coupon,User_Coupon
                WHERE User_Coupon.Obj_Status = 1 AND Base_Coupon.Obj_Status = 1");
            sql.Append(" AND Base_Coupon.CommitStatus = 1");
            sql.Append(" AND User_Coupon.CouponID = Base_Coupon.ID ");
            sql.Append(" AND User_Coupon.CouponStatus = 1 ");
            //sql.Append(" AND Base_Coupon.UseEndDate > GETDATE() ");
            sql.Append(" AND User_Coupon.UserID = @UserID ");
            sql.Append(" AND User_Coupon.ID = @UserCouponID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@UserCouponID", DbType.Int32, iUserCouponID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_CouponDC.GetEntity(reader);

                }
            }

            if (entity != null && entity.UseClass == 1)
            {
                entity.CouponProductList = Base_CouponProduct_SELECT_List(entity.CouponID);
            }

            return entity;
        }

        /// <summary>
        /// 获取优惠券限制品类
        /// </summary>
        /// <param name="iCouponID"></param>
        /// <returns></returns>
        private IList<Base_CouponProductDC> Base_CouponProduct_SELECT_List(int iCouponID)
        {
            List<Base_CouponProductDC> list = new List<Base_CouponProductDC>();
            Base_CouponProductDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Base_CouponProduct.*
                ,Wash_Product.Name
                ,Wash_Product.SalesPrice
                FROM Base_CouponProduct,Wash_Product 
            WHERE CouponID = @CouponID 
            AND Base_CouponProduct.ProductID = Wash_Product.ID
            AND Base_CouponProduct.Obj_Status = 1
            AND Wash_Product.Obj_Status = 1
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@CouponID", DbType.Int32, iCouponID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Base_CouponProductDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        /// <summary>
        /// 用户使用优惠券
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCouponID"></param>
        /// <param name="iOrderNumber"></param>
        /// <returns></returns>
        public int User_Coupon_Use(Guid iUserID, int iUserCouponID, string iOrderNumber)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Coupon_Use");

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@UserCouponID", DbType.Int32, iUserCouponID);
            db.AddInParameter(cmd, "@UseOrderNumber", DbType.String, iOrderNumber);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            return Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));
        }

        /// <summary>
        /// 获取用户卡
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardID"></param>
        /// <returns></returns>
        public User_CardDC User_Card_SELECT_Entity(Guid iUserID, int iUserCardID)
        {
            User_CardDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();
            sql.Append(@"
            SELECT User_Card.ID, Base_Card.Type,Base_Card.Number,Base_Card.Password,Base_Card.Money,
                    Base_Card.Balance,Base_Card.ExpiryDate,Base_Card.CityCode,Base_Card.Batch,
                    Base_Card.Enable,Base_Card.Used,User_Card.CardID,User_Card.UserID
                FROM Base_Card,User_Card
                WHERE Base_Card.ID = User_Card.CardID
                AND User_Card.UserID = @UserID
                AND User_Card.ID = @ID
                AND Base_Card.Obj_Status = 1
                AND User_Card.Obj_Status = 1
                AND Base_Card.Enable = 1
                AND User_Card.RelationType = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@ID", DbType.Int32, iUserCardID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_CardDC.GetEntity(reader);
                }
            }
            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserCouponID"></param>
        /// <returns></returns>
        public User_CouponDC User_Coupon_SELECT_Entity(int iUserCouponID)
        {
            User_CouponDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlwhere = new StringBuilder();

            sqlwhere.Append(@"SELECT 
                    User_Coupon.*,Base_Coupon.Name,Base_Coupon.UseClass,Base_Coupon.UseType,
                    Base_Coupon.FaceValue,Base_Coupon.MinMoney,Base_Coupon.MaxMoney,Base_Coupon.TotalCount,
                    Base_Coupon.SendCount,Base_Coupon.ValidDay,User_Info.MPNo
                FROM Base_Coupon,User_Coupon,User_Info WHERE User_Coupon.Obj_Status = 1 AND Base_Coupon.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Base_Coupon.CommitStatus = 1 ");
            sqlwhere.Append(@" AND User_Coupon.CouponID = Base_Coupon.ID ");
            sqlwhere.Append(@" AND User_Coupon.CouponStatus IN (1,2) ");
            sqlwhere.Append(@" AND User_Coupon.UserID = User_Info.ID ");

            sqlwhere.Append(@" AND User_Coupon.ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sqlwhere.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iUserCouponID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    entity = User_CouponDC.GetEntity(reader);
                }
            }

            return entity;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public int User_Coupon_Bind(Guid iUserID, string iCouponCode)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_User_Coupon_Bind");

            iCouponCode = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(iCouponCode);

            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);
            db.AddInParameter(cmd, "@CouponCode", DbType.String, iCouponCode);
            db.AddOutParameter(cmd, "@Rtn", DbType.Int32, int.MaxValue);

            db.ExecuteNonQuery(cmd);

            var rtn = Convert.ToInt32(db.GetParameterValue(cmd, "@Rtn"));

            return rtn;
        }

        /// <summary>
        /// 优惠券支付金额变更
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iPaymentID"></param>
        /// <param name="iPayMoney"></param>
        /// <returns></returns>
        public bool Order_Pay_Coupon_EditProduct(int iOrderID, int iPaymentID, decimal iPayMoney)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Order_Payment SET PayMoney = @PayAmount WHERE ID = @PaymentID AND OID = @OID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@PayAmount", DbType.Decimal, iPayMoney);

            db.AddInParameter(cmd, "@PaymentID", DbType.Int32, iPaymentID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iPaymentID"></param>
        /// <param name="iPayMoney"></param>
        /// <returns></returns>
        public bool Order_Pay_Coupon_Factory(int iOrderID, int iPaymentID, decimal iPayMoney)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET PayAmount = PayAmount + @PayAmount WHERE ID = @ID; ");
            sql.Append(@"UPDATE Order_Payment SET PayMoney = @PayAmount WHERE ID = @PaymentID; ");
            sql.Append(
@"IF((SELECT 1 FROM Order_Order WHERE TotalAmount <= PayAmount AND ID = @ID) > 0)
BEGIN
    UPDATE Order_Order SET PayStatus = 1 WHERE ID = @ID
END");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@PayAmount", DbType.Decimal, iPayMoney);

            db.AddInParameter(cmd, "@PaymentID", DbType.Int32, iPaymentID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iPayMoney"></param>
        /// <returns></returns>
        public bool Order_Pay_Balance_Factory(int iOrderID, decimal iPayMoney)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET PayAmount = PayAmount + @PayAmount WHERE ID = @ID; ");
            sql.Append(
@"IF((SELECT COUNT(0) FROM Order_Order WHERE TotalAmount = PayAmount AND ID = @ID) > 0)
BEGIN
    UPDATE Order_Order SET PayStatus = 1 WHERE ID = @ID                     
END");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@PayAmount", DbType.Decimal, iPayMoney);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iExpectTime"></param>
        /// <returns></returns>
        public bool Order_ExpectTime_Change(int iOrderID, DateTime iExpectTime)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Order_Order SET ExpectTime = @ExpectTime WHERE ID = @ID; 
                         EXEC PROC_Order_ChangeExpectTime_Message @ID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@ExpectTime", DbType.DateTime, iExpectTime);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 完成订单推广送券
        /// </summary>
        /// <param name="iOrderID"></param>
        public void Order_Finish_SendCoupon(int iOrderID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            DbCommand cmd = db.GetStoredProcCommand("PROC_Order_Finish_SendCoupon");

            db.AddInParameter(cmd, "@OrderID", DbType.String, iOrderID);

            db.ExecuteNonQuery(cmd);

        }

        /// <summary>
        /// 修改产品步骤
        /// </summary>
        /// <param name="iOrderProductID"></param>
        /// <param name="iStep"></param>
        /// <returns></returns>
        public bool Order_Product_UPDATE_Step(int iOrderProductID, int iStep)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Order_Product SET Step = @Step ");

            if (iStep == 5)
            {
                sql.Append(" ,Step5Time = GETDATE() ");
            }

            sql.Append(" WHERE ID = @ID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderProductID);

            db.AddInParameter(cmd, "@Step", DbType.Int32, iStep);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 工厂洗涤条码导出
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_Order_StatDC> Order_Product_WashStep_Code_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Order_Order_StatDC> list = new List<Order_Order_StatDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_Product.Code ");
            sqlwhere.Append(@" Order_Order.ID = Order_Product.OID ");
            sqlwhere.Append(@" AND Order_Product.Code IS NOT NULL ");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Order_Product.Obj_Status = 1 ");

            sqlorder.Append(@" Order_Order.ID ");

            //创建时间开始 
            sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Order_Step WHERE Order_Step.OID = Order_Order.ID AND Order_Step.Obj_Status = 1 AND Order_Step.Type = 4 AND Order_Step.Obj_Cdate >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束 
            sqlwhere.Append(" AND Order_Step.Obj_Cdate <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "') ");

            var rtn = new RecordCountEntity<Order_Order_StatDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_Product", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_Product", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_Order_StatDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 订单步骤时间统计
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_Order_StatDC> Order_Order_StepTime_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Order_Order_StatDC> list = new List<Order_Order_StatDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_Order.Channel,Order_ConsigneeAddress.Address,");
            sqlfields.Append(@" Order_Order.ExpectTime,Order_Order.Step,Order_Order.CompleteTime,Order_Order.PushExpressTime,Order_Order.AllFinishTime ");
            sqlfields.Append(@" ,Order_Order.Step3Time AS StepTime3 ");
            sqlfields.Append(@" ,Order_Order.Step4Time AS StepTime4 ");
            sqlfields.Append(@" ,Order_Order.Step5Time AS StepTime5 ");

            sqlwhere.Append(@" Order_Order.ID = Order_ConsigneeAddress.OID ");
            sqlwhere.Append(@" AND Order_ConsigneeAddress.Type = 1 ");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 ");

            sqlwhere.Append(@" AND Order_Order.OrderStatus = 2 ");

            sqlorder.Append(@" Order_Order.ID ");

            //创建时间开始 
            sqlwhere.Append(" AND Order_Order.Obj_Cdate >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束 
            sqlwhere.Append(" AND Order_Order.Obj_Cdate <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Order_Order_StatDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_Order_StatDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 订单出库查询
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_Order_StatDC> Order_Order_OutFactory_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Order_Order_StatDC> list = new List<Order_Order_StatDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_Order.OrderClass,Order_Order.Channel,Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Consignee,Order_ConsigneeAddress.Address,");
            sqlfields.Append(@" Order_Order.ExpectTime,Order_Order.Step,Order_Order.CompleteTime,Order_Order.PushExpressTime,Order_Order.AllFinishTime ");

            sqlwhere.Append(@" Order_Order.ID = Order_ConsigneeAddress.OID ");
            sqlwhere.Append(@" AND Order_ConsigneeAddress.Type = 2 ");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Order_Order.OrderStatus = 2 ");

            sqlorder.Append(@" Order_Order.ID ");

            //创建时间开始 
            sqlwhere.Append(" AND EXISTS(SELECT 1 FROM Order_Product WHERE Order_Product.OID = Order_Order.ID AND Order_Product.Obj_Status = 1 ");
            sqlwhere.Append(" AND Order_Product.Step5Time >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //创建时间结束 
            sqlwhere.Append(" AND Order_Product.Step5Time <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' )");

            var rtn = new RecordCountEntity<Order_Order_StatDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_Order_StatDC.GetEntity(reader);

                    if (entity != null)
                    {
                        entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 订单入库查询
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_Order_StatDC> Order_Order_InFactory_Stat(
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Order_Order_StatDC> list = new List<Order_Order_StatDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_Order.OrderClass,Order_Order.Channel,Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Consignee,Order_ConsigneeAddress.Address,");
            sqlfields.Append(@" Order_Order.ExpectTime,Order_Order.Step,Order_Order.CompleteTime,Order_Order.PushExpressTime,Order_Order.AllFinishTime ");

            sqlwhere.Append(@" Order_Order.ID = Order_ConsigneeAddress.OID ");
            sqlwhere.Append(@" AND Order_ConsigneeAddress.Type = 1 ");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Order_Order.OrderStatus = 2 ");

            sqlorder.Append(@" Order_Order.ID ");

            //时间开始 
            sqlwhere.Append(" AND Order_Order.Step4Time >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //时间结束 
            sqlwhere.Append(" AND Order_Order.Step4Time <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Order_Order_StatDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_Order_StatDC.GetEntity(reader);

                    if (entity != null)
                    {
                        entity.ProductList = Order_Product_SELECT_List(entity.ID, entity.OrderClass);
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 修改物流类型
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iType"></param>
        /// <returns></returns>
        public bool Order_Order_UPDATE_GetExpressType(int iOrderID, int iType)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append(@"UPDATE Order_Order SET GetExpressType = @GetExpressType WHERE ID = @ID");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            //
            db.AddInParameter(cmd, "@GetExpressType", DbType.Int32, iType);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 订单步骤时间预警
        /// </summary>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_Order_StatDC> Order_Order_Alarm_Stat(
            bool iGetPackage, bool iWash, bool iSendPackage, bool iAll,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Order_Order_StatDC> list = new List<Order_Order_StatDC>();
            var rtn = new RecordCountEntity<Order_Order_StatDC>();

            if (!(iGetPackage || iWash || iSendPackage || iAll))
            {
                return rtn;
            }

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_Order.Channel,");
            sqlfields.Append(@" Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Consignee,Order_ConsigneeAddress.Address,");
            sqlfields.Append(@" Order_Order.ExpectTime,Order_Order.Step,Order_Order.PushExpressTime, ");
            sqlfields.Append(@" Order_Order.CompleteTime,Order_Order.AllFinishTime ");

            sqlfields.Append(@" ,Order_Order.Step1Time AS StepTime1 ");
            sqlfields.Append(@" ,Order_Order.Step3Time AS StepTime3 ");
            sqlfields.Append(@" ,Order_Order.Step4Time AS StepTime4 ");
            sqlfields.Append(@" ,Order_Order.Step5Time AS StepTime5 ");
            sqlfields.Append(@" ,Order_Order.Step7Time AS StepTime7 ");

            sqlwhere.Append(@" Order_Order.ID = Order_ConsigneeAddress.OID ");
            sqlwhere.Append(@" AND Order_ConsigneeAddress.Type = 1 ");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Order_Order.OrderStatus = 2 ");

            sqlwhere.Append(@" AND( ");

            //取件12小时
            if (iGetPackage)
            {
                sqlwhere.Append(@" (DATEDIFF(HOUR,Order_Order.ExpectTime,ISNULL(Order_Order.Step4Time,GETDATE())) >= 12) ");
            }
            //洗涤24小时
            if (iWash)
            {
                if (iGetPackage)
                {
                    sqlwhere.Append(@" OR ");
                }
                sqlwhere.Append(@" (DATEDIFF(HOUR,Order_Order.Step4Time,ISNULL(Order_Order.Step5Time,GETDATE())) >= 24) ");
            }
            //送件12小时
            if (iSendPackage)
            {
                if (iGetPackage || iWash)
                {
                    sqlwhere.Append(@" OR ");
                }
                sqlwhere.Append(@" (DATEDIFF(HOUR,Order_Order.Step5Time,ISNULL(Order_Order.AllFinishTime,GETDATE())) >= 12) ");
            }
            //全程
            if (iAll)
            {
                if (iGetPackage || iSendPackage || iSendPackage)
                {
                    sqlwhere.Append(@" OR ");
                }
                sqlwhere.Append(@" DATEDIFF(HOUR,Order_Order.ExpectTime,ISNULL(Order_Order.AllFinishTime,GETDATE())) >= 48 ");
            }

            sqlwhere.Append(@" )");

            sqlorder.Append(@" Order_Order.ID ");

            //预约时间开始 
            sqlwhere.Append(" AND Order_Order.ExpectTime >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //预约时间结束 
            sqlwhere.Append(" AND Order_Order.ExpectTime <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_Order_StatDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 订单步骤时间预警
        /// </summary>
        /// <param name="iGetPackage"></param>
        /// <param name="iWash"></param>
        /// <param name="iSendPackage"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Order_Order_StatDC> Order_Order_Warning_Stat(
            bool iGetPackage, bool iWash, bool iSendPackage,
            DateTime iStartDate, DateTime iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Order_Order_StatDC> list = new List<Order_Order_StatDC>();
            var rtn = new RecordCountEntity<Order_Order_StatDC>();

            if (!(iGetPackage || iWash || iSendPackage))
            {
                return rtn;
            }

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlfields.Append(@" Order_Order.ID,Order_Order.OrderNumber,Order_Order.Channel,");
            sqlfields.Append(@" Order_ConsigneeAddress.MPNo,Order_ConsigneeAddress.Consignee,Order_ConsigneeAddress.Address,");
            sqlfields.Append(@" Order_Order.ExpectTime,Order_Order.Step,Order_Order.PushExpressTime, ");
            sqlfields.Append(@" Order_Order.CompleteTime,Order_Order.AllFinishTime ");

            sqlfields.Append(@" ,Order_Order.Step1Time AS StepTime1 ");
            sqlfields.Append(@" ,Order_Order.Step3Time AS StepTime3 ");
            sqlfields.Append(@" ,Order_Order.Step4Time AS StepTime4 ");
            sqlfields.Append(@" ,Order_Order.Step5Time AS StepTime5 ");
            sqlfields.Append(@" ,Order_Order.Step7Time AS StepTime7 ");

            sqlwhere.Append(@" Order_Order.ID = Order_ConsigneeAddress.OID ");
            sqlwhere.Append(@" AND Order_ConsigneeAddress.Type = 1 ");
            sqlwhere.Append(@" AND Order_Order.Obj_Status = 1 ");
            sqlwhere.Append(@" AND Order_Order.Obj_Cuser >= 0 ");
            sqlwhere.Append(@" AND Order_Order.OrderStatus = 2 ");

            sqlwhere.Append(@" AND( ");

            //晚上17点以后
            if (iGetPackage)
            {
                sqlwhere.Append(@" (Order_Order.Step4Time IS NULL AND Order_Order.ExpectTime <= GETDATE() AND (DATEPART(HOUR,GETDATE()) > 17 OR DATEDIFF(DAY,Order_Order.ExpectTime,GETDATE()) > 0) ) ");
            }
            //洗涤24小时
            if (iWash)
            {
                if (iGetPackage)
                {
                    sqlwhere.Append(@" OR ");
                }
                sqlwhere.Append(@" (Order_Order.Step5Time IS NULL AND DATEDIFF(HOUR,Order_Order.Step4Time,GETDATE()) >= 24) ");
            }
            //送件12小时
            if (iSendPackage)
            {
                if (iGetPackage || iWash)
                {
                    sqlwhere.Append(@" OR ");
                }
                sqlwhere.Append(@" (Order_Order.AllFinishTime IS NULL AND DATEDIFF(HOUR,Order_Order.Step5Time,GETDATE()) >= 12) ");
            }

            sqlwhere.Append(@" )");

            sqlorder.Append(@" Order_Order.ID ");

            //预约时间开始 
            sqlwhere.Append(" AND Order_Order.ExpectTime >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            //预约时间结束 
            sqlwhere.Append(" AND Order_Order.ExpectTime <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得功能权限信息列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex("Order_Order,Order_ConsigneeAddress", "Order_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Order_Order_StatDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }

        /// <summary>
        /// 更新外部编号
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iOrderProductID"></param>
        /// <param name="iOtherCode"></param>
        /// <returns></returns>
        public bool Order_Product_UPDATE_OtherCode(int iOrderID, int iOrderProductID, string iOtherCode)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"UPDATE Order_Product SET OtherCode = @OtherCode WHERE ID = @ID AND OID = @OID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //
            db.AddInParameter(cmd, "@OID", DbType.Int32, iOrderID);
            //
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderProductID);
            //
            db.AddInParameter(cmd, "@OtherCode", DbType.String, iOtherCode);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public bool Order_Order_ContinueCheck(Guid iUserID)
        {
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT COUNT(0) FROM Order_Order WHERE Obj_Status = 1 AND OrderStatus IN (0,1,2)  AND DATEDIFF(SECOND,Obj_Cdate,GETDATE()) < 300 AND USERID = @UserID AND OrderClass = 2");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //
            db.AddInParameter(cmd, "@UserID", DbType.Guid, iUserID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                if (reader.Read())
                {
                    if (reader.GetInt32(0) > 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;


        }

        const string SQL_SELECT_ORDER_EXPORT = @"
SELECT
Order_Order.ID
,Order_Order.OrderNumber '订单号'
--,(SELECT COUNT(0) FROM Order_Product WHERE OID = Order_Order.ID) '订单件数'
,(SELECT NAME+N' ' FROM Order_Product WITH(NOLOCK) where Order_Product.OID = Order_Order.ID and Obj_Status = 1  FOR XML PATH('')) '内容'
,Order_Order.TotalAmount '总金额'
,Order_Order.PayAmount '已付金额'
,Order_Order.TotalAmount - Order_Order.PayAmount '应付金额'
,(select SUM(PayMoney) FROM Order_Payment WITH(NOLOCK) WHERE PayMoneyType = 1 AND OID = Order_Order.ID AND Obj_Status = 1) '余额'
,(select PayMoney FROM Order_Payment WITH(NOLOCK) WHERE PayMoneyType = 2 AND OID = Order_Order.ID AND Obj_Status = 1) '懒人卡'
,(select PayMoney FROM Order_Payment WITH(NOLOCK) WHERE PayMoneyType = 3 AND OID = Order_Order.ID AND Obj_Status = 1) '支付宝'
,(select PayMoney FROM Order_Payment WITH(NOLOCK) WHERE PayMoneyType = 5 AND OID = Order_Order.ID AND Obj_Status = 1) '微信支付'
,(select PayMoney FROM Order_Payment WITH(NOLOCK) WHERE PayMoneyType = 9 AND OID = Order_Order.ID AND Obj_Status = 1) '优惠券'
,(select PayMoney FROM Order_Payment WITH(NOLOCK) WHERE PayMoneyType = 0 AND OID = Order_Order.ID AND Obj_Status = 1)  '现金'
,(select PayMoney FROM Order_Payment WITH(NOLOCK) WHERE PayMoneyType = 100 AND OID = Order_Order.ID AND Obj_Status = 1)  '客服调整'
,User_Info.MPNo '下单用户手机'
,Exp_Order.Contacts '用户'
,Exp_Order.MPNo '手机'
,Exp_Order.Address '地址'
,Order_Order.ExpectTime '预计时间'
,Order_Order.Obj_Cdate '下单时间'
,Exp_Order.OperatorTime '取件完成时间'
,Order_Order.Step4Time '分拣完成时间'
,Order_Order.Step5Time '出库完成时间'
,Order_Order.AllFinishTime '全部完成时间'
,REPLACE(Order_Order.UserRemark,CHAR(10),'') '用户备注'
,REPLACE(Order_Order.CSRemark,CHAR(10),'') '客服备注'
--,Order_Order.SystemRemark '系统备注'
--,Order_Order.PrintRemark '备注'
,Order_Order.Step '步骤'
,CASE WHEN  Order_Order.Channel = 1 THEN '网站' WHEN  Order_Order.Channel = 4 THEN '微信' WHEN  Order_Order.Channel = 2 THEN 'APP' WHEN  Order_Order.Channel = 91 THEN '快递继续下单' WHEN  Order_Order.Channel = -1 THEN '客服' ELSE convert(varchar(2),Order_Order.Channel) end '渠道'
,Order_Order.GetExpressNumber '送件快递号'
,Order_Order.SendExpressNumber '取件快递号'
--select * from Exp_Preson_CommissionLog where UserID = 53
,ISNULL((select '是' from Exp_Preson_CommissionLog WITH(NOLOCK) WHERE CorrelationNo = Order_Order.OrderNumber AND ChangeType = 6 AND Obj_Status = 1),'') '是否当面单奖'
,ISNULL((select '是' from Exp_Preson_CommissionLog WITH(NOLOCK) WHERE CorrelationNo = Order_Order.OrderNumber AND ChangeType = 5 AND Obj_Status = 1),'') '新用户下单奖'
,AO.Name '承运人'
,Exp_Node.Name '站点'
,BO.Name '站长'
,Exp_Area.Name '区域名'
,CO.Name '区长'
,DATEDIFF(MINUTE,Exp_Order.ExpTime,Exp_Order.OperatorTime) '取件时常（分）'
,Order_Order.Step1Time '下单'
,Order_Order.Step2Time '取件中'
,Order_Order.Step3Time '送洗中'
,Order_Order.Step4Time '洗涤中'
,Order_Order.Step5Time '出库中'
,Order_Order.Step6Time '送件中'
,Order_Order.Step7Time '完成'
,ROW_NUMBER() OVER (ORDER BY Order_Order.ID) as ROWOA6DFN
FROM Order_Order
INNER JOIN User_Info WITH(NOLOCK) ON Order_Order.UserID = User_Info.ID
INNER JOIN Exp_Order WITH(NOLOCK) ON Order_Order.OrderNumber = Exp_Order.OutNumber AND Exp_Order.TransportType = 1
INNER JOIN Exp_Node WITH(NOLOCK) ON Exp_Order.NodeID = Exp_Node.ID
INNER JOIN Exp_Area WITH(NOLOCK) ON Exp_Node.AreaID = Exp_Area.ID
INNER JOIN PR_Operator AO WITH(NOLOCK) ON Exp_Order.OperatorID = AO.ID
INNER JOIN PR_Operator BO WITH(NOLOCK) ON Exp_Node.ManagerID = BO.ID
INNER JOIN PR_Operator CO WITH(NOLOCK) ON Exp_Area.ManagerID = CO.ID
WHERE OrderStatus = 2
AND Order_Order.TotalAmount > 0
AND Order_Order.Obj_Status = 1
AND Order_Order.TotalAmount != 0.1
AND Order_Order.Obj_Cuser >= 0
";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iKeyword"></param>
        /// <param name="iCreateStartDate"></param>
        /// <param name="iCreateEndDate"></param>
        /// <param name="iFinishStartDate"></param>
        /// <param name="iFinishEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public IList<string[]> Order_Order_Export(
            string iKeyword,
            DateTime? iCreateStartDate, DateTime? iCreateEndDate,
            DateTime? iFinishStartDate, DateTime? iFinishEndDate,
            int iPageIndex, int iPageSize)
        {
            var rtn = new List<string[]>();

            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
            StringBuilder sql = new StringBuilder();

            int startIndex = (iPageIndex - 1) * iPageSize + 1;
            int endIndex = iPageIndex * iPageSize;

            sql.Append(@"SELECT * FROM ( ");

            sql.Append(SQL_SELECT_ORDER_EXPORT);

            if (iCreateStartDate.HasValue)
                sql.Append(" AND Order_Order.Obj_Cdate >= @CreateStartDate");

            if (iCreateEndDate.HasValue)
                sql.Append(" AND Order_Order.Obj_Cdate < @CreateEndDate");

            if (iFinishStartDate.HasValue)
                sql.Append(" AND Order_Order.AllFinishTime >= @FinishStartDate");

            if (iFinishEndDate.HasValue)
                sql.Append(" AND Order_Order.AllFinishTime < @FinishEndDate");

            sql.Append(") as TempB4D6KU");
            sql.Append(" WHERE ROWOA6DFN BETWEEN " + startIndex + " AND " + endIndex);

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            //主键
            db.AddInParameter(cmd, "@CreateStartDate", DbType.DateTime, iCreateStartDate);
            db.AddInParameter(cmd, "@CreateEndDate", DbType.DateTime, iCreateEndDate);
            db.AddInParameter(cmd, "@FinishStartDate", DbType.DateTime, iFinishStartDate);
            db.AddInParameter(cmd, "@FinishEndDate", DbType.DateTime, iFinishEndDate);

            if (iPageIndex == 1)
            {
                rtn.Add(
                    new string[]
                    {
                        "ID","订单号","内容","总金额","已付金额","应付金额","余额","懒人卡","支付宝","微信支付",
                        "优惠券","现金","客服调整","下单用户手机","用户","手机","地址","预计时间","下单时间","取件完成时间","分拣完成时间",
                        "出库完成时间","全部完成时间","用户备注","客服备注","步骤","渠道","送件快递号","取件快递号","是否当面单奖","新用户下单奖",
                        "承运人","站点","站长","区域名","区长","取件时长（分）","下单","取件中","送洗中","洗涤中",
                        "出库中","送件中","完成"
                    });
            }

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    //var tmp = new string[43];

                    rtn.Add(
                    new string[]
                    {
                        reader[0].ToString(),reader[1].ToString(),reader[2].ToString(),reader[3].ToString(),reader[4].ToString(),
                        reader[5].ToString(),reader[6].ToString(),reader[7].ToString(),reader[8].ToString(),reader[9].ToString(),

                        reader[10].ToString(),reader[11].ToString(),reader[12].ToString(),reader[13].ToString(),reader[14].ToString(),
                        reader[15].ToString(),reader[16].ToString(),reader[17].ToString(),reader[18].ToString(),reader[19].ToString(),
                    
                        reader[20].ToString(),reader[21].ToString(),reader[22].ToString(),reader[23].ToString(),reader[24].ToString(),
                        reader[25].ToString(),reader[26].ToString(),reader[27].ToString(),reader[28].ToString(),reader[29].ToString(),

                        reader[30].ToString(),reader[31].ToString(),reader[32].ToString(),reader[33].ToString(),reader[34].ToString(),
                        reader[35].ToString(),reader[36].ToString(),reader[37].ToString(),reader[38].ToString(),reader[39].ToString(),

                        reader[40].ToString(),reader[41].ToString(),reader[42].ToString(),reader[43].ToString(),
                    
                    });
                }
            }

            if (rtn.Count == 1 && iPageIndex == 1)
            {
                //无数据
                rtn.Clear();
            }

            return rtn;
        }

        public DataTable Order_PayRecord_Select(string iOrderNumber, int? payType, int? Status)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("SELECT  * FROM pay_record ");

            sql.Append(" WHERE orderNumber = '" + iOrderNumber + "'");

            if (payType.HasValue)
                sql.Append(" AND payType = " + payType.Value);

            if (Status.HasValue)
                sql.Append(" AND payStatus = " + Status.Value);
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            return db.ExecuteDataSet(cmd).Tables[0];
        }

        public bool Order_PayRecord_Update(string iOrderNumber, int payType, int Status)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE pay_record SET payStatus = @Status WHERE orderNumber = @iOrderNumber and payType=@payType");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@iOrderNumber", DbType.String, iOrderNumber);
            db.AddInParameter(cmd, "@payType", DbType.Int32, payType);
            db.AddInParameter(cmd, "@Status", DbType.Int32, Status);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        public bool Order_PayAmount_Update(int iOrderID, decimal PayAmount)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append("UPDATE Order_Order SET PayAmount = @PayAmount WHERE ID = @iOrderID ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //
            db.AddInParameter(cmd, "@iOrderID", DbType.Int32, iOrderID);
            db.AddInParameter(cmd, "@PayAmount", DbType.Decimal, PayAmount);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }
    }
}
