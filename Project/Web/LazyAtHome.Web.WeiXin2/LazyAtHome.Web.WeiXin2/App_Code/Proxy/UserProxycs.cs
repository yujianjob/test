using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WeiXin2.App_Code.Proxy
{
    public class UserProxycs
    {
        public static ReturnEntity<bool> User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_Exist_Check(iParameter, iType, iUserID));
            return rtn;
        }

        public static ReturnEntity<User_CouponDC> User_Coupon_Bind(Guid iUserID, string iCouponCode)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_CouponDC>>(c => c.Proxy.User_Coupon_Bind(iUserID, iCouponCode));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<User_CouponDC>> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo, string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_CouponDC>>>(c => c.Proxy.User_Coupon_SELECT_List(iUserID, iMPNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<bool> Update_Userinfo(Guid iUserID, string iNickName, int iSex, string iMPNo, string iEmail, string iRealName, string iIDCard, int? iDistrictID, string iLocation)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.web_User_Detail_UPDATE(iUserID, iNickName, iSex, iMPNo, iEmail, iRealName, iIDCard, iDistrictID, iLocation));
            return rtn;
        }

        public static ReturnEntity<bool> Update_Userinfo(Guid iUserID, string iLoginName, string iNickName, string iMPNo, string iEmail)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_Info_Update(iUserID, iLoginName, iNickName, iMPNo, iEmail));
            return rtn;
        }

        public static ReturnEntity<User_InfoDC> User_Reg(string iParameter, LoginType iType, string iLoginPassword, string iInviteCode, string iRegisterIP, int iSite)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>(c => c.Proxy.User_Reg_Web(iParameter, iType, iLoginPassword, iInviteCode, iRegisterIP, iSite));
            return rtn;
        }

        public static ReturnEntity<User_InfoDC> User_Login(string iParameter, LoginType iType, string iLoginPassword, string iLoginIP)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>(c => c.Proxy.User_Login_Web(iParameter, iType, iLoginPassword, iLoginIP));
            return rtn;
        }

        public static ReturnEntity<User_InfoDC> Select_UserInfo(Guid userid)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_InfoDC>>(c => c.Proxy.User_Info_SELECT_Entity(userid));
            return rtn;
        }

        public static ReturnEntity<User_DetailDC> Select_UserDetail(Guid userid)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_DetailDC>>(c => c.Proxy.User_Detail_SELECT_Entity(userid));
            return rtn;
        }

        public static ReturnEntity<IList<User_ConsigneeAddressDC>> Select_UserAddress(Guid userid)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<IList<User_ConsigneeAddressDC>>>(c => c.Proxy.User_ConsigneeAddress_SELECT_List(userid));
            return rtn;
        }

        public static ReturnEntity<User_ConsigneeAddressDC> User_ConsigneeAddress_SELECT_Entity(int id)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_ConsigneeAddressDC>>(c => c.Proxy.User_ConsigneeAddress_SELECT_Entity(id));
            return rtn;
        }

        public static ReturnEntity<int> User_ConsigneeAddress_ADD(User_ConsigneeAddressDC addressItem)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<int>>(c => c.Proxy.User_ConsigneeAddress_ADD(addressItem));
            return rtn;
        }

        public static ReturnEntity<bool> User_ConsigneeAddress_DELETE(int id)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_ConsigneeAddress_DELETE(id));
            return rtn;
        }

        public static ReturnEntity<bool> User_ConsigneeAddress_UPDATE(User_ConsigneeAddressDC addressItem)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_ConsigneeAddress_UPDATE(addressItem));
            return rtn;
        }

        /// <summary>
        /// 获取用户懒人卡列表
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public static ReturnEntity<IList<User_CardDC>> Select_UserCardList(Guid userid)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<IList<User_CardDC>>>(c => c.Proxy.User_Card_SELECT_List(userid));
            return rtn;
        }

        public static ReturnEntity<User_CardDC> Add_UserBindCard(Guid iUserID, UserCardType iUserCardType, string iCardPassword)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_CardDC>>(c => c.Proxy.User_Card_Bind(iUserID, iUserCardType, iCardPassword));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<User_AmountLogDC>> Select_UserAmountLog(Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_AmountLogDC>>>(c => c.Proxy.User_AmountLog_SELECT_List(iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<User_Message_PrivateDC>> Select_UserPrivateMessage(Guid iUserID, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_Message_PrivateDC>>>(c => c.Proxy.User_Message_Private_SELECT_List(iUserID, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<bool> Update_UserModifyPass(string mpno, string newPass)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_LoginPassword_Reset(mpno, newPass));
            return rtn;
        }

        public static ReturnEntity<int> User_Invite_Count(Guid iUserID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<int>>(c => c.Proxy.User_Invite_Count(iUserID));
            return rtn;
        }

        public static ReturnEntity<bool> User_WeixinAttention_ADD(string iOpenID, DateTime iAttentionTime, string iSource)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_WeixinAttention_ADD(iOpenID, iAttentionTime, iSource));
            return rtn;
        }

        public static ReturnEntity<bool> User_WeixinAttention_Check(string iOpenID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_WeixinAttention_Check(iOpenID));
            return rtn;
        }

        public static ReturnEntity<bool> User_WeixinAttention_Remove(string iOpenID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_WeixinAttention_Remove(iOpenID));
            return rtn;
        }

        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public static ReturnEntity<User_WeixinDC> wx_User_Weixin_SELECT(string openID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_WeixinDC>>(c => c.Proxy.wx_User_Weixin_SELECT(openID));
            return rtn;
        }

        public static ReturnEntity<bool> wx_User_Weixin_BindMPNo(string openID, string mpno, string nickName, int site, string inviteCode)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.wx_User_Weixin_BindMPNo(openID, mpno, nickName, site, "", inviteCode));
            return rtn;
        }
    }
}