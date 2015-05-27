using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Survey;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class SurveyController : BaseController
    {
        /// <summary>
        /// 问卷调查展示页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return SearchSurvey(null, null);
        }


        /// <summary>
        /// 活动搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchSurvey(SurveySearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new SurveySearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            SurveyListModel list = new SurveyListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理


            RecordCountEntity<Survey_MainDC> rce = SurveyProxy.GetSurveyList(search.SurveyName, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.SurveyList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            return View("Index", list);
        }


        /// <summary>
        /// 问卷预览
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public ActionResult ViewSurvey(int sid)
        {
            Survey_MainDC ServerEntity = null;
            //获取空问卷
            ReturnEntity<Survey_MainDC> re = SurveyProxy.GetSurveyBYID(sid);
            if (re != null)
            {
                if (re.Success)
                {
                    ServerEntity = re.OBJ;
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            //把Survey_MainDC转化成客户端SurveyViewModel
            SurveyViewModel ClientEntity = GetSurveyView(ServerEntity, null);
            //SurveyViewModel ClientEntity = new SurveyViewModel();
            //if (ServerEntity != null)
            //{
            //    //基本字段赋值
            //    ClientEntity = new SurveyViewModel(ServerEntity);

            //    //问题集合赋值
            //    ClientEntity.QuestionViewList = new List<QuestionViewModel>();
            //    foreach (Survey_QuestionDC item_Question in ServerEntity.QuestionList)
            //    {
            //        QuestionViewModel qvm = new QuestionViewModel(item_Question);

            //        //答案集合赋值
            //        qvm.OptionsViewList = new List<OptionsViewModel>();
            //        foreach (Survey_OptionsDC item_Options in item_Question.OptionsList)
            //        {
            //            OptionsViewModel ovm = new OptionsViewModel(item_Options);

            //            //处理用户选择的答案
            //            ovm.AnsValue = 3;

            //            qvm.OptionsViewList.Add(ovm);
            //        }

            //        ClientEntity.QuestionViewList.Add(qvm);

            //    }
            //}

            return View("ViewSurvey", ClientEntity);
        }

        /// <summary>
        /// 展示用户的问卷
        /// </summary>
        /// <param name="aid"></param>
        /// <returns></returns>
        public ActionResult ViewUserSurvey(int aid)
        {
            Survey_AnswerDC AnswerEntity = null;
            ReturnEntity<Survey_AnswerDC> re = SurveyProxy.GetAnswerBYID(aid);
            if (re != null)
            {
                if (re.Success)
                {
                    AnswerEntity = re.OBJ;
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            Survey_MainDC ServerEntity = null;
            //获取空问卷
            ReturnEntity<Survey_MainDC> re_Survey = SurveyProxy.GetSurveyBYID(AnswerEntity.SurID);
            if (re != null)
            {
                if (re.Success)
                {
                    ServerEntity = re_Survey.OBJ;
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re_Survey.Message);
                }
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            //把Survey_MainDC转化成客户端SurveyViewModel
            SurveyViewModel ClientEntity = GetSurveyView(ServerEntity, AnswerEntity);

            return View("ViewSurvey", ClientEntity);
        }


        /// <summary>
        /// 根据Survey_MainDC和用户答题详情Survey_AnswerDC 转化成客户端SurveyViewModel
        /// </summary>
        /// <param name="ServerEntity"></param>
        /// <param name="AnswerEntity"></param>
        /// <returns></returns>
        private SurveyViewModel GetSurveyView(Survey_MainDC ServerEntity, Survey_AnswerDC AnswerEntity)
        {
            //把Survey_MainDC转化成客户端SurveyViewModel
            SurveyViewModel ClientEntity = new SurveyViewModel();
            if (ServerEntity != null)
            {
                //基本字段赋值
                ClientEntity = new SurveyViewModel(ServerEntity);

                //问题集合赋值
                ClientEntity.QuestionViewList = new List<QuestionViewModel>();
                foreach (Survey_QuestionDC item_Question in ServerEntity.QuestionList)
                {
                    QuestionViewModel qvm = new QuestionViewModel(item_Question);

                    //答案集合赋值
                    qvm.OptionsViewList = new List<OptionsViewModel>();
                    foreach (Survey_OptionsDC item_Options in item_Question.OptionsList)
                    {
                        OptionsViewModel ovm = new OptionsViewModel(item_Options);

                        //处理用户选择的答案
                        if (AnswerEntity != null && AnswerEntity.DetailList != null)
                        {
                            Survey_AnswerDetailDC ad = AnswerEntity.DetailList.FirstOrDefault(p => p.QuID == ovm.QuID);
                            if (ad != null)
                            {
                                ovm.AnsValue = ad.AnsValue;
                                if (ovm.Type == 1)
                                {
                                    ovm.AnsContent = ad.AnsContent;
                                }
                                
                            }
                        }
                        

                        qvm.OptionsViewList.Add(ovm);
                    }

                    ClientEntity.QuestionViewList.Add(qvm);

                }
            }

            if (AnswerEntity != null)
            {
                ClientEntity.UserSource = AnswerEntity.UserSource;
            }


            //if (ServerEntity.CommitStatus == 1 && (System.DateTime.Now > ServerEntity.BeginDate))
            //{
            //    //调用计算选择数量

            //    ReturnEntity<bool> re = SurveyProxy.StatSurvey(ServerEntity.ID);

            //    if (re != null)
            //    {
            //        if (re.Success)
            //        {

            //        }
            //        else
            //        {
            //            //处理报错逻辑   
            //            ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
            //        }
            //    }
            //    else
            //    {
            //        //处理报错逻辑   
            //        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            //    }

            //}

            return ClientEntity;
        }

        /// <summary>
        /// 展示问卷调查编辑页
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult EditSurvey(int sid)
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

                return Json(rjEntity, JsonRequestBehavior.AllowGet);
            }


            SurveyModel entity = null;
            if (sid == 0)
            {
                //说明是添加，实例化对象
                entity = new SurveyModel();
                entity.SurveyInfo = new Survey_MainDC();
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Survey_MainDC> re = SurveyProxy.GetSurveyBYID(sid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new SurveyModel();
                        entity.SurveyInfo = re.OBJ;
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
                    }
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                }

            }



            return View("EditSurvey", entity);
        }


        /// <summary>
        /// 问卷调查编辑
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveSurvey(SurveyModel entity, FormCollection coll)
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


            entity.SurveyInfo.BeginDate = Convert.ToDateTime(coll["SurveyInfo_BeginDate"].ToString());
            entity.SurveyInfo.EndDate = Convert.ToDateTime(coll["SurveyInfo_EndDate"].ToString()).AddHours(23).AddMinutes(59).AddSeconds(59);

            //修改人ID
            entity.SurveyInfo.Obj_Muser = item.ID;

            if (entity.SurveyInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.SurveyInfo.Obj_Cuser = item.ID;

                ReturnEntity<int> re = SurveyProxy.AddSurvey(entity.SurveyInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web;
                        logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]问卷调查信息成功", item.Name, entity.SurveyInfo.ID, entity.SurveyInfo.Title);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增名称为[{0}]活动信息成功", entity.SurveyInfo.Title);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "surveylist";
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
            }
            else
            {
                //说明是编辑
                ReturnEntity<bool> re = SurveyProxy.UpdateSurvey(entity.SurveyInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        if (re.OBJ)
                        {
                            //成功 记录操作日志
                            OperatorLogDC logobj = new OperatorLogDC();
                            logobj.OperatorID = item.ID;
                            logobj.OperatorName = item.Name;
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]问卷调查信息成功", item.Name, entity.SurveyInfo.ID, entity.SurveyInfo.Title);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]问卷调查信息成功", entity.SurveyInfo.ID, entity.SurveyInfo.Title);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "surveylist";
                        }
                        else
                        {
                            //失败
                            rjEntity.statusCode = CodeSource.StatusCode.Faild;
                            rjEntity.message = CodeSource.Common.ConstConfig.WRONG_UpdateErrorMessage;
                        }
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
            }

            //}


            return Json(rjEntity);
        }


        /// <summary>
        /// 删除问卷
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        public JsonResult DeleteSurvey(int sid)
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

            ReturnEntity<bool> re = SurveyProxy.DeleteSurvey(sid, item.ID);
            if (re != null)
            {
                if (re.Success)
                {
                    if (re.OBJ)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web;
                        logobj.LogContent = string.Format("[{0}]删除ID为[{1}]问卷调查信息成功", item.Name, sid);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("删除ID为[{0}]问卷调查信息成功", sid);
                        //rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "surveylist";
                    }
                    else
                    {
                        //失败
                        rjEntity.statusCode = CodeSource.StatusCode.Faild;
                        rjEntity.message = CodeSource.Common.ConstConfig.WRONG_DeleteErrorMessage;
                    }
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

        /// <summary>
        /// 展示问卷问题列表页
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SearchQuestion(QuestionSearchInfo search, int id, int commitstatus)
        {
            if (search == null)
                search = new QuestionSearchInfo();


            QuestionListModel list = new QuestionListModel();
            list.SearchInfo = search;
            list.SearchInfo.SurveyID = id;
            list.CommitStatus = commitstatus;

            //IList<Survey_QuestionDC> list = null;
            ReturnEntity<IList<Survey_QuestionDC>> re = SurveyProxy.GetQuestionList(id);

            if (re != null)
            {
                list.QuestionList = re.OBJ;
            }
            else
            {
                list = new QuestionListModel();
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            return View("QuestionIndex", list);
        }


        /// <summary>
        /// 展示问卷问题编辑页
        /// </summary>
        /// <param name="qid">问卷问题ID</param>
        /// <param name="sid">问卷ID</param>
        /// <param name="commitstatus">发布状态</param>
        /// <returns></returns>
        public ActionResult EditQuestion(int qid, int sid, int commitstatus)
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

                return Json(rjEntity, JsonRequestBehavior.AllowGet);
            }


            QuestionModel entity = null;
            if (qid == 0)
            {
                //说明是添加，实例化对象
                entity = new QuestionModel();
                entity.QuestionInfo = new Survey_QuestionDC();
                entity.QuestionInfo.SurID = sid;

                //默认无其他自定义选项
                entity.isOther = false;

                //添加 是可以编辑的
                entity.CanEdit = true;
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Survey_QuestionDC> re = SurveyProxy.GetQuestionBYID(qid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new QuestionModel();
                        entity.QuestionInfo = re.OBJ;


                        if (commitstatus == 0)
                        {
                            //可以编辑的
                            entity.CanEdit = true;
                        }
                        else
                        {
                            entity.CanEdit = false;
                        }
                    }
                    else
                    {
                        //处理报错逻辑   
                        ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, re.Message);
                    }
                }
                else
                {
                    //处理报错逻辑   
                    ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
                }

            }

            //补全 选型List
            List<Survey_OptionsDC> tmpList = new List<Survey_OptionsDC>();//entity.QuestionInfo.OptionsList;

            if (entity.QuestionInfo.OptionsList != null)
            {
                tmpList.AddRange(entity.QuestionInfo.OptionsList);
            }

            //判断是否有自定义选项
            foreach (Survey_OptionsDC tmp in tmpList)
            {
                if (tmp.Type == 1)
                {
                    entity.isOther = true;

                    tmpList.Remove(tmp);

                    break;
                }
            }
            
            //定义8个选项
            int count = 8;
            int existing = tmpList.Count;
            for (int i = 0; i < count - existing; i++)
            {
                tmpList.Add(new Survey_OptionsDC());
            }

            
            entity.QuestionInfo.OptionsList = tmpList;


            //if (entity.QuestionInfo.OptionsList == null)
            //{
            //    entity.QuestionInfo.OptionsList = new List<Survey_OptionsDC>();


            //    for (int i = 0; i < count; i++)
            //    {
            //        entity.QuestionInfo.OptionsList.Add(new Survey_OptionsDC());
            //    }

            //}
            //else
            //{
            //    for (int i = 0; i < count - entity.QuestionInfo.OptionsList.Count; i++)
            //    {
            //        entity.QuestionInfo.OptionsList.Add(new Survey_OptionsDC());
            //    }
            //}


            ////判断是否有自定义选项
            //foreach (Survey_OptionsDC tmp in entity.QuestionInfo.OptionsList)
            //{
            //    if (tmp.Type == 1)
            //    {
            //        entity.isOther = true;
            //        break;
            //    }
            //}



            return View("EditQuestion", entity);
        }



        /// <summary>
        /// 问卷题目编辑
        /// </summary>
        /// <returns></returns>
        public JsonResult SaveQuestion(QuestionModel entity, FormCollection coll)
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



            //修改人ID
            entity.QuestionInfo.Obj_Muser = item.ID;


            entity.QuestionInfo.OptionsList = new List<Survey_OptionsDC>();
            if (entity.QuestionInfo.Type == 3)
            {
                //说明是问答题
                Survey_OptionsDC Options = new Survey_OptionsDC();
                Options.SurID = entity.QuestionInfo.SurID;
                Options.Name = string.Empty;
                Options.Title = string.Empty;
                Options.Type = 1;   //文字

                entity.QuestionInfo.OptionsList.Add(Options);
            }
            else
            {
                //说明是选择题
                int init = 65;
                
                string selectoptions = coll["so"].ToString();

                string[] so = selectoptions.Split(',');

                if (so.Length > 0)
                {

                    int tmp = init;
                    foreach (string selectoption in so)
                    {

                        if (selectoption != string.Empty)
                        {

                            char c = Convert.ToChar(tmp++);

                            Survey_OptionsDC Options = new Survey_OptionsDC();
                            Options.SurID = entity.QuestionInfo.SurID;
                            Options.Name = c.ToString() + ".";
                            Options.Title = selectoption;
                            Options.Type = 0;   //选项

                            entity.QuestionInfo.OptionsList.Add(Options);
                        }
                    }
                }

                if (entity.isOther)
                {
                    Survey_OptionsDC Options = new Survey_OptionsDC();
                    Options.SurID = entity.QuestionInfo.SurID;
                    Options.Name = (Convert.ToChar(init + entity.QuestionInfo.OptionsList.Count)).ToString() + ".";
                    Options.Title = "其他：";
                    Options.Type = 1;   //文字

                    entity.QuestionInfo.OptionsList.Add(Options);
                }

                
            }


            if (entity.QuestionInfo.ID == 0)
            {
                //说明是添加

                //添加人ID
                entity.QuestionInfo.Obj_Cuser = item.ID;

                ReturnEntity<int> re = SurveyProxy.AddQuestion(entity.QuestionInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        //成功 记录操作日志
                        OperatorLogDC logobj = new OperatorLogDC();
                        logobj.OperatorID = item.ID;
                        logobj.OperatorName = item.Name;
                        logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web;
                        logobj.LogContent = string.Format("[{0}]新增ID为[{1}]问卷题目信息成功", item.Name, entity.QuestionInfo.ID);
                        OperatorProxy.OperateLog_Add(logobj);

                        //界面返回信息
                        rjEntity.statusCode = CodeSource.StatusCode.Success;
                        rjEntity.message = string.Format("新增ID为[{0}]活动信息成功", entity.QuestionInfo.ID);
                        rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                        rjEntity.navTabId = "questionlist";
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
            }
            else
            {
                //说明是编辑
                ReturnEntity<bool> re = SurveyProxy.UpdateQuestion(entity.QuestionInfo);
                if (re != null)
                {
                    if (re.Success)
                    {
                        if (re.OBJ)
                        {
                            //成功 记录操作日志
                            OperatorLogDC logobj = new OperatorLogDC();
                            logobj.OperatorID = item.ID;
                            logobj.OperatorName = item.Name;
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.Web; ;
                            logobj.LogContent = string.Format("[{0}]更新ID为[{1}]问卷题目信息成功", item.Name, entity.QuestionInfo.ID);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("更新ID为[{0}]问卷题目信息成功", entity.QuestionInfo.ID);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "questionlist";
                        }
                        else
                        {
                            //失败
                            rjEntity.statusCode = CodeSource.StatusCode.Faild;
                            rjEntity.message = CodeSource.Common.ConstConfig.WRONG_UpdateErrorMessage;
                        }
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
            }

            //}


            return Json(rjEntity);
        }



        public ActionResult AnswerIndex(int sid)
        {
            AnswerSearchInfo search = new AnswerSearchInfo();
            search.SurID = sid;
            return SearchAnswer(search, null);
        }

        public ActionResult SearchAnswer(AnswerSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new AnswerSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            AnswerListModel list = new AnswerListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理


            RecordCountEntity<Survey_AnswerDC> rce = SurveyProxy.GetSurveyAnswerList(search.SurID, search.UserSource, search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.AnswerList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }


            return View("AnswerIndex", list);
        }
	}
}