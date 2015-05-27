using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.Common.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Portal
{
    public partial class CommonPortal : IPR
    {
        protected LazyAtHome.WCF.Common.Business.Business.PR PRInstance = LazyAtHome.WCF.Common.Business.Business.PR.Instance;

        public ReturnEntity<int> PR_Operator_ADD(OperatorDC iOperatorDC)
        {
            Log_DeBug("PR_Operator_ADD", iOperatorDC);
            try
            {
                return PRInstance.PR_Operator_ADD(iOperatorDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_ADD");
            }
        }

        public ReturnEntity<bool> PR_Operator_UPDATE(OperatorDC iEntity)
        {
            Log_DeBug("PR_Operator_UPDATE", iEntity);
            try
            {
                return PRInstance.PR_Operator_UPDATE(iEntity);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_UPDATE");
            }
        }

        /// <summary>
        /// 上岗下岗
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOnDuty"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty)
        {
            Log_DeBug("PR_Operator_UPDATE_OnDuty", iOperatorID, iOnDuty);
            try
            {
                return PRInstance.PR_Operator_UPDATE_OnDuty(iOperatorID, iOnDuty);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_UPDATE_OnDuty", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_UPDATE_OnDuty");
            }
        }

        public ReturnEntity<bool> PR_Operator_UPDATE_Password(int iOperatorID, string iNewPassword, int iMuser)
        {
            Log_DeBug("PR_Operator_UPDATE_Password", iOperatorID, iNewPassword, iMuser);
            try
            {
                return PRInstance.PR_Operator_UPDATE_Password(iOperatorID, iNewPassword, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_UPDATE_Password", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_UPDATE_Password");
            }
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword)
        {
            Log_DeBug("PR_Operator_UPDATE_Password", iOperatorID, iOldPassword, iNewPassword);
            try
            {
                return PRInstance.PR_Operator_UPDATE_Password(iOperatorID, iOldPassword, iNewPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_UPDATE_Password", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_UPDATE_Password");
            }
        }

        public ReturnEntity<OperatorDC> PR_Operator_Login(string iLoginName, string iPassword, OperatorType iOperatorType)
        {
            Log_DeBug("PR_Operator_Login", iLoginName, iPassword, iOperatorType);
            try
            {
                return PRInstance.PR_Operator_Login(iLoginName, iPassword, iOperatorType);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_Login", ex);
                return new ReturnEntity<OperatorDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_Login");
            }
        }

        public ReturnEntity<RecordCountEntity<OperatorDC>> PR_Operator_SELECT_List(string iName, string iMpNo,
            string iEmail, int? iEnableType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize,
            int? iNodeID = null, string iKeyword = null)
        {
            Log_DeBug("PR_Operator_SELECT_List", iName, iMpNo, iEmail, iEnableType, iStartDate, iEndDate,
                iPageIndex, iPageSize, iNodeID, iKeyword);
            try
            {
                return PRInstance.PR_Operator_SELECT_List(iName, iMpNo, iEmail, iEnableType, iStartDate, iEndDate,
                    iPageIndex, iPageSize, iNodeID, iKeyword);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<OperatorDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_SELECT_List");
            }
        }

        public ReturnEntity<OperatorDC> PR_Operator_SELECT_BYID(int iID)
        {
            Log_DeBug("PR_Operator_SELECT_BYID", iID);
            try
            {
                return PRInstance.PR_Operator_SELECT_BYID(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_Operator_SELECT_BYID", ex);
                return new ReturnEntity<OperatorDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_Operator_SELECT_BYID");
            }
        }

        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        public ReturnEntity<bool> PR_OperatorLog_ADD(int iType, int iOperatorID, string iOperatorName, string iLogContent)
        {
            Log_DeBug("PR_OperatorLog_ADD", iType, iOperatorID, iOperatorName, iLogContent);
            try
            {
                return PRInstance.PR_OperatorLog_ADD(iType, iOperatorID, iOperatorName, iLogContent);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_OperatorLog_ADD", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_OperatorLog_ADD");
            }
        }

        /// <summary>
        /// 查询日志列表
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<OperatorLogDC>> PR_OperatorLog_SELECT_List(int? iType, string iOperatorName,
            string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("PR_OperatorLog_SELECT_List", iType, iOperatorName,
                    iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return PRInstance.PR_OperatorLog_SELECT_List(iType, iOperatorName,
                    iLogContent, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("PR_OperatorLog_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<OperatorLogDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("PR_OperatorLog_SELECT_List");
            }
        }

    }
}
