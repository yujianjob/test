using LazyAtHome.Library.Partners.QFExpress.Business;
using LazyAtHome.Library.Partners.QFExpress.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace LazyAtHome.Library.Partners.Console_Test
{
    public class Program
    {
        static void Main(string[] args)
        {
            LazyAtHome.Library.Partners.BaiduMap.Business b = new BaiduMap.Business();

            b.Test();
            var rtn = string.Empty;

            //DictionaryCreate();

            //StringBuilder sb = new StringBuilder();

            //sb.Append("<?xml version='1.0' encoding='UTF-8'?><Request service=\"RoutePushService\" lang=\"zh-CN\"><Body><WaybillRoute id=\"10049343560473\" mailno=\"388400612431\" orderid=\"1141487000000030000\" acceptTime=\"2014-06-03 19:16:26\" acceptAddress=\"深圳市\" remark=\"便利店交接\" opCode=\"130\"/><WaybillRoute id=\"10049343560471\" mailno=\"388400612431\" orderid=\"1141487000000030000\" acceptTime=\"2014-06-03 23:16:26\" acceptAddress=\"深圳市\" remark=\"派送成功\" opCode=\"80\"/><WaybillRoute id=\"10049343560472\" mailno=\"388400612431\" orderid=\"1141487000000030000\" acceptTime=\"2014-06-03 20:16:26\" acceptAddress=\"深圳市\" remark=\"便利店出仓\" opCode=\"123\"/><WaybillRoute id=\"10049343560474\" mailno=\"388400612431\" orderid=\"1141487000000030000\" acceptTime=\"2014-06-03 15:16:26\" acceptAddress=\"深圳市\" remark=\"已收件\" opCode=\"50\"/></Body></Request>");

            //sb.Append("<?xml version='1.0' encoding='UTF-8'?>");
            //sb.Append("<Request service=\"RoutePushService\" lang=\"zh-CN\">");
            //sb.Append("<Body>");
            //sb.Append("<WaybillRoute id=\"448055\" ");
            //sb.Append(" mailno=\"388400609564\"");
            //sb.Append(" orderid=\"114154040000007\" ");
            //sb.Append(" acceptTime=\"2014-06-04 11:45:36\"");
            //sb.Append(" acceptAddress=\"上海市\"");
            //sb.Append(" remark=\"派送成功\" opCode=\"80\"/>");
            //sb.Append("</Body>");
            //sb.Append("</Request>");

            ////LazyAtHome.Library.Partners.SFExpress.Entity.RoutePushServiceBody pushBody = new Library.Partners.SFExpress.Entity.RoutePushServiceBody(sb.ToString());
            ////if (!pushBody.Deserialize(false))
            ////{
            ////    Console.WriteLine("123");
            ////}

            //var rtn = qfexpressService("content=" + sb.ToString());

            //Test(1, 2, 10);

            //var str = "<order></order>123456";

            //var ordernm = "114107120000001";

            //LazyAtHome.Library.Partners.SFExpress.Business.OrderService order = new SFExpress.Business.OrderService();

            //LazyAtHome.Library.Partners.Common.WriteToFileEvent += WriteToFile;

            //var rtn = order.CreateReverseOrder("11415407100006",
            //    "成先生", "成先生", "13681979029", "上海市上海市宝山区美艾路177弄39号",
            //    "上海", "上海市",
            //    "懒到家", "懒到家", "4008-762-799", "上海市宝山区泰和路2038号D楼203室",
            //    "上海", "上海", DateTime.Now.AddHours(2));

            //var rtn = order.OrderSearch("1141635000000050000");

            //var rtn1 = order.CreateOrder("11415407100003",
            //    "成先生", "成先生", "13681979029", "上海市上海市宝山区美艾路177弄39号",
            //    "上海", "上海市",
            //    "懒到家", "懒到家", "4008-762-799", "上海市宝山区泰和路2038号D楼203室",
            //    "上海", "上海", 24.6m);

            //var rtn = order.CancelOrder("11415407100003");

            //var rtn = order.CreateReverseOrder("1141540700000500004", "金魁", "金魁", "13524622579", "上海市市辖区黄浦区蒙自路169号",
            //    "上海", "上海市",
            //    "洗衣工厂", "洗衣工厂", "13012345678", "上海市松江区新桥镇新庙三路1028号1幢", "上海", "上海", null);

            //var rtn = order.CreateReverseOrder("114154010000010", "金魁", "金魁", "13524622579", "上海市市辖区黄浦区和USB诶睡觉莪回家诶",
            //    "上海", "上海市",
            //    "洗衣工厂", "洗衣工厂", "13012345678", "上海市松江区新桥镇新庙三路1028号1幢", "上海", "上海", null);

            //var rtn = order.CreateReverseOrder("114107120000003", "上海懒到家", "联系人", "18616000000", "上海宝山", "上海", "上海",
            //    "上海懒到家", "jk", "13524622579", "上海宝山", "上海", "上海", null);

            //var rtn = order.CancelOrder("114107120000003");

            //var rtn1 = order.OrderSearch("114107120000003");
            //var rtn = order.CancelOrder("114107120000001");
            //var rtn = order.OrderSearch("XJFS1306170001");

            //var rtn = order.OrderCallBack("<?xml version='1.0' encoding='UTF-8'?><Request lang=\"zh-CN\" service=\"RoutePushService\"><Body><WaybillRoute id=\"396489\" mailno=\"960104008603\" orderid=\"WSD1333322170001\" acceptTime=\"2013-07-04 15:23:21\" acceptAddress=\"深圳市\" remark=\"揽件成功\" opCode=\"客户定义状态-01\"/><WaybillRoute id=\"396488\" mailno=\"960104008603\" orderid=\"WSD1333322170001\" acceptTime=\"2013-07-04 15:25:54\" acceptAddress=\"深圳市\" remark=\"快件已签收，签收人是：大熊\" opCode=\"客户定义状态-03\"/></Body></Request>");

            //var t = rtn.OBJ.GetResponse(new List<string>() { "123", "456" }, new List<string>() { "777", "888" });
            //Console.WriteLine(rtn.OBJ.mailno);

            Console.WriteLine(rtn);

            //var rtn = order.OrderCallBack("<Request service=\"RoutePushService\" lang=\"zh-CN\"> <Body> <WaybillRoute id=\"路由编号\" mailno=\"运单号\" orderid=\"订单号\" acceptTime=\"2012-01-01 15:00:00\" acceptAddress=\"路由发生地点\" remark=\"详细说明\" opCode=\"操作码\"/></Body> </Request> ");

            //str = rtn.OBJ.GetResponse(394, "错误");

            //str = rtn.OBJ.GetResponse(new List<string>() { { "1" }, { "2" } }, new List<string>() { { "9" }, { "8" } });

            //Console.WriteLine(str);

            //UpdateInfoBody b = new UpdateInfoBody("bizData=%3Corder%3E%3C%2Forder%3E&digest= LghTkEmsD2tbQ3fsIBRcBg%3D%3D");

            //order.CreateOrder("114107120000001", "114107120000001",
            //    "jk", "100000", null, "130000000", "上海", "上海", "金山区",
            //    "jk", "100000", null, "130000000", "上海", "上海", "金山区",
            //    new Dictionary<string, int>() { { "衣服", 1 } }, DateTime.Now, DateTime.Now.AddHours(2));

            //ro.CancelOrder("114107120000011", "人工取消");

            //var XmlDoc = new XmlDocument();

            //var order = XmlDoc.CreateElement("order");

            //XmlDoc.AppendChild(order);

            //Console.WriteLine(XmlDoc.InnerXml);

            //XmlDocument XmlDoc = new XmlDocument();


            //var bizData = "<order></order>";

            //var digest = "<order></order>" + "123456";

            //digest = Convert.ToBase64String(Common.ToHexByte(LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(digest)));

            //digest = digest.Replace('+', '@');

            //digest = System.Web.HttpUtility.UrlEncode(digest);

            //bizData = System.Web.HttpUtility.UrlEncode(bizData);


            //Console.WriteLine("bizData=" + bizData + "&digest=" + digest);

            //XmlDoc.LoadXml("<RequestOrder version=\"2.0\"></RequestOrder>");

            //XmlNode newElem = XmlDoc.CreateNode("element", "pages", "");

            //XmlDoc.DocumentElement.AppendChild(newElem);

            //Console.WriteLine(XmlDoc.InnerXml);

            //str = LazyAtHome.Core.Helper.CryptoHelper.MD5Encrypt(str);


            //str = Convert.ToBase64String(Encoding.Default.GetBytes(str));

            //str = Convert.ToBase64String();
            //var b = Encoding.Default.GetBytes(str);

            //var b = ToHexByte(str);

            //2e08539049ac0f6b5b4377ec20145c06
            //2e08539049ac0f6b5b4377ec20145c06

            //b = GetHash(b);


            //str = Convert.ToBase64String(b);



            //LghTkEmsD2tbQ3fsIBRcBg==

            //Console.WriteLine(str);

            //OrderServiceBody body = new OrderServiceBody()
            //{
            //    orderid = "订单号",
            //    d_company = "到件方公司名称",
            //    d_contact = "到件方联系人",
            //    d_tel = "联系电话",
            //    d_address = "到件方地址",
            //    d_city = "shanghai",
            //    d_province = "shanghai",
            //};

            //RequestOrderBody order = new RequestOrderBody()
            //{
            //    logisticProviderID = "xxx",
            //    txLogisticID = "LP07082300225709",
            //    mailNo = "XXXXXXXX",
            //    sendStartTime = DateTime.Parse("2005-08-24 08:00:00"),
            //    sendEndTime = DateTime.Parse("2005-08-24 12:00:00"),
            //};
            //order.sender = new RequestOrderBody.Address()
            //{
            //    name = "name",
            //    postCode = "postCode",
            //    phone = "phone",
            //    mobile = "mobile",
            //    prov = "浙江",
            //    city = "杭州,西湖区",
            //    address = "华星科技大厦9层",
            //};

            //order.receiver = new RequestOrderBody.Address()
            //{
            //    name = "可人",
            //    postCode = "100000",
            //    phone = "231234134",
            //    mobile = "mobile",
            //    prov = "北京",
            //    city = "北京市",
            //    address = "华星科技大厦11层",
            //};

            //order.items = new List<RequestOrderBody.Item>();
            //order.items.Add(new RequestOrderBody.Item()
            //{
            //    itemName = "joi3",
            //    Number = 1,
            //    remark = "ASDFAS",
            //});

            //string s = order.Serializer();
            //Console.WriteLine(s);

            //OrderSearchServiceBody body = new OrderSearchServiceBody()
            //{
            //    orderid = "1234456",
            //};

            //string s = body.Serializer();
            //Console.WriteLine(s);

            //string s = body.Serializer();

            //            string e = "<Response service=\"OrderService\">"
            //  + "<Head>OK</Head>"
            //  + "<Body>"
            //   + " <OrderResponse orderid=\"订单号\" mailno=\"主单,子单 1,子单 2,…,子单 n\" origincode=\"原寄 地代码\" destcode=\"目的地代码\" filter_result=\"-1\" remark=\"备注\" return_tracking_no=\"123*********\"/>"
            //  + "</Body>"
            //+ "</Response>";

            //string e = "<Response service=\"OrderService\"> <Head>ERR</Head> <ERROR code=\"NNNN\">错误详细信息</ERROR> </Response> ";

            //var s = new OrderServiceResponse(e);

            //s.Deserialize();

            //var t = new RoutePushServiceResponse();
            //t.ErrID = new List<string>();

            //t.ErrID.Add("111111");
            //t.ErrID.Add("222222");
            //t.ErrID.Add("333333");
            //t.ErrID.Add("444444");

            //t.SuccessID = new List<string>();

            //t.SuccessID.Add("111");
            //t.SuccessID.Add("222");
            //t.SuccessID.Add("333");
            //t.SuccessID.Add("444");

            //var s = t.Serializer();

            //Console.WriteLine(s);

            Console.ReadLine();
        }

        static string LogPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="Content"></param>
        public static void WriteToFile(string FileName, string Content)
        {
            ReaderWriterLock rwl = new ReaderWriterLock();
            try
            {
                if (!System.IO.Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }

                rwl.AcquireWriterLock(Timeout.Infinite);

                using (StreamWriter sw = new StreamWriter(LogPath + FileName + ".txt", true, Encoding.Default))
                {
                    sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + Content);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (rwl.IsReaderLockHeld)
                    rwl.ReleaseWriterLock();
            }
        }

        public static string qfexpressService(string xml)
        {
            HttpWebRequest webRequest = null;
            Stream requestWriter = null;
            string responseData = "";

            //webRequest = System.Net.WebRequest.Create("http://localhost:18999/sf/RoutePush") as HttpWebRequest;
            //webRequest = System.Net.WebRequest.Create("http://newsyue.8866.org/sf/RoutePush") as HttpWebRequest;
            webRequest = System.Net.WebRequest.Create("http://api.landaojia.com/WebAPI/sf") as HttpWebRequest;


            webRequest.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            webRequest.Method = "POST";
            byte[] postData = Encoding.UTF8.GetBytes(xml);
            webRequest.ContentLength = postData.Length;

            requestWriter = webRequest.GetRequestStream();
            try
            {
                requestWriter.Write(postData, 0, postData.Length);
            }
            catch
            {
                throw;
            }
            finally
            {
                requestWriter.Close();
                requestWriter = null;
            }

            responseData = WebResponseGet(webRequest);
            webRequest = null;
            return responseData;
        }

        ///<summary> 
        /// 获得请求返回值 
        ///</summary> 
        ///<param name="webRequest">请求</param>
        private static string WebResponseGet(HttpWebRequest webRequest)
        {
            StreamReader responseReader = null;
            string responseData = "";

            try
            {
                responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                webRequest.GetResponse().GetResponseStream().Close();
                responseReader.Close();
                responseReader = null;
            }

            return responseData;
        }

        static void Test(int city, int mianzhi, int count)
        {
            StringBuilder sb = new StringBuilder();

            //加密类型
            int EncType = 1;

            ////序号
            //int Seq = 1;

            ////面值
            //int mianzhi = 2;

            ////城市
            //int city = 1;

            var now = DateTime.Today;

            //时间戳
            int cDate = now.Year + now.Month + now.Day;

            cDate %= 0xFF;

            //Random rd = new Random();

            for (int Seq = 1; Seq < count + 1; Seq++)
            {
                //数据戳       2                       1                       6
                string data = IntToHex(city, 2) + IntToHex(mianzhi) + IntToHex(Seq, 6);

                //var key = rd.Next(0, 0xFF);
                //                      1               1               1               2
                string cardno = IntToHex(EncType) + IntToHex(cDate) + verifyData(data) + "FF" + data;

                //Console.WriteLine(cardno);
                cardno = XorCard(cardno);
                Console.WriteLine(cardno);
                sb.AppendLine(cardno);
            }

        }

        //static int[] orkey = {
        //                    9684,
        //                    25641,
        //                    3363,
        //                    43618
        //                    };

        //key:传入卡号 value:加密后卡号
        static IDictionary<string, string> OffSetDictionary = new Dictionary<string, string>();

        static int Con_key = 123456;

        static void DictionaryCreate()
        {
            List<int> tmp = new List<int>();

            for (int i = 0; i < 0xFFFF; i++)
            {
                tmp.Add(i);
            }

            for (int i = 0; i < 0xFFFF; i++)
            {
                var key = IntToHex(i, 4);

                var value = tmp[Con_key % tmp.Count];

                tmp.Remove(value);

                Con_key = Math.Abs((int)((Con_key * Math.PI) % 1.0 * 1000000));

                OffSetDictionary.Add(key, IntToHex(value, 4));
            }
        }

        static string XorCard(string card)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 4; i++)
            {
                var tmp = card.Substring(i * 4, 4);

                tmp = OffSetDictionary[tmp];

                sb.Append(tmp);
            }

            return sb.ToString();

        }

        const int datakey = 3225;

        static string verifyData(string data)
        {
            int total = 0;

            foreach (var item in data.ToArray())
            {
                var temp = HexToInt(item.ToString());

                if (temp % 2 == 0)
                {
                    total += temp;
                }
                else
                {
                    total += temp * 2;
                }
            }
            total = total ^ datakey;
            total = total % 0xFF;

            return IntToHex(total);

        }

        static string IntToHex(int i)
        {
            return i.ToString("X");
        }

        static string IntToHex(int i, int width)
        {
            return i.ToString("X").PadLeft(width, '0');
        }

        static int HexToInt(string i)
        {
            return Convert.ToInt32(i, 16);
        }
    }
}
