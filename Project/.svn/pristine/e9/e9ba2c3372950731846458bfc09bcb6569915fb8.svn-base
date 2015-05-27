using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using LazyAtHome.Core.Infrastructure.WorkQueue;
using LazyAtHome.Service.WorkQueue.Contract.DataContract.Notify;

namespace LazyAtHome.Service.WorkQueue.Plugin.Notify
{
    public class NotifyPlugin : PluginBase
    {
        public NotifyPlugin()
        {
            TypeName = "Notify";
        }

        public override void Execute(IQueueItem item)
        {
            base.Execute(item);

            NotifyMessageItem msg = (NotifyMessageItem)item;
            Console.WriteLine(item.ToString());
            InsertData(msg);

            switch (msg.Class)
            {
                case 9001:
                    //用户5分钟内联系用户
                    Express_OperatorAddMoneyByFiveMin(msg.PersonnelID, msg.OrderNumber);
                    break;
                default:
                    Console.WriteLine("无特殊处理");
                    break;
            }
        }

        private const string PARAM_OperatorLog_OperatorID = "@OperatorID";
        private const string PARAM_OperatorLog_OperatorName = "@OperatorName";
        private const string PARAM_OperatorLog_Type = "@Type";
        private const string PARAM_OperatorLog_LogContent = "@LogContent";

        private void InsertData(NotifyMessageItem dataItem)
        {
            string strSql = "INSERT INTO [dbo].[Base_Notify] ([EventNumber],[OrderNumber],[RoleID],[PersonnelID],[Class],[Title],[Content],[Level],[Status],[NotifyUserList],[IsSms],[IsEmail],[Obj_Status],[Obj_Remark],[Obj_Cuser])"
                + " VALUES (@EventNumber,@OrderNumber,@RoleID,@PersonnelID,@Class,@Title,@Content,@Level,@Status,@NotifyUserList,@IsSms,@IsEmail,@Obj_Status,@Obj_Remark,@Obj_Cuser)";

            DbCommand cmd = new SqlCommand();
            cmd.CommandText = strSql;
            cmd.CommandType = CommandType.Text;

            SqlParameter[] paras = {
                        new SqlParameter("@EventNumber", SqlDbType.VarChar, 50),
                        new SqlParameter("@OrderNumber", SqlDbType.VarChar, 50),                        
                        new SqlParameter("@RoleID", SqlDbType.Int),
                        new SqlParameter("@PersonnelID", SqlDbType.Int),
                        new SqlParameter("@Class", SqlDbType.Int),
                        new SqlParameter("@Title", SqlDbType.NVarChar,200),
                        new SqlParameter("@Content", SqlDbType.NVarChar, 2000),
                        new SqlParameter("@Level", SqlDbType.Int),
                        new SqlParameter("@Status", SqlDbType.Int),
                        new SqlParameter("@NotifyUserList", SqlDbType.VarChar,50),
                        new SqlParameter("@IsSms", SqlDbType.Bit),
                        new SqlParameter("@IsEmail", SqlDbType.Bit),
                        new SqlParameter("@Obj_Status", SqlDbType.TinyInt),
                        new SqlParameter("@Obj_Remark", SqlDbType.NVarChar,2000),
                        new SqlParameter("@Obj_Cuser", SqlDbType.Int)};

            paras[0].Value = dataItem.EventNumber;
            if (string.IsNullOrWhiteSpace(dataItem.OrderNumber))
                paras[1].Value = DBNull.Value;
            else
                paras[1].Value = dataItem.OrderNumber;
            paras[2].Value = dataItem.RoleID;
            paras[3].Value = dataItem.PersonnelID;
            paras[4].Value = dataItem.Class;
            paras[5].Value = dataItem.Title;
            if (string.IsNullOrWhiteSpace(dataItem.Content))
                paras[6].Value = DBNull.Value;
            else
                paras[6].Value = dataItem.Content;
            paras[7].Value = dataItem.Level;
            paras[8].Value = dataItem.Status;
            if (dataItem.NotifyUserList == null || dataItem.NotifyUserList.Count == 0)
                paras[9].Value = DBNull.Value;
            else
                paras[9].Value = string.Join(",", dataItem.NotifyUserList);
            paras[10].Value = dataItem.IsSms;
            paras[11].Value = dataItem.IsEmail;
            paras[12].Value = dataItem.Obj_Status;
            if (string.IsNullOrWhiteSpace(dataItem.Obj_Remark))
                paras[13].Value = DBNull.Value;
            else
                paras[13].Value = dataItem.Obj_Remark;
            paras[14].Value = dataItem.Obj_Cuser;

            cmd.Parameters.AddRange(paras);

            _SqlCommandOperation_NonQuery(TypeName, cmd);

            cmd.Parameters.Clear();
            if (dataItem.IsSms)
            {                
                strSql = "select MpNo from PR_Operator where ID = " + dataItem.PersonnelID;
                cmd.CommandText = strSql;
                var strSendMpno = _SqlCommandOperation_Scalar(TypeName, cmd);
                if (strSendMpno != null)
                {
                    strSql = "Insert Into Base_SmsSend (Mobile, Content) Values ('{0}', '{1}')";
                    cmd.CommandText = string.Format(strSql, strSendMpno, dataItem.Title + "," + dataItem.Content + ",订单号:" + dataItem.OrderNumber);
                    _SqlCommandOperation_NonQuery(TypeName, cmd);
                }
            }
        }

        public void Express_OperatorAddMoneyByFiveMin(int userID, string orderNum)
        {
            ArrayList arrSqlList = new ArrayList();
            string strSql = "";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "Select Commission From Exp_Preson_Account Where UserID = " + userID;
            var tmpRtn1 = _SqlCommandOperation_Scalar(TypeName, cmd);
            if (tmpRtn1 == null)
            {
                Console.WriteLine("为用户增加5分钟电话回访奖励失败，未找到用户：" + userID);
                return;
            }
            decimal _OperatorMoney = Convert.ToDecimal(tmpRtn1);

            strSql = "Insert Into Exp_Preson_CommissionLog (UserID, ChangeType, BeforeValue, ChangeValue, AfterValue, CorrelationNo, Obj_Remark) Values ({0}, {1}, {2}, {3}, {4}, '{5}', '{6}')";
            strSql = string.Format(strSql, userID, 1, _OperatorMoney, 1, _OperatorMoney + 1, orderNum, "用户5分钟回访奖励");
            arrSqlList.Add(strSql);
            strSql = "Update Exp_Preson_Account Set Commission = Commission + " + 1 + " Where UserID = " + userID;
            arrSqlList.Add(strSql);

            if (!_SqlCommandOperation_Transaction(TypeName, arrSqlList))
                Console.WriteLine("为用户增加5分钟电话回访奖励失败，执行数据库更新失败：" + userID);
        }
    }
}