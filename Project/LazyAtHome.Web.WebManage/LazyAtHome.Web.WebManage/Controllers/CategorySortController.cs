using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.CategorySort;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class CategorySortController : Controller
    {
        /// <summary>
        /// 显示产品排序搜索
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            CategorySortListModel list = new CategorySortListModel();
            list.SearchInfo = new CategorySortSearchInfo();
            list.SearchInfo.ClassID = 0;
            list.SearchInfo.ParentClassID = 0;
            return View("Index", list);
        }

        /// <summary>
        /// 根据小类ID查询
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchCategorySort(CategorySortSearchInfo search, int? pageNum, int BDVIdL1, int BDVIdL2)
        {
            if (search == null)
                search = new CategorySortSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            search.ParentClassID = BDVIdL1;
            search.ClassID = BDVIdL2;

            CategorySortListModel list = new CategorySortListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息


            //页面与接口数据处理


            //获取产品列表
            IList<Wash_CategoryDC> WashCategory = CategoryProxy.GetCategoryListBYClassID(search.ClassID);
            if (WashCategory != null)
            {
                list.CategoryList = WashCategory;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("Index", list);
        }


        public JsonResult EditCategorySort(int classid, string sort)
        {
            CodeSource.ReturnJsonEntity rjEntity = new CodeSource.ReturnJsonEntity();

            //获取登录信息
            OperatorDC item = CodeSource.SiteSession.OperatorInfo;
            //未登录返回登陆页
            if (item == null)
            {
                //界面返回信息
                rjEntity.statusCode = CodeSource.StatusCode.SessionTimeout;
                rjEntity.message = CodeSource.Common.ConstConfig.WRONG_SessionTimeoutMessage;

                return Json(rjEntity);
            }

            ReturnEntity<bool> re = CategoryProxy.EditCategorySort(classid, sort);
            if (re != null)
            {
                if (re.Success)
                {
                    //此操作比较特殊，只需要判re.Success即可
                    //成功 记录操作日志
                    OperatorLogDC logobj = new OperatorLogDC();
                    logobj.OperatorID = item.ID;
                    logobj.OperatorName = item.Name;
                    logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Order;
                    logobj.LogContent = string.Format("[{0}]对小类ID为[{1}]进行排序修改", item.Name, classid);
                    OperatorProxy.OperateLog_Add(logobj);

                    //界面返回信息
                    rjEntity.statusCode = CodeSource.StatusCode.Success;
                    rjEntity.message = string.Format("对小类ID为[{0}]进行进行排序修改成功", classid);
                    //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                    //rjEntity.navTabId = "";
                }
                else
                {
                    //失败
                    rjEntity.statusCode = CodeSource.StatusCode.Faild;
                    rjEntity.message = re.Message;
                }
            }
            else
            {
                //失败
                rjEntity.statusCode = CodeSource.StatusCode.Faild;
                rjEntity.message = CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage;
            }
            return Json(rjEntity);
        
        }
	}
}