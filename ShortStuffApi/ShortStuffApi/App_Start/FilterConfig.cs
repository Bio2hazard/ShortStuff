// ShortStuffApi
// FilterConfig.cs

using System.Web.Mvc;

namespace ShortStuffApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
