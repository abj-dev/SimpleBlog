using System.Linq;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTab("Posts")]
    public class PostsController : Controller
    {
        // ReSharper disable once InconsistentNaming
        private const int _postsPerPage = 5;

        // GET: Admin/Posts
        public ActionResult Index(int currentPage = 1)
        {
            var totalPostsCount = Database.NHibernateSession.Query<Post>().Count();

            var currentPostsForPage = Database.NHibernateSession.Query<Post>()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((currentPage - 1) * _postsPerPage)
                .Take(_postsPerPage)
                .ToArray();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostsForPage, totalPostsCount, currentPage, _postsPerPage)
            });
        }
    }
}