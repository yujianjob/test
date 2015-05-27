using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Contract.InterfaceContract
{
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IQFExpress
    {

        /// <summary>
        /// 
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
        [OperationContract]
        ReturnEntity<bool> CreateOrder(LoginCredentials iCredentials, string iOrderNumber, string iExpressNumber,
            string j_Name, string j_PostCode, string j_Phone, string j_Mobile, string j_Province, string j_City, string j_Address,
            string d_Name, string d_PostCode, string d_Phone, string d_Mobile, string d_Province, string d_City, string d_Address,
            IDictionary<string, int> iItemList, DateTime? iExpectStartTime = null, DateTime? iExpectEndTime = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iRemark"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<bool> CancelOrder(LoginCredentials iCredentials, string iOrderNumber, string iRemark);

    }
}
