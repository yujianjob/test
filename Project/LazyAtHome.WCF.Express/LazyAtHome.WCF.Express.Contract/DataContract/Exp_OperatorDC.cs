//using System;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Collections.Generic;
//using System.Runtime.Serialization;
//using System.ComponentModel.DataAnnotations;
//using LazyAtHome.Core.Infrastructure.WCF;

//namespace LazyAtHome.WCF.Express.Contract.DataContract
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    [DataContract]
//    [Serializable]
//    public class Exp_OperatorDC : EntityBase
//    {
//        #region Model
//        /// <summary>
//        /// 主键
//        /// </summary>
//        [Display(Name = "主键")]
//        [DataMember]
//        public int ID { set; get; }

//        /// <summary>
//        /// 登录名
//        /// </summary>
//        [Display(Name = "登录名")]
//        [DataMember]
//        public string LoginName { set; get; }

//        /// <summary>
//        /// 登录密码
//        /// </summary>
//        [Display(Name = "登录密码")]
//        [DataMember]
//        public string LoginPwd { set; get; }

//        /// <summary>
//        /// 类型
//        /// </summary>
//        [Display(Name = "类型")]
//        [DataMember]
//        public int Type { set; get; }

//        /// <summary>
//        /// 站点ID
//        /// </summary>
//        [Display(Name = "站点ID")]
//        [DataMember]
//        public int NodeID { set; get; }

//        /// <summary>
//        /// 姓名
//        /// </summary>
//        [Display(Name = "姓名")]
//        [DataMember]
//        public string Name { set; get; }

//        /// <summary>
//        /// 手机号
//        /// </summary>
//        [Display(Name = "手机号")]
//        [DataMember]
//        public string MpNo { set; get; }

//        /// <summary>
//        /// 启用状态
//        /// </summary>
//        [Display(Name = "启用状态")]
//        [DataMember]
//        public int Enable { set; get; }

//        /// <summary>
//        /// 在岗状态
//        /// </summary>
//        [Display(Name = "在岗状态")]
//        [DataMember]
//        public int OnDuty { set; get; }

//        #endregion Model

//        /// <summary>
//        /// 站点名称
//        /// </summary>
//        [DataMember]
//        public string NodeName { set; get; }

//        /// <summary>
//        /// 仓库ID
//        /// </summary>
//        [DataMember]
//        public int StorageID { set; get; }

//        /// <summary>
//        /// 根据Reader生成Exp_OperatorDC对象
//        /// </summary>
//        /// <param name="reader">数据集</param>
//        public static Exp_OperatorDC GetEntity(IDataReader reader)
//        {
//            Exp_OperatorDC entity = null;

//            entity = new Exp_OperatorDC();

//            entity.AutoGetEntity(reader);

//            return entity;
//        }
//    }
//}
