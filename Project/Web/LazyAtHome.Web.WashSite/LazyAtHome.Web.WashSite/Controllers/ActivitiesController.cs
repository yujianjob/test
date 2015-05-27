using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WashSite.Controllers
{
    public class ActivitiesController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [NoCacheFilter]
        public ActionResult Submit(int id = 1, string param = "")
        {
            var user = GetUserInfo();
            var Cart = user.ActiveCart = new Models.User.Cart();
            string msg = "";
            IList<string> idList = null;
            IList<int> xList = null;
            string[] arrParam = null;

            switch (id)
            {
                case 1:
                    Cart.Name = "床品四件套原价80元现价50元";

                    CartActiveAdd(74, 2);
                    CartActiveAdd(75, 1);
                    CartActiveAdd(76, 1);

                    Cart.PayPrice = 50;
                    Cart.SalePrice = Cart.TotalPrice - 50;
                    break;
                case 2:
                    Cart.Name = "冬装保养";

                    idList = new List<string>() { "5", "12", "16", "14" };

                    xList = new List<int>();
                    arrParam = param.Split(',');
                    foreach (var str in arrParam)
                    {
                        if (idList.Contains(str))
                            xList.Add(int.Parse(str));
                        else
                        {
                            msg = "您选择的产品中包含不参与活动的产品。";
                            break;
                        }
                    }
                    if (msg != "")
                        break;
                    if (xList.Count != 3)
                    {
                        msg = "您选择的产品数量错误。";
                        break;
                    }

                    foreach (var item in xList)
                    {
                        CartActiveAdd(item, 1);
                    }

                    Cart.PayPrice = 88;
                    Cart.SalePrice = Cart.TotalPrice - 88;
                    break;
                case 3:
                    idList = new List<string>() { "52", "53", "56", "42", "41", "39", "58", "72", "54" };

                    xList = new List<int>();
                    arrParam = param.Split(',');
                    foreach (var str in arrParam)
                    {
                        if (idList.Contains(str))
                            xList.Add(int.Parse(str));
                        else
                        {
                            msg = "您选择的产品中包含不参与活动的产品。";
                            break;
                        }
                    }
                    if (msg != "")
                        break;
                    if (xList.Count == 0)
                    {
                        msg = "您尚未选择任何产品。";
                        break;
                    }

                    if (xList.Count < 5)
                    {
                        foreach (var item in xList)
                        {
                            CartProductAdd(item, 1);
                        }
                        return RedirectToAction("Index", "Cart");
                    }
                    else
                    {
                        Cart.Name = "洗五免一";
                        foreach (var item in xList)
                        {
                            CartActiveAdd(item, 1);
                        }

                        decimal maxMoney = Cart.ProductList.Max(p => p.ProductInfo.MarketPrice);

                        //Cart.TotalCount = 4;
                        //Cart.TotalPrice = 132;
                        Cart.PayPrice = Cart.TotalPrice - maxMoney;
                        Cart.SalePrice = maxMoney;
                    }
                    break;
                default:
                    break;
            }

            if (msg != "")
                return View("Error", "", msg);

            var rtnAddress = App_Code.Proxy.UserProxycs.Select_UserAddress(user.User.ID);
            if (rtnAddress.Success)
            {
                ViewBag.AddressList = rtnAddress.OBJ;
            }

            var rtnCardList = App_Code.Proxy.UserProxycs.Select_UserCardList(user.User.ID);
            if (rtnCardList.Success)
                ViewBag.CardList = rtnCardList.OBJ;

            var rtnCouponList = App_Code.Proxy.UserProxycs.User_Coupon_SELECT_List(user.User.ID, null, null, WCF.UserSystem.Contract.Enumerate.CouponStatus.Normal, null, null, 1, 20);
            if (rtnCouponList.Success)
                ViewBag.CouponList = rtnCouponList.OBJ.ReturnList;

            return View(user);
        }

        public JsonResult SubmitProcess(int address, string remark)
        {
            var user = GetUserInfo();
            int code = 1;
            string rtnMsg = null;

            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC GetAddress = null;
            WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC SendAddress = null;

            var rtnAddress = App_Code.Proxy.UserProxycs.Select_UserAddressEntity(address);
            if (rtnAddress.Success)
            {
                GetAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
                GetAddress.Consignee = rtnAddress.OBJ.Consignee;
                GetAddress.Mpno = rtnAddress.OBJ.MPNo;
                GetAddress.Address = rtnAddress.OBJ.DistrictName + rtnAddress.OBJ.Address;
                GetAddress.CityName = rtnAddress.OBJ.CityName;
                GetAddress.ProvinceName = rtnAddress.OBJ.ProvinceName;
                GetAddress.DistrictID = rtnAddress.OBJ.DistrictID;

                SendAddress = new WCF.OrderSystem.Contract.DataContract.Order_ConsigneeAddressDC();
                SendAddress.Consignee = rtnAddress.OBJ.Consignee;
                SendAddress.Mpno = rtnAddress.OBJ.MPNo;
                SendAddress.Address = rtnAddress.OBJ.DistrictName + rtnAddress.OBJ.Address;
                SendAddress.CityName = rtnAddress.OBJ.CityName;
                SendAddress.ProvinceName = rtnAddress.OBJ.ProvinceName;
                SendAddress.DistrictID = rtnAddress.OBJ.DistrictID;
            }
            else
            {
                code = rtnAddress.Code;
                rtnMsg = rtnAddress.Message;
            }

            var cart = user.ActiveCart;

            List<int> productList = new List<int>();
            foreach (var p in cart.ProductList)
            {
                for (int i = 0; i < p.Count; i++)
                    productList.Add(p.ProductInfo.ID);
            }

            decimal realTotalPrice = cart.TotalPrice;

            //添加活动
            IDictionary<string, decimal> activeList = new Dictionary<string, decimal>();
            realTotalPrice -= cart.SalePrice;
            activeList.Add(cart.Name, cart.SalePrice);

            //计算优惠券
            IList<int> couponList = null;
            if (cart.UseCoupon.Count > 0)
            {
                couponList = new List<int>();
                foreach (KeyValuePair<int, decimal> coupon in cart.UseCoupon)
                {
                    couponList.Add(coupon.Key);
                    realTotalPrice -= coupon.Value;
                }
            }

            //创始会员8折优惠
            if (user.User.Level == (int)WCF.UserSystem.Contract.Enumerate.UserLevel.CharterMembers)
            {
                decimal charterSaleMoney = Math.Round(realTotalPrice * 0.2m, 1, MidpointRounding.AwayFromZero);
                activeList.Add("创始会员8折优惠", charterSaleMoney);
                realTotalPrice -= charterSaleMoney;
            }

            //懒人卡
            if (cart.UseCard != null)
            {
                foreach (KeyValuePair<int, decimal> item in cart.UseCard)
                {
                    realTotalPrice -= item.Value;
                }
            }

            //余额
            realTotalPrice -= cart.UseBalance;

            string url = "";
            //var rtn = App_Code.Proxy.OrderProxy.Order_Create_Activity(user.User.ID, AppConfig.SiteID, productList, 8m, 8m, cart.UseBalance, cart.UseCard, GetAddress, SendAddress, cart.TotalPrice - cart.SalePrice, cart.Name, cart.SalePrice);
            var rtn = App_Code.Proxy.OrderProxy.Order_Create_Common(user.User.ID, AppConfig.SiteID, productList, 8m, 8m, cart.UseBalance, cart.UseCard, GetAddress, SendAddress, realTotalPrice, activeList, couponList, false);
            if (rtn.Success)
            {
                if (user.ActiveCart.PayPrice > 0)
                    url = Url.Action("Payment", "Cart", new { OrderNum = rtn.OBJ.OrderNumber });
                else
                    url = Url.Action("PaymentOk", "Cart", new { OrderNum = rtn.OBJ.OrderNumber });
                Session["Order_" + rtn.OBJ.OrderNumber] = rtn.OBJ;
                user.ActiveCart.Clear();
                UpdateUserInfo();
            }
            else
            {
                code = rtn.Code;
                rtnMsg = rtn.Message;
            }

            return Json(new { Code = code, Msg = rtnMsg, TagUrl = url });
        }

        /// <summary>
        /// 床品四件套
        /// </summary>
        /// <returns></returns>
        public ActionResult FourPiece()
        {
            return View();
        }

        /// <summary>
        /// 冬装保养
        /// </summary>
        /// <returns></returns>
        public ActionResult WinterWear()
        {
            return View();
        }

        public ActionResult Core()
        {
            return View();
        }

        public ActionResult weixin()
        {
            return View();
        }

        /// <summary>
        /// 洗五免一 夏装洗涤
        /// </summary>
        /// <returns></returns>
        public ActionResult SummerWear()
        {
            return View();
        }

        public ActionResult RegGifts()
        {
            return View();
        }

        public ActionResult Bossmad()
        {
            var user = GetUserInfo();
            if (user.User != null)
                ViewBag.Code = user.User.RecommendedCode;
            return View();
        }

        public ActionResult Recruit()
        {
            return View();
        }

        [Authorize]
        /// <summary>
        /// 问卷调查
        /// </summary>
        /// <returns></returns>
        public ActionResult Survey()
        {
            var xList = App_Code.Proxy.CommonProxy.Survey_Answer_SELECT_Entity(1);
            return View(xList.OBJ);
        }

        public JsonResult SurveySubmit(int SurID, string[] AnswerList)
        {
            int code = 0;
            string msg = "";

            var user = GetUserInfo();
            WCF.Common.Contract.DataContract.Base.Survey_AnswerDC _Answer = new WCF.Common.Contract.DataContract.Base.Survey_AnswerDC();

            if (user.User != null)
            {
                _Answer.UserID = user.User.ID;
                if (!string.IsNullOrEmpty(user.User.MPNo))
                    _Answer.UserSource = user.User.MPNo;
                else if (!string.IsNullOrEmpty(user.User.Email))
                    _Answer.UserSource = user.User.Email;
                else
                    _Answer.UserSource = "";
            }
            _Answer.SurID = SurID;//问卷表ID
            _Answer.DetailList = new List<WCF.Common.Contract.DataContract.Base.Survey_AnswerDetailDC>();

            foreach (var strAnswer in AnswerList)
            {
                WCF.Common.Contract.DataContract.Base.Survey_AnswerDetailDC detail = new WCF.Common.Contract.DataContract.Base.Survey_AnswerDetailDC();
                string[] arrAnswer = strAnswer.Split('_');
                detail.QuID = Convert.ToInt32(arrAnswer[0]);
                detail.AnsValue = Convert.ToInt32(arrAnswer[1]);

                if (arrAnswer[2] == "1")
                    detail.AnsContent = arrAnswer[3];

                _Answer.DetailList.Add(detail);
            }

            var rtn = App_Code.Proxy.CommonProxy.Survey_Answer_ADD(_Answer);
            if (rtn.Success)
                code = 1;
            else
            {
                code = rtn.Code;
                msg = rtn.Message;
            }

            return Json(new { Code = code, Msg = msg });
        }
    }
}