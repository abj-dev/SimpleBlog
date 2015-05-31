using System.Web;
using System.Web.Mvc;
using SimpleBlog.Infrastructure;

namespace SimpleBlog
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new TransactionFilter());

            // Remove the below line if handling errors manually.
            // filters.Add(new HandleErrorAttribute());
        }
    }
}
