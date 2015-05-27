using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.WCF.UserSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class User_DetailDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public Guid ID { set; get; }

        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int SeedID { set; get; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Display(Name = "昵称")]
        [DataMember]
        public string NickName { set; get; }

        /// <summary>
        /// 生日
        /// </summary>
        [Display(Name = "生日")]
        [DataMember]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Date)]
        public DateTime? Birthday { set; get; }

        /// <summary>
        /// 性别(1:男 0:女 2:保密)
        /// </summary>
        [Display(Name = "性别")]
        [DataMember]
        public int? Sex { set; get; }

        /// <summary>
        /// 身份证
        /// </summary>
        [Display(Name = "身份证")]
        [DataMember]
        public string IDCard { set; get; }

        /// <summary>
        /// 真实名称
        /// </summary>
        [Display(Name = "真实名称")]
        [DataMember]
        public string RealName { set; get; }

        /// <summary>
        /// 省市区
        /// </summary>
        [Display(Name = "省市区")]
        [DataMember]
        public int? DistrictID { set; get; }

        /// <summary>
        /// 所在地地址
        /// </summary>
        [Display(Name = "所在地地址")]
        [DataMember]
        public string Location { set; get; }

        /// <summary>
        /// 婚姻状况(1:未婚 2:已婚 3:离异)
        /// </summary>
        [Display(Name = "婚姻状况")]
        [DataMember]
        public int? MaritalStatus { set; get; }

        /// <summary>
        /// 月收入(1:0-1999 2:2000-4999)
        /// </summary>
        [Display(Name = "月收入")]
        [DataMember]
        public int? Salary { set; get; }

        /// <summary>
        /// 兴趣爱好(空格分隔)
        /// </summary>
        [Display(Name = "兴趣爱好")]
        [DataMember]
        public string Hobbies { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成User_DetailDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static User_DetailDC GetEntity(IDataReader reader)
        {
            User_DetailDC entity = null;

            entity = new User_DetailDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
