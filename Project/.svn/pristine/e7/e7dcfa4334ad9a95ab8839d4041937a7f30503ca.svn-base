using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Express.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Exp_NodeDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 站点名
        /// </summary>
        [Display(Name = "站点名")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 站点编号
        /// </summary>
        [Display(Name = "站点编号")]
        [DataMember]
        public string No { set; get; }

        /// <summary>
        /// 行政区
        /// </summary>
        [Display(Name = "行政区")]
        [DataMember]
        public int DistrictID { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        [Display(Name = "类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        [Display(Name = "地址")]
        [DataMember]
        public string Address { set; get; }

        /// <summary>
        /// 纬度
        /// </summary>
        [Display(Name = "纬度")]
        [DataMember]
        public decimal? Latitude { set; get; }

        /// <summary>
        /// 经度
        /// </summary>
        [Display(Name = "经度")]
        [DataMember]
        public decimal? Longitude { set; get; }

        /// <summary>
        /// 半径
        /// </summary>
        [Display(Name = "半径")]
        [DataMember]
        public int Radius { set; get; }

        /// <summary>
        /// 关键字
        /// </summary>
        [Display(Name = "关键字")]
        [DataMember]
        public string Keyword { set; get; }

        /// <summary>
        /// 仓库ID
        /// </summary>
        [Display(Name = "仓库ID")]
        [DataMember]
        public int StorageID { set; get; }

        /// <summary>
        /// 上级ID(站点->干线,干线->工厂)
        /// </summary>
        [Display(Name = "上级ID")]
        [DataMember]
        public int ParentID { set; get; }

        /// <summary>
        /// 站长
        /// </summary>
        [Display(Name = "站长")]
        [DataMember]
        public int ManagerID { set; get; }

        /// <summary>
        /// 联系人ID
        /// </summary>
        [Display(Name = "联系人ID")]
        [DataMember]
        public int LinkManID { set; get; }

        /// <summary>
        /// 保安队长ID
        /// </summary>
        [Display(Name = "保安队长ID")]
        [DataMember]
        public int CaptainID { set; get; }

        /// <summary>
        /// 启用状态
        /// </summary>
        [Display(Name = "启用状态")]
        [DataMember]
        public int Enable { set; get; }

        /// <summary>
        /// 预警类型
        /// </summary>
        [Display(Name = "预警类型")]
        [DataMember]
        public int AlarmType { set; get; }

        /// <summary>
        /// 区域ID
        /// </summary>
        [Display(Name = "区域ID")]
        [DataMember]
        public int? AreaID { set; get; }

        /// <summary>
        /// 站点开发者ID
        /// </summary>
        [Display(Name = "站点开发者ID")]
        [DataMember]
        public int CreateOperatorID { set; get; }

        #endregion Model
        
        /// <summary>
        /// 上级编号
        /// </summary>
        [Display(Name = "上级编号")]
        [DataMember]
        public string ParentNo { set; get; }
        
        /// <summary>
        /// 上级名称
        /// </summary>
        [Display(Name = "上级名称")]
        [DataMember]
        public string ParentName { set; get; }
        
        /// <summary>
        /// 行政区
        /// </summary>
        [Display(Name = "行政区")]
        [DataMember]
        public string DistrictName { set; get; }

        /// <summary>
        /// 负责人名
        /// </summary>
        [Display(Name = "负责人名")]
        [DataMember]
        public string ManagerName { set; get; }

        /// <summary>
        /// 联系人名
        /// </summary>
        [Display(Name = "联系人名")]
        [DataMember]
        public string LinkManName { set; get; }

        /// <summary>
        /// 保安队长
        /// </summary>
        [Display(Name = "保安队长")]
        [DataMember]
        public string CaptainName { set; get; }

        /// <summary>
        /// 区域名
        /// </summary>
        [Display(Name = "区域名")]
        [DataMember]
        public string AreaName { set; get; }

        /// <summary>
        /// 站点开发者名
        /// </summary>
        [Display(Name = "站点开发者名")]
        [DataMember]
        public string CreateOperatorName { set; get; }


        public IList<string> KeywordList
        {
            get
            {
                if (!string.IsNullOrEmpty(Keyword))
                {
                    return Keyword.Split('|');
                }
                else
                {
                    return new List<string>();
                }
            }
            set { }
        }
        /// <summary>
        /// 根据Reader生成Exp_NodeDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Exp_NodeDC GetEntity(IDataReader reader)
        {
            Exp_NodeDC entity = null;

            entity = new Exp_NodeDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
