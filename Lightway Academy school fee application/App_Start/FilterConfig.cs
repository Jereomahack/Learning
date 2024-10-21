using System.Web;
using System.Web.Mvc;

namespace Lightway_Academy_school_fee_application
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
