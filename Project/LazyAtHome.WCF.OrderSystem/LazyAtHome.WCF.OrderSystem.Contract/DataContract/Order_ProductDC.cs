using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using LazyAtHome.Core.Infrastructure.WCF;

namespace LazyAtHome.WCF.OrderSystem.Contract.DataContract
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    [Serializable]
    public class Order_ProductDC : EntityBase
    {
        #region Model
        /// <summary>
        /// 主键
        /// </summary>
        [Display(Name = "主键")]
        [DataMember]
        public int ID { set; get; }

        /// <summary>
        /// 订单表ID
        /// </summary>
        [Display(Name = "订单表ID")]
        [DataMember]
        public int OID { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        [DataMember]
        public int ProductID { set; get; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        [DataMember]
        public string Name { set; get; }

        /// <summary>
        /// 产品类型
        /// </summary>
        [Display(Name = "产品类型")]
        [DataMember]
        public int Type { set; get; }

        /// <summary>
        /// 单价
        /// </summary>
        [Display(Name = "单价")]
        [DataMember]
        public decimal Price { set; get; }

        /// <summary>
        /// 属性
        /// </summary>
        [Display(Name = "属性")]
        [DataMember]
        public string Attribute { set; get; }

        /// <summary>
        /// 订单产品识别编号
        /// </summary>
        [Display(Name = "订单产品识别编号")]
        [DataMember]
        public string Code { set; get; }

        /// <summary>
        /// 订单产品识别编号
        /// </summary>
        [Display(Name = "订单产品识别编号")]
        [DataMember]
        public string OtherCode { set; get; }

        /// <summary>
        /// 返洗状态
        /// </summary>
        [Display(Name = "返洗状态")]
        [DataMember]
        public int ReturnStatus { set; get; }

        /// <summary>
        /// 工厂洗涤状态(1:可以洗,2:无法洗)
        /// </summary>
        [Display(Name = "工厂洗涤状态")]
        [DataMember]
        public int FactoryWash { set; get; }

        /// <summary>
        /// 用户手机
        /// </summary>
        [Display(Name = "用户手机")]
        [DataMember]
        public string UserMPNo { set; get; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [Display(Name = "用户姓名")]
        [DataMember]
        public string UserName { set; get; }

        #endregion Model

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        [DataMember]
        public string WebName { set; get; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [Display(Name = "产品ID")]
        [DataMember]
        public int CategoryID { set; get; }

        /// <summary>
        /// 产品名称
        /// </summary>
        [Display(Name = "产品名称")]
        [DataMember]
        public string CategoryName { set; get; }

        /// <summary>
        /// 产品图片
        /// </summary>
        [Display(Name = "产品图片")]
        [DataMember]
        public string PictureM { set; get; }

        /// <summary>
        /// 产品图片
        /// </summary>
        [Display(Name = "产品图片")]
        [DataMember]
        public string PictureS { set; get; }

        /// <summary>
        /// 用户签收状态
        /// </summary>
        [Display(Name = "用户签收状态")]
        [DataMember]
        public int UserSignType { set; get; }

        /// <summary>
        /// 用户签收时间
        /// </summary>
        [Display(Name = "用户签收时间")]
        [DataMember]
        public DateTime? UserSignTime { set; get; }

        /// <summary>
        /// 进度
        /// </summary>
        [Display(Name = "进度")]
        [DataMember]
        public int Step { set; get; }

        /// <summary>
        /// 出库时间
        /// </summary>
        [Display(Name = "出库时间")]
        [DataMember]
        public DateTime? Step5Time { set; get; }

        /// <summary>
        /// 排序索引
        /// </summary>
        [Display(Name = "排序索引")]
        [DataMember]
        public int Index { set; get; }

        /// <summary>
        /// 根据Reader生成Order_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_ProductDC GetEntity(IDataReader reader)
        {
            Order_ProductDC entity = null;

            entity = new Order_ProductDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.PictureS))
            {
                entity.PictureS = System.Configuration.ConfigurationManager.AppSettings["Category_Picture"] + entity.PictureS;
            }
            if (!string.IsNullOrWhiteSpace(entity.PictureM))
            {
                entity.PictureM = System.Configuration.ConfigurationManager.AppSettings["Category_Picture"] + entity.PictureM;
            }


            return entity;
        }

        /// <summary>
        /// 根据Reader生成Order_ProductDC对象
        /// </summary>
        /// <param name="reader">数据集</param>
        public static Order_ProductDC GetEntity_Mall(IDataReader reader)
        {
            Order_ProductDC entity = null;

            entity = new Order_ProductDC();

            entity.AutoGetEntity(reader);

            if (!string.IsNullOrWhiteSpace(entity.PictureS))
            {
                entity.PictureS = System.Configuration.ConfigurationManager.AppSettings["MallProduct_Picture"] + entity.PictureS;
            }
            if (!string.IsNullOrWhiteSpace(entity.PictureM))
            {
                entity.PictureM = System.Configuration.ConfigurationManager.AppSettings["MallProduct_Picture"] + entity.PictureM;
            }


            return entity;
        }
    }
}
