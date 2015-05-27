using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using LazyAtHome.WCF.WebService.Contract.DataContract.PR;
using LazyAtHome.WCF.WebService.Contract.InterfaceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Portal
{
    public class WashProductPortal : BasePortal, IWashProduct
    {
        protected LazyAtHome.WCF.WebService.Business.Business.WashProduct WashProductInstance = LazyAtHome.WCF.WebService.Business.Business.WashProduct.Instance;
        
        /// <summary>
        /// 获取小类
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ClassDC>> Wash_Class_SELECT_List_Second(LoginCredentials iCredentials)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<IList<Wash_ClassDC>>(-99, "操作员验证错误");
            }
            return WashProductInstance.Wash_Class_SELECT_List_Second();
        }

        /// <summary>
        /// 获取所有运价
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ProductDC>> Wash_Product_SELECT_List(LoginCredentials iCredentials)
        {
            if (!base.LoginCredentials_Check(iCredentials))
            {
                return new ReturnEntity<IList<Wash_ProductDC>>(-99, "操作员验证错误");
            }
            return WashProductInstance.Wash_Product_SELECT_List();
        }
    }
}
