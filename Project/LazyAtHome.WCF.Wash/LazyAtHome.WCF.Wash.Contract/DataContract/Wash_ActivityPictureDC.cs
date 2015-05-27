using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Wash.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Wash_ActivityPictureDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 活动ID
        /// </summary>
        [Display(Name = "活动ID")]
        [DataMember]
        public int ActivityID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 图片路径
        /// </summary>
        [Display(Name = "图片路径")]
        [DataMember]
        public string Picture { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Wash_ActivityPictureDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_ActivityPictureDC GetEntity(IDataReader reader)
        {
            Wash_ActivityPictureDC entity = null;

            entity = new Wash_ActivityPictureDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
