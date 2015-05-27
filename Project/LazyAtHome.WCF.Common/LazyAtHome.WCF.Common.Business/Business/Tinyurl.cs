using LazyAtHome.Core.Infrastructure.WCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.Business.Business
{
    public partial class Base
    {
        //private static DateTime dt = DateTime.Now;

        //private static Random rd = new Random(dt.GetHashCode());

        public ReturnEntity<string> Tinyurl_CreateNew(string iUrl)
        {
            return new ReturnEntity<string>(tinyurlDAL.Tinyurl_CreateNew(iUrl));
        }

        public ReturnEntity<string> Tinyurl_Get(string iCode)
        {
            return new ReturnEntity<string>(tinyurlDAL.Tinyurl_Get(iCode));
        }

        private static string Convert10to62(long value)
        {
            if (value == 0)
                return "0";
            var rtn = string.Empty;
            while (value != 0)
            {
                var mo = value % 62;
                if (mo >= 0 && mo <= 9)
                {
                    rtn = (char)(mo + 48) + rtn;
                }
                else if (mo >= 10 && mo <= 35)
                {
                    rtn = (char)(mo + 55) + rtn;
                }
                else if (mo >= 36 && mo <= 62)
                {
                    rtn = (char)(mo + 61) + rtn;
                }
                value = value / 62;
            }

            return rtn;
        }
    }
}
