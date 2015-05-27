using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;
using System.Reflection;

namespace LazyAtHome.Web.StoreManage.Models.ProductCategory
{
    /// <summary>
    /// 运价信息
    /// 用于页面上显示的
    /// </summary>
    [Serializable]
    public class Product : Wash_ProductDC
    {
        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public Product()
        { }

                /// <summary>
        /// 根据服务端数据对象.
        /// 构造一个客户端数据对象
        /// </summary>
        /// <param name="baseData"></param>
        public Product(Wash_ProductDC baseData)
        {
            Type myType = baseData.GetType();

            // 取得对象的所有属性.
            PropertyInfo[] propArray = myType.GetProperties();

            foreach (PropertyInfo prop in propArray)
            {
                if (prop.CanRead && prop.CanWrite)
                {
                    Object propVal = prop.GetValue(baseData, null);
                    prop.SetValue(this, propVal, null);
                }
            }
        }


        #region 自定义属性 用于页面显示

        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsSelect { set; get; }

        /// <summary>
        /// 选择数量
        /// </summary>
        public int SelectCount { set; get; }

        #endregion
    }
}