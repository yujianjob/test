using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using LazyAtHome.Core.Infrastructure.WCF;
using System.Reflection;
using System.Collections;

namespace LazyAtHome.WCF.Common.Contract.DataContract.PR
{
    /// <summary>
    /// 角色
    /// </summary>
    [DataContract]
    [Serializable]
    public class RoleDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string Name { set; get; }

        #endregion Model

        public static int Role_CustomerService = 10;

        public static int Role_Admin = 999;

        /// <summary>
        /// 配置用
        /// </summary>
        public static IList<RoleDC> CONFIG_LIST = new List<RoleDC>()
        {
            new RoleDC(){ ID = 1,Name = "保安" },
            new RoleDC(){ ID = 2,Name = "保安队长" },
            new RoleDC(){ ID = 3,Name = "站长（站点负责人）" },
            new RoleDC(){ ID = 4,Name = "工厂分拣员" },
            new RoleDC(){ ID = 5,Name = "工厂入库员" },
            new RoleDC(){ ID = 6,Name = "工厂出库员" },
            new RoleDC(){ ID = 7,Name = "工厂负责人" },
            new RoleDC(){ ID = 8,Name = "干线人员" },
            new RoleDC(){ ID = 9,Name = "干线负责人" },
            new RoleDC(){ ID = 10,Name = "客服" },
            new RoleDC(){ ID = 11,Name = "地推人员" },
            new RoleDC(){ ID = 12,Name = "地推组长" },
            new RoleDC(){ ID = 13,Name = "区域经理" },
            new RoleDC(){ ID = 14,Name = "物流主管" },
            new RoleDC(){ ID = 15,Name = "城市经理" },
            new RoleDC(){ ID = 16,Name = "财务" },

            new RoleDC(){ ID = 999,Name = "系统管理员" },
            new RoleDC(){ ID = 900,Name = "CEO" },
        };
    }



}
