using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Winform.FactoryPortal.Model
{
    /// <summary>
    /// 操作日志实体
    /// </summary>
    public class OperatorLog
    {

        //[Display(Name = "主键")]
        public int ID { get; set; }
        //[Display(Name = "日志内容")]
        public string LogContent { get; set; }
        //[Display(Name = "操作人")]
        public int OperatorID { get; set; }
        //[Display(Name = "操作人名字")]
        public string OperatorName { get; set; }
        //[Display(Name = "日志类型")]
        public int Type { get; set; }
    }
}
