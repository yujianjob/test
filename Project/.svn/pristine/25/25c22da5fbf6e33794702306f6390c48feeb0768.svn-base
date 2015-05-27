using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LazyAtHome.Web.WebManage.Models.Stat
{
    public class StatData
    {
        public string[] Label { set; get; }
        public int[] Data { set; get; }



        //public string[] L_Value
        //{
        //    get 
        //    {
        //        return new string[] {"A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "A10", "A11", "A12"};
        //    }
        //}

        public string D_Value
        {
            get
            {
                string rtn = string.Empty;
                foreach (var item in Data)
                {
                    rtn += item + ",";
                }

                if (rtn.Length > 0)
                {
                    rtn = rtn.Substring(0, rtn.Length - 1);
                }

                return rtn;

            }
        }

        public string L_Value
        {
            get
            {
                string rtn = string.Empty;
                foreach (var item in Label)
                {
                    rtn += item + ",";
                }

                if (rtn.Length > 0)
                {
                    rtn = rtn.Substring(0, rtn.Length - 1);
                }

                return rtn;
                
            }
        }

    }
}