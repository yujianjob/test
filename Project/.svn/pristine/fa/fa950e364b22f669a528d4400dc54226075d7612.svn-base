using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Library.Partners.QFExpress.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.QFExpress.Business
{
    public class OrderService : BusinessBase
    {
        /// <summary>
        /// 创建物流订单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="j_Name"></param>
        /// <param name="j_PostCode"></param>
        /// <param name="j_Phone"></param>
        /// <param name="j_Mobile"></param>
        /// <param name="j_Province"></param>
        /// <param name="j_City"></param>
        /// <param name="j_Address"></param>
        /// <param name="d_Name"></param>
        /// <param name="d_PostCode"></param>
        /// <param name="d_Phone"></param>
        /// <param name="d_Mobile"></param>
        /// <param name="d_Province"></param>
        /// <param name="d_City"></param>
        /// <param name="d_Address"></param>
        /// <param name="iItemList"></param>
        /// <param name="iExpectStartTime"></param>
        /// <param name="iExpectEndTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> CreateOrder(string iOrderNumber, string iExpressNumber,
            string j_Name, string j_PostCode, string j_Phone, string j_Mobile, string j_Province, string j_City, string j_Address,
            string d_Name, string d_PostCode, string d_Phone, string d_Mobile, string d_Province, string d_City, string d_Address,
            IDictionary<string, int> iItemList, DateTime? iExpectStartTime = null, DateTime? iExpectEndTime = null)
        {
            if (iItemList == null || iItemList.Count == 0)
            {
                return new ReturnEntity<bool>(-1, "物品列表空");
            }

            RequestOrderBody order = new RequestOrderBody()
            {
                txLogisticID = iOrderNumber,
                mailNo = iExpressNumber,
                sendStartTime = iExpectStartTime,
                sendEndTime = iExpectEndTime,
            };

            order.sender = new RequestOrderBody.Address()
            {
                name = j_Name,
                postCode = j_PostCode,
                phone = j_Phone,
                mobile = j_Mobile,
                prov = j_Province,
                city = j_City,
                address = j_Address,
            };

            order.receiver = new RequestOrderBody.Address()
            {
                name = d_Name,
                postCode = d_PostCode,
                phone = d_Phone,
                mobile = d_Mobile,
                prov = d_Province,
                city = d_City,
                address = d_Address,
            };

            order.items = new List<RequestOrderBody.Item>();

            foreach (var item in iItemList)
            {
                order.items.Add(new RequestOrderBody.Item()
                {
                    itemName = item.Key,
                    Number = item.Value,
                });
            }

            try
            {
                ResponseBody obj = null;

                var response = this.qfexpressService(order.Serializer());

                obj = new ResponseBody(response);

                if (!obj.Deserialize() || !obj.Success)
                {
                    return new ReturnEntity<bool>(obj.ErrCode, obj.ErrMessage);
                }
                else
                {
                    return new ReturnEntity<bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new ReturnEntity<bool>(-999, ex.Message);
            }

        }

        /// <summary>
        /// 取消物流订单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iRemark"></param>
        /// <returns></returns>
        public ReturnEntity<bool> CancelOrder(string iOrderNumber, string iRemark)
        {
            RequestCancelBody order = new RequestCancelBody()
            {
                txLogisticID = iOrderNumber,
                remark = iRemark,
            };

            var response = this.qfexpressService(order.Serializer());

            ResponseBody obj = new ResponseBody(response);

            if (!obj.Deserialize() || !obj.Success)
            {
                return new ReturnEntity<bool>(obj.ErrCode, obj.ErrMessage);
            }
            else
            {
                return new ReturnEntity<bool>(true);
            }
        }

        /// <summary>
        /// 物流回调
        /// </summary>
        /// <param name="iMessage"></param>
        /// <returns></returns>
        public ReturnEntity<UpdateInfoBody> OrderCallBack(string iMessage)
        {
            UpdateInfoBody body = new UpdateInfoBody(iMessage);

            if (body.ErrCode != 0)
            {
                return new ReturnEntity<UpdateInfoBody>(body.ErrCode, body.ErrMessage);
            }
            if (!body.Deserialize())
            {
                return new ReturnEntity<UpdateInfoBody>(body.ErrCode, body.ErrMessage);
            }

            return new ReturnEntity<UpdateInfoBody>(body);
        }

    }
}
