using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.File;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Express.Contract.DataContract;

namespace LazyAtHome.Web.WebManage
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    public class BaseController : Controller
    {

        protected void AreaListSet()
        {
            LazyAtHome.Core.Infrastructure.WCF.ReturnEntity<IList<Exp_AreaDC>> list  = ExpressProxy.GetExpAreaList();
            IList<Exp_AreaDC> areaList = list.OBJ.ToList();
            if (areaList.FirstOrDefault(p => p.ID == -1) == null)
            {
                areaList.Insert(0, new Exp_AreaDC() { ID = -1, Name = "请选择" });
            }
            var query = areaList.Select(c => new { Value = c.ID, Text = c.Name });
            ViewData["AreaID"] = new SelectList(query.AsEnumerable(), "Value", "Text");
        }




        protected void SiteListSet()
        {
            IList<Base_SiteDC> siteList = BaseInfoProxy.Base_GetAllSite();
            siteList.Insert(0, new Base_SiteDC() { ID = -1, Name = "请选择" });
            var query = siteList.Select(c => new { Value = c.ID, Text = c.Name });
            ViewData["Site"] = new SelectList(query.AsEnumerable(), "Value", "Text");
        }

        protected void SiteListSet(string Name)
        {
            IList<Base_SiteDC> siteList = BaseInfoProxy.Base_GetAllSite();
            siteList.Insert(0, new Base_SiteDC() { ID = -1, Name = "请选择" });
            var query = siteList.Select(c => new { Value = c.ID, Text = c.Name });
            ViewData[Name] = new SelectList(query.AsEnumerable(), "Value", "Text");
        }

        

        protected void RoleListSet()
        {
            IList<RoleDC> siteList = OperatorProxy.GetRoleList();

            if (siteList.FirstOrDefault(p => p.ID == -1) == null)
            {
                siteList.Insert(0, new RoleDC() { ID = -1, Name = "请选择" });
            }

            
            var query = siteList.Select(c => new { Value = c.ID, Text = c.Name });
            ViewData["Role"] = new SelectList(query.AsEnumerable(), "Value", "Text");

        }

        /// <summary>
        /// 获取固定日期
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="weekday"></param>
        /// <param name="Number"></param>
        /// <returns></returns>
        public DateTime GetWeekUpOfDate(DateTime dt,DayOfWeek weekday,int Number)
        {
            int wd1=(int)weekday;
            int wd2=(int)dt.DayOfWeek;
            return wd2 == wd1 ? dt.AddDays(7 * Number) : dt.AddDays(7 * Number - wd2 + wd1);
        }


        /// <summary>
        /// 结束时间加上一天 以便查询
        /// </summary>
        /// <param name="iDateTime"></param>
        /// <returns></returns>
        public DateTime? FormatDateTimeAddOneDay(DateTime? iDateTime)
        {
            if (iDateTime != null)
            {
                return ((DateTime)iDateTime).AddDays(1);
            }
            else
            {
                return iDateTime;
            }
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="imagetype"></param>
        /// <returns></returns>
        public JsonResult UploadImage(string imagetype, string appname)
        {
            //HttpPostedFileBase fileData = Request.Files["categoryimagel"];
            HttpPostedFileBase fileData = Request.Files[imagetype];
            if (fileData != null && fileData.ContentLength > 0)
            {
                //获取图片 并调用文件服务器上传图片
                UploadInfoDC fileInfoModel = new UploadInfoDC();
                //begin
                long fileSize = fileData.InputStream.Length;
                fileInfoModel.PostArray = new byte[fileSize];
                fileData.InputStream.Read(fileInfoModel.PostArray, 0, (int)fileSize);
                fileInfoModel.FileName = fileData.FileName;

                //图片上传
                string FileName = CommonProxy.UpLoadFile((int)CodeSource.Common.ProjectType.Wash, fileInfoModel, appname);
                if (!string.IsNullOrEmpty(FileName))
                {
                    return Json(new { status = 1, filename = FileName, type = imagetype, message = "上传成功" });
                }
                else
                {
                    return Json(new { status = 0, filename = "", type = "", message = "上传失败" });
                }
            }

            else
                return Json(new { status = 0, filename = "", type = "", message = "上传失败" });
        }





        /// <summary>  
        /// 得到本周第一天(以星期天为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekFirstDaySun(DateTime datetime)
        {
            //星期天为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期六为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekLastDaySat(DateTime datetime)
        {
            //星期六为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            int daydiff = (7 - weeknow) - 1;

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }  











        ///// <summary>
        ///// 在菜单栏显示信息
        ///// </summary>
        //public void ShowMessage(string Title, string Message, CodeSource.Common.MessageTypeEnum messageType = CodeSource.Common.MessageTypeEnum.Info)
        //{
        //    string msg = "【" + Title + "】 " + Message;
        //    TempData["_MenuMessage"] = msg;

        //    string msgType = string.Empty;
        //    switch (messageType)
        //    {
        //        case CodeSource.Common.MessageTypeEnum.Info:
        //            msgType = "green";
        //            break;
        //        case CodeSource.Common.MessageTypeEnum.Alear:
        //            msgType = "orange";
        //            break;
        //        case CodeSource.Common.MessageTypeEnum.Error:
        //            msgType = "red";
        //            break;
        //    }
        //    TempData["_MenuMessageType"] = msgType;
        //}
	}
}