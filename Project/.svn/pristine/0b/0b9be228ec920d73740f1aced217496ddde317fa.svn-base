using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.Wash.Contract.ClientProxy;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Business
{
    public class WashProduct
    {
        public WashProduct()
        {

        }

        static WashProduct _WashProduct;
        public static WashProduct Instance
        {
            get
            {
                if (_WashProduct == null)
                {
                    _WashProduct = new WashProduct();
                }
                return _WashProduct;
            }
        }

        /// <summary>
        /// 获取小类
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ClassDC>> Wash_Class_SELECT_List_Second()
        {
            var entity = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<IList<Wash_ClassDC>>>
                (client => client.Proxy.Wash_Class_SELECT_List_ALL());

            if (entity != null && entity.Success && entity.OBJ != null)
            {
                return new ReturnEntity<IList<Wash_ClassDC>>(entity.OBJ.Where(p => p.ParentID != 0).ToList());
            }
            else
            {
                return new ReturnEntity<IList<Wash_ClassDC>>(-1, "获取数据失败");
            }
        }

        /// <summary>
        /// 获取所有运价
        /// </summary>
        /// <returns></returns>
        public ReturnEntity<IList<Wash_ProductDC>> Wash_Product_SELECT_List()
        {
            var entity = WCFInvokeHelper<ProductClient>.Invoke<ReturnEntity<RecordCountEntity<Wash_ProductDC>>>
             (client => client.Proxy.Wash_Product_SELECT_List(null, null, 1, 1, null, null, 1, 500));
            if (entity != null && entity.Success && entity.OBJ != null && entity.OBJ.ReturnList != null)
            {
                return new ReturnEntity<IList<Wash_ProductDC>>(entity.OBJ.ReturnList);
            }
            else
            {
                return new ReturnEntity<IList<Wash_ProductDC>>(-1, "获取数据失败");
            }

        }

    }
}
