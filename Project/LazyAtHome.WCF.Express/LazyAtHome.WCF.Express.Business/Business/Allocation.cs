using LazyAtHome.Library.Partners.BaiduMap.Entity;
using LazyAtHome.WCF.Common.Contract.DataContract.PR;
using LazyAtHome.WCF.Common.Contract.Enumerate;
using LazyAtHome.WCF.Express.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Business.Business
{
    public class Allocation
    {

        private LazyAtHome.WCF.Express.Interface.IDAL.IExpressDAL expressDAL;

        public Allocation()
        {
            if (expressDAL == null)
                expressDAL = new LazyAtHome.WCF.Express.DAL.ExpressDAL();

            NodeList = expressDAL.Exp_Node_SELECT_List_Allocation();
            //NodeList = expressDAL.Exp_Node_SELECT_List(null, null, null, null, null, null, 1, 1000).ReturnList;
        }

        static Allocation _Allocation;

        public static Allocation Instance
        {
            get
            {
                if (_Allocation == null)
                {
                    _Allocation = new Allocation();
                }
                return _Allocation;
            }
        }

        public bool NodeListNeedRefresh = false;

        private IList<Exp_NodeDC> NodeList { get; set; }

        private void NodeRefresh()
        {
            if (NodeListNeedRefresh)
            {
                //NodeList = expressDAL.Exp_Node_SELECT_List(null, null, null, null, null, null, 1, 1000).ReturnList;

                NodeList = expressDAL.Exp_Node_SELECT_List_Allocation();

                Console.WriteLine(DateTime.Now.ToString() + " 站点缓存刷新");
                NodeListNeedRefresh = false;
            }
        }

        public void Process()
        {
            NodeRefresh();

            RunOrder_New();
        }

        public void RunOrder_Old()
        {
            ////获取未分配数据  AllotStatus = 0 Step = 0 TransportType = 1

            //var tmp_Address = string.Empty;
            //var tmp_NodeID = 0;
            //string tmp_NodeName = string.Empty;

            //var orderList = expressDAL.Exp_Order_SELECT_List_UnAllocation();
            //if (orderList == null || orderList.Count == 0)
            //    return;
            //foreach (var order in orderList)
            //{
            //    int allotStatus = 0;
            //    int nodeID = 0;
            //    string nodeName = string.Empty;

            //    //地址与上次相同
            //    if (!string.IsNullOrWhiteSpace(tmp_Address) && tmp_Address == order.Address)
            //    {
            //        allotStatus = 1;
            //        nodeID = tmp_NodeID;
            //        nodeName = tmp_NodeName;
            //    }

            //    if (allotStatus == 0)
            //    {
            //        //第一次遍历关键字
            //        foreach (var node in NodeList)
            //        {
            //            //关键字like
            //            foreach (var keyword in node.KeywordList)
            //            {
            //                if (order.Address.Contains(keyword))
            //                {
            //                    //关键字符合
            //                    allotStatus = 1;
            //                    nodeID = node.ID;
            //                    nodeName = node.Name;
            //                    break;
            //                }
            //            }
            //        }
            //    }

            //    //第二次遍历经纬度
            //    if (allotStatus == 0)
            //    {
            //        foreach (var node in NodeList)
            //        {
            //            if (node.Latitude.HasValue && node.Longitude.HasValue)
            //            {
            //                //计算经纬度
            //                var location = geocoder(order.Address);
            //                if (location != null)
            //                {
            //                    //更新经纬度
            //                    expressDAL.Exp_Order_UPDATE_Location(order.ID, location.lat, location.lng);
            //                    //检查半径
            //                    if (CheckDistance(location.lat, location.lng, node.Latitude.Value, node.Longitude.Value, node.Radius))
            //                    {
            //                        //在范围内
            //                        allotStatus = 1;
            //                        nodeID = node.ID;
            //                        nodeName = node.Name;
            //                        break;
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    //更新分配状态
            //    if (allotStatus == 1)
            //    {
            //        //成功分配
            //        if (order.TransportType == 1)
            //        {
            //            expressDAL.Exp_Order_UPDATE_Allocation(order.ID, nodeID);
            //        }
            //        else
            //        {
            //            ManageExpress.Instance.Exp_Order_Allocation(order.ID, null, nodeID, -1);
            //        }

            //        Console.WriteLine("订单分配: " + nodeName + "\t " + order.Address);

            //        tmp_Address = order.Address;
            //        tmp_NodeID = nodeID;
            //        tmp_NodeName = nodeName;
            //    }
            //    else
            //    {
            //        //分配自己人 
            //        if (order.TransportType == 1)
            //        {
            //            expressDAL.Exp_Order_UPDATE_Allocation(order.ID, 1);
            //            //expressDAL.Exp_Order_Allocation(order.ID, 12, 1, -1);
            //        }
            //        else
            //        {
            //            expressDAL.Exp_Order_Allocation(order.ID, null, 1, -1);
            //        }

            //        Console.WriteLine("订单分配: 默认\t " + order.Address);

            //        tmp_Address = order.Address;
            //        tmp_NodeID = 1;
            //        tmp_NodeName = "默认";
            //    }
            //}
        }

        public void RunOrder_New()
        {
            //var tmp_Address = string.Empty;
            //Exp_NodeDC tmp_Node = null;

            var orderList = expressDAL.Exp_Order_SELECT_List_UnAllocation();
            if (orderList == null || orderList.Count == 0)
                return;
            foreach (var order in orderList)
            {
                if (!string.IsNullOrEmpty(order.InviteCode))
                {
                    //有邀请人,直接推送到他手中
                    var oper = expressDAL.PR_Operator_SELECT_BYCode(order.InviteCode);
                    if (oper != null)
                    {
                        //只有保安队长和保安可以强制推送 2014-12-16 guominjie
                        if (oper.RoleID == 1 || oper.RoleID == 2)
                        {
                            //直接分配到该人手里
                            expressDAL.Exp_Order_Allocation_ToOperator(order.ID, order.OutNumber, oper.NodeID, oper.ID, order.Address);

                            Console.WriteLine("强制分配: " + oper.NodeName + "  " + oper.Name + "\t " + order.Address);

                            continue;
                        }
                    }
                }

                var node = GetNode_Address(order.Address, order.ID);
                if (node == null)
                {
                    //设置为无法分配
                    expressDAL.Exp_Order_Allocation_Fail(order.OutNumber);
                    Console.WriteLine("地址无法分配:" + order.Address);

                    //通知客服
                    CommonExpress.NotifySend(order.OutNumber, RoleDC.Role_CustomerService, 0, 0, "地址无法分配",
                        "地址无法分配 订单地址:" + order.Address, (int)NotifyLevel.Warning4, false, false, 0);
                }
                else
                {
                    //分配到快递接单中
                    if (!expressDAL.Exp_Order_Allocation_Success(order.ID, node.ID, node.No, order.Address, node.AlarmType))
                    {
                        Console.WriteLine("地址无法分配,快递员无在岗" + "\t " + order.Address);

                        var oper = expressDAL.PR_Operator_SELECT_BYID(node.ManagerID);
                        if (oper != null)
                        {
                            //通知客服
                            CommonExpress.NotifySend(order.OutNumber, RoleDC.Role_CustomerService, 0, 0, "站点无保安在岗",
                             "站点名称:" + node.Name + " 负责人:" + oper.Name + " " + oper.MpNo + " 订单地址:" + order.Address, (int)NotifyLevel.Warning4, true, false, 0);

                            ////通知站点负责人
                            //CommonExpress.NotifySend(order.OutNumber, oper.RoleID, node.ManagerID, 0, "站点无保安在岗",
                            // "站点名称:" + node.Name + " 负责人:" + oper.Name + " " + oper.MpNo + " 订单地址:" + order.Address, (int)NotifyLevel.Warning4, true, false, 0);
                        }
                        else
                        {
                            //通知客服
                            CommonExpress.NotifySend(order.OutNumber, RoleDC.Role_CustomerService, 0, 0, "站点无保安在岗",
                             "!!!站点无负责人!! " + "站点名称:" + node.Name + " 订单地址:" + order.Address, (int)NotifyLevel.Warning4, true, false, 0);
                        }
                    }
                    else
                    {
                        Console.WriteLine("订单分配: " + node.Name + "\t " + order.Address);
                    }
                }
            }
        }

        public Exp_NodeDC GetNode_Address(string address, int? orderid = null)
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                //地址为空
                return null;
            }

            //第一次遍历关键字
            foreach (var node in NodeList)
            {
                //关键字like
                foreach (var keyword in node.KeywordList)
                {
                    if (address.Contains(keyword))
                    {
                        //关键字符合
                        return node;
                    }
                }
            }

            try
            {
                if (address.IndexOf('弄') > 0)
                {
                    address = address.Substring(0, address.IndexOf('弄') + 1);
                }
                else if (address.IndexOf('号') > 0)
                {
                    address = address.Substring(0, address.IndexOf('号') + 1);
                }
            }
            catch { }

            //第二次算半径
            decimal address_lat;
            decimal address_lng;
            var location = geocoder(address);
            if (location == null)
            {
                Console.WriteLine("地址无法计算经纬度:" + address);
                return null;
            }
            else
            {
                address_lat = location.lat;
                address_lng = location.lng;
                if (orderid.HasValue)
                {
                    expressDAL.Exp_Order_UPDATE_Location(orderid.Value, location.lat, location.lng);
                }
            }

            //找最近符合距离的
            var dis_NodeList = NodeList.Where(p => p.Latitude.HasValue && p.Longitude.HasValue
                        && GetDistance(Convert.ToDouble(p.Latitude.Value),
                                        Convert.ToDouble(p.Longitude.Value),
                                        Convert.ToDouble(location.lat),
                                        Convert.ToDouble(location.lng)) < p.Radius)
                    .OrderBy(p => GetDistance(Convert.ToDouble(p.Latitude.Value),
                                        Convert.ToDouble(p.Longitude.Value),
                                        Convert.ToDouble(location.lat),
                                        Convert.ToDouble(location.lng)));
            if (dis_NodeList.Count() > 0)
            {
                return dis_NodeList.First();
            }
            else
            {
                //无法匹配
                return null;
            }


            //foreach (var node in NodeList)
            //{
            //    if (!node.Latitude.HasValue || !node.Longitude.HasValue)
            //    {
            //        //node无设置经纬度
            //        continue;
            //    }
            //    //计算经纬度
            //    if (CheckDistance(location.lat, location.lng, node.Latitude.Value, node.Longitude.Value, node.Radius))
            //    {
            //        return node;
            //    }
            //}

            //无法匹配
            //return null;
        }

        public Exp_NodeDC GetNode_Location(decimal iLatitude, decimal iLongitude)
        {
            //找最近符合距离的
            var dis_NodeList = NodeList.Where(p => p.Latitude.HasValue && p.Longitude.HasValue
                        && GetDistance(Convert.ToDouble(p.Latitude.Value),
                                        Convert.ToDouble(p.Longitude.Value),
                                        Convert.ToDouble(iLatitude),
                                        Convert.ToDouble(iLongitude)) < p.Radius)
                    .OrderBy(p => GetDistance(Convert.ToDouble(p.Latitude.Value),
                                        Convert.ToDouble(p.Longitude.Value),
                                        Convert.ToDouble(iLatitude),
                                        Convert.ToDouble(iLongitude)));
            if (dis_NodeList.Count() > 0)
            {
                return dis_NodeList.First();
            }
            else
            {
                //无法匹配
                return null;
            }
        }



        Library.Partners.BaiduMap.Business BaiduMapBusiness = new Library.Partners.BaiduMap.Business();

        private GeocoderReturn.Geocoder.Location geocoder(string iAddress)
        {
            try
            {
                var rtn = BaiduMapBusiness.geocoder("上海市", iAddress);
                if (rtn != null && rtn.result != null && rtn.result.location != null)
                {
                    return rtn.result.location;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("经纬度转换失败:" + ex.Message);
                return null;
            }


        }

        ///// <summary>
        ///// 检查是否在范围内
        ///// </summary>
        ///// <param name="lat1"></param>
        ///// <param name="lng1"></param>
        ///// <param name="lat2"></param>
        ///// <param name="lng2"></param>
        ///// <param name="d"></param>
        ///// <returns></returns>
        //private bool CheckDistance(decimal lat1, decimal lng1, decimal lat2, decimal lng2, double d)
        //{
        //    if (d <= 0) return false;
        //    return GetDistance(Convert.ToDouble(lat1), Convert.ToDouble(lng1), Convert.ToDouble(lat2), Convert.ToDouble(lng2)) * 1000 < d;
        //}

        private const double EARTH_RADIUS = 6378.137;//地球半径

        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS * 1000;
            //s = Math.Round(s * 10000) / 10000;
            return s;
        }
    }
}
