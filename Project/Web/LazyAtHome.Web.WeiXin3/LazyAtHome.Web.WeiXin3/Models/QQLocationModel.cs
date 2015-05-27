using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LazyAtHome.Web.WeiXin3.Models
{
    public class QQLocationEntity
    {
        public int status { get; set; }
        public string message { get; set; }

        public ResultEntity result { get; set; }
    }

    public class ResultEntity
    {
        public LocationEntity location { get; set; }
        public string address { get; set; }
        public FormattedAddressesEntity formatted_addresses { get; set; }
        public AddressComponentEntity address_component { get; set; }
        public AddressReferenceEntity address_reference { get; set; }
    }

    public class AddressComponentEntity
    {
        public string newss { get; set; }
        public string nation { get; set; }
        public string province { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street { get; set; }
        public string street_number { get; set; }
    }

    public class LocationEntity
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }

    public class FormattedAddressesEntity
    {
        public string recommend { get; set; }
        public string rough { get; set; }
    }

    public class AddressReferenceEntity
    {
        public Landmarkl2Entity landmark_l2 { get; set; }
    }

    public class Landmarkl2Entity
    {
        public string title { get; set; }
    }
}
