using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LazyAtHome.Web.WebManage.Models.Product;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Web.Base;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.Web.WebManage.CodeSource.Proxy;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;
using LazyAtHome.WCF.Common.Contract.DataContract.File;
using System.Xml;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class ProductController : BaseController
    {
        /// <summary>
        /// 显示产品运价列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            //ProductListModel list = new ProductListModel();
            //list.SearchInfo = new ProductSearchInfo();
            //return View("Index", list);

            return SearchProduct(null, null);
        }


        /// <summary>
        /// 运价搜索
        /// </summary>
        /// <param name="search"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public ActionResult SearchProduct(ProductSearchInfo search, int? pageNum)
        {
            if (search == null)
                search = new ProductSearchInfo();

            if (pageNum.HasValue)
                search.pageNum = pageNum.Value;//设置当前页

            ProductListModel list = new ProductListModel();
            list.SearchInfo = search;//用于在页面上保留查询信息

            //页面与接口数据处理
            if (search.CommitStatus == -1)
            {
                search.CommitStatus = null;
            }
            if (search.Type == -1)
            {
                search.Type = null;
            }


            RecordCountEntity<Wash_ProductDC> rce = ProductProxy.GetProductList(search.ProductName, search.ProductCode, search.Type, search.CommitStatus, search.DateFrom, FormatDateTimeAddOneDay(search.DateTo), search.pageNum, search.numPerPage);

            if (rce != null)
            {
                search.RecCount = rce.RecordCount;//设置查询总记录数
                list.ProductList = rce.ReturnList;
            }
            else
            {
                //处理报错逻辑   
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_WCFErrorMessage);
            }

            return View("Index", list);
        }



        /// <summary>
        /// 展示运价编辑页面
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult EditProduct(int pid)
        {
            //获取分组XML信息
            string filePath = string.Empty;
            try
            {
                filePath = Server.MapPath("~/ProductGroup.xml");
            }
            catch (Exception e)
            {
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "路径："+filePath+ "错误：" + e.Message);
                
            }
            //ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, filePath);
            //ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, filePath);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNodeList lists = doc.SelectNodes("ProductGroups/ProductGroup");

                IList<ProductGroup> pgList = new List<ProductGroup>();
                foreach (XmlNode node in lists)
                {
                    ProductGroup item = new ProductGroup();
                    item.Name = node.Attributes["name"].Value;
                    item.Value = Convert.ToInt32(node.Attributes["value"].Value);
                    pgList.Add(item);
                }

                var query = pgList.Select(c => new { Value = c.Value, Text = c.Name });
                ViewData["ProductGroup"] = new SelectList(query.AsEnumerable(), "Value", "Text");
            }
            catch (Exception e)
            {
                //CodeSource.Common.WriteLog.tradeLog("路径：" + filePath + "错误：" + e.Message);
                ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, "路径：" + filePath + "错误：" + e.Message);
            }

            
            //if (System.IO.File.Exists(filePath))
            //{
            //    doc.Load(filePath);
            //    XmlNodeList lists = doc.SelectNodes("ProductGroups/ProductGroup");

            //    IList<ProductGroup> pgList = new List<ProductGroup>();
            //    foreach (XmlNode node in lists)
            //    {
            //        ProductGroup item = new ProductGroup();
            //        item.Name = node.Attributes["name"].Value;
            //        item.Value = Convert.ToInt32(node.Attributes["value"].Value);
            //        pgList.Add(item);
            //    }

            //    var query = pgList.Select(c => new { Value = c.Value, Text = c.Name });
            //    ViewData["ProductGroup"] = new SelectList(query.AsEnumerable(), "Value", "Text");
            //}


            ProductModel entity = null;
            if (pid == 0)
            {
                //说明是添加，实例化对象
                entity = new ProductModel();
                entity.ProductInfo = new Wash_ProductDC();
                entity.ProductInfo.CommitStatus = 0;
            }
            else
            {
                //说明是编辑，根据ID获取实体
                ReturnEntity<Wash_ProductDC> re = ProductProxy.GetProductBYID(pid);
                if (re != null)
                {
                    if (re.Success)
                    {
                        entity = new ProductModel();
                        entity.ProductInfo = re.OBJ;


                        //运价移出图片
                        //if (entity.ProductInfo.PictureList != null)
                        //{
                        //    Wash_ProductPictureDC pic = entity.ProductInfo.PictureList.FirstOrDefault(p => p.Type == 1);
                        //    if (pic != null)
                        //    {
                        //        entity.FileNameProductImgL = pic.PirtureL;
                        //        entity.PathProductImgL = CodeSource.Common.ConstConfig.IMAGE_PATH + CodeSource.Common.ConstConfig.PRODUCT_IMG_PATH + entity.FileNameProductImgL;
                        //    }
                        //}
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

            //城市下拉框
            SiteListSet();

            return View("EditProduct", entity);
        }

        [HttpPost]
        public JsonResult SaveProduct(ProductModel entity, int BDVIdL1, int BDVIdL2, int BDVIdL3, string strAttributeIDSelected, string FileNameProductImgL, FormCollection coll)
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

            //移除属性备选列表
            //entity.strAttributeIDSelected = strAttributeIDSelected;

            //判断是否有选中属性
            //if (string.IsNullOrEmpty(entity.strAttributeIDSelected))
            //{
            //    //没有选中，直接报错
            //    rjEntity.statusCode = CodeSource.StatusCode.Faild;
            //    rjEntity.message = "请选择产品属性";
            //}
            //else
            //{
                //移除属性备选列表
                //#region 整理页面上选择的属性配置

                //string[] arrTemp = entity.strAttributeIDSelected.Split(',');
                //if (entity.ProductInfo != null)
                //{
                //    entity.ProductInfo.AttributeList = new List<Wash_ProductAttributeDC>();
                //    foreach (string id in arrTemp)
                //    {
                //        Wash_ProductAttributeDC Attribute = new Wash_ProductAttributeDC();
                //        Attribute.AttributeID = Convert.ToInt32(id);
                //        Attribute.AttributeName = coll["txtsubitem" + id];
                //        Attribute.Name = coll["txtsubname" + id];
                //        Attribute.ParentAttributeID = Convert.ToInt32(coll["paid" + id]);
                //        Attribute.Price = Convert.ToDecimal(coll["price" + id]);
                //        Attribute.Selected = coll["ckselect" + id] == "on" ? 1 : 0;
                //        Attribute.Default = coll["ckdefault" + id] == "on" ? 1 : 0;
                //        Attribute.Content = coll["content" + id];

                //        entity.ProductInfo.AttributeList.Add(Attribute);
                //    }


                //}
                //#endregion

                //修改人ID
                entity.ProductInfo.Obj_Muser = item.ID;



                //审核人
                if (entity.ProductInfo.CommitStatus == 1)
                {
                    //设置成已上线
                    entity.ProductInfo.OnOperatorID = item.ID;
                    entity.ProductInfo.OnDate = System.DateTime.Now;
                }
                else if (entity.ProductInfo.CommitStatus == 2)
                {
                    //设置成已下线
                    entity.ProductInfo.OffOperatorID = item.ID;
                    entity.ProductInfo.OffDate = System.DateTime.Now;
                }

                entity.ProductInfo.CategoryID = BDVIdL3;

                //运价移出图片
                ////图片集合为空 实例化
                //entity.ProductInfo.PictureList = new List<Wash_ProductPictureDC>();
                ////产品大图
                //if (!string.IsNullOrEmpty(FileNameProductImgL))
                //{
                //    Wash_ProductPictureDC ProductPictureL = new Wash_ProductPictureDC();
                //    ProductPictureL.Type = 1; //运价图片为1
                //    ProductPictureL.PirtureL = FileNameProductImgL;

                //    entity.ProductInfo.PictureList.Add(ProductPictureL);
                //}


                if (entity.ProductInfo.ID == 0)
                {
                    //说明是添加

                    //添加人ID
                    entity.ProductInfo.Obj_Cuser = item.ID;


                    ReturnEntity<int> re = ProductProxy.AddProduct(entity.ProductInfo);
                    if (re != null)
                    {
                        if (re.Success)
                        {
                            //成功 记录操作日志
                            OperatorLogDC logobj = new OperatorLogDC();
                            logobj.OperatorID = item.ID;
                            logobj.OperatorName = item.Name;
                            logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                            logobj.LogContent = string.Format("[{0}]新增ID为[{1}]名称为[{2}]运价信息成功", item.Name, entity.ProductInfo.ID, entity.ProductInfo.Name);
                            OperatorProxy.OperateLog_Add(logobj);

                            //界面返回信息
                            rjEntity.statusCode = CodeSource.StatusCode.Success;
                            rjEntity.message = string.Format("新增名称为[{0}]运价信息成功", entity.ProductInfo.Name);
                            rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                            rjEntity.navTabId = "productlist";
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
                    ReturnEntity<bool> re = ProductProxy.UpdateProduct(entity.ProductInfo);
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
                                logobj.Type = (int)CodeSource.Common.OperateLogTypeEnum.ProductCategory; ;
                                logobj.LogContent = string.Format("[{0}]更新ID为[{1}]名称为[{2}]运价信息成功", item.Name, entity.ProductInfo.ID, entity.ProductInfo.Name);
                                OperatorProxy.OperateLog_Add(logobj);

                                //界面返回信息
                                rjEntity.statusCode = CodeSource.StatusCode.Success;
                                rjEntity.message = string.Format("更新ID为[{0}]名称为[{1}]运价信息成功", entity.ProductInfo.ID, entity.ProductInfo.Name);
                                rjEntity.callbackType = CodeSource.CallbackType.CloseCurrent;
                                rjEntity.navTabId = "productlist";
                            }
                            else
                            {
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



        //运价移除属性备选列表
        //public ActionResult CategorySelect(int categoryid, int productid)
        //{

            //运价移除属性备选列表
            ////根据产品id获取 产品配置的属性列表
            //IList<ProductAttribute> selectList = null;

            //if (categoryid == 0)
            //{
            //    selectList = new List<ProductAttribute>();
            //}
            //else
            //{
            //    selectList = GetProductAttribute(categoryid, productid, null); 
            //}            
            //return PartialView("ProductAttributeSelect", selectList);
        //}


        ////运价移除属性备选列表
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="CategoryId"></param>
        ///// <param name="ProductId"></param>
        ///// <param name="SelectedAttribute"></param>
        ///// <returns></returns>
        //private IList<ProductAttribute> GetProductAttribute(int CategoryId, int ProductId, IList<Wash_ProductAttributeDC> SelectedAttribute)
        //{
        //    IList<ProductAttribute> rtn = null;

        //    IList<Wash_ProductAttributeDC> listselected = null;

        //    //根据CategoryId获取 产品配置的属性列表
        //    ReturnEntity<Wash_CategoryDC> re = CategoryProxy.GetCategoryBYID(CategoryId);


        //    //根据ProductId获取 已经选的配置的属性列表
        //    if (ProductId != 0)
        //    {
        //        ReturnEntity<IList<Wash_ProductAttributeDC>> reselected = ProductProxy.GetProductAttributeBYProductID(ProductId);
        //        if (reselected.Success && reselected.OBJ != null)
        //        {
        //            listselected = reselected.OBJ;
        //        }          
        //    }

        //    if (re.Success && re.OBJ != null)
        //    {
        //        //从缓存中获取所有属性大类
        //        IList<Wash_AttributeDC> alist = ProductAttributeProxy.GetAttributeFirstList();

        //        //获取备选属性配置
        //        IList<Wash_AttributeDC> list = re.OBJ.AttributeList_GetParentList(alist);

        //        rtn = new List<ProductAttribute>();
        //        //根据获取到的属性对象，整理客户端对象ProductAttribute
        //        foreach (Wash_AttributeDC item_p in list)
        //        {
        //            //大类
        //            ProductAttribute pa = new ProductAttribute();
        //            pa.ParentAttribute = item_p;
        //            //小类
        //            pa.SubAttributeList = new List<WashProductAttribute>();
        //            foreach (Wash_AttributeDC item_s in item_p.ChildAttributeList)
        //            {
        //                WashProductAttribute wpa = new WashProductAttribute();
        //                wpa.SubWashProductAttribute = new Wash_ProductAttributeDC();

        //                if (listselected != null)
        //                {


        //                    //定义flag 判断集合中有没有
        //                    bool flag = false;
        //                    foreach (var tmp_item in listselected)
        //                    {
        //                        if (tmp_item.AttributeID == item_s.ID)
        //                        {
        //                            flag = true;
        //                            wpa.isSelected = true;//已经选择的产品属性中存在 为选中
        //                            wpa.SubWashProductAttribute.AttributeID = tmp_item.AttributeID;
        //                            wpa.SubWashProductAttribute.AttributeName = tmp_item.AttributeName;
        //                            wpa.SubWashProductAttribute.ParentAttributeID = tmp_item.ParentAttributeID;
        //                            wpa.SubWashProductAttribute.Content = tmp_item.Content;
        //                            wpa.SubWashProductAttribute.Default = tmp_item.Default;
        //                            wpa.SubWashProductAttribute.Name = tmp_item.Name;
        //                            wpa.SubWashProductAttribute.Selected = tmp_item.Selected;
        //                            wpa.SubWashProductAttribute.Price = tmp_item.Price;
        //                            wpa.SubWashProductAttribute.ProductID = tmp_item.ProductID;
        //                            break;
        //                        }
        //                    }
        //                    if (!flag)
        //                    {
        //                        //说明未被选中
        //                        wpa.isSelected = false; //添加时 默认为false;
        //                        wpa.SubWashProductAttribute.AttributeID = item_s.ID;
        //                        wpa.SubWashProductAttribute.AttributeName = item_s.Name;
        //                        wpa.SubWashProductAttribute.ParentAttributeID = item_p.ID;
        //                    }
        //                }
        //                else
        //                { 
        //                    //添加
        //                    wpa.isSelected = false; //添加时 默认为false;
        //                    wpa.SubWashProductAttribute.AttributeID = item_s.ID;
        //                    wpa.SubWashProductAttribute.AttributeName = item_s.Name;
        //                    wpa.SubWashProductAttribute.ParentAttributeID = item_p.ID;
        //                }

        //                pa.SubAttributeList.Add(wpa);
        //            }
        //            rtn.Add(pa);    
        //        }
        //    }
        //    else
        //    {
        //        //处理报错逻辑   
        //        //ViewBag.Script = CodeSource.WebUtility.AlertMsg(CodeSource.AlertMsgType.Error, CodeSource.Common.ConstConfig.WRONG_SearchMessage);
        //    }


        //    return rtn;
        //}
	}
}