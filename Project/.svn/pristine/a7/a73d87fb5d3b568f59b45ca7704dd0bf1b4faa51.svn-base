using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using LazyAtHome.WCF.WebService.Contract.DataContract.SFExpress;
using LazyAtHome.WCF.WebService.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Portal
{
    public class SFExpressPortal : BasePortal, ISFExpress
    {
        protected LazyAtHome.WCF.WebService.Business.Business.SFExpress SFExpressInstance = LazyAtHome.WCF.WebService.Business.Business.SFExpress.Instance;

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="iCredentials"></param>
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
        public ReturnEntity<OrderDC> CreateOrder(LoginCredentials iCredentials, string iOrderid,
            string j_Company, string j_Contact, string j_Tel, string j_Address, string j_City, string j_Province,
            string d_Company, string d_Contact, string d_Tel, string d_Address, string d_City, string d_Province,
            decimal? iPayment = null)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<OrderDC>(-99, "操作员验证错误");
            }

            return SFExpressInstance.CreateOrder(iOrderid,
                         j_Company, j_Contact, j_Tel, j_Address, j_City, j_Province,
                         d_Company, d_Contact, d_Tel, d_Address, d_City, d_Province, iPayment);
        }

        /// <summary>
        /// 订单结果查询
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        public ReturnEntity<OrderDC> OrderSearch(LoginCredentials iCredentials, string iOrderid)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<OrderDC>(-99, "操作员验证错误");
            }

            return SFExpressInstance.OrderSearch(iOrderid);
        }
    }
}
