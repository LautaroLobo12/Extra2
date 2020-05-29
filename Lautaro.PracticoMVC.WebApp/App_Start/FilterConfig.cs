using System.Web;
using System.Web.Mvc;

namespace Lautaro.PracticoMVC.WebApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
