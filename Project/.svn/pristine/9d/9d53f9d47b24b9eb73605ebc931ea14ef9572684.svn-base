using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.API.Models.ExpressMobileResultModels
{
    public class OrderModel
    {
        public int id;

        private string _name;

        /// <summary>
        /// 收件人
        /// </summary>
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if (_name != null)
                {
                    _name = _name.Trim('\n', '\r');
                }
            }
        }

        /// <summary>
        /// 电话
        /// </summary>
        public string mpno;

        private string _address;

        /// <summary>
        /// 详细地址
        /// </summary>
        public string address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                if (_address != null)
                {
                    _address = _address.Trim('\n', '\r');
                }
            }
        }

        /// <summary>
        /// 产品总数
        /// </summary>
        public int count;

        /// <summary>
        /// 此次送件数
        /// </summary>
        public int sendcount;

        /// <summary>
        /// 物流单号
        /// </summary>
        public string expnumber;

        /// <summary>
        /// 联系状态
        /// </summary>
        public int linkstatus;

        /// <summary>
        /// 应收金额
        /// </summary>
        public string money;

        /// <summary>
        /// 备注
        /// </summary>
        public string remark;

        /// <summary>
        /// 挂起状态
        /// </summary>
        public int wait;

        /// <summary>
        /// 状态
        /// </summary>
        public string status;

        /// <summary>
        /// 产品列表
        /// </summary>
        public IList<ProductModel> itemlist;

    }

    public class ProductModel
    {
        public int id { get; set; }
        /// <summary>
        /// 产品名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int count;
    }
}