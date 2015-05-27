//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using LazyAtHome.WCF.Common.Contract.DataContract.PR;
//using LazyAtHome.WCF.Common.Contract.ClientProxy;
//using LazyAtHome.Winform.FactoryPortal.Common;
//using LazyAtHome.Core.Helper;
//using LazyAtHome.Core.Infrastructure.WCF;

//namespace LazyAtHome.Winform.FactoryPortal.Proxy
//{
//    /// <summary>
//    /// 管理员wcf代理类
//    /// </summary>
//    public class OperatorProxy
//    {
//        /// <summary>
//        /// 管理员登录
//        /// </summary>
//        /// <param name="loginName">登录名</param>
//        /// <param name="loginPwd">登录密码</param>
//        /// <returns></returns>
//        public static OperatorDC Login(string loginName, string loginPwd)
//        {
//            OperatorDC entity = null;
//            ReturnEntity<OperatorDC> re = null;
//            try
//            {
//                re = WCFInvokeHelper<PRClient>.Invoke<ReturnEntity<OperatorDC>>
//                   (client => client.Proxy.PR_Operator_Login(loginName, loginPwd, WCF.Common.Contract.Enumerate.OperatorType.Manage));
//            }
//            catch (Exception ex)
//            {
//                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|OperatorProxy Login|" + ex.Message);
//            }

//            if (re != null && re.Success)
//            {
//                entity = re.OBJ;
//            }
//            else
//            {
//                //处理报错信息
//            }
//            return entity;
//        }








//        /// <summary>
//        /// 添加操作日志
//        /// </summary>
//        /// <param name="iOperatorLogDC">操作日志实体类</param>
//        /// <returns></returns>
//        public static bool OperateLog_Add(OperatorLogDC iOperatorLogDC)
//        {

//            return true;
//        }
//    }
//}
