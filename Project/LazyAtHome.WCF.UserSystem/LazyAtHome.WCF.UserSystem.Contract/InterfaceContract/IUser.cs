using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.DataContract.weixin;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IUser
    {
        /// <summary>
        /// 用户存在查询
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Exist_Check(string iParameter, LoginType iType, Guid? iUserID = null);

        /// <summary>
        /// 昵称重名验证
        /// </summary>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_NickName_Check(string iNickName, Guid? iUserID = null);

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iInviteCode"></param>
        /// <param name="iRegisterIP"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_InfoDC> User_Reg_Web(string iParameter, LoginType iType,
            string iLoginPassword, string iInviteCode, string iRegisterIP, int iSite);

        /// <summary>
        /// 网站登录
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iLoginIP"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_InfoDC> User_Login_Web(string iParameter, LoginType iType,
            string iLoginPassword, string iLoginIP);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<User_InfoDC>> User_Info_SELECT_List(string iLoginName, string iMPNo,
           string iEmail, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_InfoDC> User_Info_SELECT_Entity(Guid iID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        [OperationContract(Name = "User_Info_SELECT_Entity_Openid")]
        ReturnEntity<User_InfoDC> User_Info_SELECT_Entity(string iOpenid, OAuthType iOAuthType);

        /// <summary>
        /// 查询用户详情
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_DetailDC> User_Detail_SELECT_Entity(Guid iID);

        /// <summary>
        /// 修改用户详情
        /// </summary>
        /// <param name="iUser_DetailDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Detail_UPDATE(User_DetailDC iUser_DetailDC);

        /// <summary>
        /// 修改登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_LoginPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword);

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_LoginPassword_Reset(Guid iUserID, string iNewPassword);

        /// <summary>
        /// 重置登录密码
        /// </summary>
        /// <param name="iUserMPNo"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        [OperationContract(Name = "User_LoginPassword_Reset_MPNo")]
        ReturnEntity<bool> User_LoginPassword_Reset(string iUserMPNo, string iNewPassword);

        /// <summary>
        /// 修改支付密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iOldPassword"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_PayPassword_Change(Guid iUserID, string iOldPassword, string iNewPassword);

        /// <summary>
        /// 重置支付密码
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNewPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_PayPassword_Reset(Guid iUserID, string iNewPassword);

        /// <summary>
        /// 修改登录名
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLoginName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_LoginName_Change(Guid iUserID, string iLoginName);

        /// <summary>
        /// 修改手机号
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_MPNo_Change(Guid iUserID, string iMPNo);

        /// <summary>
        /// 修改邮箱
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Email_Change(Guid iUserID, string iEmail);

        /// <summary>
        /// 修改昵称
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_NickName_Change(Guid iUserID, string iNickName);

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
        [OperationContract]
        ReturnEntity<bool> web_User_Detail_UPDATE(Guid iUserID, string iNickName, int iSex, string iMPNo
          , string iEmail, string iRealName, string iIDCard, int? iDistrictID, string iLocation);

        /// <summary>
        /// 用户邀请数量
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> User_Invite_Count(Guid iUserID);

        /// <summary>
        /// 更新用户基本信息
        /// </summary>
        /// <param name="iUser_InfoDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Info_UPDATE(User_InfoDC iUser_InfoDC);

        /// <summary>
        /// 获取收货地址
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<User_ConsigneeAddressDC>> User_ConsigneeAddress_SELECT_List(Guid iUserID);

        /// <summary>
        /// 根据ID查询
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_ConsigneeAddressDC> User_ConsigneeAddress_SELECT_Entity(int iID);

        /// <summary>
        /// 收货地址
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> User_ConsigneeAddress_ADD(User_ConsigneeAddressDC iUser_ConsigneeAddressDC);

        /// <summary>
        /// 收货地址更新
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_ConsigneeAddress_UPDATE(User_ConsigneeAddressDC iUser_ConsigneeAddressDC);

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_ConsigneeAddress_DELETE(int iID);

        /// <summary>
        /// 设置默认地址
        /// </summary>
        /// <param name="iConsigneeAddressID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_ConsigneeAddress_SetDefault(int iID);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<User_AmountLogDC>> User_AmountLog_SELECT_List(
             Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);


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
        [OperationContract]
        ReturnEntity<RecordCountEntity<User_ScoreLogDC>> User_ScoreLog_SELECT_List(
             Guid? iUserID, string iMPNo, DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_CardDC> User_Card_Bind(Guid iUserID, UserCardType iUserCardType, string iCardPassword);

        /// <summary>
        /// 卡绑定前检查
        /// </summary>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Card_Bind_Check(UserCardType iUserCardType, string iCardPassword);

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<User_CardDC>> User_Card_SELECT_List(Guid iUserID);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<Base_CardLogDC>> Base_CardLog_SELECT_List(Guid? iUserID, int? iCardID,
            DateTime? iStartDate, DateTime? iEndDate, int iPageIndex, int iPageSize);

        /// <summary>
        /// 检查用户是否绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> wx_User_Weixin_CheckIsBind(string iOpenid);

        /// <summary>
        /// 微信用户信息获取
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_WeixinDC> wx_User_Weixin_SELECT(string iOpenid);

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
        [OperationContract]
        ReturnEntity<User_WeixinDC> wx_User_Weixin_Bind_Part(string iOpenid, string iMPNo, string iNickName,
            int iSite, string iIPAddress, string iInviteCode = null);

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
        [OperationContract]
        ReturnEntity<bool> wx_User_Weixin_BindMPNo(string iOpenid, string iMPNo, string iNickName,
            int iSite, string iIPAddress, string iInviteCode = null);

        /// <summary>
        /// 解绑用户微信
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> wx_User_Weixin_UnBindMPNo(string iOpenid, string iMPNo);

        /// <summary>
        /// 收货地址管理
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<User_ConsigneeAddressDC>> wx_User_ConsigneeAddress_SELECT_List(string iOpenid);

        /// <summary>
        /// 收货地址添加
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iConsignee"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iDefault"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> wx_User_ConsigneeAddress_ADD(
            string iOpenid, string iConsignee, int iDistrictID, string iAddress, string iMPNo, bool iDefault);

        /// <summary>
        /// 收货地址更新
        /// </summary>
        /// <param name="iID"></param>
        /// <param name="iConsignee"></param>
        /// <param name="iDistrictID"></param>
        /// <param name="iAddress"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iDefault"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> wx_User_ConsigneeAddress_UPDATE(int iID, string iConsignee,
            int iDistrictID, string iAddress, string iMPNo, bool iDefault);

        /// <summary>
        /// 收货地址删除
        /// </summary>
        /// <param name="iUser_ConsigneeAddressDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> wx_User_ConsigneeAddress_DELETE(int iID);

        /// <summary>
        /// 用户卡绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iUserCardType"></param>
        /// <param name="iCardPassword"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> wx_User_Card_Bind(string iOpenid, UserCardType iUserCardType, string iCardPassword);

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<User_CardDC>> wx_User_Card_SELECT_List(string iOpenid);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<User_Message_PrivateDC>> User_Message_Private_SELECT_List(
            Guid iUserID, int iPageIndex, int iPageSize);

        /// <summary>
        /// 消息置已读
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Message_Private_Read(int iID);

        /// <summary>
        /// 消息删除
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Message_Private_DELETE(int iID);

        /// <summary>
        /// 根据ID查询消息
        /// </summary>
        /// <param name="iID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_Message_PrivateDC> User_Message_Private_SELECT_Entity(int iID);

        /// <summary>
        /// 微信获取用户消息
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<User_Message_PrivateDC>> wx_User_Message_Private_SELECT_List(
            string iOpenid, int iPageIndex, int iPageSize);

        #endregion

        /// <summary>
        /// 修改用户昵称
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iNickName"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> wx_User_NickName_Change(string iOpenid, string iNickName);

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
        [OperationContract]
        ReturnEntity<RecordCountEntity<User_CouponDC>> User_Coupon_SELECT_List(Guid? iUserID, string iMPNo,
            string iCouponName, CouponStatus? iCouponStatus, DateTime? iStartDate, DateTime? iEndDate,
            int iPageIndex, int iPageSize);

        /// <summary>
        /// 用户绑定券检查
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Coupon_Bind_Check(Guid iUserID, string iCouponCode);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iCouponCode"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_CouponDC> User_Coupon_Bind(Guid iUserID, string iCouponCode);

        #region 微信关注日志

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="iUser_WeixinAttentionLogDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_WeixinAttention_ADD(string iOpenID, DateTime iAttentionTime, string iSource);

        /// <summary>
        /// 删除关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_WeixinAttention_Remove(string iOpenID);

        /// <summary>
        /// 用户是否关注
        /// </summary>
        /// <param name="iOpenID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_WeixinAttention_Check(string iOpenID);

        /// <summary>
        /// 微信关注统计
        /// </summary>
        /// <param name="iInternalKey"></param>
        /// <param name="iStartDate"></param>
        /// <param name="iEndDate"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<User_WeixinAttentionLogStatDC>> User_WeixinAttentionLog_Stat(string iInternalKey, DateTime iStartDate, DateTime iEndDate);

        #endregion

        /// <summary>
        /// app登录
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iLoginIP"></param>
        /// <param name="iVersion"></param>
        /// <param name="iMobileType"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_InfoDC> User_Login_App(string iParameter, LoginType iType,
            string iLoginPassword, string iLoginIP, string iVersion, int iMobileType, string iMobileFlag);

        /// <summary>
        /// app注册
        /// </summary>
        /// <param name="iParameter"></param>
        /// <param name="iType"></param>
        /// <param name="iLoginPassword"></param>
        /// <param name="iInviteCode"></param>
        /// <param name="iRegisterIP"></param>
        /// <param name="iVersion"></param>
        /// <param name="iMobileType"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_InfoDC> User_Reg_App(string iParameter, LoginType iType, string iLoginPassword,
            string iInviteCode, string iRegisterIP, string iVersion, int iMobileType, string iMobileFlag);

        /// <summary>
        /// app登出
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_LoginOut(Guid iUserID, int iMobileType);

        /// <summary>
        /// 唤醒日志
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iMobileType"></param>
        /// <param name="iMobileFlag"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_WakeUp(Guid iUserID, int iMobileType, string iMobileFlag);

        /// <summary>
        /// 投诉
        /// </summary>
        /// <param name="iUser_ComplaintsDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> User_Complaints_ADD(User_ComplaintsDC iUser_ComplaintsDC);

        #region User_RegisterSource

        /// <summary>
        /// 新增 User_RegisterSource
        /// </summary>
        /// <param name="iUser_RegisterSourceDC"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<int> User_RegisterSource_ADD(User_RegisterSourceDC iUser_RegisterSourceDC);

        /// <summary>
        /// 删除User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <param name="iMuser">操作人</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_RegisterSource_DELETE(int iID, int iMuser);

        /// <summary>
        /// 查询全部User_RegisterSource
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<RecordCountEntity<User_RegisterSourceDC>> User_RegisterSource_SELECT_List(int? iType, int iPageIndex, int iPageSize);

        /// <summary>
        /// 根据ID查询User_RegisterSource
        /// </summary>
        /// <param name="iID">主键</param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_RegisterSourceDC> User_RegisterSource_SELECT_Entity(int iID);

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
        [OperationContract]
        ReturnEntity<bool> User_Info_Update(Guid iUserID,
          string iLoginName, string iNickName, string iMPNo, string iEmail);

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<User_CardDC>> Web_User_Card_SELECT_List(Guid iUserID);

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
        [OperationContract]
        ReturnEntity<User_InfoDC> User_Reg_CustomerService(string iParameter, LoginType iType);

        /// <summary>
        /// 微信用户创建
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iSite"></param>
        /// <param name="iIPAddress"></param>
        /// <param name="iInviteCode"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<User_WeixinDC> wx_User_Weixin_Create(string iOpenid, int iSite, string iIPAddress, string iInviteCode);

        /// <summary>
        /// 微信旧用户绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        [OperationContract(Name = "wx_User_Weixin_Bind_MPNo")]
        ReturnEntity<User_WeixinDC> wx_User_Weixin_Login(string iOpenid, string iMPNo);

        /// <summary>
        /// 微信旧用户绑定
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iMPNo"></param>
        /// <returns></returns>
        [OperationContract(Name = "wx_User_Weixin_Bind_Other")]
        ReturnEntity<User_WeixinDC> wx_User_Weixin_Login(string iOpenid, string iLoginName, string iPassword);

        /// <summary>
        /// 微信用户注册
        /// </summary>
        /// <param name="iOpenid"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iPassword"></param>
        /// <param name="iEmail"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> wx_User_Weixin_Reg(string iOpenid, string iLoginName, string iPassword, string iEmail);

        /// <summary>
        /// 用户更新地理位置
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLatitude"></param>
        /// <param name="iLongitude"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> User_Info_UPDATE_Location(Guid iUserID, decimal iLatitude, decimal iLongitude);

    }
}
