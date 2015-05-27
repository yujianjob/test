using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Business.Business
{
    public partial class Order
    {

        /// <summary>
        /// 完成商城订单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iProductList"></param>
        /// <returns></returns>
        private ReturnEntity<bool> Order_Order_FinishMallProduct(string iOrderNumber, IList<Order_ProductDC> iProductList)
        {
            foreach (var item in iProductList)
            {
                if (item == null)
                {
                    return new ReturnEntity<bool>(-1, "商城产品为空");
                }
                //懒人卡
                switch (item.Type)
                {
                    //懒人卡
                    case 1:
                        var rtn = Order_Order_FinishMallProduct_Card(iOrderNumber, item);
                        if (rtn == null || !rtn.Success || !rtn.OBJ)
                        {
                            return rtn;
                        }
                        break;
                    default:
                        return new ReturnEntity<bool>(-1, "不支持的商城产品");
                }



            }
            return new ReturnEntity<bool>(true);
        }

        Random rd = new Random(new object().GetHashCode());

        /// <summary>
        /// 完成商城订单(懒人卡)
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iProduct"></param>
        /// <returns></returns>
        private ReturnEntity<bool> Order_Order_FinishMallProduct_Card(string iOrderNumber, Order_ProductDC iProduct)
        {
            if (string.IsNullOrWhiteSpace(iProduct.Attribute))
            {
                return new ReturnEntity<bool>(-1, "不支持的商城产品Attribute空");
            }
            var tmp = iProduct.Attribute.Split('_');
            if (tmp.Length != 2)
            {
                return new ReturnEntity<bool>(-1, "不支持的商城产品 " + iProduct.Attribute);
            }
            switch (tmp[0])
            {
                //实物属性
                case "1":
                    return new ReturnEntity<bool>(-1, "现不支持的实物懒人卡");
                    break;
                //电子属性
                case "2":

                    //面值
                    var faceValue = tmp[1];

                    //卡号前缀
                    var cardno = (int.Parse(faceValue) / 100).ToString().PadLeft(2, '0') + "E";

                    //卡号序列号
                    cardno += (orderDAL.Base_Card_GetLastID_E(cardno) + 1).ToString().PadLeft(10, '0');

                    //计算密码
                    var psw = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(
                                IntToHex(rd.Next(0, 65535), 4)
                                + IntToHex(rd.Next(0, 65535), 4)
                                + IntToHex(rd.Next(0, 65535), 4)
                                + IntToHex(rd.Next(0, 65535), 4)
                             );
                    //密码防重
                    while (orderDAL.Base_Card_Password_Exists(psw))
                    {
                        psw = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(
                                IntToHex(rd.Next(0, 65535), 4)
                                 + IntToHex(rd.Next(0, 65535), 4)
                                 + IntToHex(rd.Next(0, 65535), 4)
                                 + IntToHex(rd.Next(0, 65535), 4)
                             );
                    }

                    //创建卡号
                    var id = orderDAL.Base_Card_Create(1, cardno, psw, decimal.Parse(faceValue), iOrderNumber);
                    if (id <= 0)
                    {
                        return new ReturnEntity<bool>(-1, "创建电子懒人卡失败");
                    }

                    //更新订单信息
                    orderDAL.Order_Product_UPDATE_Code(iProduct.ID, psw, null);

                    return new ReturnEntity<bool>(true);

                default:
                    return new ReturnEntity<bool>(-1, "不支持的懒人卡类型");
            }

        }

        /// <summary>
        /// 十进制转十六进制
        /// </summary>
        /// <param name="i"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        static string IntToHex(int i, int width)
        {
            return i.ToString("X").PadLeft(width, '0');
        }
    }
}
