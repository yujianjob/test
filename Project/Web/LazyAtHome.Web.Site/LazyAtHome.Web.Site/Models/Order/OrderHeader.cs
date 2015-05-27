using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LazyAtHome.Web.Site.Models.Order
{

    public class OrderHeader
    {
        public string Member { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        [Display(Name = "收件人")]
        [Required(ErrorMessage = "请输入收件人姓名")]
        [MaxLength(30, ErrorMessage = "收件人姓名长度不可超过30个字符")]
        public string ContactName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        [Display(Name = "联系电话")]
        [Required(ErrorMessage = "请输入您的联系电话")]
        [MaxLength(15, ErrorMessage = "联系电话的长度不能超过15位")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.PhoneNumber)]
        public string ContactPhoneNo { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        [Display(Name = "收货地址")]
        [Required(ErrorMessage = "请输入收货地址")]
        public string ContactAddress { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        [Display(Name = "总金额")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [Display(Name = "订单备注")]
        [DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        public string Memo { get; set; }

        /// <summary>
        /// 订购时间
        /// </summary>
        [Display(Name = "订购时间")]
        public DateTime BuyOn { get; set; }

    }
}