using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.WCF.Common.LazyCard
{
    public class CardController
    {
        IList<CardDC> CardList = null;

        CardType CardType;

        CityCode CityCode;

        FaceValue FaceValue;

        int Count;

        int StartNo;

        public CardController(CardType iCardType, CityCode iCityCode, FaceValue iFaceValue, int iStartNo, int iCount)
        {
            this.CardType = iCardType;
            this.CityCode = iCityCode;
            this.FaceValue = iFaceValue;
            this.StartNo = iStartNo;
            this.Count = iCount;
        }

        public void CreateCard()
        {
            CardList = new List<CardDC>(this.Count);

            for (int i = StartNo; i < StartNo + Count; i++)
            {
                var entity = new CardDC(this.CardType, this.CityCode, this.FaceValue, i);

                CardList.Add(entity);
            }
            Console.WriteLine("生成完成...");
        }

        public void SaveToFile()
        {
            if (CardList == null)
            {
                return;
            }
            string _FileName = ((int)CardType).ToString() + "_"
                            + ((int)CityCode).ToString().PadLeft(2, '0') + "_"
                            + FaceValue.ToString().TrimStart('x') + "_"
                            + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            StringBuilder sb = new StringBuilder();

            foreach (var item in CardList)
            {
                sb.AppendLine(item.ToString());
            }

            FileController.WriteToFile(_FileName, sb.ToString());

        }

        public void Auto()
        {
            this.CreateCard();
            this.SaveToFile();
        }
    }
}
