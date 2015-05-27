using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.LazyCard
{
    public class CardDC
    {
        static DateTime dt = DateTime.Now;

        private static Random rd = new Random(dt.GetHashCode());

        CardType CardType;

        CityCode CityCode;

        FaceValue FaceValue;

        int No;

        string Password;

        string EnPassword;

        public CardDC(CardType iCardType, CityCode iCityCode, FaceValue iFaceValue, int iNo)
        {
            this.CardType = iCardType;
            this.CityCode = iCityCode;
            this.FaceValue = iFaceValue;
            this.No = iNo;

            #region 16位编码

            //this.Password = IntToHex(rd.Next(0, 65535), 4)
            //    + IntToHex(rd.Next(0, 65535), 4)
            //    + IntToHex(rd.Next(0, 65535), 4)
            //    + IntToHex(rd.Next(0, 65535), 4);

            #endregion

            #region 6位编码

            //6位编码
            //var tm1 = Convert10to36(rd.Next(0, 36));
            //var tm2 = Convert10to36(rd.Next(0, 36));
            //var tm3 = Convert10to36(rd.Next(0, 36));
            //var tm4 = Convert10to36(rd.Next(0, 36));
            //var tm5 = Convert10to36(rd.Next(0, 36));
            //var tm6 = Convert10to36(rd.Next(0, 36));

            //this.Password = tm1 + tm2 + tm3 + tm4 + tm5 + tm6;

            var tm1 = Convert10to36(rd.Next(0, 36));
            var tm2 = Convert10to36(rd.Next(0, 36));
            var tm3 = Convert10to36(rd.Next(0, 36));
            var tm4 = Convert10to36(rd.Next(0, 36));
            var tm5 = Convert10to36(rd.Next(0, 36));
            var tm6 = Convert10to36(rd.Next(0, 36));
            var tm7 = Convert10to36(rd.Next(0, 36));
            var tm8 = Convert10to36(rd.Next(0, 36));

            //this.Password = "RH" + tm1 + tm2 + tm3 + tm4;// +tm5; +tm6;
            this.Password = "RH" + tm1 + tm2 + tm3 + tm4 + tm5 + tm6 + tm7 + tm8;

            #endregion

            #region 62进制

            //for (int i = 0; i < 8; i++)
            //{
            //    this.Password += Convert10to62(rd.Next(0, 62));
            //}

            #endregion

            //this.Password = Convert10to36(rd.Next(0, 36))
            //    + Convert10to36(rd.Next(0, 36))
            //    + Convert10to36(rd.Next(0, 36))
            //    + Convert10to36(rd.Next(0, 36))
            //    + Convert10to36(rd.Next(0, 36))
            //    + Convert10to36(rd.Next(0, 36));

            //if (this.Password.Length != 6)
            //{
            //    Console.Write("11");
            //}

            this.EnPassword = LazyAtHome.Core.Helper.CryptoHelper.DESEncryptString(this.Password);
        }

        /// <summary>
        /// 生成方式:
        /// Type:1     Type(1位) + 城市代码(2位) + 面值(2位) + 序号(7位)
        /// </summary>
        string CardNumber
        {
            get
            {
                switch (this.CardType)
                {
                    case LazyCard.CardType.LazyCard:
                        return ((int)CardType).ToString()
                            + ((int)CityCode).ToString().PadLeft(2, '0')
                            + ((int)FaceValue).ToString().PadLeft(2, '0')
                            + No.ToString().PadLeft(7, '0');
                    default:
                        throw new Exception("生成卡号不支持的类型");
                }
            }
        }

        static string IntToHex(int i, int width)
        {
            return i.ToString("X").PadLeft(width, '0');
        }

        private static string Convert10to36(long value)
        {
            if (value == 0)
                return "0";
            var rtn = string.Empty;
            while (value != 0)
            {
                var mo = value % 36;
                if (mo >= 0 && mo <= 9)
                {
                    rtn = (char)(mo + 48) + rtn;
                }
                else if (mo >= 10 && mo <= 35)
                {
                    rtn = (char)(mo + 55) + rtn;
                }
                value = value / 36;
            }
            rtn = rtn.Replace('O', '0');
            rtn = rtn.Replace('I', '1');

            return rtn;
        }

        public override string ToString()
        {
            var rtn = (int)CardType + "\t"
                + (int)CityCode + "\t"
                + FaceValue.ToString().TrimStart('x') + "\t"
                + CardNumber + "\t"
                + Password + "\t";

            if (Password.Length == 16)
            {
                rtn += Password.Substring(0, 4) + "-"
                    + Password.Substring(4, 4) + "-"
                    + Password.Substring(8, 4) + "-"
                    + Password.Substring(12, 4);
            }
            else
            {
                rtn += Password;
            }

            rtn += "\t"
             + EnPassword;

            return rtn;
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
