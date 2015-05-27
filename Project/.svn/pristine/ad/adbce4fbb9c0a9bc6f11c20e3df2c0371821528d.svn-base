using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.InternalExpress.Business
{
    public class OrderService : BusinessBase
    {
        /// <summary>
        /// 生成物流订单
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iRemark"></param>
        /// <returns></returns>
        public ReturnEntity<bool> CreateExpressOrder(string iOutNumber,
            int iGetDistrictID, string iGetAddress, string iGetContacts, string iGetMpno, DateTime iGetExpTime,
            int iSendDistrictID, string iSendAddress, string iSendContacts, string iSendMpno, DateTime iSendExpTime,
            string iPackageInfo, int iPackageCount, decimal iChargeFee, string iUserRemark)
        {
            var response = this.Proxy.CreateExpressOrder(
                    iOutNumber,
                    iGetDistrictID, iGetAddress, iGetContacts, iGetMpno, iGetExpTime,
                    iSendDistrictID, iSendAddress, iSendContacts, iSendMpno, iSendExpTime,
                    iPackageInfo, iPackageCount, iChargeFee, iUserRemark);

            return response;
        }

        /// <summary>
        /// 更新代收货款
        /// </summary>
        /// <param name="iOutNumber"></param>
        /// <param name="iChargeFee"></param>
        /// <param name="iPackageInfo"></param>
        /// <param name="iPackageCount"></param>
        /// <returns></returns>
        public ReturnEntity<bool> Exp_Order_UPDATE_Send_ChargeFee(string iOutNumber, decimal iChargeFee, string iPackageInfo, int iPackageCount)
        {
            var response = this.Proxy.Exp_Order_UPDATE_Send_ChargeFee(iOutNumber, iChargeFee, iPackageInfo, iPackageCount);

            return response;
        }
    }
}
