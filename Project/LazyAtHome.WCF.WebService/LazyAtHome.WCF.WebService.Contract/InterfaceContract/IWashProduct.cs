using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
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
    public interface IWashProduct
    {
        /// <summary>
        /// 获取小类
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Wash_ClassDC>> Wash_Class_SELECT_List_Second(LoginCredentials iCredentials);

        /// <summary>
        /// 获取所有运价
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<Wash_ProductDC>> Wash_Product_SELECT_List(LoginCredentials iCredentials);
    }
}
