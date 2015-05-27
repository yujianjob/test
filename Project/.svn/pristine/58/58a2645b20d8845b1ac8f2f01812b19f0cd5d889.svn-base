using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using LazyAtHome.Core.Infrastructure.WCF;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_SharePictureDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 晒单ID
        /// </summary>
        [Display(Name = "晒单ID")]
        [DataMember]
        public int ShareID { set; get; }

        /// <summary>
        /// 类型(1:洗前 2:洗后)
        /// </summary>
        [Display(Name = "类型(1:洗前 2:洗后)")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "路径")]
        [DataMember]
        public string Path { set; get; }

        /// <summary>
        /// 图片说明
        /// </summary>
        [Display(Name = "图片说明")]
        [DataMember]
        public string Content { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_SharePictureDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_SharePictureDC GetEntity(IDataReader reader)
        {
            User_SharePictureDC entity = null;

            entity = new User_SharePictureDC();

            entity.AutoGetEntity(reader);

            return entity;
        }

    }
}
