using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Base
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Base_WebAttributeDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 页面名称
        /// </summary>
        [Display(Name = "页面名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 页面链接
        /// </summary>
        [Display(Name = "页面链接")]
        [DataMember]
        public string Page { set; get; }

        /// <summary>
        /// 标题
        /// </summary>
        [Display(Name = "标题")]
        [DataMember]
        public string Title { set; get; }

        /// <summary>
        /// 关键字
        /// </summary>
        [Display(Name = "关键字")]
        [DataMember]
        public string Keywords { set; get; }

        /// <summary>
        /// 说明
        /// </summary>
        [Display(Name = "说明")]
        [DataMember]
        public string Description { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Base_WebAttributeDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_WebAttributeDC GetEntity(IDataReader reader)
        {
            Base_WebAttributeDC entity = null;

            entity = new Base_WebAttributeDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
