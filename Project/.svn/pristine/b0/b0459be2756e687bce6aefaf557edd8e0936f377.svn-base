using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.Wash.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Wash_ActivityDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 链接
        /// </summary>
        [Display(Name = "链接")]
        [DataMember]
        public string Link { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Display(Name = "开始时间")]
        [DataMember]
        public DateTime BeginDate { set; get; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Display(Name = "结束时间")]
        [DataMember]
        public DateTime EndDate { set; get; }

        /// <summary>
        /// 站点
        /// </summary>
        [Display(Name = "站点")]
        [DataMember]
        public int Site { set; get; }

        /// <summary>
        /// 渠道
        /// </summary>
        [Display(Name = "渠道")]
        [DataMember]
        public int Channel { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        [Display(Name = "排序")]
        [DataMember]
        public int Sort { set; get; }

        /// <summary>
        /// 大图
        /// </summary>
        [Display(Name = "大图")]
        [DataMember]
        public string PictureL { set; get; }

        /// <summary>
        /// 中图
        /// </summary>
        [Display(Name = "中图")]
        [DataMember]
        public string PictureM { set; get; }

        /// <summary>
        /// 小图
        /// </summary>
        [Display(Name = "小图")]
        [DataMember]
        public string PictureS { set; get; }

        /// <summary>
        /// 图片描述
        /// </summary>
        [Display(Name = "图片描述")]
        [DataMember]
        public string PictureAlt { set; get; }

        /// <summary>
        /// 确认状态
        /// </summary>
        [Display(Name = "确认状态")]
        [DataMember]
        public int CommitStatus { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Wash_ActivityDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_ActivityDC GetEntity(IDataReader reader)
        {
            Wash_ActivityDC entity = null;

            entity = new Wash_ActivityDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
