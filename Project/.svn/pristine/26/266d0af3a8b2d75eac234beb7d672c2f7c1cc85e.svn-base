using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.Winform.FactoryPortal.Common;
using LazyAtHome.Winform.FactoryPortal.WashProductService;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.Core.Helper;

namespace LazyAtHome.Winform.FactoryPortal.Business
{
    public class FormWashProductClient : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<IWashProduct>
    {
        public FormWashProductClient()
            : base("WSHttpBinding_IWashProduct")
        { }
    }

    /// <summary>
    /// 产品运价逻辑类
    /// </summary>
    public class WashProduct : Base
    {
        //public IWashProduct WSWashProductService = new WashProductClient();
        public FormWashProductClient WSWashProductService = new FormWashProductClient();

        /// <summary>
        /// 获取产品运价小类
        /// </summary>
        public IList<Wash_ClassDC> GetWashClassList()
        {
            IList<Wash_ClassDC> list = null;

            ReturnEntity<Wash_ClassDC[]> re = null;

            WashProductService.LoginCredentials login = Common.ConstConfig.GetWashProductLogin();

            try
            {
                //re = WSWashProductService.Proxy.Wash_Class_SELECT_List_Second(login);
                re = WCFInvokeHelper<FormWashProductClient>.Invoke<ReturnEntity<Wash_ClassDC[]>>
                    (client => client.Proxy.Wash_Class_SELECT_List_Second(login));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashProduct GetWashClassList|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    list = re.OBJ;
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return list;
        }



        /// <summary>
        /// 获取产品运价
        /// </summary>
        public IList<Wash_ProductDC> GetWashProductList()
        {
            IList<Wash_ProductDC> list = null;

            ReturnEntity<Wash_ProductDC[]> re = null;

            WashProductService.LoginCredentials login = Common.ConstConfig.GetWashProductLogin();

            try
            {
                //re = WSWashProductService.Proxy.Wash_Product_SELECT_List(login);

                re = WCFInvokeHelper<FormWashProductClient>.Invoke<ReturnEntity<Wash_ProductDC[]>>
                    (client => client.Proxy.Wash_Product_SELECT_List(login));

            }
            catch (Exception ex)
            {
                WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, "|WashProduct GetWashProductList|" + ex.Message);
                LastError = ex.Message;
            }
            if (re != null)
            {
                if (re.Success)
                {
                    list = re.OBJ;
                }
                else
                {
                    //处理报错信息
                    LastError = re.Message;
                }

            }
            return list;
        }



    }
}
