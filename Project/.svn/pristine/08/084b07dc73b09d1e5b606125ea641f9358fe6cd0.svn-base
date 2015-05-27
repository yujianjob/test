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
        /// <param name="iVersion"></param>
        /// <returns></returns>
        public ReturnEntity<User_InfoDC> User_Reg_CustomerService(string iParameter, LoginType iType)
        {
            var userid_Rtn = userDAL.User_Exist_Check(iParameter, iType);
            if (userid_Rtn.HasValue)
            {
                return new ReturnEntity<User_InfoDC>(userDAL.User_Info_SELECT_Entity(userid_Rtn.Value));
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
                    //_NickName = iParameter.Split('@')[0];
                    //if (_NickName.Length > 20)
                    //{
                    //    _NickName = _NickName.Substring(0, 20);
                    //}
                    break;
                default:
                    //_NickName = "ldj" + rd.Next(1000, 10000);
                    break;
            }

            //while (userDAL.User_NickName_Exist(_NickName))
            //{
            //    switch (iType)
            //    {
            //        case LoginType.LoginName:
            //            _NickName = iParameter + rd.Next(1000, 10000);
            //            break;
            //        default:
            //            _NickName = "ldj" + rd.Next(1000, 10000);
            //            break;
            //    }
            //}

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

            //生成用户
            var _User_Info = new User_InfoDC()
            {
                ID = _UserID,
                LoginName = _LoginName,
                MPNo = _MPno,
                Email = _Email,
                LoginPassword = string.Empty,
                Type = 1,
                Site = null,
                LastLoginTime = DateTime.Now,
                UserStatus = 1,
                AccountStatus = 1,
                RegistSource = 0,
            };

            var _User_Detail = new User_DetailDC()
            {
                //NickName = _NickName,
            };

            var _User_RegisterLog = new User_RegisterLogDC()
            {
                Channel = 0,
                RegisterIP = string.Empty,
                SourceID = "CustomerService",
                InviteCode = string.Empty,
            };

            if (!userDAL.User_Regist(_User_Info, _User_Detail, _User_RegisterLog))
            {
                return new ReturnEntity<User_InfoDC>(-2, "用户注册失败!");
            }

            return User_Info_SELECT_Entity(_UserID);
        }

    }
}
