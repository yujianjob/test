using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WeiXin4.Controllers
{
    public class LocationController : BaseController
    {
        public JsonResult GetUserLocation()
        {
            //return GetBaiduLocation();
            //return GetSogouLocation();
            return GetQQLocation();
        }

        public JsonResult GetBaiduLocation()
        {
            int code = 1;
            string msg = "";
            string address = "";

            var objLocation = HttpContext.Cache[AppConfig.Cache_Location + _UserOpenID];            
            if (objLocation != null)
            {
                string requestUrl = "http://api.map.baidu.com/geocoder/v2/?ak=" + AppConfig.Baidu_AK + "&output=xml&location=" + objLocation.ToString() + "&coordtype=wgs84ll";
                App_Code.UtilityFunc.Add(requestUrl);
                string strXml = App_Code.UtilityFunc.WebGetRequest(requestUrl);

                try
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(strXml);
                    System.Xml.XmlNode node = xmlDoc.DocumentElement.SelectNodes("/GeocoderSearchResponse/result/formatted_address")[0];
                    address = node.InnerText;
                }
                catch (Exception ex)
                {
                    App_Code.UtilityFunc.Add(ex.Message);
                    code = -1;
                    msg = "获取当前位置失败";
                }
            }
            else
            {
                code = -2;
                msg = "未获取到当前位置";
            }

            return Json(new { Code = code, Msg = msg, Address = address });
        }

        public JsonResult GetQQLocation()
        {
            int code = 1;
            string msg = "";
            string address = "";

            var objLocation = HttpContext.Cache[AppConfig.Cache_Location + _UserOpenID];            
            if (objLocation != null)
            {
                string requestUrl = "http://apis.map.qq.com/ws/geocoder/v1?coord_type=1&key=O3MBZ-TRCW5-XGUIJ-QHO5L-MVOGH-YQF72&location=" + objLocation.ToString();
                App_Code.UtilityFunc.Add(requestUrl);
                string strJson = App_Code.UtilityFunc.WebGetRequest(requestUrl);

                try
                {
                    Models.QQLocationEntity location = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.QQLocationEntity>(strJson);
                    if (location.status == 0)
                    {
                        //address = location.result.address_component.city + location.result.address_component.district + location.result.address_component.street_number;
                        address = location.result.address_component.street_number;
                        if (location.result.address_reference != null && location.result.address_reference.landmark_l2 != null)
                            address += location.result.address_reference.landmark_l2.title;
                    }
                    else
                    {
                        code = -1;
                        msg = "获取当前位置失败";
                    }

                }
                catch (Exception ex)
                {
                    App_Code.UtilityFunc.Add(ex.Message);
                    code = -1;
                    msg = "获取当前位置失败";
                }
            }
            else
            {
                code = -2;
                msg = "未获取到当前位置";
            }

            return Json(new { Code = code, Msg = msg, Address = address });
        }

        public JsonResult GetSogouLocation()
        {
            int code = 1;
            string msg = "";
            string address = "";

            var objLocation = HttpContext.Cache[AppConfig.Cache_Location + _UserOpenID];
            if (objLocation != null)
            {
                string requestUrl = "http://api.go2map.com/engine/api/regeocoder/xml?points=" + objLocation.ToString() + "&type=1&contenttype=utf8";
                App_Code.UtilityFunc.Add(requestUrl);
                string strXml = App_Code.UtilityFunc.WebGetRequest(requestUrl);

                try
                {
                    System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
                    xmlDoc.LoadXml(strXml);
                    System.Xml.XmlNode node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/province")[0];//省
                    address = node.InnerText;
                    node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/city")[0];//市
                    address += node.InnerText;
                    node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/district")[0];//区
                    address += node.InnerText;
                    node = xmlDoc.DocumentElement.SelectNodes("/xml/response/data/address")[0];//地址
                    address += node.InnerText;
                }
                catch (Exception ex)
                {
                    App_Code.UtilityFunc.Add(ex.Message);
                    code = -1;
                    msg = "获取当前位置失败";
                }
            }
            else
            {
                code = -2;
                msg = "未获取到当前位置";
            }

            return Json(new { Code = code, Msg = msg, Address = address });
        }
    }
}