using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Library.Partners.SFExpress.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.SFExpress.Business
{
    public class OrderService : BusinessBase
    {
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <param name="j_Company"></param>
        /// <param name="j_Contact"></param>
        /// <param name="j_Tel"></param>
        /// <param name="j_Address"></param>
        /// <param name="j_City"></param>
        /// <param name="j_Province"></param>
        /// <param name="d_Company"></param>
        /// <param name="d_Contact"></param>
        /// <param name="d_Tel"></param>
        /// <param name="d_Address"></param>
        /// <param name="d_City"></param>
        /// <param name="d_Province"></param>
        /// <param name="iPayment"></param>
        /// <returns></returns>
        public ReturnEntity<OrderServiceResponse> CreateOrder(string iOrderid,
            string j_Company, string j_Contact, string j_Tel, string j_Address, string j_City, string j_Province,
            string d_Company, string d_Contact, string d_Tel, string d_Address, string d_City, string d_Province,
            decimal? iPayment = null)
        {
            //Random rd = new Random();
            //return new ReturnEntity<OrderServiceResponse>(new OrderServiceResponse(null)
            //{
            //    destcode = rd.Next(0, 1000).ToString(),
            //    orderid = iOrderid,
            //    mailno = DateTime.Now.ToString("yyyyMMddHHmmss") + rd.Next(0, 10000).ToString().PadLeft(4, '0'),
            //    origincode = rd.Next(0, 1000).ToString(),
            //    filter_result = 2,
            //    remark = "3",
            //});


            OrderServiceBody body = new OrderServiceBody()
            {
                orderid = iOrderid,

                j_company = j_Company,
                j_contact = j_Contact,
                j_tel = j_Tel,
                j_address = j_Address,
                j_city = j_City,
                j_province = j_Province,

                d_company = d_Company,
                d_contact = d_Contact,
                d_tel = d_Tel,
                d_address = d_Address,
                d_city = d_City,
                d_province = d_Province,

                Payment = iPayment,

            };

            body.d_province = body.d_province.TrimEnd('市');
            body.j_province = body.j_province.TrimEnd('市');

            body.d_city = body.d_city.TrimEnd('市');
            body.j_city = body.j_city.TrimEnd('市');

            try
            {
                var response = this.sfexpressService(body.Serializer());

                OrderServiceResponse obj = new OrderServiceResponse(response);

                if (!obj.Deserialize())
                    return new ReturnEntity<OrderServiceResponse>(obj.ErrCode, obj.ErrMessage);
                else
                    return new ReturnEntity<OrderServiceResponse>(obj);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<OrderServiceResponse>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 生成取货订单
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <param name="j_Company"></param>
        /// <param name="j_Contact"></param>
        /// <param name="j_Tel"></param>
        /// <param name="j_Address"></param>
        /// <param name="j_City"></param>
        /// <param name="j_Province"></param>
        /// <param name="d_Company"></param>
        /// <param name="d_Contact"></param>
        /// <param name="d_Tel"></param>
        /// <param name="d_Address"></param>
        /// <param name="d_City"></param>
        /// <param name="d_Province"></param>
        /// <returns></returns>
        public ReturnEntity<OrderReverseServiceResponse> CreateReverseOrder(string iOrderid,
            string j_Company, string j_Contact, string j_Tel, string j_Address, string j_City, string j_Province,
            string d_Company, string d_Contact, string d_Tel, string d_Address, string d_City, string d_Province,
            DateTime? iExpectTime = null)
        {
            //Random rd = new Random();
            //return new ReturnEntity<OrderReverseServiceResponse>(new OrderReverseServiceResponse(null)
            //{
            //    destcode = rd.Next(0, 1000).ToString(),
            //    orderid = iOrderid,
            //    mailno = DateTime.Now.ToString("yyyyMMddHHmmss") + rd.Next(0, 10000).ToString().PadLeft(4, '0'),
            //    origincode = rd.Next(0, 1000).ToString(),
            //    filter_result = 2,
            //    remark = "3",
            //});

            OrderReverseServiceBody body = new OrderReverseServiceBody()
            {
                orderid = iOrderid,

                j_company = j_Company,
                j_contact = j_Contact,
                j_tel = j_Tel,
                j_address = j_Address,
                j_city = j_City,
                j_province = j_Province,

                d_company = d_Company,
                d_contact = d_Contact,
                d_tel = d_Tel,
                d_address = d_Address,
                d_city = d_City,
                d_province = d_Province,

                sendstarttime = iExpectTime,

            };

            body.d_province = body.d_province.TrimEnd('市');
            body.j_province = body.j_province.TrimEnd('市');

            try
            {
                var response = this.sfexpressService(body.Serializer());

                OrderReverseServiceResponse obj = new OrderReverseServiceResponse(response);

                if (!obj.Deserialize())
                    return new ReturnEntity<OrderReverseServiceResponse>(obj.ErrCode, obj.ErrMessage);
                else
                    return new ReturnEntity<OrderReverseServiceResponse>(obj);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<OrderReverseServiceResponse>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 订单发货取消
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<OrderConfirmServiceResponse> CancelOrder(string iOrderid)
        {
            OrderConfirmServiceBody body = new OrderConfirmServiceBody()
            {
                orderid = iOrderid,
                dealtype = 2,
            };

            try
            {
                var response = this.sfexpressService(body.Serializer());

                OrderConfirmServiceResponse obj = new OrderConfirmServiceResponse(response);

                if (!obj.Deserialize())
                    return new ReturnEntity<OrderConfirmServiceResponse>(obj.ErrCode, obj.ErrMessage);
                else
                    return new ReturnEntity<OrderConfirmServiceResponse>(obj);
            }
            catch (Exception ex)
            {
                return new ReturnEntity<OrderConfirmServiceResponse>(-999, ex.Message);
            }
        }

        /// <summary>
        /// 订单结果查询
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<OrderSearchServiceResponse> OrderSearch(string iOrderid)
        {
            OrderSearchServiceBody body = new OrderSearchServiceBody()
            {
                orderid = iOrderid,
            };

            var response = this.sfexpressService(body.Serializer());

            OrderSearchServiceResponse obj = new OrderSearchServiceResponse(response);

            if (!obj.Deserialize())
                return new ReturnEntity<OrderSearchServiceResponse>(obj.ErrCode, obj.ErrMessage);
            else
                return new ReturnEntity<OrderSearchServiceResponse>(obj);
        }

        /// <summary>
        /// 物流回调
        /// </summary>
        /// <param name="iMessage"></param>
        /// <returns></returns>
        public ReturnEntity<RoutePushServiceBody> OrderCallBack(string iMessage)
        {
            RoutePushServiceBody body = new RoutePushServiceBody(iMessage);

            if (!body.Deserialize(false))
            {
                return new ReturnEntity<RoutePushServiceBody>(body.ErrCode, body.ErrMessage);
            }

            return new ReturnEntity<RoutePushServiceBody>(body);
        }
    }
}
