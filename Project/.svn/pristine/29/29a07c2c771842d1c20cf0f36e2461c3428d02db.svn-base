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
    public partial class HomeController : BaseController
    {
        //public JsonResult index()
        //{
        //    //var d = new { aa = 1, bb = 2, cc = "33" };
        //    var d = new LazyAtHome.API.Mobile.Models.LocalModels.WashServiceFee()
        //    {
        //        @checked = 1,
        //        groupid = "123123",
        //        id = 1,
        //        mustcheck = 1,
        //        name = "name"
        //    };
        //    return Json(d, JsonRequestBehavior.AllowGet);
        //}


        //4.26	获取行政区列表
        public JsonResult DistrictList()
        {
            try
            {
                var baseDistrict = CommonProxy.Cache_Base_District_GetList();
                if (baseDistrict == null)
                {
                    return MyJson(BaseResult.BadResult(-1, "行政区列表为空"));
                }

                var rtn = new DistrictListResult();

                rtn.districtlist = new List<DistrictModel<DistrictModel<DistrictModel<object>>>>();

                //省级
                foreach (var base_Prov in baseDistrict.Where(p => p.Level == 1))
                {
                    if (base_Prov.Code1 != "31")
                    {
                        //非上海
                        continue;
                    }

                    var Prov = new DistrictModel<DistrictModel<DistrictModel<object>>>();

                    Prov.id = base_Prov.ID;
                    Prov.name = base_Prov.Name;
                    Prov.dislist = new List<DistrictModel<DistrictModel<object>>>();

                    //城市
                    foreach (var base_City in baseDistrict.Where(p => p.Level == 2 && p.Code1 == base_Prov.Code1))
                    {
                        var City = new DistrictModel<DistrictModel<object>>();
                        City.id = base_City.ID;
                        City.name = base_City.Name;
                        City.dislist = new List<DistrictModel<object>>();

                        //行政区
                        foreach (var base_Dist in baseDistrict.Where(p => p.Level == 3 && p.Code1 == base_City.Code1 && p.Code2 == base_City.Code2))
                        {
                            var Dist = new DistrictModel<object>();
                            Dist.id = base_Dist.ID;
                            Dist.name = base_Dist.Name;

                            City.dislist.Add(Dist);
                        }

                        Prov.dislist.Add(City);
                    }

                    rtn.districtlist.Add(Prov);
                }

                return MyJson(rtn);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }


        /// <summary>
        /// 4.34	唤醒日志
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="mobiletype"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public JsonResult WakeUp(string cid, int mobiletype, string flag)
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
                UserProxy.User_WakeUp(userid, mobiletype, flag);

                //返回
                return MyJson(BaseResult.Success);
            }
            catch (Exception ex)
            {
                return MyJson(BaseResult.SystemError(ex));
            }
        }

        /// <summary>
        /// 4.35	检查更新
        /// </summary>
        /// <param name="version"></param>
        /// <param name="mobiletype"></param>
        /// <returns></returns>
        public JsonResult Version(string version, int mobiletype)
        {
            VersionResult rtn = new VersionResult();

            rtn.startpage = APPConfig.startpage;

            if (mobiletype == 1)
            {
                //iphone
                rtn.version = "1.0.0";
                rtn.minversion = "1.0.0";

                rtn.url = APPConfig.iphone_download_url;
                return MyJson(rtn);
            }
            else if (mobiletype == 2)
            {
                //android
                rtn.version = "1.0.0";
                rtn.minversion = "1.0.0";
                rtn.url = APPConfig.android_download_url;
                return MyJson(rtn);
            }
            else
            {
                return MyJson(BaseResult.ParametersError);
            }
        }

    }



}