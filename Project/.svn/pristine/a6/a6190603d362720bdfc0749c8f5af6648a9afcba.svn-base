using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WeiXin4.App_Code.Proxy
{
    class UserProxycs
    {
        public static ReturnEntity<bool> User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_Exist_Check(iParameter, iType, iUserID));
            return rtn;
        }

        public static ReturnEntity<bool> wx_User_Weixin_BindMPNo(string openID, string mpno, string nickName, int site, string inviteCode)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.wx_User_Weixin_BindMPNo(openID, mpno, nickName, site, "", inviteCode));
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

        public static ReturnEntity<User_WeixinDC> wx_User_Weixin_SELECT(string openID)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_WeixinDC>>(c => c.Proxy.wx_User_Weixin_SELECT(openID));
            return rtn;
        }

        public static ReturnEntity<User_WeixinDC> wx_User_Weixin_Create(string iOpenid, int iSite, string iIPAddress, string iInviteCode)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_WeixinDC>>(c => c.Proxy.wx_User_Weixin_Create(iOpenid, iSite, iIPAddress, iInviteCode));
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

        public static ReturnEntity<RecordCountEntity<User_CouponDC>> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo, string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_CouponDC>>>(c => c.Proxy.User_Coupon_SELECT_List(iUserID, iMPNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<User_CouponDC> User_Coupon_Bind(Guid iUserID, string iCouponCode)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<User_CouponDC>>(c => c.Proxy.User_Coupon_Bind(iUserID, iCouponCode));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<User_AmountLogDC>> User_AmountLog_SELECT_List(Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<UserClient>.Invoke<ReturnEntity<RecordCountEntity<User_AmountLogDC>>>(c => c.Proxy.User_AmountLog_SELECT_List(iUserID, iMPNo, iStartDate, iEndDate, iPageIndex, iPageSize));
            return rtn;
        }
    }
}
