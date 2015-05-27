using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Common.Contract.DataContract.File;
using LazyAtHome.WCF.Common.Contract.ClientProxy;
using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Web.WebManage.CodeSource.Common;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.CodeSource.Proxy
{
    /// <summary>
    /// 通用操作wcf代理类
    /// </summary>
    public class CommonProxy
    {
        public static string UpLoadFile(int ProjectType, UploadInfoDC UploadFile, string appName)
        {
            //KeyImage = string.Empty;
            //UrlImage = string.Empty;

            //返回的图片明
            string rtn = null;

            //图片
            if (UploadFile != null)
            {
                //图片名称:id+图片类型
                //string fileName = UploadFile.FileName;
                //string imageType = fileName.Substring(fileName.IndexOf(".") + 1);
                //UploadFile.FileName = string.Format("{0}.{1}", ID, imageType);
                UploadFile.ProjectType = ProjectType;//Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["imgprojecttype"]);//工程编号 TO DO
                //UploadFile.ModuleType = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ModuleType"]);//图片类型 TO DO
                UploadFile.CustomPath = System.Configuration.ConfigurationManager.AppSettings[appName];//图片所在文件
                UploadFile.NewFileName = true;
                UploadFile.Overwrite = true;

                //上传

                ReturnEntity<UploadResultDC> rce = null;
                
                try
                {
                    rce = WCFInvokeHelper<FileClient>.Invoke<ReturnEntity<UploadResultDC>>
                    (client => client.Proxy.File_Upload(UploadFile));
                }
                catch (Exception ex)
                {
                    WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CommonProxy UpLoadFile|" + ex.Message);
                }
                if (rce != null && rce.Success)
                {
                    if (rce.OBJ.Result > 0)
                    {
                        rtn = rce.OBJ.FileName;
                    }
                }
                
            }
            return rtn;
        }





        #region 消息通知

        /// <summary>
        /// 获取消息通知列表
        /// </summary>
        /// <param name="iEventNumber"></param>
        /// <param name="iOrderNumber"></param>
        /// <param name="iRoleID"></param>
        /// <param name="iPersonnelID"></param>
        /// <param name="iTitle"></param>
        /// <param name="iLevel"></param>
        /// <param name="iStatus"></param>
        /// <param name="iPageIndex"></param>
        /// <param name="iPageSize"></param>
        /// <returns></returns>
        public static RecordCountEntity<Base_NotifyDC> GetNotifyList(string iEventNumber, string iOrderNumber, int? iRoleID, int? iPersonnelID, string iTitle, int? iLevel, int? iStatus, int iPageIndex, int iPageSize)
        {
            RecordCountEntity<Base_NotifyDC> rtn = null;
            ReturnEntity<RecordCountEntity<Base_NotifyDC>> rce = null;
            try
            {
                rce = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<RecordCountEntity<Base_NotifyDC>>>
                    (client => client.Proxy.Base_Notify_SELECT_List(iEventNumber, iOrderNumber, iRoleID, iPersonnelID, iTitle, iLevel, iStatus, iPageIndex, iPageSize));
            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CommonProxy GetNotifyList|" + ex.Message);
            }
            if (rce != null && rce.Success)
            {
                rtn = rce.OBJ;
            }
            return rtn;
        }


        /// <summary>
        /// 根据ID获取消息通知
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<Base_NotifyDC> GetNotifyBYID(int id)
        {
            ReturnEntity<Base_NotifyDC> re = null;
            try
            {

                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<Base_NotifyDC>>
                   (client => client.Proxy.Base_Notify_SELECT_Entity(id));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CommonProxy GetNotifyBYID|" + ex.Message);
            }
            return re;

        }


        /// <summary>
        /// 领取 通知消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UPDATENotifyGet(int iNotifyID, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Base_Notify_UPDATE_Get(iNotifyID, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CommonProxy UPDATENotifyGet|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 更新 通知消息 备注
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UPDATENotifyResult(int iNotifyID, string iResult, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Base_Notify_UPDATE_Result(iNotifyID, iResult, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CommonProxy UPDATENotifyResult|" + ex.Message);
            }
            return re;
        }

        /// <summary>
        /// 通知消息 处理完成/关闭
        /// </summary>
        /// <param name="iNotifyID"></param>
        /// <param name="iResult"></param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        public static ReturnEntity<bool> UPDATENotifyFinish(int iNotifyID, string iResult, int iStatus, int iMuser)
        {
            ReturnEntity<bool> re = null;

            try
            {
                re = WCFInvokeHelper<BaseClient>.Invoke<ReturnEntity<bool>>
                   (client => client.Proxy.Base_Notify_UPDATE_Finish(iNotifyID, iResult, iStatus, iMuser));
            }
            catch (System.ServiceModel.FaultException ex)
            {
                //return ex.Message;
                WriteLog.tradeLog(CodeSource.Common.ConstConfig.WRONG_LOG_PATH, "|CommonProxy UPDATENotifyFinish|" + ex.Message);
            }
            return re;
        }


        #endregion
    }
}