using LazyAtHome.Core.Enumerate;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Express.Contract.DataContract;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.DAL
{
    public class AppExpressDAL : DALBase, LazyAtHome.WCF.Express.Interface.IDAL.IAppExpressDAL
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public AppExpressDAL()
        {
            APPModule = ApplicationModule.Common;
        }

        //        /// <summary>
        //        /// 
        //        /// </summary>
        //        /// <param name="iLoginName"></param>
        //        /// <param name="iLoginPassword"></param>
        //        /// <returns></returns>
        //        public Exp_OperatorDC Exp_Operator_SELECT_Entity(string iLoginName, string iLoginPassword)
        //        {
        //            Exp_OperatorDC entity = null;
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);
        //            StringBuilder sql = new StringBuilder();
        //            sql.Append(@"SELECT * FROM Exp_Operator
        //                            WHERE LoginName = @LoginName
        //                            AND LoginPwd = @LoginPwd
        //                            AND Obj_Status = 1 ");
        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

        //            //登录名
        //            db.AddInParameter(cmd, "@LoginName", DbType.String, iLoginName);
        //            //密码
        //            db.AddInParameter(cmd, "@LoginPwd", DbType.String, iLoginPassword);

        //            using (IDataReader reader = db.ExecuteReader(cmd))
        //            {
        //                if (reader.Read())
        //                {
        //                    entity = Exp_OperatorDC.GetEntity(reader);
        //                }
        //            }
        //            return entity;
        //        }

        //        /// <summary>
        //        /// 上岗下岗
        //        /// </summary>
        //        /// <param name="iOperatorID"></param>
        //        /// <param name="iOnDuty"></param>
        //        /// <returns></returns>
        //        public bool Exp_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty)
        //        {
        //            StringBuilder sql = new StringBuilder();
        //            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

        //            sql.Append(@"
        //            INSERT INTO Exp_Operator_DutyLog(OperatorID,DutyStatus) VALUES(@ID,@OnDuty)
        //            UPDATE Exp_Operator SET OnDuty = @OnDuty WHERE ID = @ID
        //                        ");

        //            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
        //            //主键
        //            db.AddInParameter(cmd, "@ID", DbType.Int32, iOperatorID);
        //            //
        //            db.AddInParameter(cmd, "@OnDuty", DbType.Int32, iOnDuty);
        //            //添加共通参数
        //            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        //        }

        /// <summary>
        /// 待接列表
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <returns></returns>
        public IList<Exp_OrderDC> Exp_Order_SELECT_WaitAccept(int iOperatorID)
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            Exp_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT TOP 50 * FROM Exp_Order WHERE Obj_Status = 1 AND TransportType = 1 AND Step = 0 
                AND EXISTS (SELECT 1 FROM Exp_Order_Operator_Temp WHERE Obj_Status = 1 AND OrderID = Exp_Order.ID AND OperatorID = @OperatorID)
                AND DATEDIFF(DAY,GETDATE(),ExpTime) <= 0
                ORDER BY ExpTime DESC
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Exp_OrderDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        //接单
        public bool Exp_Order_Accept(int iOrderID, string iOutNumber, int iOperatorID, int iNodeID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order SET OperatorID = @OperatorID, NodeID = @NodeID, Step = 1, TakeTime = GETDATE(), Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND Step = 0 AND TransportType = 1 AND ID = @OrderID
UPDATE Exp_Order SET OperatorID = @OperatorID, NodeID = @NodeID, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND Step = 0 AND TransportType = 2 AND OutNumber = @OutNumber
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND OrderID = @OrderID
");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            db.AddInParameter(cmd, "@NodeID", DbType.Int32, iNodeID);
            //添加共通参数
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        //拒接单
        public int Exp_Order_UnAccept(int iOrderID, int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order_Operator_Temp SET Obj_Status = 6, Obj_Mdate = GETDATE() WHERE Obj_Status = 1 AND OrderID = @OrderID AND OperatorID = @OperatorID
IF((SELECT COUNT(0) FROM Exp_Order_Operator_Temp WHERE OrderID = @OrderID AND Obj_Status = 1) = 0)
BEGIN
    UPDATE Exp_Order SET AllotStatus = 2,SystemRemark = '拒接' WHERE ID = @OrderID
END
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            db.ExecuteNonQuery(cmd);

            if (Exp_Order_UnAccept_Count(iOrderID) == 0)
            {
                return -1;
            }

            return 1;
        }


        //拒接单
        public int Exp_Order_UnAccept_Count(int iOrderID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"SELECT COUNT(0) FROM Exp_Order_Operator_Temp WHERE OrderID = @OrderID AND Obj_Status = 1");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    return reader.GetInt32(0);
                }
            }

            return 0;
        }

        //待收件列表
        public IList<Exp_OrderDC> Exp_Order_SELECT_WaitTake(int iOperatorID)
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            Exp_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT TOP 300 * FROM Exp_Order WHERE Obj_Status = 1 AND TransportType = 1 AND Step = 1 AND OperatorID = @OperatorID
                AND DATEDIFF(DAY,GETDATE(),ExpTime) <= 0 AND DATEDIFF(DAY,GETDATE(),ExpTime) >= -15
                ORDER BY ExpTime DESC");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Exp_OrderDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        //收件完成
        public bool Exp_Order_TakeComplete(int iOrderID, string iExpNumber, int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order SET Step = 2, ExpNumber = @ExpNumber, OperatorTime = GETDATE() 
    WHERE Obj_Status = 1 AND Step = 1 AND ID = @OrderID AND OperatorID = @OperatorID
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            db.AddInParameter(cmd, "@ExpNumber", DbType.String, iExpNumber);

            //添加共通参数
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        //收件失败
        public bool Exp_Order_TakeFail(int iOrderID, int iOperatorID, string iFailReason)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order SET WaitProcess = 1, Obj_Mdate = GETDATE(), StepRemark = @StepRemark
    WHERE Step = 1 AND Obj_Status = 1 AND ID = @OrderID AND OperatorID = @OperatorID
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            db.AddInParameter(cmd, "@StepRemark", DbType.String, iFailReason);
            //添加共通参数
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        //扫描揽件
        public bool Exp_Order_TakeSend(string iOutNumber, int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order SET Step = 4, OperatorID = @OperatorID, OperatorTime = GETDATE() 
WHERE Obj_Status = 1 AND TransportType = 2 AND OutNumber = @OutNumber
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OutNumber", DbType.String, iOutNumber);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        //待送件列表
        public IList<Exp_OrderDC> Exp_Order_SELECT_WaitSend(int iOperatorID)
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            Exp_OrderDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append(
@"SELECT TOP 50 *, 
(SELECT COUNT(0) FROM Exp_StorageItem WHERE Number = Exp_Order.OutNumber AND Obj_Status = 1 AND ItemType = 2 AND TargetType = 8) AS SendCount
FROM Exp_Order WHERE Obj_Status = 1 
AND TransportType = 2 AND OperatorID = @OperatorID
AND EXISTS (SELECT 1 FROM Exp_StorageItem WHERE Number = Exp_Order.OutNumber AND Obj_Status = 1 AND ItemType = 2 AND TargetType = 8)
");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Exp_OrderDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }

        //送件完成
        public bool Exp_Order_SendComplete(int iOrderID, int iOperatorID)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);

            sql.Append(@"
UPDATE Exp_Order SET Step = 90, CompleteTime = GETDATE() WHERE Obj_Status = 1 AND Step IN (1,2,3,4) AND ID = @OrderID AND OperatorID = @OperatorID AND TransportType = 2
            ");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@OrderID", DbType.Int32, iOrderID);

            db.AddInParameter(cmd, "@OperatorID", DbType.Int32, iOperatorID);

            //添加共通参数
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 电话联系用户
        /// </summary>
        /// <param name="iOrderID"></param>
        /// <param name="iStatus"></param>
        /// <param name="iCallTime"></param>
        /// <param name="iSecond"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public bool Exp_Order_UPDATE_CallUser(int iOrderID, int iStatus, DateTime iCallTime, int iSecond, int iMuser)
        {
            StringBuilder sql = new StringBuilder();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Write);
            sql.Append("UPDATE Exp_Order SET CallUserStatus = @CallUserStatus, CallUserTime = @CallUserTime, CallUserSecond = @CallUserSecond, Obj_Muser = @Muser, Obj_Mdate = getdate() WHERE ID = @ID AND CallUserStatus = 0");
            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());
            //主键
            db.AddInParameter(cmd, "@ID", DbType.Int32, iOrderID);
            db.AddInParameter(cmd, "@CallUserStatus", DbType.Int32, iStatus);
            db.AddInParameter(cmd, "@CallUserTime", DbType.DateTime, iCallTime);
            db.AddInParameter(cmd, "@CallUserSecond", DbType.Int32, iSecond);
            //操作人
            db.AddInParameter(cmd, "@Muser", DbType.Int32, iMuser);
            return db.ExecuteNonQuery(cmd) > 0 ? true : false;
        }

        /// <summary>
        /// 获取干线下的站点
        /// </summary>
        /// <param name="iLineID"></param>
        /// <returns></returns>
        public IList<Exp_NodeDC> Exp_Node_LineSite(int iLineID)
        {
            List<Exp_NodeDC> list = new List<Exp_NodeDC>();
            Exp_NodeDC entity = null;
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT TOP 300 * FROM Exp_Node WHERE Obj_Status = 1 AND Enable = 1 AND Type = 1 AND ParentID = @ParentID");

            DbCommand cmd = db.GetSqlStringCommand(sql.ToString());

            db.AddInParameter(cmd, "@ParentID", DbType.Int32, iLineID);

            using (IDataReader reader = db.ExecuteReader(cmd))
            {
                while (reader.Read())
                {
                    entity = Exp_NodeDC.GetEntity(reader);
                    if (entity != null)
                        list.Add(entity);
                }
            }

            return list;
        }



        /// <summary>
        /// 仓库搜索
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iName"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public RecordCountEntity<Exp_OrderDC> Exp_Order_SELECT_List_Log(
            int iTransportType, int? iNodeID, int? iLineID,
            int iDateType,
            DateTime  iStartDate, DateTime  iEndDate,
            int iPageIndex, int iPageSize)
        {
            List<Exp_OrderDC> list = new List<Exp_OrderDC>();
            Database db = DBHelper.CreateDataBase(APPModule, LazyAtHome.Core.Enumerate.DataAccessPatterns.Query);

            StringBuilder sqlfields = new StringBuilder();
            StringBuilder sqlwhere = new StringBuilder();
            StringBuilder sqlorder = new StringBuilder();
            sqlwhere.Append(@" Exp_Order.Obj_Status = 1 ");
            sqlorder.Append(@" Exp_Order.ExpTime DESC ");

            sqlfields.Append("Exp_Order.*, PR_Operator.Name AS OperatorName, Exp_Node.Name AS NodeName");

            sqlwhere.Append(" AND Exp_Order.TransportType = '" + iTransportType + "' ");

            if (iNodeID.HasValue)
                sqlwhere.Append(" AND Exp_Order.NodeID = '" + iNodeID + "' ");

            if (iLineID.HasValue)
                sqlwhere.Append(" AND Exp_Order.NodeID IN( SELECT ID FROM Exp_Node WHERE Type = 1 AND Obj_Status = 1 AND ParentID = '" + iLineID + "') ");

            switch (iDateType)
            {
                case 1://绑定物流条码时间
                    sqlwhere.Append(" AND EXISTS (SELECT 1 FROM Exp_StorageItem WHERE Number = Exp_Order.OutNumber AND ItemType = 1 AND Obj_Status = 1 AND TargetTime1 >= '" + iStartDate.ToString("yyyy-MM-dd HH:mm:ss") + "' AND TargetTime1 <= '" + iEndDate.ToString("yyyy-MM-dd HH:mm:ss") + "' ) ");

                    break;



                default:
                    sqlwhere.Append(" AND 1 = 2 ");
                    break;
            }


            //            //物流单号
            //            if (!string.IsNullOrWhiteSpace(iExpNumber))

            //            //物流单号
            //            if (!string.IsNullOrWhiteSpace(iOutNumber))
            //                sqlwhere.Append(" AND Exp_Order.OutNumber = '" + iOutNumber + "' ");

            //            //运输类型
            //            if (iTransportType.HasValue)
            //                sqlwhere.Append(" AND Exp_Order.TransportType = '" + iTransportType + "' ");

            //            if (iStep.HasValue)
            //                sqlwhere.Append(" AND Exp_Order.Step = '" + iStep + "' ");

            //            //用户地址
            //            if (!string.IsNullOrWhiteSpace(iAddress))
            //                sqlwhere.Append(" AND Exp_Order.Address LIKE '%" + DBHelper.SqlLikeReplaceForParam(iAddress) + "%' ");
            //            //用户联系人
            //            if (!string.IsNullOrWhiteSpace(iContacts))
            //                sqlwhere.Append(" AND Exp_Order.Contacts LIKE '%" + DBHelper.SqlLikeReplaceForParam(iContacts) + "%' ");
            //            //用户手机
            //            if (!string.IsNullOrWhiteSpace(iMpno))
            //                sqlwhere.Append(" AND Exp_Order.Mpno LIKE '%" + DBHelper.SqlLikeReplaceForParam(iMpno) + "%' ");

            //            if (!string.IsNullOrWhiteSpace(iKeyword))
            //                sqlwhere.Append(@"
            //                    AND (PR_Operator.MpNo LIKE '%" + DBHelper.SqlLikeReplaceForParam(iKeyword) + @"%' 
            //                        OR PR_Operator.Name LIKE '%" + DBHelper.SqlLikeReplaceForParam(iKeyword) + @"%' 
            //                    )");

            ////创建时间开始
            //if (iStartDate.HasValue)
            //    sqlwhere.Append(" AND Exp_Order.Obj_Mdate >= '" + iStartDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");
            ////创建时间结束
            //if (iEndDate.HasValue)
            //    sqlwhere.Append(" AND Exp_Order.Obj_Mdate <= '" + iEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' ");

            var rtn = new RecordCountEntity<Exp_OrderDC>();

            //取得总数量
            rtn.RecordCount = ExecuteReaderRecordCountByPageIndex(@"Exp_Order LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule);
            //取得列表
            using (IDataReader reader = ExecuteReaderPaginationByPageIndex(@"Exp_Order LEFT JOIN PR_Operator ON Exp_Order.OperatorID = PR_Operator.ID LEFT JOIN Exp_Node ON Exp_Order.NodeID = Exp_Node.ID"
                , "Exp_Order.ID", sqlfields.ToString(), sqlwhere.ToString(), sqlorder.ToString(), null, iPageIndex, iPageSize, APPModule))
            {
                while (reader.Read())
                {
                    var entity = Exp_OrderDC.GetEntity(reader);

                    if (entity != null)
                    {
                        list.Add(entity);
                    }
                }
            }
            rtn.ReturnList = list;
            return rtn;
        }
    }
}
