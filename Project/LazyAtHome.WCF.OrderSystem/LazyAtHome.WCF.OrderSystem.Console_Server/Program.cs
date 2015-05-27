using LazyAtHome.WCF.OrderSystem.Business.Portal;
using LazyAtHome.WCF.OrderSystem.Contract.Enumerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Console_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "OrderService";

            //OrderSystem.Business.Business.Order.Instance.Order_Order_Finish("115019690000015");

            //var rtn1 = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString("50ADFA7028EA2873");
            //var rtn2 = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString("B2FF927DE1B44C3D");

            //Console.WriteLine(rtn1 + rtn2);


            //return;
            //OrderSystem.Business.Business.Order.Instance.Order_Order_Pay("114351660000254", 4.7m, PayMoneyType.AliPay,
            //    PayMoneyChannel.Weixin, DateTime.Now, "2014122954545407");
            //return;
            //OrderSystem.Business.Business.Order.Instance.Order_Factory_AddProduct(
            //    8728, new List<LazyAtHome.WCF.OrderSystem.Contract.DataContract.Order_ProductDC>()
            //{
            //   new LazyAtHome.WCF.OrderSystem.Contract.DataContract.Order_ProductDC(){ ProductID = 1,Type = 1,Name="xz",Code ="124141212" },
            //  new LazyAtHome.WCF.OrderSystem.Contract.DataContract.Order_ProductDC(){ ProductID = 2,Type =1, Name="xz",Code ="124141211" },
            //}, -1);

            //Regex r = new Regex(@"^\d{6}$");
            //Console.WriteLine("123456".Substring(3, 3));

            //Console.WriteLine(r.IsMatch("123"));
            //Console.WriteLine(r.IsMatch("532426"));
            //Console.WriteLine(r.IsMatch("5324326?"));
            //Console.WriteLine(r.IsMatch("5324326a"));
            //Console.WriteLine(r.IsMatch("5324326a"));
            //System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^1\\d{10}$", System.Text.RegularExpressions.RegexOptions.Compiled);

            //Console.WriteLine(r.IsMatch("13d11111111"));
            //Console.WriteLine(r.IsMatch("13311111111"));
            //Console.WriteLine(r.IsMatch(""));


            //OrderSystem.Business.Business.Order.Instance.Order_Order_Cancel(1032, OrderStatus.UserChannel);
            //OrderSystem.Business.Business.Order.Instance.Order_Order_Finish("114255000000004");
            //OrderSystem.Business.Business.Order.Instance.Order_Order_Merger(4075, 4076);
            //var rtn = OrderSystem.Business.Business.Order.Instance.Order_Order_SELECT_Entity("114156090000018", true, true, true, true, true, true);


            //var rtn = OrderSystem.Business.Business.Order.Instance.Order_CustomerService_Onekey_Create(
            //    new Guid("1fb5a3e6-8007-4a3e-9695-7a607799d888"),
            //    new Contract.DataContract.Order_ConsigneeAddressDC()
            //       {
            //           DistrictID = 310107,
            //           Address = "详细地址",
            //           Phone = "13524622579",
            //           Type = 1,
            //           ZipCode = "200000",
            //           Consignee = "jk",
            //           Email = "a@b.com",
            //           Mpno = "13524622579",
            //       },
            //       new Contract.DataContract.Order_ConsigneeAddressDC()
            //       {
            //           DistrictID = 310107,
            //           Address = "详细地址",
            //           Phone = "13524622579",
            //           Type = 1,
            //           ZipCode = "200000",
            //           Consignee = "jk",
            //           Email = "a@b.com",
            //           Mpno = "13524622579",
            //       }, DateTime.Now, null, null, -1, null, null, null

            //    );

            //var rtn = OrderSystem.Business.Business.Order.Instance.Order_Web_Create(
            //    new Guid("1fb5a3e6-8007-4a3e-9695-7a607799d888"), 1,
            //    new List<int>() { 22 }, 12, 0
            //     , 18,
            //     null,
            //    //new Dictionary<int, decimal>() //懒人卡
            //    //{
            //    //    {2006,65}
            //    //},
            //new Contract.DataContract.Order_ConsigneeAddressDC()
            //       {
            //           DistrictID = 310107,
            //           Address = "详细地址",
            //           Phone = "13524622579",
            //           Type = 1,
            //           ZipCode = "200000",
            //           Consignee = "jk",
            //           Email = "a@b.com",
            //           Mpno = "13524622579",
            //       },
            //new Contract.DataContract.Order_ConsigneeAddressDC()
            //       {
            //           DistrictID = 310107,
            //           Address = "详细地址",
            //           Phone = "13524622579",
            //           Type = 1,
            //           ZipCode = "200000",
            //           Consignee = "jk",
            //           Email = "a@b.com",
            //           Mpno = "13524622579",
            //       }, 0,
            //       null,
            //    //new Dictionary<string, decimal>() { 
            //    //    //测试优惠
            //    //{ "活动优惠", 20m } },
            //       null,
            //    //new List<int>() { 8 }, 
            //        true);

            //var rtn = OrderSystem.Business.Business.Order.Instance.Order_Web_Create_Activity(
            //    new Guid("AC4D613D-5099-4E2F-8BD7-814FAE11F5EB"), 1, new List<int>() { 
            //     { 52 },
            //     { 53},
            //     { 56 },
            //     { 42 },
            //     { 41  },
            //     { 39  },
            //     { 58  },
            //     { 72 },
            //     { 54  },
            //     }, 8, 8
            //     , 30, new Dictionary<int, decimal>() { { 2003, 100m } },
            //new Contract.DataContract.Order_ConsigneeAddressDC()
            //       {
            //           DistrictID = 310107,
            //           Address = "详细地址",
            //           Phone = "13524622579",
            //           Type = 1,
            //           ZipCode = "200000",
            //           Consignee = "jk",
            //           Email = "a@b.com",
            //           Mpno = "13524622579",
            //       },
            //new Contract.DataContract.Order_ConsigneeAddressDC()
            //       {
            //           DistrictID = 310107,
            //           Address = "详细地址",
            //           Phone = "13524622579",
            //           Type = 1,
            //           ZipCode = "200000",
            //           Consignee = "jk",
            //           Email = "a@b.com",
            //           Mpno = "13524622579",
            //       }, 7, "123", 30);
            //var rtn = OrderSystem.Business.Business.Order.Instance.Order_Mall_Web_Create(new Guid("6F2BDC73-D948-43B4-8165-FDEA517CC44F")
            //     , 1, new Dictionary<int, int>() { { 1, 2 } }, 8, -8
            //     , new Contract.DataContract.Order_ConsigneeAddressDC()
            //       {
            //           DistrictID = 310107,
            //           Address = "详细地址",
            //           Phone = "13524622579",
            //           Type = 1,
            //           ZipCode = "200000",
            //           Consignee = "jk",
            //           Email = "a@b.com",
            //           Mpno = "13524622579",
            //       }
            //       , 200);

            //Console.WriteLine(rtn);

            //var rtn1 = OrderSystem.Business.Business.Order.Instance.Order_Order_Pay(rtn.OBJ.OrderNumber, 200, PayMoneyType.AliPay, PayMoneyChannel.Web, DateTime.Now, "1234");

            //Console.WriteLine(rtn1);

            //OrderSystem.Business.Business.Order.Instance.Order_Product_SELECT_List_Store(
            //    new Guid("2d1c2ec0-533d-49f2-ad0d-f27e94a0b9c6"), "123", null, null, DateTime.Parse("2014-05-14"), DateTime.Now);
            //Console.WriteLine("开启相关服务:");
            //OrderSystem.Business.Business.Order.Instance.Order_Order_Return(1, new List<int>() { 2 });
            //var rtn = OrderSystem.Business.Business.Order.Instance.Order_Weixin_Create("wx9ab0f092bce2810D", 1,
            //      new List<int>() { 1 }, 8, 8, 20,
            //      null, new Contract.DataContract.Order_ConsigneeAddressDC()
            //      {
            //          DistrictID = 310107,
            //          Address = "详细地址",
            //          Phone = "13524622579",
            //          Type = 1,
            //          ZipCode = "200000",
            //          Consignee = "jk",
            //          Email = "a@b.com",
            //          Mpno = "13524622579",
            //      }, new Contract.DataContract.Order_ConsigneeAddressDC()
            //      {
            //          DistrictID = 310107,
            //          Address = "详细地址",
            //          Phone = "13524622579",
            //          Type = 1,
            //          ZipCode = "200000",
            //          Consignee = "jk",
            //          Email = "a@b.com",
            //          Mpno = "13524622579",
            //      }, 20
            //      );

            //var rtn = OrderSystem.Business.Business.Order.Instance.Order_Weixin_Create("testUserOpenID_444444", 1,
            //      new List<int>() { 1 }, 0, 0,0,
            //      null,
            //      //new Dictionary<int, decimal>() { { 2, 10 } }, 
            //      new Contract.DataContract.Order_ConsigneeAddressDC()
            //      {
            //          DistrictID = 310107,
            //          Address = "详细地址",
            //          Phone = "13524622579",
            //          Type = 1,
            //          ZipCode = "200000",
            //          Consignee = "jk",
            //          Email = "a@b.com",
            //          Mpno = "13524622579",
            //      }, new Contract.DataContract.Order_ConsigneeAddressDC()
            //      {
            //          DistrictID = 310107,
            //          Address = "详细地址",
            //          Phone = "13524622579",
            //          Type = 1,
            //          ZipCode = "200000",
            //          Consignee = "jk",
            //          Email = "a@b.com",
            //          Mpno = "13524622579",
            //      }, 0, null, null, true, null, "RH0WH7"
            //      );

            //OrderSystem.Business.Business.Order.Instance.Order_Order_Pay(
            //    rtn.OBJ.OrderNumber, 2, Contract.Enumerate.PayMoneyType.AliPay, Contract.Enumerate.PayMoneyChannel.Weixin, DateTime.Now, "123123123"
            //    );

            //OrderSystem.Business.Business.Order.Instance.Order_Onekey_Create(new Guid("CB7500E5-BD88-4D03-9A73-87ECD8E0B958"), 1, Contract.Enumerate.Channel.Weixin,
            //    new Contract.DataContract.Order_ConsigneeAddressDC()
            //    {
            //        DistrictID = 310107,
            //        Address = "详细地址",
            //        Phone = "13524622579",
            //        Type = 1,
            //        ZipCode = "200000",
            //        Consignee = "jk",
            //        Email = "a@b.com",
            //        Mpno = "13524622579",
            //    },
            //    new Contract.DataContract.Order_ConsigneeAddressDC()
            //    {
            //        DistrictID = 310107,
            //        Address = "详细地址",
            //        Phone = "13524622579",
            //        Type = 1,
            //        ZipCode = "200000",
            //        Consignee = "jk",
            //        Email = "a@b.com",
            //        Mpno = "13524622579",
            //    }, null
            //    );

            ServiceHost serviceHost = new ServiceHost(typeof(OrderPortal));
            serviceHost.Opened += serviceHost_Opened;
            serviceHost.Open();

            Console.WriteLine("开启相关服务完成");
            Console.ReadLine();
        }
        static void serviceHost_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("\t已经开启OrderPortal服务..");
        }
    }
}
