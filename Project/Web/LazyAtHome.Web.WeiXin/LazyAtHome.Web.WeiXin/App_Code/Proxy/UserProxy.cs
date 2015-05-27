using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;
using LazyAtHome.WCF.UserSystem.Contract.ClientProxy;
using LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;

namespace LazyAtHome.Web.WeiXin.App_Code.Proxy
{
    public class UserProxy
    {
        public static void aaa()
        {
            ExtendClient client;
            BaseClient c;
        }

        /// <summary>
        /// 获取邀请人数
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public static ReturnEntity<int> User_Invite_Count(Guid iUserID)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<int>>(c => c.Proxy.User_Invite_Count(iUserID));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<User_CouponDC>> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo, string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<RecordCountEntity<User_CouponDC>>>(c => c.Proxy.User_Coupon_SELECT_List(iUserID, iMPNo, iCouponName, iCouponStatus, iStartDate, iEndDate, iPageIndex, iPageSize));
            return rtn;
        }

        /// <summary>
        /// 检查微信OPENID是否绑定
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Select_CheckBinding(string openID)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.wx_User_Weixin_CheckIsBind(openID));
            return rtn;
        }

        /// <summary>
        /// 返回用户信息
        /// </summary>
        /// <param name="openID"></param>
        /// <returns></returns>
        public static ReturnEntity<User_WeixinDC> Select_UserInfo(string openID)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<User_WeixinDC>>(c => c.Proxy.wx_User_Weixin_SELECT(openID));
            return rtn;
        }

        /// <summary>
        /// 用户绑定
        /// </summary>
        /// <param name="openID"></param>
        /// <param name="mpno"></param>
        /// <param name="nickName"></param>
        /// <param name="site"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Update_UserBind(string openID, string mpno, string nickName, int site,string inviteCode)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.wx_User_Weixin_BindMPNo(openID, mpno, nickName, site, "", inviteCode));
            return rtn;
        }

        public static ReturnEntity<bool> Update_UserCardBind(string openID, string cardPass)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.wx_User_Card_Bind(openID, WCF.UserSystem.Contract.Enumerate.UserCardType.LazyCard, cardPass));
            return rtn;
        }

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iConsignee">收件人</param>
        /// <param name="iDistrictID">区域编号</param>
        /// <param name="iAddress">地址</param>
        /// <param name="iMPNo">收件人手机号</param>
        /// <param name="iDefault">是否默认地址</param>
        /// <returns></returns>
        public static ReturnEntity<int> Update_UserAddressAdd(string iOpenid, string iConsignee, int iDistrictID, string iAddress, string iMPNo, bool iDefault)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<int>>(c => c.Proxy.wx_User_ConsigneeAddress_ADD(iOpenid, iConsignee, iDistrictID, iAddress, iMPNo, iDefault));
            return rtn;
        }

        /// <summary>
        /// 更新地址
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iConsignee"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iDefault"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> Update_UserAddressUpdate(int iID, string iConsignee, int iDistrictID, string iAddress, string iMPNo, bool iDefault)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.wx_User_ConsigneeAddress_UPDATE(iID, iConsignee, iDistrictID, iAddress, iMPNo, iDefault));
            return rtn;
        }

        public static ReturnEntity<IList<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC>> Select_UserAddressList(string openid)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<IList<WCF.UserSystem.Contract.DataContract.User_ConsigneeAddressDC>>>(c => c.Proxy.wx_User_ConsigneeAddress_SELECT_List(openid));
            return rtn;
        }

        public static ReturnEntity<RecordCountEntity<WCF.UserSystem.Contract.DataContract.User_Message_PrivateDC>> Select_User_Notify(string iOpenid, int iPageIndex = 1, int iPageSize = 10)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<RecordCountEntity<WCF.UserSystem.Contract.DataContract.User_Message_PrivateDC>>>(c => c.Proxy.wx_User_Message_Private_SELECT_List(iOpenid, iPageIndex, iPageSize));
            return rtn;
        }

        public static ReturnEntity<bool> Delete_User_Notify(int id)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_Message_Private_DELETE(id));
            return rtn;
        }

        public static ReturnEntity<bool> Delete_User_Address(int id)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_ConsigneeAddress_DELETE(id));
            return rtn;
        }

        public static ReturnEntity<bool> Update_User_AddressDefault(int id)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_ConsigneeAddress_SetDefault(id));
            return rtn;
        }

        public static ReturnEntity<bool> User_NickName_Check(string nickName)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_NickName_Check(nickName));
            return rtn;
        }

        public static ReturnEntity<bool> User_NickName_Change(Guid userid, string nickName)
        {
            var rtn = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_NickName_Change(userid, nickName));
            return rtn;
        }

        public static ReturnEntity<bool> User_WeixinAttention_ADD(string iOpenID, DateTime iAttentionTime, string iSource)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_WeixinAttention_ADD(iOpenID, iAttentionTime, iSource));
            return rtn;
        }

        public static ReturnEntity<bool> User_WeixinAttention_Check(string iOpenID)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_WeixinAttention_Check(iOpenID));
            return rtn;
        }

        public static ReturnEntity<bool> User_WeixinAttention_Remove(string iOpenID)
        {
            var rtn = WCFInvokeHelper<ExtendClient>.Invoke<ReturnEntity<bool>>(c => c.Proxy.User_WeixinAttention_Remove(iOpenID));
            return rtn;
        }
    }
}