using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Contract.DataContract.Base
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Base_SiteDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string Pinyin { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal? Longitude { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public decimal? Latitude { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int IsHot { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int IsShow { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Base_SiteDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_SiteDC GetEntity(IDataReader reader)
        {
            Base_SiteDC entity = null;

            entity = new Base_SiteDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
