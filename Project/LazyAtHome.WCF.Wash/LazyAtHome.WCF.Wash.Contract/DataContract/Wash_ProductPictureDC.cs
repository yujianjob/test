//using LazyAtHome.Core.Infrastructure.WCF;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Data;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Threading.Tasks;

//namespace LazyAtHome.WCF.Wash.Contract.DataContract
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    [DataContract]
//    [Serializable]
//    public class Wash_ProductPictureDC : EntityBase
//    {
//        #region Model
//        /// <summary>
//        /// 
//        /// </summary>
//        [Display(Name = "主键")]
//        [DataMember]
//        public int ID { set; get; }

//        /// <summary>
//        /// 产品运价ID
//        /// </summary>
//        [Display(Name = "产品运价ID")]
//        [DataMember]
//        public int ProductID { set; get; }

//        /// <summary>
//        /// 类别
//        /// </summary>
//        [Display(Name = "类别")]
//        [DataMember]
//        public int Type { set; get; }

//        /// <summary>
//        /// 大图
//        /// </summary>
//        [Display(Name = "大图")]
//        [DataMember]
//        public string PictureL { set; get; }

//        /// <summary>
//        /// 中图
//        /// </summary>
//        [Display(Name = "中图")]
//        [DataMember]
//        public string PictureM { set; get; }

//        /// <summary>
//        /// 小图
//        /// </summary>
//        [Display(Name = "小图")]
//        [DataMember]
//        public string PictureS { set; get; }

//        /// <summary>
//        /// 描述
//        /// </summary>
//        [Display(Name = "描述")]
//        [DataMember]
//        public string Content { set; get; }

//        #endregion Model

//        /// <summary>
//        /// 根据Reader生成Wash_ProductPictureDC对象
//        /// </summary>
//        /// <param name="reader">数据集</param>
//        public static Wash_ProductPictureDC GetEntity(IDataReader reader)
//        {
//            Wash_ProductPictureDC entity = null;

//            entity = new Wash_ProductPictureDC();

//            entity.AutoGetEntity(reader);

//            return entity;
//        }

//    }
//}
