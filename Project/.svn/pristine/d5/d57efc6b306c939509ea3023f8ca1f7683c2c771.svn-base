using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using LazyAtHome.WCF.Common.Contract.DataContract.Base;

namespace LazyAtHome.Web.WebManage.Models.Survey
{
    /// <summary>
    /// 问卷预览
    /// 客户端实体类
    /// </summary>
    public class SurveyViewModel : Survey_MainDC
    {
        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public SurveyViewModel()
        {
        }

        /// <summary>
        /// 根据服务端数据对象.
        /// 构造一个客户端数据对象
        /// </summary>
        /// <param name="baseData"></param>
        public SurveyViewModel(Survey_MainDC baseData)
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

        /// <summary>
        /// 问题集合
        /// </summary>
        public IList<QuestionViewModel> QuestionViewList { get; set; }


        /// <summary>
        /// 用户来源
        /// </summary>
        public string UserSource { get; set; }

    }

    public class QuestionViewModel : Survey_QuestionDC
    {
        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public QuestionViewModel()
        {
        }

        /// <summary>
        /// 根据服务端数据对象.
        /// 构造一个客户端数据对象
        /// </summary>
        /// <param name="baseData"></param>
        public QuestionViewModel(Survey_QuestionDC baseData)
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

        /// <summary>
        /// 选项集合
        /// </summary>
        public IList<OptionsViewModel> OptionsViewList { get; set; }
    }


    /// <summary>
    /// 问卷选项
    /// 客户端实体类
    /// </summary>
    public class OptionsViewModel : Survey_OptionsDC
    {
        /// <summary>
        /// 默认构造函数.
        /// </summary>
        public OptionsViewModel()
        {
        }

                /// <summary>
        /// 根据服务端数据对象.
        /// 构造一个客户端数据对象
        /// </summary>
        /// <param name="baseData"></param>
        public OptionsViewModel(Survey_OptionsDC baseData)
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

        public string strSelPct
        {
            get
            {
                return "  (" + SelPct + "人选择)";
            }
        }

        /// <summary>
        /// 用户选择的答案值
        /// </summary>
        public int AnsValue { set; get; }

        /// <summary>
        /// 用户选择的答案内容
        /// </summary>
        public string AnsContent { set; get; }


        /// <summary>
        /// 该选项是否被选中
        /// </summary>
        public bool isSelected
        { 
            //set;
            get 
            {
                if ((AnsValue | Seq) == AnsValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


    
    }
}