using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.WebService.Console_Client
{
    class Program
    {
        static void Main(string[] args)
        {

            //PRService.IPR prs = new PRService.PRClient();

            //var entity = prs.PR_Operator_Login("admin", "1234", PRService.OperatorType.Manage);
            //Console.WriteLine(entity.OBJ.LoginName);

            //WashProductService.IWashProduct wr = new WashProductService.WashProductClient();

            //var entity = wr.Wash_Product_SELECT_List(new WashProductService.LoginCredentials(){
            // LoginName = "yyf",
            //  Password= "123456",
            //   OperatorType = WashProductService.OperatorType.Factory,
            //});

            WashOrderService.IWashOrder wr = new WashOrderService.WashOrderClient();

            var entity = wr.Order_Order_SELECT_Entity_Express(new WashOrderService.LoginCredentials()
            {
                LoginName = "yyf",
                Password = "123456",
                OperatorType = WashOrderService.OperatorType.Factory,
            }, "114125540000002", true, false, false, false, false, false);

            Console.WriteLine(entity);

            Console.ReadLine();








        }
    }
}
