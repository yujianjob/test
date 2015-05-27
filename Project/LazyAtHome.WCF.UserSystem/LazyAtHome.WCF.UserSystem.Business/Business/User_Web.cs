using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.UserSystem.Contract.DataContract;
using LazyAtHome.WCF.UserSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.UserSystem.Business.Business
{
    public partial class User
    {

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
        public ReturnEntity<User_InfoDC> User_Reg_Web(string iParameter, LoginType iType,
            string iLoginPassword, string iInviteCode, string iRegisterIP, int iSite)
        {
            if (userDAL.User_Exist_Check(iParameter, iType).HasValue)
            {
                return new ReturnEntity<User_InfoDC>(-1, "注册帐号已存在");
            }

            var _UserID = Guid.NewGuid();
            string _LoginName = null;
            string _MPno = null;
            string _Email = null;
            //string _NickName = null;

            var rd = new Random(this.GetHashCode());

            switch (iType)
            {
                case LoginType.LoginName:
                case LoginType.MPNo:
                    //_NickName = iParameter;
                    break;
                case LoginType.Email:
                    break;
                default:
                    //_NickName = "ldj" + rd.Next(1000, 10000);
                    break;
            }

            switch (iType)
            {
                case LoginType.LoginName://用户名注册
                    _LoginName = iParameter;
                    break;
                case LoginType.MPNo://手机号注册
                    _MPno = iParameter;
                    break;
                case LoginType.Email://邮箱注册
                    _Email = iParameter;
                    break;
                default:
                    throw new Exception("注册类型错误");
            }
            Guid? iUserID;

            var regRtn = User_register(_LoginName, _MPno, _Email, iLoginPassword, RegChannel.Web, iSite.ToString(), iSite, iRegisterIP, iInviteCode, out iUserID);

            if (regRtn.Success)
            {
                return User_Login_Web(iParameter, iType, iLoginPassword, iRegisterIP);
            }
            else
            {
                return new ReturnEntity<User_InfoDC>(regRtn.Code, regRtn.Message);
            }
            ////获取邀请用户
            //Guid? _InviteUserID = userDAL.User_Exist_Check(iInviteCode, LoginType.RecommendedCode, null);

            ////生成用户
            //var _User_Info = new User_InfoDC()
            //{
            //    ID = _UserID,
            //    LoginName = _LoginName,
            //    MPNo = _MPno,
            //    Email = _Email,
            //    LoginPassword = LazyAtHome.Core.Helper.CryptoHelper.Encrypt(iLoginPassword, LazyAtHome.Core.Enumerate.CryptoMode.MD5),
            //    Type = 1,
            //    Site = iSite,
            //    LastLoginTime = DateTime.Now,
            //    UserStatus = 1,
            //    AccountStatus = 1,
            //    RegistSource = 1,
            //};

            //var _User_Detail = new User_DetailDC()
            //{
            //    //NickName = _NickName,
            //};

            //var _User_RegisterLog = new User_RegisterLogDC()
            //{
            //    Channel = 5,
            //    RegisterIP = iRegisterIP,
            //    SourceID = iSite.ToString(),
            //    InviteCode = iInviteCode,
            //};

            //if (!userDAL.User_Regist(_User_Info, _User_Detail, _User_RegisterLog))
            //{
            //    return new ReturnEntity<User_InfoDC>(-2, "用户注册失败!");
            //}

        }


        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="iUserID"></param>
        /// <param name="iLoginName"></param>
        /// <param name="iNickName"></param>
        /// <param name="iMPNo"></param>
        /// <param name="iEmail"></param>
        /// <returns></returns>
        public ReturnEntity<bool> User_Info_Update(Guid iUserID,
            string iLoginName, string iNickName, string iMPNo, string iEmail)
        {
            if (!string.IsNullOrWhiteSpace(iLoginName))
            {
                if (userDAL.User_Exist_Check(iLoginName, Contract.Enumerate.LoginType.LoginName, iUserID).HasValue)
                {
                    return new ReturnEntity<bool>(-1, "用户名已存在");
                }
            }
            if (!string.IsNullOrWhiteSpace(iNickName))
            {
                if (userDAL.User_NickName_Exist(iNickName, iUserID))
                {
                    return new ReturnEntity<bool>(-1, "昵称已存在");
                }
            }

            if (!string.IsNullOrWhiteSpace(iMPNo))
            {
                if (userDAL.User_Exist_Check(iMPNo, Contract.Enumerate.LoginType.MPNo, iUserID).HasValue)
                {
                    return new ReturnEntity<bool>(-1, "手机号已存在");
                }
            }

            if (!string.IsNullOrWhiteSpace(iEmail))
            {
                if (userDAL.User_Exist_Check(iEmail, Contract.Enumerate.LoginType.Email, iUserID).HasValue)
                {
                    return new ReturnEntity<bool>(-1, "邮箱已存在");
                }
            }
            //------------------------------------------------------
            if (!string.IsNullOrWhiteSpace(iLoginName))
            {
                if (!userDAL.User_LoginName_Change(iUserID, iLoginName))
                {
                    return new ReturnEntity<bool>(-1, "用户名更新失败");
                }
            }
            if (!string.IsNullOrWhiteSpace(iNickName))
            {
                if (!userDAL.User_NickName_Change(iUserID, iNickName))
                {
                    return new ReturnEntity<bool>(-1, "昵称更新失败");
                }
            }

            if (!string.IsNullOrWhiteSpace(iMPNo))
            {
                if (!userDAL.User_MPNo_Change(iUserID, iMPNo))
                {
                    return new ReturnEntity<bool>(-1, "手机号更新失败");
                }
            }

            if (!string.IsNullOrWhiteSpace(iEmail))
            {
                if (!userDAL.User_Email_Change(iUserID, iEmail))
                {
                    return new ReturnEntity<bool>(-1, "邮箱更新失败");
                }
            }

            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 用户卡列表获取
        /// </summary>
        /// <param name="iUserID"></param>
        /// <returns></returns>
        public ReturnEntity<IList<User_CardDC>> Web_User_Card_SELECT_List(Guid iUserID)
        {
            var cardList = userDAL.User_Card_SELECT_List(iUserID);

            if (cardList != null)
            {
                foreach (var item in cardList)
                {
                    var logList = userDAL.Base_CardLog_SELECT_List(iUserID, item.CardID, null, null, 1, 10);
                    if (logList != null)
                        item.CardLogList = logList.ReturnList;
                }
            }
            return new ReturnEntity<IList<User_CardDC>>(cardList);
        }
    }
}
