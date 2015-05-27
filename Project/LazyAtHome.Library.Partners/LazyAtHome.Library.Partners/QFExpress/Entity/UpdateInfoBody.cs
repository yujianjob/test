using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.QFExpress.Entity
{
    /// <summary>
    /// 回调
    /// </summary>
    public class UpdateInfoBody : EntityBase
    {
        #region

        /// <summary>
        /// 订单号
        /// </summary>
        public string txLogisticID { get; set; }

        /// <summary>
        /// 运单号
        /// </summary>
        public string mailNo { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public InfoType infoType { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public InfoContent infoContent { get; set; }

        /// <summary>
        /// 通知内容文字
        /// </summary>
        public string infoContentStr { get; set; }

        /// <summary>
        /// 备注，当infoType为STATUS时的失败原因；UNACCEPT（不接单）和NOT_SEND (揽收不成功)有规范的失败原因
        /// </summary>
        public string remark { get; set; }

        public enum InfoType
        {
            /// <summary>
            /// 物流状态
            /// </summary>
            STATUS,
            /// <summary>
            /// 物流公司链接
            /// </summary>
            LOGISTICS_LINK,
            /// <summary>
            /// 物流公司工作单号
            /// </summary>
            LOGISTICS_PROVIDER_ID,
        }

        public enum InfoContent
        {
            /// <summary>
            /// 接单
            /// </summary>
            ACCEPT,
            /// <summary>
            /// 不接单
            /// </summary>
            UNACCEPT,
            /// <summary>
            /// 揽收成功
            /// </summary>
            GOT,
            /// <summary>
            /// 揽收不成功
            /// </summary>
            NOT_SEND,
            /// <summary>
            /// 送达成功
            /// </summary>
            SIGNED,
            /// <summary>
            /// 送达失败
            /// </summary>
            FAILED,
        }

        #endregion

        public UpdateInfoBody(string iData)
        {
            //解析
            if (string.IsNullOrWhiteSpace(iData))
            {
                this.ErrCode = -1;
                this.ErrMessage = "接收数据为空";
                return;
            }
            var pms = iData.Split('&');
            if (pms.Length != 2)
            {
                this.ErrCode = -1;
                this.ErrMessage = "参数错误";
                return;
            }

            var tmp = pms[0].Split('=');
            if (tmp.Length != 2)
            {
                this.ErrCode = -1;
                this.ErrMessage = "参数错误";
                return;
            }
            var bizData = tmp[1];
            bizData = System.Web.HttpUtility.UrlDecode(bizData);

            tmp = pms[1].Split('=');
            if (tmp.Length != 2)
            {
                this.ErrCode = -1;
                this.ErrMessage = "参数错误";
                return;
            }
            var digest = tmp[1];

            //解密
            var tmp_digest = bizData + logisticProviderID;
            tmp_digest = Convert.ToBase64String(Common.ToHexByte(LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(tmp_digest)));
            tmp_digest = tmp_digest.Replace('+', '@');
            tmp_digest = System.Web.HttpUtility.UrlEncode(tmp_digest);

            if (digest != tmp_digest)
            {
                this.ErrCode = -1;
                this.ErrMessage = "参数错误";
            }

            //xml文件传入base
            this._xmlString = bizData;
        }

        public override bool AnalyzeBody()
        {
            if (this.ErrCode != 0)
            {
                return false;
            }

            //订单号 
            var node = XmlDoc.SelectSingleNode("/UpdateInfo/txLogisticID");
            if (node != null)
            {
                this.txLogisticID = node.InnerText;
            }
            else
            {
                this.ErrMessage = "物流平台的物流号为空";
                return false;
            }

            //运单号
            node = XmlDoc.SelectSingleNode("/UpdateInfo/mailNo");
            if (node != null)
            {
                this.mailNo = node.InnerText;
            }

            //通知类型
            node = XmlDoc.SelectSingleNode("/UpdateInfo/infoType");
            if (node != null)
            {
                var tmpEnu = InfoType.STATUS;
                if (Enum.TryParse<InfoType>(node.InnerText, out tmpEnu))
                {
                    this.infoType = tmpEnu;
                }
                else
                {
                    this.ErrCode = -1;
                    this.ErrMessage = "无法识别的通知类型";
                    return false;
                }
            }
            else
            {
                this.ErrCode = -1;
                this.ErrMessage = "通知类型为空";
                return false;
            }

            //通知内容
            node = XmlDoc.SelectSingleNode("/UpdateInfo/infoContent");
            if (node != null)
            {
                this.infoContentStr = node.InnerText;

                if (this.infoType == InfoType.STATUS)
                {
                    var tmpEnu = InfoContent.ACCEPT;
                    if (Enum.TryParse<InfoContent>(node.InnerText, out tmpEnu))
                    {
                        this.infoContent = tmpEnu;
                    }
                    else
                    {
                        this.ErrCode = -1;
                        this.ErrMessage = "无法识别的通知内容";
                        return false;
                    }
                }
            }
            else
            {
                this.ErrCode = -1;
                this.ErrMessage = "通知类型为空";
                return false;
            }

            //备注
            node = XmlDoc.SelectSingleNode("/UpdateInfo/remark");
            if (node != null)
            {
                this.remark = node.InnerText;
            }

            return true;
        }

        public string GetResponse(string iOrderNumber, bool iSuccess, string iErrorMessage)
        {
            ResponseBody res = new ResponseBody(iOrderNumber, iSuccess, iErrorMessage);

            return res.Serializer();
        }

    }
}
