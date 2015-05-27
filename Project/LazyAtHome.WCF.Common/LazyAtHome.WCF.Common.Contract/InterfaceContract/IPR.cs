using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IPR
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iOperatorDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> PR_Operator_ADD(OperatorDC iOperatorDC);

        /// <summary>
        /// 编辑用户登录信息
        /// </summary>
        /// <param name="iEntity"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> PR_Operator_UPDATE(OperatorDC iEntity);

        /// <summary>
        /// 上岗下岗
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOnDuty"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> PR_Operator_UPDATE_OnDuty(int iOperatorID, int iOnDuty);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iNewPassword"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        [OperationContract(Name = "PR_Operator_UPDATE_Password_Reset")]
        ReturnEntity<bool> PR_Operator_UPDATE_Password(int iOperatorID, string iNewPassword, int iMuser);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="iOperatorID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> PR_Operator_UPDATE_Password(int iOperatorID, string iOldPassword, string iNewPassword);

        /// <summary>								
        /// 操作员登录						
        /// </summary>								
        /// <param name="iLoginName">登录名</param>								
        /// <param name="iPassword">密码</param>								
        /// <param name="iOperatorType"></param>								
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<OperatorDC> PR_Operator_Login(string iLoginName, string iPassword, OperatorType iOperatorType);

        /// <summary>								
        /// 获取人员列表								
        /// </summary>								
        /// <param name="iName">名称</param>	
        /// <param name="iMpNo">手机</param>
        /// <param name="iEmail">邮箱</param>
        /// <param name="iEnableType">登陆状态</param>
        /// <param name="oInfo">分页信息</param>
        /// <returns></returns>								
        [OperationContract]
        ReturnEntity<RecordCountEntity<OperatorDC>> PR_Operator_SELECT_List(string iName, string iMpNo,
            string iEmail, int? iEnableType, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize, int? iNodeID = null, string iKeyword = null);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iVUL_ID">PRL_ID</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<OperatorDC> PR_Operator_SELECT_BYID(int iID);

        /// <summary>
        /// 新增操作员日志
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iOperatorID"></param>
        /// <param name="iOperatorName"></param>
        /// <param name="iLogContent"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> PR_OperatorLog_ADD(int iType, int iOperatorID, string iOperatorName, string iLogContent);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<OperatorLogDC>> PR_OperatorLog_SELECT_List(int? iType, string iOperatorName,
            string iLogContent, DateTime iStartDate, DateTime iEndDate, int iPageIndex, int iPageSize);


    }
}
