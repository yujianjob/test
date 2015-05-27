using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.DataContract.weixin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.InterfaceContract
{
    /// <summary>
    /// 
    /// </summary>
    [ServiceContract(SessionMode = SessionMode.Required)]
    public interface IwxProduct
    {
        /// <summary>
        /// 获取所有小类
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<wx_Wash_ClassDC>> wx_Wash_Class_SELECT_List();

        /// <summary>
        /// 根据小类ID获取运产品表
        /// </summary>
        /// <param name="iClassID"></param>
        /// <param name="iSite"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<IList<wx_Wash_CategoryDC>> wx_Wash_Category_SELECT_List(int iClassID, int iSite);

        /// <summary>
        /// 根据CategoryID获取运价
        /// </summary>
        /// <param name="iCategoryID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<wx_Wash_CategoryDC> wx_Wash_Category_SELECT_Entity(int iCategoryID);

        /// <summary>
        /// 根据ProductID获取运价
        /// </summary>
        /// <param name="iProductID"></param>
        /// <returns></returns>
        [OperationContract]
        ReturnEntity<wx_Wash_ProductDC> wx_Wash_Product_SELECT_Entity(int iProductID);
    }
}
