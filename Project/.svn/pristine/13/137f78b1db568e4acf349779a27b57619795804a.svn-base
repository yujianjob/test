using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.Web.WeiXin.Models.JsonResult
{
    public class WeixinUserInfoResult
    {
        [Display(Name = "是否订阅")]
        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，拉取不到其余信息
        /// </summary>
        public int subscribe { get; set; }

        [Display(Name = "用户标识")]
        /// <summary>
        /// 普通用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }

        [Display(Name = "昵称")]
        /// <summary>
        /// 普通用户的昵称
        /// </summary>
        public string nickname { get; set; }

        [Display(Name = "头像")]
        /// <summary>
        /// 普通用户的头像链接
        /// </summary>
        public string headimgurl { get; set; }

        [Display(Name = "语言")]
        /// <summary>
        /// 普通用户的语言，简体中文为zh_CN
        /// </summary>
        public string language { get; set; }

        [Display(Name="性别")]
        public int sex { get; set; }
        [Display(Name = "城市")]
        public string city { get; set; }
        [Display(Name = "省份")]
        public string province { get; set; }
        [Display(Name = "国家")]
        public string country { get; set; }
        [Display(Name = "关注时间")]
        public string subscribe_time { get; set; }
    }
}