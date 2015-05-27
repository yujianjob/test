using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WeiXin4.Models
{
    public class EntityModel
    {
        public class resultEntity<T>
        {
            public bool succFlag;
            public string msg;
            public T data;
        }

        public class DataEntity
        {
            public int resultCode;
        }

        public class amountEntity
        {
            public string amount;
        }

        public class UserInfoEntity
        {
            public string mpNo;
            public string userId;
        }

        /// <summary>
        /// 地址信息
        /// </summary>
        public class AdressInfo
        {
            public int id;
            public string userId;
            public string consigneeName;
            public string consigneeMpNo;
            public string address;
            public bool defaultFlag;
        }

        /// <summary>
        /// 订单信息
        /// </summary>
        public class OrderInfo
        {
            public string orderNumber;
            public string userId;
            public string userName;
            public string mpNo;
            public string address;
            public string orderTime;
            public string orderStatus;
            public string orderStatusDesc;
            public string orderStep;
            public string orderStepDesc;
            public string payStatus;
            public string payStatusDesc;
        }

        public class OrderInfoList
        {
            public List<OrderInfo> dataList;
            public string pageCount;
            public string rowCount;
            public string pageNo;
            public string pageSize;
        }

        public class WashItem
        {
            public string washItemName;
            public string price;
            public string subtNum;
            public string subtPrice;
        }

        public class OrderDetailInfo
        {
            public OrderInfo orderSimpleVo;
            public List<WashItem> washItemSubtVoList;
            public string totalPrice;
        }

        public class UserModel2
        {
            public string MpNo;
            public string Openid;
            public string Money;
            public string UserId;
        }

        public class ProuductInfo
        {
            public string caId;
            public string caName;
            public string clId;
            public string clName;
            public string prId;
            public string prMarketPrice;
            public string prName;
            public string prSalesPrice;
            public string prType;
        }

        public class ProductItem
        {
            public string price;
            public List<ProuductInfo>  washProductPriceVOList;
        }

    }
}