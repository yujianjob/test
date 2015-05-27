using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LazyAtHome.WCF.Express.Contract.DataContract
{
    [DataContract]
    [Serializable]
    public class RoutePushModel : BaseModel
    {
        [DataMember]
        public IList<Exp_RoutePushDC> list { get; set; }
    }
}
