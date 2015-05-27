using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.WorkQueue.Contract.DataContract.Log
{
    [Serializable]
    public class OperatorLogItem : LazyAtHome.Core.Infrastructure.WorkQueue.IQueueItem
    {
        private string _TypeName = "Log_Operator";
        public string TypeName
        {
            set { }
            get
            {
                return _TypeName;
            }
        }

        public int OperatorID { get; set; }
        public string OperatorName { get; set; }

        public int Type { get; set; }

        public string LogContent { get; set; }
    }
}
