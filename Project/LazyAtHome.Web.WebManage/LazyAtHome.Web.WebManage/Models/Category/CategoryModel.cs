using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LazyAtHome.WCF.Wash.Contract.DataContract;

namespace LazyAtHome.Web.WebManage.Models.Category
{
    /// <summary>
    /// 产品页信息
    /// </summary>
    [Serializable]
    public class CategoryModel
    {
        /// <summary>
        /// 产品基础信息
        /// </summary>
        public Wash_CategoryDC CategoryInfo { get; set; }

        //移除属性备选列表
        //private string _strAttributeIDSelected;
        ///// <summary>
        ///// 选中属性ID字符串，用于页面存放
        ///// </summary>
        //public string strAttributeIDSelected
        //{
        //    get
        //    {
        //        return _strAttributeIDSelected; 
        //    }
        //  set
        //  {
        //      this._strAttributeIDSelected = value;
        //      if (!string.IsNullOrEmpty(_strAttributeIDSelected))
        //      {
        //          string[] arrTemp = _strAttributeIDSelected.Split(',');
        //          if (CategoryInfo != null)
        //          {
        //              CategoryInfo.AttributeList = new List<Wash_AttributeDC>();
        //              foreach (string item in arrTemp)
        //              {
        //                  Wash_AttributeDC Attribute = new Wash_AttributeDC();
        //                  Attribute.ID = Convert.ToInt32(item);
        //                  CategoryInfo.AttributeList.Add(Attribute);
        //              }


        //          }
        //      }
        //  }
        //}

        ///// <summary>
        ///// 备选属性
        ///// </summary>
        //public IList<CategroyAttribute> AttributeList { get; set; }
    }
}