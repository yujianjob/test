using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Contract.DataContract
{
    [DataContract]
    [Serializable]
    public class BaseModel
    {
        [DataMember]
        public int code { get; set; }

        [DataMember]
        public string message { get; set; }
    }
}
