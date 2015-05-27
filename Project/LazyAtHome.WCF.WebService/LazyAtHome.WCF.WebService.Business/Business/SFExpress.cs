using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Library.Partners.SFExpress.Entity;
using LazyAtHome.WCF.WebService.Contract.DataContract.SFExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class SFExpress
    {
        public SFExpress()
        {
            LazyAtHome.Library.Partners.Common.WriteToFileEvent += Common.WriteToFile;
        }

        static SFExpress _SFExpress;
        public static SFExpress Instance
        {
            get
            {
                if (_SFExpress == null)
                {
                    _SFExpress = new SFExpress();
                }
                return _SFExpress;
            }
        }

        private LazyAtHome.Library.Partners.SFExpress.Business.OrderService _OrderService = new LazyAtHome.Library.Partners.SFExpress.Business.OrderService();


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
        public ReturnEntity<OrderDC> CreateOrder(string iOrderid,
            string j_Company, string j_Contact, string j_Tel, string j_Address, string j_City, string j_Province,
            string d_Company, string d_Contact, string d_Tel, string d_Address, string d_City, string d_Province,
            decimal? iPayment = null)
        {
            var rtn = _OrderService.CreateOrder(iOrderid,
                         j_Company, j_Contact, j_Tel, j_Address, j_City, j_Province,
                         d_Company, d_Contact, d_Tel, d_Address, d_City, d_Province,
                         iPayment);
            if (rtn != null)
            {
                if (rtn.Success && rtn.OBJ != null)
                {
                    OrderDC entity = new OrderDC()
                    {
                        orderid = rtn.OBJ.orderid,
                        mailno = rtn.OBJ.mailno,
                        origincode = rtn.OBJ.origincode,
                        destcode = rtn.OBJ.destcode,
                        filter_result = rtn.OBJ.filter_result,
                        remark = rtn.OBJ.remark,
                    };
                    return new ReturnEntity<OrderDC>(entity);
                }
                else
                {
                    return new ReturnEntity<OrderDC>(rtn.Code, rtn.Message);
                }
            }
            else
            {
                return new ReturnEntity<OrderDC>(-1, "调用系统失败,返回对象空");
            }
        }

        /// <summary>
        /// 订单结果查询
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<OrderDC> OrderSearch(string iOrderid)
        {
            var rtn = _OrderService.OrderSearch(iOrderid);
            if (rtn != null)
            {
                if (rtn.Success && rtn.OBJ != null)
                {
                    OrderDC entity = new OrderDC()
                    {
                        orderid = rtn.OBJ.orderid,
                        mailno = rtn.OBJ.mailno,
                        origincode = rtn.OBJ.origincode,
                        destcode = rtn.OBJ.destcode,
                        filter_result = rtn.OBJ.filter_result,
                        remark = rtn.OBJ.remark,
                    };
                    return new ReturnEntity<OrderDC>(entity);
                }
                else
                {
                    return new ReturnEntity<OrderDC>(rtn.Code, rtn.Message);
                }
            }
            else
            {
                return new ReturnEntity<OrderDC>(-1, "调用系统失败,返回对象空");
            }
        }
    }



}
