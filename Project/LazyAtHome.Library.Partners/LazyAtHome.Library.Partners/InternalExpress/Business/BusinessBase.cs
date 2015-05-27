using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.InternalExpress.Business
{
    public class BusinessBase : LazyAtHome.Core.Infrastructure.WCF.Client.ClientProxyBase<InternalExpressService.IInternalExpress>
    {

        public BusinessBase()
            : base("WSHttpBinding_IInternalExpress")
        {

        }

    }
}
