using System.Web.Mvc;
using SimpleBlog.NHibernate;

namespace SimpleBlog.Infrastructure
{
    public class TransactionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Database.NHibernateSession.BeginTransaction();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
                Database.NHibernateSession.Transaction.Commit();
            else
                Database.NHibernateSession.Transaction.Rollback();
        }
    }
}