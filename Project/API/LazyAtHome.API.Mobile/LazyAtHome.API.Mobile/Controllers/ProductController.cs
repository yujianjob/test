using LazyAtHome.API.Mobile.App_Code.Proxy;
using LazyAtHome.API.Mobile.Models.JsonResultModels;
using LazyAtHome.API.Mobile.Models.LocalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.API.Mobile.Controllers
{
    public partial class HomeController
    {
        /// <summary>
        /// 4.14	获取服务费列表与广告
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult ServiceFee(string cid)
        {
            if (string.IsNullOrWhiteSpace(cid))
            {
                return MyJson(BaseResult.ParametersError);
            }
            Guid userid = Guid.Empty;

            if (!Guid.TryParse(cid, out userid))
            {
                return MyJson(BaseResult.ParametersError);
            }

            try
            {
                ServiceFeeResult rtn = new ServiceFeeResult();

                rtn.servad = new List<string>()
                {
                    "12:00以前下单今日取件",
                    "12:00以后下单明日取件",
                    //"18元快递费，免费洗衣3件",
                    //"24元快递费，免费洗衣4件"
                    //"APP下单满25免运费",
                    //"创始会员全场免运费",
                    //"每日特惠",
                };

                rtn.servfee = new List<WashServiceFee>();

                //普通快递
                rtn.servfee.Add(new WashServiceFee()
                {
                    id = 1,
                    name = "同城快递(18元免运费)",
                    groupid = "8E72913B",
                    mustcheck = 1,
                    @checked = 1,
                    money = 1800,
                });
                ////续件快递
                //rtn.servfee.Add(new WashServiceFee()
                //{
                //    id = 2,
                //    name = "续件快递",
                //    groupid = "8E72913B",
                //    //groupid = "B8FBA507",
                //    mustcheck = 1,
                //    @checked = 1,
                //    money = 600,
                //});

                ////24小时加急
                //rtn.servfee.Add(new WashServiceFee()
                //{
                //    id = 3,
                //    name = "24小时加急",
                //    groupid = "DCA4E865",
                //    mustcheck = 0,
                //    @checked = 0,
                //    money = 10000,
                //});

                ////普通包装
                //rtn.servfee.Add(new WashServiceFee()
                //{
                //    id = 3,
                //    name = "普通包装",
                //    groupid = "03A392B8",
                //    mustcheck = 0,
                //    @checked = 0,
                //    money = 0,
                //});
                ////精美包装
                //rtn.servfee.Add(new WashServiceFee()
                //{
                //    id = 4,
                //    name = "精美包装",
                //    groupid = "03A392B8",
                //    mustcheck = 0,
                //    @checked = 0,
                //    money = 1000,
                //});

                //返回
                return MyJson(rtn);

            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.15	获取洗涤服务列表
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        public JsonResult ServiceList(string cid)
        {
            try
            {
                var classRtn = WashProxy.web_Wash_Class_SELECT_List_Second_Detail(1);
                if (classRtn == null || classRtn.OBJ == null)
                {
                    return MyJson(BaseResult.EmptyResult);
                }
                //返回对象
                ServiceListResult rtn = new ServiceListResult();
                rtn.service = new List<WashServiceModel>();

                //循环小类
                foreach (var classItem in classRtn.OBJ)
                {
                    var washservice = new WashServiceModel()
                    {
                        id = classItem.ID,
                        name = classItem.Name,
                        classlist = new List<WashClassModel>(),
                    };

                    if (classItem.CategoryList != null)
                    {
                        foreach (var category in classItem.CategoryList)
                        {
                            var washclass = new WashClassModel()
                            {
                                id = category.ID,
                                name = category.Name,
                                productlist = new List<WashProductModel>(),
                            };

                            if (category.ProductList != null)
                            {
                                foreach (var productItem in category.ProductList)
                                {
                                    //将运价加入列表
                                    washclass.productlist.Add(new WashProductModel()
                                    {
                                        id = productItem.ID,
                                        name = productItem.Name,
                                        marketprice = Convert.ToInt32(productItem.MarketPrice * 100),
                                        price = Convert.ToInt32(productItem.SalesPrice * 100),
                                    });
                                }
                            }
                            //将产品加入列表
                            washservice.classlist.Add(washclass);
                        }
                    }
                    //将服务加入列表
                    rtn.service.Add(washservice);
                }

                //返回
                return MyJson(rtn);

            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }
    }
}