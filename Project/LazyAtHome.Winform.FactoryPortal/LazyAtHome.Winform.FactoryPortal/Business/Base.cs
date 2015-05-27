using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LazyAtHome.Winform.FactoryPortal.PRService;
using LazyAtHome.Winform.FactoryPortal.Common;

namespace LazyAtHome.Winform.FactoryPortal.Business
{
    public class Base
    {
        //public string LastError { get; set; }

        private string strLastError;
        public string LastError
        {
            get { return strLastError; }
            set 
            {             
                strLastError = value;

                Common.WriteLog.tradeLog(ConstConfig.WRONG_LOG_PATH, strLastError);
            }
        }

        
    }
}
