using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Interface.IDAL
{
    public interface IUserDAL
    {
        /// <summary>
        /// 用户存在查询(存在返回True)
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        Guid? User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null);

        /// <summary>
        /// 昵称重名验证
        /// </summary>
        /// <param name="iNickName"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        bool User_NickName_Exist(string iNickName, Guid? iUserID = null);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <param name="iUser_DetailDC"></param>
        /// <param name="iUser_RegisterLogDC"></param>
        /// <returns></returns>
        bool User_Regist(User_InfoDC iUser_InfoDC, User_DetailDC iUser_DetailDC, User_RegisterLogDC iUser_RegisterLogDC);

        //后台用户列表
        RecordCountEntity<User_InfoDC> User_Info_SELECT_List(string iLoginName, string iMPNo,
            string iEmail, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 修改用户详情
        /// </summary>
        /// <param name="iPR_OperatorDC"></param>
        /// <returns></returns>
        bool User_Detail_UPDATE(User_DetailDC iUser_DetailDC);

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        User_InfoDC User_Info_SELECT_Entity(Guid iID);

        /// <summary>
        /// 用户获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        User_InfoDC User_Info_SELECT_Entity(string iOpenid, OAuthType iOAuthType);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        User_InfoDC User_Info_SELECT_Entity(string iMPNo);

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <returns></returns>
        User_InfoDC User_Info_SELECT_Entity(string iParameter, LoginType iType, string iLoginPassword);

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        User_DetailDC User_Detail_SELECT_Entity(Guid iID);

        ///// <summary>
        ///// 注册日志
        ///// </summary>
        ///// <param name="iUser_RegisterLogDC"></param>
        ///// <returns></returns>
        //bool User_RegisterLog_ADD(User_RegisterLogDC iUser_RegisterLogDC);

        /// <summary>
        /// 登录日志
        /// </summary>
        /// <param name="iUser_LoginLogDC"></param>
        /// <returns></returns>
        bool User_LoginLog_ADD(User_LoginLogDC iUser_LoginLogDC);

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        bool User_LoginPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword);

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        bool User_LoginPassword_Reset(Guid iUserID, string iNewPassword);

        /// <summary>
        /// 修改支付密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        bool User_PayPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword);

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        bool User_PayPassword_Reset(Guid iUserID, string iNewPassword);

        /// <summary>
        /// 修改登录名
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLoginName"></param>
        /// <returns></returns>
        bool User_LoginName_Change(Guid iUserID, string iLoginName);

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        bool User_MPNo_Change(Guid iUserID, string iMPNo);

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        bool User_Email_Change(Guid iUserID, string iEmail);

        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        bool User_NickName_Change(Guid iUserID, string iNickName);

        /// <summary>
        /// 用户邀请数量
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        int User_Invite_Count(Guid iUserID);

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <returns></returns>
        bool User_Info_UPDATE(User_InfoDC iUser_InfoDC);

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        IList<User_ConsigneeAddressDC> User_ConsigneeAddress_SELECT_List(Guid iUserID);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        User_ConsigneeAddressDC User_ConsigneeAddress_SELECT_Entity(int iID);

        /// <summary>
        /// 收货地址添加
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        int User_ConsigneeAddress_ADD(User_ConsigneeAddressDC iUser_ConsigneeAddressDC);

        /// <summary>
        /// 收货地址更新
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        bool User_ConsigneeAddress_UPDATE(User_ConsigneeAddressDC iUser_ConsigneeAddressDC);

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="iConsigneeAddressID"></param>
        /// <returns></returns>
        bool User_ConsigneeAddress_SetDefault(int iID);

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        bool User_ConsigneeAddress_DELETE(int iID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<User_AmountLogDC> User_AmountLog_SELECT_List(
            Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<User_ScoreLogDC> User_ScoreLog_SELECT_List(
            Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 用户卡绑定
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        int User_Card_Bind(Guid iUserID, UserCardType iUserCardType, string iCardPassword);

        /// <summary>
        /// 卡绑定前检查
        /// </summary>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        int User_Card_Bind_Check(UserCardType iUserCardType, string iCardPassword);

        /// <summary>
        /// 获取用户卡列表
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        IList<User_CardDC> User_Card_SELECT_List(Guid iUserID);

        /// <summary>
        /// 获取用户卡
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardID"></param>
        /// <returns></returns>
        User_CardDC User_Card_SELECT_Entity(Guid iUserID, int iUserCardID);

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
        RecordCountEntity<Base_CardLogDC> Base_CardLog_SELECT_List(Guid? iUserID, int? iCardID,
           DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);


        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="iUser_WeixinDC"></param>
        ///// <returns></returns>
        //int User_Weixin_ADD(User_WeixinDC iUser_WeixinDC);

        ///// <summary>
        ///// 根据ID查询
        ///// </summary>
        ///// <param name="iOpenid"></param>
        ///// <returns></returns>
        //User_WeixinDC User_Weixin_SELECT_BYID(string iOpenid);

        /// <summary>
        /// 微信ID获取用户ID
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        Guid? User_Weixin_SELECT_UserID(string iOpenid);

        /// <summary>
        /// 微信ID已绑定用户
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        bool wx_User_Weixin_CheckIsBind(string iOpenid);

        ///// <summary>
        ///// 微信参数验证
        ///// </summary>
        ///// <param name="iParameter"></param>
        ///// <param name="iType"></param>
        ///// <returns></returns>
        //bool User_Weixin_Exist_Check(string iOpenid);

        ///// <summary>
        ///// 更新用户ID
        ///// </summary>
        ///// <param name="iID"></param>
        ///// <param name="iUserID"></param>
        ///// <returns></returns>
        //bool User_Weixin_UPDATE_UserID(int iID, Guid iUserID);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_OAuthDC"></param>
        /// <returns></returns>
        int User_OAuth_ADD(User_OAuthDC iUser_OAuthDC);

        /// <summary>
        /// 删除授权
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOpenid"></param>
        /// <param name="iOAuthType"></param>
        /// <returns></returns>
        bool User_OAuth_DELETE(Guid iUserID, string iOpenid, OAuthType iOAuthType);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        bool User_OAuth_DELETE(int iID);

        /// <summary>
        /// 查询全部
        /// </summary>
        /// <returns></returns>
        IList<User_OAuthDC> User_OAuth_SELECT_UserID(Guid iUserID);

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
        RecordCountEntity<User_Message_PrivateDC> User_Message_Private_SELECT_List(
            Guid iUserID, int iPageIndex, int iPageSize);

        /// <summary>
        /// 消息置已读
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        bool User_Message_Private_Read(int iID);

        /// <summary>
        /// 消息删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        bool User_Message_Private_DELETE(int iID);

        /// <summary>
        /// 根据ID查询消息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        User_Message_PrivateDC User_Message_Private_SELECT_Entity(int iID);

        #endregion

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
        RecordCountEntity<User_CouponDC> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo, string iCouponName,
            CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserCouponID"></param>
        /// <returns></returns>
        User_CouponDC User_Coupon_SELECT_Entity(int iUserCouponID);

        /// <summary>
        /// 用户绑定券检查
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        int User_Coupon_Bind_Check(Guid iUserID, string iCouponCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        int User_Coupon_Bind(Guid iUserID, string iCouponCode);

        #region 微信关注日志

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_WeixinAttentionLogDC"></param>
        /// <returns></returns>
        bool User_WeixinAttentionLog_ADD(User_WeixinAttentionLogDC iUser_WeixinAttentionLogDC);

        /// <summary>
        /// 删除关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        bool User_WeixinAttentionLog_DELETE(string iOpenID);

        /// <summary>
        /// 用户是否关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        bool User_WeixinAttention_Check(string iOpenID);

        /// <summary>
        /// 微信关注统计
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <returns></returns>
        IList<User_WeixinAttentionLogStatDC> User_WeixinAttentionLog_Stat(string iInternalKey, DateTime iStartDate, DateTime iEndDate);

        #endregion

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_AppInfoDC"></param>
        /// <returns></returns>
        int User_AppInfo_ADD(User_AppInfoDC iUser_AppInfoDC);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <returns></returns>
        bool User_LoginOut(Guid iUserID, int iMobileType);

        /// <summary>
        /// 投诉
        /// </summary>
        /// <param name="iUser_ComplaintsDC"></param>
        /// <returns></returns>
        int User_Complaints_ADD(User_ComplaintsDC iUser_ComplaintsDC);

        #region User_RegisterSource

        /// <summary>
        /// 新增 User_RegisterSource
        /// </summary>
        /// <param name="iUser_RegisterSourceDC"></param>
        /// <returns></returns>
        int User_RegisterSource_ADD(User_RegisterSourceDC iUser_RegisterSourceDC);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <returns></returns>
        bool User_RegisterSource_Exist_Check_InternalKey(string iInternalKey);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="iSourceID"></param>
        /// <returns></returns>
        bool User_RegisterSource_Exist_Check_SourceID(string iSourceID);

        /// <summary>
        /// 删除User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        bool User_RegisterSource_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询全部User_RegisterSource
        /// </summary>
        /// <param name="iType"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        RecordCountEntity<User_RegisterSourceDC> User_RegisterSource_SELECT_List(int? iType, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        User_RegisterSourceDC User_RegisterSource_SELECT_Entity(int iID);

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        string User_RegisterSource_SELECT_InternalKey(string iSourceID);
        #endregion

        /// <summary>
        /// 操作员邀请码验证
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns></returns>
        bool PR_Operator_Exist(string iCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNodeID"></param>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        bool User_Info_UPDATE_Location(Guid iUserID, int? iNodeID, decimal iLatitude, decimal iLongitude);

    }
}
