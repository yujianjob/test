using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.Web.WeiXin.Models.ViewModel
{
    public class VerifyViewModel
    {
        [Required]
        public string MPNo { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "请输入6位验证码")]
        [MaxLength(6, ErrorMessage = "请输入6位验证码")]
        public string VerifyCode { get; set; }
    }
}