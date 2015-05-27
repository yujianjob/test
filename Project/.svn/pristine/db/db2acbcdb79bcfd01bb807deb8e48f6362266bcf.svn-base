using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.WebManage.Controllers
{
    public class OperatorController : Controller
    {
        public ActionResult Test()
        {
            return View("Test1");

            //return View("Stat");
        }


        public ActionResult Stat()
        {
            LazyAtHome.Web.WebManage.Models.Stat.StatData entity = new Models.Stat.StatData();

            int count = 12;
            entity.Label = new string[count];
            for (int i = 0; i < count; i++)
            {
                entity.Label[i] = "B" + (i+1);
            }

            entity.Data = new int[count];
            for (int i = 0; i < count; i++)
            {
                entity.Data[i] = (i + 1);
            }
            entity.Data[count - 1] = 20;
            return View("Stat", entity);
        }


        //
        // GET: /Operator/
        public ActionResult Index(FormCollection list)
        {
            var dataList = CreateData();
            return View(dataList);
        }

        public ActionResult GetInfo(UserInfo user, FormCollection list)
        {
            var dataList = CreateData(false);
            return View("Index", dataList);
        }


        public IList<OBJ> CreateData(bool first = true)
        {
            IList<OBJ> xList = new List<OBJ>();

            OBJ myObj = new OBJ();
            myObj.P1 = 1;
            myObj.P2 = first ? "天津农信社" : "检索后的数据字段";
            myObj.P3 = "A120113196309052434";
            myObj.P4 = "天津市华建装饰工程有限公司";
            myObj.P5 = "联社营业部";
            myObj.P6 = "29385739203816293";
            myObj.P7 = "5级";
            myObj.P8 = "政府机构";
            myObj.P9 = "2009-05-21";
            xList.Add(myObj);

            OBJ myObj1 = new OBJ();
            myObj1.P1 = 2;
            myObj1.P2 = first ? "天津农信社" : "检索后的数据字段";
            myObj1.P3 = "A120113196309052434";
            myObj1.P4 = "天津市华建装饰工程有限公司";
            myObj1.P5 = "联社营业部";
            myObj1.P6 = "29385739203816293";
            myObj1.P7 = "5级";
            myObj1.P8 = "政府机构";
            myObj1.P9 = "2009-05-21";
            xList.Add(myObj1);

            OBJ myObj2 = new OBJ();
            myObj2.P1 = 3;
            myObj2.P2 = first ? "天津农信社" : "检索后的数据字段";
            myObj2.P3 = "A120113196309052434";
            myObj2.P4 = "天津市华建装饰工程有限公司";
            myObj2.P5 = "联社营业部";
            myObj2.P6 = "29385739203816293";
            myObj2.P7 = "5级";
            myObj2.P8 = "政府机构";
            myObj2.P9 = "2009-05-21";
            xList.Add(myObj2);

            return xList;
        }
        
    }

    public class UserInfo
    {
        public string keyword { get; set; }
        public string province { get; set; }
        //public string testkey { get; set; }

        public string yyf { get; set; }

    }

    public class OBJ
    {
        public int P1;
        public string P2;
        public string P3;
        public string P4;
        public string P5;
        public string P6;
        public string P7;
        public string P8;
        public string P9;
    }
}