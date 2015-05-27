using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
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
        /// 下单检查产品
        /// </summary>
        /// <param name="iOrderID">订单ID,空为0</param>
        /// <param name="iProductList">产品列表(请定义 Type = 1 否则不处理)</param>
        /// <param name="iSalesPrice">是否使用销售价</param>
        /// <param name="RealProductAmount">产品总金额</param>
        /// <param name="iMuser"></param>
        /// <returns></returns>
        private ReturnEntity<bool> Order_Create_Check_Product(
            int iOrderID
            , IList<Order_ProductDC> iProductList
            , bool iSalesPrice
            , out decimal RealProductAmount
            , int iMuser)
        {
            RealProductAmount = 0;

            if (iProductList == null || iProductList.Count == 0)
            {
                return new ReturnEntity<bool>(-1, "产品列表错误");
            }

            foreach (var item in iProductList)
            {
                if (item.Obj_Status == 6)
                {
                    //需被删除的产品,不处理
                    continue;
                }
                if (item.Type == 0)
                {
                    //人工定义产品,不处理
                    RealProductAmount += item.Price;
                    continue;
                }

                var product = orderDAL.Wash_Product_SELECT_Entity(item.ProductID);
                if (product == null)
                {
                    return new ReturnEntity<bool>(-1, "产品不存在 " + item.ProductID);
                }
                if (product.CommitStatus != 1)
                {
                    return new ReturnEntity<bool>(-1, "产品未上线或已下线");
                }
                if (product.Type != 1)
                {
                    return new ReturnEntity<bool>(-1, "产品非普通产品");
                }

                item.OID = iOrderID;
                item.Name = product.Name;
                //干洗产品
                item.Type = 1;
                item.Price = iSalesPrice ? product.SalesPrice : product.MarketPrice;

                RealProductAmount += item.Price;

                item.Obj_Status = 1;
                item.Obj_Cuser = iMuser;
                item.Obj_Muser = iMuser;
            }
            return new ReturnEntity<bool>(true);
        }

        /// <summary>
        /// 下单检查优惠券
        /// </summary>
        /// <param name="iUserID">用户ID</param>
        /// <param name="iUserCouponID">用户券ID</param>
        /// <param name="iCheckCoupon">检查有效期等</param>
        /// <param name="iProductList">产品列表</param>
        /// <param name="iExpressFee">快递费</param>
        /// <param name="iFaceValue">优惠券面值</param>
        /// <param name="iExpressFeeFree">快递减免</param>
        /// <returns></returns>
        private ReturnEntity<bool> Order_Create_Check_Coupon(
            Guid iUserID
            , int iUserCouponID
            , bool iCheckCoupon
            , IList<Order_ProductDC> iProductList
            , decimal iExpressFee
            , out decimal iFaceValue
            , out bool iExpressFeeFree
            )
        {
            iFaceValue = 0;
            iExpressFeeFree = false;

            var userCoupon = orderDAL.User_Coupon_SELECT_Entity(iUserCouponID);
            if (userCoupon == null)
            {
                Log_Info("优惠券不存在" + iUserID + "_" + iUserCouponID);
                return new ReturnEntity<bool>(-1, "优惠券不存在");
            }
            if (iCheckCoupon)
            {
                if (userCoupon.UseBeginDate >= DateTime.Now)
                {
                    Log_Info("优惠券未开始" + iUserID + "_" + iUserCouponID);
                    return new ReturnEntity<bool>(-1, "优惠券未开始");
                }
                if (userCoupon.UseEndDate <= DateTime.Now)
                {
                    Log_Info("优惠券已过期" + iUserID + "_" + iUserCouponID);
                    return new ReturnEntity<bool>(-1, "优惠券已过期");
                }
            }

            //18元券 只抵6元和9元
            if (userCoupon.CouponID == 2)
            {
                var Money_99_Sum = iProductList.Where(p => p.Price == 9.9m).Sum(p => p.Price);
                if (Money_99_Sum > 19.8m)
                {
                    //抵19.8元
                    iFaceValue = 19.8m;
                    iExpressFeeFree = true;
                }
                else if (Money_99_Sum > 0)
                {
                    //抵不到18元
                    iFaceValue = Money_99_Sum;
                    iExpressFeeFree = true;
                }
                else
                {
                    iFaceValue = 0;
                }
            }
            //抵用6元或9元产品
            else if (userCoupon.CouponID == 4 || userCoupon.CouponID == 5)
            {
                if (iProductList.Count(p => p.Price == 9.9m) > 0)
                {
                    //抵9.9元
                    iFaceValue = 9.9m;
                    iExpressFeeFree = true;
                }
                else
                {
                    iFaceValue = 0;
                }
            }
            //若海抵用券 订单金额全免
            else if (userCoupon.CouponID == 6)
            {
                iFaceValue = iProductList.Sum(p => p.Price);
                iExpressFeeFree = true;
            }
            //其他券,无面值,面值设置为0
            else if (userCoupon.FaceValue <= 0)
            {
                iFaceValue = 0;
            }
            //普通抵用券
            else
            {
                var realProductAmount = iProductList.Sum(p => p.Price);
                //面值大于订单金额
                if (userCoupon.FaceValue > (realProductAmount + iExpressFee))
                {
                    //面值设置为订单金额
                    iFaceValue = (realProductAmount + iExpressFee);
                }
                else
                {
                    //面值等于优惠券面值
                    iFaceValue = userCoupon.FaceValue;
                }
            }
            return new ReturnEntity<bool>(true);
        }
    }
}
