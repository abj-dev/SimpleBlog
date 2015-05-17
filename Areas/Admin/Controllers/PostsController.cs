using System;
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
            var totalPostsCount = Database.Session.Query<Post>().Count();

            var currentPostsForPage = Database.Session.Query<Post>()
                .OrderByDescending(x => x.CreatedAt)
                .Skip((currentPage - 1) * _postsPerPage)
                .Take(_postsPerPage)
                .ToArray();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(currentPostsForPage, totalPostsCount, currentPage, _postsPerPage)
            });
        }

        public ActionResult New()
        {
            return View("Form", new PostsForm
            {
                IsNew = true
            });
        }

        public ActionResult Edit(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if(post == null)
                return HttpNotFound();

            return View("Form", new PostsForm
            {
                IsNew = false,
                PostId = id,
                Content = post.Content,
                Slug = post.Slug,
                Title = post.Title
            });
        }
        
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    PostingUser = Auth.SiteAuth.CurrentUser
                };
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);

                if(post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if(post == null)
                return new HttpNotFoundResult();

            post.DeletedAt = DateTime.UtcNow;
            
            Database.Session.Update(post);

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                return new HttpNotFoundResult();

            Database.Session.Delete(post);

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                return new HttpNotFoundResult();

            post.DeletedAt = null;

            Database.Session.Update(post);

            return RedirectToAction("Index");
        }
    }
}