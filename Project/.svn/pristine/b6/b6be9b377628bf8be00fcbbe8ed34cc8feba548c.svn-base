using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.LazyCard
{
    class Program
    {
        static void Main(string[] args)
        {
            //var c = LazyAtHome.Core.Helper.CryptoHelper.DESDecryptString("CA7D19C127877F4F");

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.Write(Convert10to62(i));
            //}
            //Console.WriteLine(c);
            //CardController cc = new CardController(CardType.LazyCard, CityCode.ShangHai, FaceValue.x100, 1, 400);

            //cc.Auto();

            //cc = new CardController(CardType.LazyCard, CityCode.ShangHai, FaceValue.x200, 1, 300);

            //cc.Auto();

            //cc = new CardController(CardType.LazyCard, CityCode.ShangHai, FaceValue.x500, 1, 200);

            //cc.Auto();

            //cc = new CardController(CardType.LazyCard, CityCode.ShangHai, FaceValue.x1000, 1, 100);

            //cc.Auto();

            //CardController cc = new CardController(CardType.LazyCard, CityCode.ShangHai, FaceValue.x0, 1, 3100);

            CardController cc = new CardController(CardType.LazyCard, CityCode.ShangHai, FaceValue.x50, 1, 5000);

            cc.Auto();

            Console.WriteLine("执行完成...");
            Console.ReadLine();
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
