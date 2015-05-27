using System.Web;
using System.Web.Mvc;

namespace LazyAtHome.Web.StoreManage
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
