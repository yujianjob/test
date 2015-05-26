using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.API.Mobile.App_Code.Proxy
{
    public class UserProxy
    {

        //4.1	用户登录	7
        public static ReturnEntity<User_InfoDC> User_Login_App(string iParameter, LoginType iType,
           string iLoginPassword, string iLoginIP, string iVersion, int iMobileType, string iMobileFlag)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>(
                c => c.Proxy.User_Login_App(iParameter, iType, iLoginPassword, iLoginIP, iVersion, iMobileType, iMobileFlag));

            return rtn;
        }

        //4.2	用户名有效性检查	7
        public static ReturnEntity<bool> User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_Exist_Check(iParameter, iType, iUserID));

            return rtn;
        }

        //4.3	用户注册	7
        public static ReturnEntity<User_InfoDC> User_Reg_App(string iParameter, LoginType iType,
            string iLoginPassword, string iInviteCode, string iRegisterIP, string iVersion, int iMobileType,
            string iMobileFlag)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>(
                c => c.Proxy.User_Reg_App(iParameter, iType, iLoginPassword, iInviteCode, iRegisterIP, iVersion, iMobileType, iMobileFlag));

            return rtn;
        }


        //4.5	设置新密码	8
        public static ReturnEntity<bool> User_LoginPassword_Reset(string iUserMPNo, string iNewPassword)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_LoginPassword_Reset(iUserMPNo, iNewPassword));

            return rtn;
        }


        //4.12	用户默认地址获取	10
        public static ReturnEntity<IList<User_ConsigneeAddressDC>> User_ConsigneeAddress_SELECT_List(Guid iUserID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<IList<User_ConsigneeAddressDC>>>(
                c => c.Proxy.User_ConsigneeAddress_SELECT_List(iUserID));

            return rtn;
        }

        //4.18	获取用户信息	12
        public static ReturnEntity<User_InfoDC> User_Info_SELECT_Entity(Guid iUserID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>(
                c => c.Proxy.User_Info_SELECT_Entity(iUserID));

            return rtn;
        }


        //4.19	保存用户信息	13
        //public static ReturnEntity<bool> User_Info_UPDATE(User_InfoDC iUser_InfoDC)
        //{
        //    var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
        //        c => c.Proxy.User_Info_UPDATE(iUser_InfoDC));

        //    return rtn;
        //}
        public static ReturnEntity<bool> User_LoginName_Change(Guid iUserID, string iLoginName)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_LoginName_Change(iUserID, iLoginName));

            return rtn;
        }
        public static ReturnEntity<bool> User_Email_Change(Guid iUserID, string iEmail)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_Email_Change(iUserID, iEmail));

            return rtn;
        }

        //4.20	修改登录密码(暂无)	13


        //4.21	用户注销	13
        public static ReturnEntity<bool> User_LoginOut(Guid iUserID, int iMobileType)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_LoginOut(iUserID, iMobileType));

            return rtn;
        }

        //4.22	检查新手机是否存在	13
        //4.23	检查手机号与验证码匹配	14
        //4.24	改绑新手机	14
        public static ReturnEntity<bool> User_MPNo_Change(Guid iUserID, string iMPNo)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_MPNo_Change(iUserID, iMPNo));

            return rtn;
        }

        //4.25	获取用户地址列表	14

        //4.27	新增或保存用户地址	15
        public static ReturnEntity<int> User_ConsigneeAddress_ADD(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<int>>(
                c => c.Proxy.User_ConsigneeAddress_ADD(iUser_ConsigneeAddressDC));

            return rtn;
        }

        public static ReturnEntity<User_ConsigneeAddressDC> User_ConsigneeAddress_SELECT_Entity(int iID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_ConsigneeAddressDC>>(
                c => c.Proxy.User_ConsigneeAddress_SELECT_Entity(iID));

            return rtn;
        }

        public static ReturnEntity<bool> User_ConsigneeAddress_UPDATE(User_ConsigneeAddressDC iUser_ConsigneeAddressDC)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_ConsigneeAddress_UPDATE(iUser_ConsigneeAddressDC));

            return rtn;
        }

        //4.28	删除用户地址	15
        public static ReturnEntity<bool> User_ConsigneeAddress_DELETE(int iID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_ConsigneeAddress_DELETE(iID));

            return rtn;
        }

        //4.29	获取懒人卡列表	15
        public static ReturnEntity<IList<User_CardDC>> User_Card_SELECT_List(Guid iUserID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<IList<User_CardDC>>>(
                c => c.Proxy.User_Card_SELECT_List(iUserID));

            return rtn;
        }
        public static ReturnEntity<RecordCountEntity<Base_CardLogDC>> Base_CardLog_SELECT_List(Guid? iUserID, int? iCardID, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<Base_CardLogDC>>>(
                c => c.Proxy.Base_CardLog_SELECT_List(iUserID, iCardID, iStartDate, iEndDate, iPageIndex, iPageSize));

            return rtn;
        }

        //4.30	添加懒人卡	16
        public static ReturnEntity<User_CardDC> User_Card_Bind(Guid iUserID, UserCardType iUserCardType, string iCardPassword)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_CardDC>>(
                c => c.Proxy.User_Card_Bind(iUserID, iUserCardType, iCardPassword));

            return rtn;
        }

        //4.31	获取优惠券列表	16
        public static ReturnEntity<RecordCountEntity<User_CouponDC>> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo, string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_CouponDC>>>(
                c => c.Proxy.User_Coupon_SELECT_List(iUserID, iMPNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize));

            return rtn;
        }

        //4.32	添加优惠券	16
        public static ReturnEntity<User_CouponDC> User_Coupon_Bind(Guid iUserID, string iCouponPassword)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_CouponDC>>(
                c => c.Proxy.User_Coupon_Bind(iUserID, iCouponPassword));

            return rtn;
        }

        //4.33	用户反馈	17
        public static ReturnEntity<int> User_Complaints_ADD(User_ComplaintsDC iUser_ComplaintsDC)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<int>>(
                c => c.Proxy.User_Complaints_ADD(iUser_ComplaintsDC));

            return rtn;
        }
        //4.34	唤醒日志	17
        public static ReturnEntity<bool> User_WakeUp(Guid iUserID, int iMobileType, string iMobileFlag)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(
                c => c.Proxy.User_WakeUp(iUserID, iMobileType, iMobileFlag));

            return rtn;
        }
        //4.35	检查更新	17

    }
}