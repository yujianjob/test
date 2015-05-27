using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Express.Contract.Enumerate
{
    /// <summary>
    /// 目标库类型
    /// </summary>
    [DataContract]
    public enum StorageTargetType
    {
        /// <summary>
        ///  0:无
        /// </summary>
        [EnumMember]
        None = 0,

        /// <summary>
        ///  1:送站点
        /// </summary>
        [EnumMember]
        ToSite = 1,

        /// <summary>
        ///  2:送干线
        /// </summary>
        [EnumMember]
        ToLine = 2,

        /// <summary>
        ///  3:送工厂
        /// </summary>
        [EnumMember]
        ToFactory = 3,

        /// <summary>
        ///  4:待分拣
        /// </summary>
        [EnumMember]
        ToCheck = 4,

        /// <summary>
        ///  5:送洗
        /// </summary>
        [EnumMember]
        ToWash = 5,

        /// <summary>
        ///  6:工厂打包
        /// </summary>
        [EnumMember]
        FactoryPick = 6,

        /// <summary>
        ///  7:工厂出库
        /// </summary>
        [EnumMember]
        FactoryOut = 7,

        /// <summary>
        /// 8:送用户
        /// </summary>
        [EnumMember]
        ToUser = 8,
    }

    /// <summary>
    /// 物品类型
    /// </summary>
    [DataContract]
    public enum StorageItemType
    {
        /// <summary>
        ///  1:包裹 
        /// </summary>
        [EnumMember]
        Package = 1,

        /// <summary>
        ///  2:衣物
        /// </summary>
        [EnumMember]
        Clothing = 2,

    }

    /// <summary>
    /// 物品类型
    /// </summary>
    [DataContract]
    public enum SystemStorage
    {
        /// <summary>
        ///  1:废品库
        /// </summary>
        [EnumMember]
        Damage = 1,

        /// <summary>
        ///  2:丢件库
        /// </summary>
        [EnumMember]
        Lost = 2,

        /// <summary>
        ///  3:分拣完成库
        /// </summary>
        [EnumMember]
        CheckComplete = 3,

        /// <summary>
        ///  4:送件完成库
        /// </summary>
        [EnumMember]
        SendComplete = 4,
    }

    /// <summary>
    /// 仓库类型
    /// </summary>
    [DataContract]
    public enum StorageType
    {
        /// <summary>
        ///  0:系统
        /// </summary>
        [EnumMember]
        System = 0,

        /// <summary>
        ///  1:站点
        /// </summary>
        [EnumMember]
        Site = 1,

        /// <summary>
        ///  2:干线
        /// </summary>
        [EnumMember]
        Line = 2,

        /// <summary>
        ///  3:工厂
        /// </summary>
        [EnumMember]
        Factory = 3,
    }
}
