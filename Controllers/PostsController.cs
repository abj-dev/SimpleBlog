using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Infrastructure;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
    public class PostsController : Controller
    {
        private const Int32 PostsPerPage = 3;

        public ActionResult Index(int currentPage = 1)
        {
            var baseQuery = Database.Session.Query<Post>().Where(w => w.DeletedAt == null).OrderByDescending(o => o.Id);

            var totalPostCount =
                baseQuery.Count();

            var postIds =
                baseQuery.Skip((currentPage - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Select(s => s.Id)
                .ToArray();

            var posts =
                baseQuery.Where(w => postIds.Contains(w.Id))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.PostingUser)
                .ToList();

            return View(new PostsIndex
            {
                Posts = new PagedData<Post>(posts, totalPostCount, currentPage, PostsPerPage)
            });
        }

        public ActionResult Show(string idAndSlug)
        {
            var result = ExtractIdAndSlug(idAndSlug);
            if (result == null)
                return HttpNotFound();

            var post = Database.Session.Load<Post>(result.Id);
            if (post == null || post.IsDeleted)
                return HttpNotFound();

            if (!post.Slug.Equals(result.Slug, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Post", new { id = result.Id, slug = post.Slug });

            return View(new PostsShow
            {
                Post = post
            });
        }

        public ActionResult SelectTag(string idAndSlug, int currentPage = 1)
        {
            var result = ExtractIdAndSlug(idAndSlug);
            if (result == null)
                return HttpNotFound();

            var tag = Database.Session.Load<Tag>(result.Id);
            if (tag == null)
                return HttpNotFound();

            if (!tag.Slug.Equals(result.Slug, StringComparison.CurrentCultureIgnoreCase))
                return RedirectToRoutePermanent("Tag", new { id = result.Id, slug = tag.Slug });

            var totalPostCount = 
                tag.Posts.Count();

            var postIds =
                tag.Posts.OrderByDescending(o => o.Id)
                .Skip((currentPage - 1) * PostsPerPage)
                .Take(PostsPerPage)
                .Where(t => t.DeletedAt == null)
                .Select(t => t.Id)
                .ToArray();

            var posts =
                Database.Session.Query<Post>()
                .OrderByDescending(p => p.CreatedAt)
                .Where(p => postIds.Contains(p.Id))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.PostingUser)
                .ToList();

            return View(new PostsTag
            {
                Tag = tag,
                Posts = new PagedData<Post>(posts, totalPostCount, currentPage, PostsPerPage)
            });
        }

        private Id_Slug_Result ExtractIdAndSlug(string idAndSlug)
        {
            var matches = Regex.Match(idAndSlug, @"^(\d+)\-(.*)?$");
            if (!matches.Success)
                return null;

            var id = int.Parse(matches.Result("$1"));
            var slug = matches.Result("$2");

            return new Id_Slug_Result(id, slug);
        }
    }
}