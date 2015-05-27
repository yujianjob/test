using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;


namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 用户wcf代理类
    /// </summary>
    public class UserProxy
    {
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="iLoginName"></param>
        /// <param name="iMpNo"></param>
        /// <param name="iEmail"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<User_InfoDC> GetUserList(string iLoginName, string iMpNo, string iEmail, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<User_InfoDC> rtn = null;
            ReturnEntity<RecordCountEntity<User_InfoDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_InfoDC>>>
                    (client => client.Proxy.User_Info_SELECT_List(iLoginName,iMpNo, iEmail, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|OperatorProxy GetManagerList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }

        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<User_InfoDC> GetUserInfo(Guid id)
        {

            ReturnEntity<User_InfoDC> re = null;
            
            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>
                (client => client.Proxy.User_Info_SELECT_Entity(id));

                //if (result != null)
                //    result.HeadImgPath = string.IsNullOrEmpty(result.HeadImgPath) ? CodeSource.Common.ConstConfig.DEFAULT_HEAD_IMAGE_PATH : (CodeSource.Common.ConstConfig.IMAGE_PATH + CodeSource.Common.ConstConfig.HEAD_IMG_PATH + "/" + WebUtility.RemovePIC(result.HeadImgPath));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy GetUserInfo|" + ex.Message);
            }

            return re;
        }


        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<User_DetailDC> GetUserDetail(Guid id)
        {

            ReturnEntity<User_DetailDC> re = null;

            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_DetailDC>>
                (client => client.Proxy.User_Detail_SELECT_Entity(id));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy GetUserDetail|" + ex.Message);
            }

            return re;
        }


        /// <summary>
        /// 后台注册
        /// </summary>
        /// <param name="mpno"></param>
        /// <returns></returns>
        public static ReturnEntity<User_InfoDC> RegByCustomerService(string mpno)
        {
            WCF.UserSystem.Contract.Enumerate.LoginType type = WCF.UserSystem.Contract.Enumerate.LoginType.MPNo;

            System.Text.RegularExpressions.Regex rMobile = new System.Text.RegularExpressions.Regex("^1\\d{10}$", System.Text.RegularExpressions.RegexOptions.Compiled);

            if (!rMobile.IsMatch(mpno))
                type = WCF.UserSystem.Contract.Enumerate.LoginType.LoginName;
            else
                type = WCF.UserSystem.Contract.Enumerate.LoginType.MPNo;

            ReturnEntity<User_InfoDC> re = null;

            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>
                (client => client.Proxy.User_Reg_CustomerService(mpno, type));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy RegByCustomerService|" + ex.Message);
            }

            return re;
        }


        /// <summary>
        /// 获取用户详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<User_ConsigneeAddressDC>> GetUserConsigneeAddressList(Guid id)
        {

            ReturnEntity<IList<User_ConsigneeAddressDC>> re = null;

            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<IList<User_ConsigneeAddressDC>>>
                (client => client.Proxy.User_ConsigneeAddress_SELECT_List(id));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy GetUserConsigneeAddressList|" + ex.Message);
            }

            return re;
        }



        /// <summary>
        /// 添加用户地址
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        public static ReturnEntity<int> AddUserConsigneeAddress(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {

            ReturnEntity<int> re = null;

            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<int>>
                (client => client.Proxy.User_ConsigneeAddress_ADD(iUser_ConsigneeAddressDC));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy AddUserConsigneeAddress|" + ex.Message);
            }

            return re;
        }


        /// <summary>
        /// 获取用户积分日志列表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMpNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<User_ScoreLogDC> GetUserScoreLogList(Guid? iUserID, string iMpNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<User_ScoreLogDC> rtn = null;
            ReturnEntity<RecordCountEntity<User_ScoreLogDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_ScoreLogDC>>>
                    (client => client.Proxy.User_ScoreLog_SELECT_List(iUserID, iMpNo, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy GetUserScoreLogList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 获取用户余额日志列表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMpNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<User_AmountLogDC> GetUserAmountLogList(Guid? iUserID, string iMpNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<User_AmountLogDC> rtn = null;
            ReturnEntity<RecordCountEntity<User_AmountLogDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_AmountLogDC>>>
                    (client => client.Proxy.User_AmountLog_SELECT_List(iUserID, iMpNo, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy GetUserAmountLogList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 用户优惠券查询
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMpNo"></param>
        /// <param name="iCouponName"></param>
        /// <param name="iCouponStatus"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<User_CouponDC> GetUserCouponList(Guid? iUserID, string iMpNo, string iCouponName, WCF.UserSystem.Contract.Enumerate.CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<User_CouponDC> rtn = null;
            ReturnEntity<RecordCountEntity<User_CouponDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_CouponDC>>>
                    (client => client.Proxy.User_Coupon_SELECT_List(iUserID,iMpNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy GetUserCouponList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }



        /// <summary>
        /// 修改会员等级
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> EditUserLevel(User_InfoDC iUser_InfoDC)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.User_Info_UPDATE(iUser_InfoDC));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy EditUserLevel|" + ex.Message);
            }
            return re;
        }




        /// <summary>
        /// 绑定优惠券
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        public static ReturnEntity<User_CouponDC> BindUserCoupon(Guid iUserID, string iCouponCode)
        {
            ReturnEntity<User_CouponDC> re = null;

            try
            {
                re = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_CouponDC>>
                   (client => client.Proxy.User_Coupon_Bind(iUserID, iCouponCode));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|UserProxy BindUserCoupon|" + ex.Message);
            }
            return re;
        }


    }
}