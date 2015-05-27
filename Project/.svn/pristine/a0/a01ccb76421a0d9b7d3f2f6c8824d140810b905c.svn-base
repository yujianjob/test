using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Interface.IDAL
{
    public interface IPRDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iPR_OperatorDC"></param>
        /// <returns></returns>
        int PR_Operator_ADD(OperatorDC iPR_OperatorDC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iNodeID"></param>
        /// <returns></returns>
        OperatorDC PR_Operator_SELECT_BYNodeManagerID(int iNodeID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        OperatorDC PR_Operator_SELECT_BYID(int ID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <param name="iOperatorType"></param>
        /// <returns></returns>
        OperatorDC PR_Operator_SELECT_Entity(string iLoginName, string iPassword, OperatorType iOperatorType);

        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="iName"></param>
        /// <param name="iMpNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iEnableType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<OperatorDC> PR_Operator_SELECT_List(string iName, string iMpNo, string iEmail,
            int? iEnableType, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize,
            int? NodeID, string iKeyword);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<OperatorDC> PR_Operator_SELECT_List_ALL();

        /// <summary>								
        /// 检查登录名是否重复				
        /// </summary>								
        /// <param name="iLoginName">登录名</param>	
        /// <param name="iID">用户ID</param>
        /// <returns></returns>	
        bool PR_Operator_HasLoginName(int iID, string iLoginName);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="iPR_OperatorDC"></param>
        /// <returns></returns>
        bool PR_Operator_UPDATE(OperatorDC iOperatorDC);

        /// <summary>
        /// 上岗下岗
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOnDuty"></param>
        /// <returns></returns>
        bool PR_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="iOperatorDC"></param>
        /// <returns></returns>
        bool PR_Operator_UPDATE_Password(int iOperatorID, string iNewPassword, int iMuser);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        bool PR_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword);

        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iPR_OperatorLogDC"></param>
        /// <returns></returns>
        int PR_OperatorLog_ADD(OperatorLogDC iPR_OperatorLogDC);

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
        RecordCountEntity<OperatorLogDC> PR_OperatorLog_SELECT_List(int? iType, string iOperatorName,
            string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);
    }
}
