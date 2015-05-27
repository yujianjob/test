using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using LazyAtHome.WCF.WebService.Contract.DataContract.SFExpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface ISFExpress
    {
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
        /// <param name="iPayment"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<OrderDC> CreateOrder(LoginCredentials iCredentials, string iOrderid,
            string j_Company, string j_Contact, string j_Tel, string j_Address, string j_City, string j_Province,
            string d_Company, string d_Contact, string d_Tel, string d_Address, string d_City, string d_Province,
            decimal? iPayment = null);

        /// <summary>
        /// 订单结果查询
        /// </summary>
        /// <param name="iOrderid"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<OrderDC> OrderSearch(LoginCredentials iCredentials, string iOrderid);

    }
}
