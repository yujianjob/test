using LazyAtHome.WCF.Common.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Contract.DataContract.PR
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class LoginCredentials
    {
        [DataMember]
        public string LoginName { set; get; }

        [DataMember]
        public string Password { set; get; }

        [DataMember]
        public OperatorType OperatorType { set; get; }


        public string CacheKey
        {
            get
            {

                return "WS_OP_" + LoginName + "_" + (int)OperatorType;
            }
            set { }
        }
    }
}
