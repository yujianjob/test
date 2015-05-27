using LazyAtHome.Core.Helper;
using LazyAtHome.Core.Infrastructure.WCF;
using LazyAtHome.WCF.OrderSystem.Contract.ClientProxy;
using LazyAtHome.WCF.OrderSystem.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.OrderSystem.Console_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            WCFInvokeHelper<OrderClient>.Invoke<ReturnEntity<Order_OrderDC>>
                 (client => client.Proxy.Order_Onekey_Create("wx9ab0f092bce2810D", 1, Contract.Enumerate.Channel.Weixin,
              new Contract.DataContract.Order_ConsigneeAddressDC()
              {
                  DistrictID = 310107,
                  Address = "详细地址",
                  Phone = "13524622579",
                  Type = 1,
                  ZipCode = "200000",
                  Consignee = "jk",
                  Email = "a@b.com",
                  Mpno = "13524622579",
              },
              new Contract.DataContract.Order_ConsigneeAddressDC()
              {
                  DistrictID = 310107,
                  Address = "详细地址",
                  Phone = "13524622579",
                  Type = 1,
                  ZipCode = "200000",
                  Consignee = "jk",
                  Email = "a@b.com",
                  Mpno = "13524622579",
              }, null
              ));


            Console.ReadLine();
        }
    }
}
