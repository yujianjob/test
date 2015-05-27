using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Library.Partners.BaiduMap.Entity
{
    public class GeocoderReturn : BaseReturn
    {
        public Geocoder result { get; set; }

        public class Geocoder
        {
            public Location location { get; set; }

            /// <summary>
            /// 位置的附加信息，是否精确查找。1为精确查找，0为不精确。
            /// </summary>
            public int precise { get; set; }

            /// <summary>
            /// 可信度
            /// </summary>
            public int confidence { get; set; }

            /// <summary>
            /// 地址类型
            /// </summary>
            public string level { get; set; }


            /// <summary>
            /// 根据经纬度坐标获取地址
            /// </summary>
            public class Location
            {
                /// <summary>
                /// 经度
                /// </summary>
                public decimal lng { get; set; }

                /// <summary>
                /// 纬度
                /// </summary>
                public decimal lat { get; set; }
            }
        }
    }
}
