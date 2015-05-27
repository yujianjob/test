using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Service.WorkQueue.Contract.DataContract.Notify
{

    [Serializable]
    public class NotifyMessageItem : LazyAtHome.Core.Infrastructure.WCF.EntityBase, LazyAtHome.Core.Infrastructure.WorkQueue.IQueueItem
    {
        private string _TypeName = "Notify";
        public string TypeName
        {
            set { }
            get
            {
                return _TypeName;
            }
        }

        /// <summary>
        /// 事件编号
        /// </summary>
        public string EventNumber { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNumber { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }

        /// <summary>
        /// 人员ID
        /// </summary>
        public int PersonnelID { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public int Class { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 状态(待处理、处理中、完成、关闭)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 通知人员列表
        /// </summary>
        public IList<int> NotifyUserList { get; set; }
        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSms { set; get; }
        /// <summary>
        /// 是否发送邮件
        /// </summary>
        public bool IsEmail { set; get; }

        public override string ToString()
        {
            string format = "TypeName:{0} OrderNumber{1} EventNumber:{2} RoleID:{3} PersonnelID:{4} Class:{5} Title:{6} Content:{7} Level:{8} Status:{9} IsSms:{10} IsEmail:{11}";
            return string.Format(format, TypeName, OrderNumber, EventNumber, RoleID, PersonnelID, Class, Title, Content, Level, Status, IsSms, IsEmail);
        }
    }
}