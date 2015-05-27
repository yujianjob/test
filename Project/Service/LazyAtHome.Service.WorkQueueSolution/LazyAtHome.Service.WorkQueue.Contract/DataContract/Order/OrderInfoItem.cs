using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.WorkQueue.Contract.DataContract.Order
{
    public class OrderInfoItem : LazyAtHome.Core.Infrastructure.WorkQueue.IQueueItem
    {
        private string _TypeName = "Order_Operator";
        public string TypeName
        {
            set { }
            get
            {
                return _TypeName;
            }
        }

        public string OrderNumber { get; set; }
        public int OperatoFlag { get; set; }
        
    }
}
