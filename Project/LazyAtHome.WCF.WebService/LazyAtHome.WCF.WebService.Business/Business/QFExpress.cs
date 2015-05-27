using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Business.Business
{
    public class QFExpress
    {

        public QFExpress()
        {

        }

        static QFExpress _QFExpress;
        public static QFExpress Instance
        {
            get
            {
                if (_QFExpress == null)
                {
                    _QFExpress = new QFExpress();
                }
                return _QFExpress;
            }
        }

        private LazyAtHome.Library.Partners.QFExpress.Business.OrderService _OrderService = new Library.Partners.QFExpress.Business.OrderService();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iExpressNumber"></param>
        /// <param name="j_Name"></param>
        /// <param name="j_PostCode"></param>
        /// <param name="j_Phone"></param>
        /// <param name="j_Mobile"></param>
        /// <param name="j_Province"></param>
        /// <param name="j_City"></param>
        /// <param name="j_Address"></param>
        /// <param name="d_Name"></param>
        /// <param name="d_PostCode"></param>
        /// <param name="d_Phone"></param>
        /// <param name="d_Mobile"></param>
        /// <param name="d_Province"></param>
        /// <param name="d_City"></param>
        /// <param name="d_Address"></param>
        /// <param name="iItemList"></param>
        /// <param name="iExpectStartTime"></param>
        /// <param name="iExpectEndTime"></param>
        /// <returns></returns>
        public ReturnEntity<bool> CreateOrder(string iOrderNumber, string iExpressNumber,
          string j_Name, string j_PostCode, string j_Phone, string j_Mobile, string j_Province, string j_City, string j_Address,
          string d_Name, string d_PostCode, string d_Phone, string d_Mobile, string d_Province, string d_City, string d_Address,
          IDictionary<string, int> iItemList, DateTime? iExpectStartTime = null, DateTime? iExpectEndTime = null)
        {
            return _OrderService.CreateOrder(iOrderNumber, iExpressNumber,
                j_Name, j_PostCode, j_Phone, j_Mobile, j_Province, j_City, j_Address,
                d_Name, d_PostCode, d_Phone, d_Mobile, d_Province, d_City, d_Address,
                iItemList, iExpectStartTime, iExpectEndTime);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iOrderNumber"></param>
        /// <param name="iRemark"></param>
        /// <returns></returns>
        public ReturnEntity<bool> CancelOrder(string iOrderNumber, string iRemark)
        {
            return _OrderService.CancelOrder(iOrderNumber, iRemark);
        }


    }
}
