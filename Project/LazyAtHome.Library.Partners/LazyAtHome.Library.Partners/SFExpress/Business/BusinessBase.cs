using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.SFExpress.Business
{
    public class BusinessBase : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<SFExpressService.IService>
    {

        public BusinessBase()
            : base("CommonServicePort")
        {

        }

        protected string sfexpressService(string xml)
        {
            Common.WriteToFile("SF_" + DateTime.Today.ToString("yyyyMMdd"), xml);

            SFExpressService.sfexpressServiceBody sfes = new SFExpressService.sfexpressServiceBody();
            sfes.arg0 = xml;

            SFExpressService.sfexpressService req = new SFExpressService.sfexpressService(sfes);
            SFExpressService.sfexpressServiceResponse resp = this.Proxy.sfexpressService(req);
            SFExpressService.sfexpressServiceResponseBody body = resp.Body;

            Common.WriteToFile("SF_" + DateTime.Today.ToString("yyyyMMdd"), body.@return);

            return System.Web.HttpUtility.UrlDecode(body.@return);
        }
    }
}
