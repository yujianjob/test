using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Infrastructure.WCF.Service;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using LazyAtHome.WCF.UserSystem.Contract.InterfaceContract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Business.Portal
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.PerSession)]
    public partial class UserPortal : ServiceBase, IUser
    {
        protected void Log_Fatal(string iMethodName, Exception iException)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Red;
            //VEBS.Core.Helper.LogHelper.Log_Fatal("调用异常 " + iMethodName, 99, iException.Message + "\r\n" + GetFrameString(iException));
            Console.WriteLine("调用异常 " + iMethodName + 99 + iException.Message + "\r\n" + GetFrameString(iException));
            Console.ResetColor();
        }

        protected void Log_Warn(string iMethodName, string iMessage)
        {
            //VEBS.Core.Helper.LogHelper.Log_Info(iMethodName, 99, iMessage);
        }

        /// <summary>
        /// 获取异常堆栈
        /// </summary>
        /// <param name="iException"></param>
        /// <returns></returns>
        public static string GetFrameString(Exception iException)
        {
            bool flag = true;
            Hashtable rtn = new Hashtable();
            try
            {
                System.Diagnostics.StackTrace trace = new System.Diagnostics.StackTrace(iException);
                foreach (System.Diagnostics.StackFrame frame in trace.GetFrames())
                {
                    if (flag)
                    {
                        flag = false;
                        continue;
                    }
                    if (frame.GetMethod().IsPublic)
                    {
                        //方法名
                        var methodName = frame.GetMethod().Name;

                        //类名
                        var tmpString = string.Empty;
                        if (frame.GetMethod().DeclaringType != null)
                        {
                            //方法名
                            tmpString = frame.GetMethod().DeclaringType.FullName + ".";
                            if (tmpString.StartsWith("System."))
                            {
                                //如果是系统级的，直接跳过
                                continue;
                            }
                        }
                        tmpString += methodName;
                        if (!rtn.ContainsKey(tmpString))
                            rtn.Add(tmpString, null);
                    }
                }
            }
            catch { }
            var rtnstring = "\n";
            foreach (var item in rtn.Keys)
            {
                rtnstring += item + "\n";
            }
            return rtnstring;
        }

        /// <summary>
        /// 调用日志
        /// </summary>
        /// <param name="iMethodName"></param>
        /// <param name="iParams"></param>
        protected void Log_DeBug(string iMethodName, params object[] iParams)
        {
            Console.WriteLine(string.Format("{0}\t{1}", iMethodName, String.Join("_", iParams)));
            //LazyAtHome.Core.Helper..Log_Debug("调用日志", 99
            //        , string.Format("{0}\t{1}"
            //        , iMethodName
            //        , String.Join("_", iParams)
            //        )
            //    );
        }

        protected LazyAtHome.WCF.UserSystem.Business.Business.User UserInstance = LazyAtHome.WCF.UserSystem.Business.Business.User.Instance;


        /// <summary>
        /// 用户存在查询
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null)
        {
            Log_DeBug("User_Exist_Check", iParameter, iType, iUserID);
            try
            {
                return UserInstance.User_Exist_Check(iParameter, iType, iUserID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Exist_Check", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Exist_Check");
            }
        }

        /// <summary>
        /// 昵称重名验证
        /// </summary>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_NickName_Check(string iNickName, Guid? iUserID = null)
        {
            Log_DeBug("User_NickName_Check", iNickName, iUserID);
            try
            {
                return UserInstance.User_NickName_Check(iNickName, iUserID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_NickName_Check", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_NickName_Check");
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iInviteCode"></param>
        /// <param name="iRegisterIP"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_InfoDC> User_Reg_Web(string iParameter, LoginType iType, string iLoginPassword, string iInviteCode, string iRegisterIP, int iSite)
        {
            Log_DeBug("User_Reg_Web", iParameter, iType, iLoginPassword, iInviteCode, iRegisterIP, iSite);

            ReturnEntity<User_InfoDC> rtn = null;

            try
            {
                rtn = UserInstance.User_Reg_Web(iParameter, iType, iLoginPassword, iInviteCode, iRegisterIP, iSite);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Reg_Web", ex);
                return new ReturnEntity<User_InfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Reg_Web");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iLoginIP"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Login_Web(string iParameter, LoginType iType, string iLoginPassword, string iLoginIP)
        {
            Log_DeBug("User_Login_Web", iParameter, iType, iLoginPassword, iLoginIP);
            try
            {
                return UserInstance.User_Login_Web(iParameter, iType, iLoginPassword, iLoginIP);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Login_Web", ex);
                return new ReturnEntity<User_InfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Reg_Web");
            }
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="iLoginName"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_InfoDC>> User_Info_SELECT_List(string iLoginName, string iMPNo,
            string iEmail, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("User_Info_SELECT_List", iLoginName, iMPNo,
                iEmail, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return UserInstance.User_Info_SELECT_List(iLoginName, iMPNo,
                    iEmail, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Info_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<User_InfoDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Info_SELECT_List");
            }
        }

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Info_SELECT_Entity(Guid iID)
        {
            Log_DeBug("User_Info_SELECT_Entity", iID);
            try
            {
                return UserInstance.User_Info_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Info_SELECT_Entity", ex);
                return new ReturnEntity<User_InfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Info_SELECT_Entity");
            }
        }

        /// <summary>
        /// 用户获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Info_SELECT_Entity(string iOpenid, OAuthType iOAuthType)
        {
            Log_DeBug("User_Info_SELECT_Entity", iOpenid, iOAuthType);
            try
            {
                return UserInstance.User_Info_SELECT_Entity(iOpenid, iOAuthType);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Info_SELECT_Entity", ex);
                return new ReturnEntity<User_InfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Info_SELECT_Entity");
            }
        }

        /// <summary>
        /// 查询用户详情
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<User_DetailDC> User_Detail_SELECT_Entity(Guid iID)
        {
            Log_DeBug("User_Detail_SELECT_Entity", iID);
            try
            {
                return UserInstance.User_Detail_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Detail_SELECT_Entity", ex);
                return new ReturnEntity<User_DetailDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Detail_SELECT_Entity");
            }
        }

        /// <summary>
        /// 修改用户详情
        /// </summary>
        /// <param name="iUser_DetailDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Detail_UPDATE(User_DetailDC iUser_DetailDC)
        {
            Log_DeBug("User_Detail_UPDATE", iUser_DetailDC);
            try
            {
                return UserInstance.User_Detail_UPDATE(iUser_DetailDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Detail_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Detail_UPDATE");
            }
        }

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> User_LoginPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword)
        {
            Log_DeBug("User_LoginPassword_Change", iUserID, iOldPassword, iNewPassword);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = UserInstance.User_LoginPassword_Change(iUserID, iOldPassword, iNewPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_LoginPassword_Change", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_LoginPassword_Change");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginPassword_Reset(Guid iUserID, string iNewPassword)
        {
            Log_DeBug("User_LoginPassword_Reset", iUserID, iNewPassword);
            try
            {
                return UserInstance.User_LoginPassword_Reset(iUserID, iNewPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_LoginPassword_Reset", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_LoginPassword_Reset");
            }
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginPassword_Reset(string iUserMPNo, string iNewPassword)
        {
            Log_DeBug("User_LoginPassword_Reset", iUserMPNo, iNewPassword);
            try
            {
                return UserInstance.User_LoginPassword_Reset(iUserMPNo, iNewPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_LoginPassword_Reset", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_LoginPassword_Reset");
            }
        }

        /// <summary>
        /// 修改支付密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_PayPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword)
        {
            Log_DeBug("User_PayPassword_Change", iUserID, iOldPassword, iNewPassword);
            try
            {
                return UserInstance.User_PayPassword_Change(iUserID, iOldPassword, iNewPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_PayPassword_Change", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_PayPassword_Change");
            }
        }

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_PayPassword_Reset(Guid iUserID, string iNewPassword)
        {
            Log_DeBug("User_PayPassword_Reset", iUserID, iNewPassword);
            try
            {
                return UserInstance.User_PayPassword_Reset(iUserID, iNewPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_PayPassword_Reset", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_PayPassword_Reset");
            }
        }

        /// <summary>
        /// 修改登录名
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLoginName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginName_Change(Guid iUserID, string iLoginName)
        {
            Log_DeBug("User_LoginName_Change", iUserID, iLoginName);
            try
            {
                return UserInstance.User_LoginName_Change(iUserID, iLoginName);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_LoginName_Change", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_LoginName_Change");
            }
        }

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_MPNo_Change(Guid iUserID, string iMPNo)
        {
            Log_DeBug("User_MPNo_Change", iUserID, iMPNo);
            try
            {
                return UserInstance.User_MPNo_Change(iUserID, iMPNo);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_MPNo_Change", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_MPNo_Change");
            }
        }

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Email_Change(Guid iUserID, string iEmail)
        {
            Log_DeBug("User_Email_Change", iUserID, iEmail);
            try
            {
                return UserInstance.User_Email_Change(iUserID, iEmail);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Email_Change", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Email_Change");
            }
        }

        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_NickName_Change(Guid iUserID, string iNickName)
        {
            Log_DeBug("User_NickName_Change", iUserID, iNickName);
            try
            {
                return UserInstance.User_NickName_Change(iUserID, iNickName);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_NickName_Change", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_NickName_Change");
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNickName"></param>
        /// <param name="iSex"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iRealName"></param>
        /// <param name="iIDCard"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iLocation"></param>
        /// <returns></returns>
        public ReturnEntity<bool> web_User_Detail_UPDATE(Guid iUserID, string iNickName, int iSex, string iMPNo
            , string iEmail, string iRealName, string iIDCard, int? iDistrictID, string iLocation)
        {
            Log_DeBug("web_User_Detail_UPDATE", iUserID, iNickName, iSex, iMPNo
                    , iEmail, iRealName, iIDCard, iDistrictID, iLocation);
            try
            {
                return UserInstance.web_User_Detail_UPDATE(iUserID, iNickName, iSex, iMPNo
                    , iEmail, iRealName, iIDCard, iDistrictID, iLocation);
            }
            catch (Exception ex)
            {
                Log_Fatal("web_User_Detail_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("web_User_Detail_UPDATE");
            }
        }

        /// <summary>
        /// 用户邀请数量
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_Invite_Count(Guid iUserID)
        {
            Log_DeBug("User_Invite_Count", iUserID);
            try
            {
                return UserInstance.User_Invite_Count(iUserID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Invite_Count", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Invite_Count");
            }
        }

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Info_UPDATE(User_InfoDC iUser_InfoDC)
        {
            Log_DeBug("User_Info_UPDATE", iUser_InfoDC);
            try
            {
                return UserInstance.User_Info_UPDATE(iUser_InfoDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Info_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Info_UPDATE");
            }
        }

        /// <summary>
        /// 收货地址列表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_ConsigneeAddressDC>> User_ConsigneeAddress_SELECT_List(Guid iUserID)
        {
            Log_DeBug("User_ConsigneeAddress_SELECT_List", iUserID);
            try
            {
                return UserInstance.User_ConsigneeAddress_SELECT_List(iUserID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_ConsigneeAddress_SELECT_List", ex);
                return new ReturnEntity<IList<User_ConsigneeAddressDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_ConsigneeAddress_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<User_ConsigneeAddressDC> User_ConsigneeAddress_SELECT_Entity(int iID)
        {
            Log_DeBug("User_ConsigneeAddress_SELECT_Entity", iID);
            try
            {
                return UserInstance.User_ConsigneeAddress_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_ConsigneeAddress_SELECT_Entity", ex);
                return new ReturnEntity<User_ConsigneeAddressDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_ConsigneeAddress_SELECT_Entity");
            }
        }

        /// <summary>
        /// 收货地址添加
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_ConsigneeAddress_ADD(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            Log_DeBug("User_ConsigneeAddress_ADD", iUser_ConsigneeAddressDC);
            try
            {
                return UserInstance.User_ConsigneeAddress_ADD(iUser_ConsigneeAddressDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_ConsigneeAddress_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_ConsigneeAddress_ADD");
            }
        }

        /// <summary>
        /// 收货地址更新
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_ConsigneeAddress_UPDATE(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            Log_DeBug("User_ConsigneeAddress_UPDATE", iUser_ConsigneeAddressDC);
            try
            {
                return UserInstance.User_ConsigneeAddress_UPDATE(iUser_ConsigneeAddressDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_ConsigneeAddress_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_ConsigneeAddress_UPDATE");
            }
        }

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_ConsigneeAddress_DELETE(int iID)
        {
            Log_DeBug("User_ConsigneeAddress_DELETE", iID);
            try
            {
                return UserInstance.User_ConsigneeAddress_DELETE(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_ConsigneeAddress_DELETE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_ConsigneeAddress_DELETE");
            }
        }

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="iConsigneeAddressID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_ConsigneeAddress_SetDefault(int iID)
        {
            Log_DeBug("User_ConsigneeAddress_SetDefault", iID);
            try
            {
                return UserInstance.User_ConsigneeAddress_SetDefault(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_ConsigneeAddress_SetDefault", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_ConsigneeAddress_SetDefault");
            }
        }

        /// <summary>
        /// 余额日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_AmountLogDC>> User_AmountLog_SELECT_List(
              Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("User_AmountLog_SELECT_List", iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return UserInstance.User_AmountLog_SELECT_List(iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_AmountLog_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<User_AmountLogDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_AmountLog_SELECT_List");
            }
        }

        /// <summary>
        /// 积分日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_ScoreLogDC>> User_ScoreLog_SELECT_List(
              Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("User_ScoreLog_SELECT_List", iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return UserInstance.User_ScoreLog_SELECT_List(iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_ScoreLog_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<User_ScoreLogDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_ScoreLog_SELECT_List");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_CardDC> User_Card_Bind(Guid iUserID, UserCardType iUserCardType, string iCardPassword)
        {
            Log_DeBug("User_Card_Bind", iUserID, iUserCardType, iCardPassword);

            ReturnEntity<User_CardDC> rtn = null;

            try
            {
                rtn = UserInstance.User_Card_Bind(iUserID, iUserCardType, iCardPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Card_Bind", ex);
                return new ReturnEntity<User_CardDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Card_Bind");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 卡绑定前检查
        /// </summary>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Card_Bind_Check(UserCardType iUserCardType, string iCardPassword)
        {
            Log_DeBug("User_Card_Bind_Check", iUserCardType, iCardPassword);
            try
            {
                return UserInstance.User_Card_Bind_Check(iUserCardType, iCardPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Card_Bind_Check", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Card_Bind_Check");
            }
        }

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_CardDC>> User_Card_SELECT_List(Guid iUserID)
        {
            Log_DeBug("User_Card_SELECT_List", iUserID);
            try
            {
                return UserInstance.User_Card_SELECT_List(iUserID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Card_SELECT_List", ex);
                return new ReturnEntity<IList<User_CardDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Card_SELECT_List");
            }
        }

        /// <summary>
        /// 查询卡消费日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCardID"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<Base_CardLogDC>> Base_CardLog_SELECT_List(Guid? iUserID, int? iCardID,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            Log_DeBug("Base_CardLog_SELECT_List", iUserID, iCardID, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return UserInstance.Base_CardLog_SELECT_List(iUserID, iCardID, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("Base_CardLog_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<Base_CardLogDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("Base_CardLog_SELECT_List");
            }
        }

        ///// <summary>
        ///// 检查用户是否存在
        ///// </summary>
        ///// <param name="iOpenid"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> wx_User_Weixin_Exist_Check(string iOpenid)
        //{
        //    Log_DeBug("wx_User_Weixin_Exist_Check", iOpenid);
        //    try
        //    {
        //        return ExtendInstance.wx_User_Weixin_Exist_Check(iOpenid);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_Fatal("wx_User_Weixin_Exist_Check", ex);
        //        return new ReturnEntity<bool>(-999, ex.Message);
        //    }
        //    finally
        //    {
        //        Log_DeBug("wx_User_Weixin_Exist_Check");
        //    }
        //}

        ///// <summary>
        ///// 添加微信用户
        ///// </summary>
        ///// <param name="iOpenid"></param>
        ///// <returns></returns>
        //public ReturnEntity<bool> wx_User_Weixin_ADD(string iOpenid, string iNickName, int iSex,
        //    string iProvince, string iCity, string iCountry, string iHeadimgUrl)
        //{
        //    Log_DeBug("wx_User_Weixin_ADD", iOpenid, iNickName, iSex,
        //      iProvince, iCity, iCountry, iHeadimgUrl);
        //    try
        //    {
        //        return ExtendInstance.wx_User_Weixin_ADD(iOpenid, iNickName, iSex,
        //            iProvince, iCity, iCountry, iHeadimgUrl);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log_Fatal("wx_User_Weixin_ADD", ex);
        //        return new ReturnEntity<bool>(-999, ex.Message);
        //    }
        //    finally
        //    {
        //        Log_DeBug("wx_User_Weixin_ADD");
        //    }
        //}

        /// <summary>
        /// 检查用户是否绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_Weixin_CheckIsBind(string iOpenid)
        {
            Log_DeBug("wx_User_Weixin_CheckIsBind", iOpenid);
            try
            {
                return UserInstance.wx_User_Weixin_CheckIsBind(iOpenid);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_CheckIsBind", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_CheckIsBind");
            }
        }

        /// <summary>
        /// 微信用户信息获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_SELECT(string iOpenid)
        {
            Log_DeBug("wx_User_Weixin_SELECT", iOpenid);
            try
            {
                return UserInstance.wx_User_Weixin_SELECT(iOpenid);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_SELECT", ex);
                return new ReturnEntity<User_WeixinDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_SELECT");
            }
        }


        /// <summary>
        /// 微信用户绑定手机
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iNickName"></param>
        /// <param name="iSite"></param>
        /// <param name="iIPAddress"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Bind_Part(string iOpenid, string iMPNo, string iNickName,
            int iSite, string iIPAddress, string iInviteCode = null)
        {
            Log_DeBug("wx_User_Weixin_Bind_Part", iOpenid, iMPNo, iNickName, iSite, iIPAddress, iInviteCode);

            ReturnEntity<User_WeixinDC> rtn = null;

            try
            {
                rtn = UserInstance.wx_User_Weixin_Bind_Part(iOpenid, iMPNo, iNickName, iSite, iIPAddress, iInviteCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_Bind_Part", ex);
                return new ReturnEntity<User_WeixinDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_Bind_Part");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 微信用户绑定手机
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iNickName"></param>
        /// <param name="iSite"></param>
        /// <param name="iIPAddress"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> wx_User_Weixin_BindMPNo(string iOpenid, string iMPNo, string iNickName,
            int iSite, string iIPAddress, string iInviteCode = null)
        {
            Log_DeBug("wx_User_Weixin_BindMPNo", iOpenid, iMPNo, iNickName, iSite, iIPAddress, iInviteCode);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = UserInstance.wx_User_Weixin_BindMPNo(iOpenid, iMPNo, iNickName, iSite, iIPAddress, iInviteCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_BindMPNo", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_BindMPNo");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 解绑用户微信
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_Weixin_UnBindMPNo(string iOpenid, string iMPNo)
        {
            Log_DeBug("wx_User_Weixin_UnBindMPNo", iOpenid, iMPNo);
            try
            {
                return UserInstance.wx_User_Weixin_UnBindMPNo(iOpenid, iMPNo);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_UnBindMPNo", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_UnBindMPNo");
            }
        }

        /// <summary>
        /// 收货地址列表
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_ConsigneeAddressDC>> wx_User_ConsigneeAddress_SELECT_List(string iOpenid)
        {
            Log_DeBug("wx_User_ConsigneeAddress_SELECT_List", iOpenid);
            try
            {
                return UserInstance.wx_User_ConsigneeAddress_SELECT_List(iOpenid);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_ConsigneeAddress_SELECT_List", ex);
                return new ReturnEntity<IList<User_ConsigneeAddressDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_ConsigneeAddress_SELECT_List");
            }
        }

        /// <summary>
        /// 收货地址添加
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> wx_User_ConsigneeAddress_ADD(
            string iOpenid, string iConsignee, int iDistrictID, string iAddress, string iMPNo, bool iDefault)
        {
            Log_DeBug("wx_User_ConsigneeAddress_ADD", iOpenid, iConsignee, iDistrictID, iAddress, iMPNo, iDefault);
            try
            {
                return UserInstance.wx_User_ConsigneeAddress_ADD(iOpenid, iConsignee, iDistrictID, iAddress, iMPNo, iDefault);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_ConsigneeAddress_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_ConsigneeAddress_ADD");
            }
        }

        /// <summary>
        /// 收货地址更新
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iConsignee"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_ConsigneeAddress_UPDATE(int iID, string iConsignee, int iDistrictID,
            string iAddress, string iMPNo, bool iDefault)
        {
            Log_DeBug("wx_User_ConsigneeAddress_UPDATE", iID, iConsignee, iDistrictID, iAddress, iMPNo, iDefault);
            try
            {
                return UserInstance.wx_User_ConsigneeAddress_UPDATE(iID, iConsignee, iDistrictID, iAddress, iMPNo, iDefault);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_ConsigneeAddress_UPDATE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_ConsigneeAddress_UPDATE");
            }
        }

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_ConsigneeAddress_DELETE(int iID)
        {
            Log_DeBug("wx_User_ConsigneeAddress_DELETE", iID);
            try
            {
                return UserInstance.wx_User_ConsigneeAddress_DELETE(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_ConsigneeAddress_DELETE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_ConsigneeAddress_DELETE");
            }
        }

        /// <summary>
        /// 用户卡绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> wx_User_Card_Bind(string iOpenid, UserCardType iUserCardType, string iCardPassword)
        {
            Log_DeBug("wx_User_Card_Bind", iOpenid, iUserCardType, iCardPassword);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = UserInstance.wx_User_Card_Bind(iOpenid, iUserCardType, iCardPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Card_Bind", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Card_Bind");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_CardDC>> wx_User_Card_SELECT_List(string iOpenid)
        {
            Log_DeBug("wx_User_Card_SELECT_List", iOpenid);
            try
            {
                return UserInstance.wx_User_Card_SELECT_List(iOpenid);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Card_SELECT_List", ex);
                return new ReturnEntity<IList<User_CardDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Card_SELECT_List");
            }
        }

        #region 用户消息

        /// <summary>
        /// 查询用户消息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCardID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_Message_PrivateDC>> User_Message_Private_SELECT_List(
            Guid iUserID, int iPageIndex, int iPageSize)
        {
            Log_DeBug("User_Message_Private_SELECT_List", iUserID, iPageIndex, iPageSize);
            try
            {
                return UserInstance.User_Message_Private_SELECT_List(iUserID, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Message_Private_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<User_Message_PrivateDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Message_Private_SELECT_List");
            }
        }

        /// <summary>
        /// 消息置已读
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Message_Private_Read(int iID)
        {
            Log_DeBug("User_Message_Private_Read", iID);
            try
            {
                return UserInstance.User_Message_Private_Read(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Message_Private_Read", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Message_Private_Read");
            }
        }

        /// <summary>
        /// 消息删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Message_Private_DELETE(int iID)
        {
            Log_DeBug("User_Message_Private_DELETE", iID);
            try
            {
                return UserInstance.User_Message_Private_DELETE(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Message_Private_DELETE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Message_Private_DELETE");
            }
        }

        /// <summary>
        /// 根据ID查询消息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        public ReturnEntity<User_Message_PrivateDC> User_Message_Private_SELECT_Entity(int iID)
        {
            Log_DeBug("User_Message_Private_SELECT_Entity", iID);
            try
            {
                return UserInstance.User_Message_Private_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Message_Private_SELECT_Entity", ex);
                return new ReturnEntity<User_Message_PrivateDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Message_Private_SELECT_Entity");
            }
        }

        /// <summary>
        /// 微信获取用户消息
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_Message_PrivateDC>> wx_User_Message_Private_SELECT_List(
            string iOpenid, int iPageIndex, int iPageSize)
        {
            Log_DeBug("wx_User_Message_Private_SELECT_List", iOpenid, iPageIndex, iPageSize);
            try
            {
                return UserInstance.wx_User_Message_Private_SELECT_List(iOpenid, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Message_Private_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<User_Message_PrivateDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Message_Private_SELECT_List");
            }
        }

        #endregion

        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        public ReturnEntity<bool> wx_User_NickName_Change(string iOpenid, string iNickName)
        {
            Log_DeBug("wx_User_NickName_Change", iOpenid, iNickName);
            try
            {
                return UserInstance.wx_User_NickName_Change(iOpenid, iNickName);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_NickName_Change", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_NickName_Change");
            }
        }

        /// <summary>
        /// 查询优惠券列表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iCouponName"></param>
        /// <param name="iCouponStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_CouponDC>> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo,
            string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize)
        {
            Log_DeBug("User_Coupon_SELECT_List", iUserID, iMPNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize);
            try
            {
                return UserInstance.User_Coupon_SELECT_List(iUserID, iMPNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Coupon_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<User_CouponDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Coupon_SELECT_List");
            }
        }

        /// <summary>
        /// 用户绑定券检查
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Coupon_Bind_Check(Guid iUserID, string iCouponCode)
        {
            Log_DeBug("User_Coupon_Bind_Check", iUserID, iCouponCode);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = UserInstance.User_Coupon_Bind_Check(iUserID, iCouponCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Coupon_Bind_Check", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Coupon_Bind_Check");
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_CouponDC> User_Coupon_Bind(Guid iUserID, string iCouponCode)
        {
            Log_DeBug("User_Coupon_Bind", iUserID, iCouponCode);

            ReturnEntity<User_CouponDC> rtn = null;

            try
            {
                rtn = UserInstance.User_Coupon_Bind(iUserID, iCouponCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Coupon_Bind", ex);
                return new ReturnEntity<User_CouponDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Coupon_Bind");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        #region 微信关注日志

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_WeixinAttentionLogDC"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WeixinAttention_ADD(string iOpenID, DateTime iAttentionTime, string iSource)
        {
            Log_DeBug("User_WeixinAttention_ADD", iOpenID, iAttentionTime, iSource);
            try
            {
                return UserInstance.User_WeixinAttention_ADD(iOpenID, iAttentionTime, iSource);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_WeixinAttention_ADD", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_WeixinAttention_ADD");
            }
        }

        /// <summary>
        /// 删除关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WeixinAttention_Remove(string iOpenID)
        {
            Log_DeBug("User_WeixinAttention_Remove", iOpenID);
            try
            {
                return UserInstance.User_WeixinAttention_Remove(iOpenID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_WeixinAttention_Remove", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_WeixinAttention_Remove");
            }
        }

        /// <summary>
        /// 用户是否关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WeixinAttention_Check(string iOpenID)
        {
            Log_DeBug("User_WeixinAttention_Check", iOpenID);
            try
            {
                return UserInstance.User_WeixinAttention_Check(iOpenID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_WeixinAttention_Check", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_WeixinAttention_Check");
            }
        }

        /// <summary>
        /// 微信关注统计
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_WeixinAttentionLogStatDC>> User_WeixinAttentionLog_Stat(string iInternalKey, DateTime iStartDate, DateTime iEndDate)
        {
            Log_DeBug("User_WeixinAttentionLog_Stat", iInternalKey, iStartDate, iEndDate);
            try
            {
                return UserInstance.User_WeixinAttentionLog_Stat(iInternalKey, iStartDate, iEndDate);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_WeixinAttentionLog_Stat", ex);
                return new ReturnEntity<IList<User_WeixinAttentionLogStatDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_WeixinAttentionLog_Stat");
            }
        }

        #endregion

        /// <summary>
        /// app登录
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iLoginIP"></param>
        /// <param name="iVersion"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Login_App(string iParameter, LoginType iType,
           string iLoginPassword, string iLoginIP, string iVersion, int iMobileType, string iMobileFlag)
        {
            Log_DeBug("User_Login_App", iParameter, iType, iLoginPassword, iLoginIP, iVersion, iMobileType, iMobileFlag);
            try
            {
                return UserInstance.User_Login_App(iParameter, iType, iLoginPassword, iLoginIP, iVersion, iMobileType, iMobileFlag);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Login_App", ex);
                return new ReturnEntity<User_InfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Login_App");
            }
        }


        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iInviteCode"></param>
        /// <param name="iRegisterIP"></param>
        /// <param name="iVersion"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_InfoDC> User_Reg_App(string iParameter, LoginType iType, string iLoginPassword,
            string iInviteCode, string iRegisterIP, string iVersion, int iMobileType, string iMobileFlag)
        {
            Log_DeBug("User_Reg_App", iParameter, iType, iLoginPassword, iInviteCode, iRegisterIP, iVersion, iMobileType, iMobileFlag);

            ReturnEntity<User_InfoDC> rtn = null;

            try
            {
                rtn = UserInstance.User_Reg_App(iParameter, iType, iLoginPassword, iInviteCode, iRegisterIP, iVersion, iMobileType, iMobileFlag);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Reg_App", ex);
                return new ReturnEntity<User_InfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Reg_App");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_LoginOut(Guid iUserID, int iMobileType)
        {
            Log_DeBug("User_LoginOut", iUserID, iMobileType);
            try
            {
                return UserInstance.User_LoginOut(iUserID, iMobileType);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_LoginOut", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_LoginOut");
            }
        }

        /// <summary>
        /// 唤醒日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <param name="iMobileFlag"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_WakeUp(Guid iUserID, int iMobileType, string iMobileFlag)
        {
            Log_DeBug("User_WakeUp", iUserID, iMobileType, iMobileFlag);
            try
            {
                return UserInstance.User_WakeUp(iUserID, iMobileType, iMobileFlag);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_WakeUp", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_WakeUp");
            }
        }

        /// <summary>
        /// 投诉
        /// </summary>
        /// <param name="iUser_ComplaintsDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_Complaints_ADD(User_ComplaintsDC iUser_ComplaintsDC)
        {
            Log_DeBug("User_Complaints_ADD", iUser_ComplaintsDC);
            try
            {
                return UserInstance.User_Complaints_ADD(iUser_ComplaintsDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Complaints_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Complaints_ADD");
            }
        }

        #region User_RegisterSource

        /// <summary>
        /// 新增 User_RegisterSource
        /// </summary>
        /// <param name="iUser_RegisterSourceDC"></param>
        /// <returns></returns>
        public ReturnEntity<int> User_RegisterSource_ADD(User_RegisterSourceDC iUser_RegisterSourceDC)
        {
            Log_DeBug("User_RegisterSource_ADD", iUser_RegisterSourceDC);
            try
            {
                return UserInstance.User_RegisterSource_ADD(iUser_RegisterSourceDC);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_RegisterSource_ADD", ex);
                return new ReturnEntity<int>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_RegisterSource_ADD");
            }
        }

        /// <summary>
        /// 删除User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        public ReturnEntity<bool> User_RegisterSource_DELETE(int iID, int iMuser)
        {
            Log_DeBug("User_RegisterSource_DELETE", iID, iMuser);
            try
            {
                return UserInstance.User_RegisterSource_DELETE(iID, iMuser);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_RegisterSource_DELETE", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_RegisterSource_DELETE");
            }
        }

        /// <summary>
        /// 查询全部User_RegisterSource
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<RecordCountEntity<User_RegisterSourceDC>> User_RegisterSource_SELECT_List(int? iType, int iPageIndex, int iPageSize)
        {
            Log_DeBug("User_RegisterSource_SELECT_List", iType, iPageIndex, iPageSize);
            try
            {
                return UserInstance.User_RegisterSource_SELECT_List(iType, iPageIndex, iPageSize);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_RegisterSource_SELECT_List", ex);
                return new ReturnEntity<RecordCountEntity<User_RegisterSourceDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_RegisterSource_SELECT_List");
            }
        }

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        public ReturnEntity<User_RegisterSourceDC> User_RegisterSource_SELECT_Entity(int iID)
        {
            Log_DeBug("User_RegisterSource_SELECT_Entity", iID);
            try
            {
                return UserInstance.User_RegisterSource_SELECT_Entity(iID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_RegisterSource_SELECT_Entity", ex);
                return new ReturnEntity<User_RegisterSourceDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_RegisterSource_SELECT_Entity");
            }
        }

        #endregion


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iNickName"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iEmail"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> User_Info_Update(Guid iUserID,
            string iLoginName, string iNickName, string iMPNo, string iEmail)
        {
            Log_DeBug("User_Info_Update", iUserID, iLoginName, iNickName, iMPNo, iEmail);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = UserInstance.User_Info_Update(iUserID, iLoginName, iNickName, iMPNo, iEmail);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Info_Update", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Info_Update");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_CardDC>> Web_User_Card_SELECT_List(Guid iUserID)
        {
            Log_DeBug("User_Card_SELECT_List", iUserID);
            try
            {
                return UserInstance.Web_User_Card_SELECT_List(iUserID);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Card_SELECT_List", ex);
                return new ReturnEntity<IList<User_CardDC>>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Card_SELECT_List");
            }
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iInviteCode"></param>
        /// <param name="iRegisterIP"></param>
        /// <param name="iVersion"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_InfoDC> User_Reg_CustomerService(string iParameter, LoginType iType)
        {
            Log_DeBug("User_Reg_CustomerService", iParameter, iType);

            ReturnEntity<User_InfoDC> rtn = null;

            try
            {
                rtn = UserInstance.User_Reg_CustomerService(iParameter, iType);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Reg_CustomerService", ex);
                return new ReturnEntity<User_InfoDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Reg_CustomerService");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 微信用户注册
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iSite"></param>
        /// <param name="iIPAddress"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Create(string iOpenid, int iSite, string iIPAddress, string iInviteCode)
        {
            Log_DeBug("wx_User_Weixin_Create", iOpenid, iSite, iIPAddress, iInviteCode);

            ReturnEntity<User_WeixinDC> rtn = null;

            try
            {
                rtn = UserInstance.wx_User_Weixin_Create(iOpenid, iSite, iIPAddress, iInviteCode);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_Create", ex);
                return new ReturnEntity<User_WeixinDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_Create");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 微信旧用户绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Login(string iOpenid, string iMPNo)
        {
            Log_DeBug("wx_User_Weixin_Login", iOpenid, iMPNo);

            ReturnEntity<User_WeixinDC> rtn = null;

            try
            {
                rtn = UserInstance.wx_User_Weixin_Login(iOpenid, iMPNo);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_Login", ex);
                return new ReturnEntity<User_WeixinDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_Login");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 微信旧用户绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<User_WeixinDC> wx_User_Weixin_Login(string iOpenid, string iLoginName, string iPassword)
        {
            Log_DeBug("wx_User_Weixin_Login", iOpenid, iLoginName, iPassword);

            ReturnEntity<User_WeixinDC> rtn = null;

            try
            {
                rtn = UserInstance.wx_User_Weixin_Login(iOpenid, iLoginName, iPassword);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_Login", ex);
                return new ReturnEntity<User_WeixinDC>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_Login");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        /// <summary>
        /// 微信用户注册
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <param name="iEmail"></param>
        /// <returns></returns>
        [OperationBehavior(TransactionAutoComplete = false, TransactionScopeRequired = true)]
        public ReturnEntity<bool> wx_User_Weixin_Reg(string iOpenid, string iLoginName, string iPassword, string iEmail)
        {
            Log_DeBug("wx_User_Weixin_Reg", iOpenid, iLoginName, iPassword, iEmail);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = UserInstance.wx_User_Weixin_Reg(iOpenid, iLoginName, iPassword, iEmail);
            }
            catch (Exception ex)
            {
                Log_Fatal("wx_User_Weixin_Reg", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("wx_User_Weixin_Reg");
            }
            if (rtn != null && rtn.Success)
            {
                OperationContext.Current.SetTransactionComplete();
            }
            return rtn;
        }

        
        /// <summary>
        /// 用户更新地理位置
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Info_UPDATE_Location(Guid iUserID, decimal iLatitude, decimal iLongitude)
        {
            Log_DeBug("User_Info_UPDATE_Location", iUserID, iLatitude, iLongitude);

            ReturnEntity<bool> rtn = null;

            try
            {
                rtn = UserInstance.User_Info_UPDATE_Location(iUserID, iLatitude, iLongitude);
            }
            catch (Exception ex)
            {
                Log_Fatal("User_Info_UPDATE_Location", ex);
                return new ReturnEntity<bool>(-999, ex.Message);
            }
            finally
            {
                Log_DeBug("User_Info_UPDATE_Location");
            }
            return rtn;
        }
    }
}
