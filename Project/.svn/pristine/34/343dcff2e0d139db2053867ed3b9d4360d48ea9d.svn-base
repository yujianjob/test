using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.Wash.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Wash_Store_OperatorDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 门店ID
        /// </summary>
        [Display(Name = "门店ID")]
        [DataMember]
        public int StoreID { set; get; }

        /// <summary>
        /// 编号
        /// </summary>
        [Display(Name = "编号")]
        [DataMember]
        public string StoreCode { set; get; }

        /// <summary>
        /// 登录名
        /// </summary>
        [Display(Name = "登录名")]
        [DataMember]
        public string LoginName { set; get; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        [DataMember]
        public string LoginPwd { set; get; }

        /// <summary>
        /// 姓名
        /// </summary>
        [Display(Name = "姓名")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 联系手机
        /// </summary>
        [Display(Name = "联系手机")]
        [DataMember]
        public string MPNo { set; get; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Display(Name = "是否启用")]
        [DataMember]
        public int Enable { set; get; }

        /// <summary>
        /// 是否最高权限
        /// </summary>
        [Display(Name = "是否最高权限")]
        [DataMember]
        public int IsAdmin { set; get; }

        #endregion Model

        /// <summary>
        /// 根据Reader生成Wash_Store_OperatorDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Wash_Store_OperatorDC GetEntity(IDataReader reader)
        {
            Wash_Store_OperatorDC entity = null;

            entity = new Wash_Store_OperatorDC();

            entity.AutoGetEntity(reader);

            return entity;
        }
    }
}
