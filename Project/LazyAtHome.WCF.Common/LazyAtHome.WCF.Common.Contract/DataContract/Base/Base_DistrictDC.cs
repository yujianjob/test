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
    public class Base_DistrictDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public string Code1 { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public string Code2 { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public string Code3 { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public int Level { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "")]
        [DataMember]
        public string FullName { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Base_DistrictDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Base_DistrictDC GetEntity(IDataReader reader)
        {
            Base_DistrictDC entity = null;

            entity = new Base_DistrictDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
