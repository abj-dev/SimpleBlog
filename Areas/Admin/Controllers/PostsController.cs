using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;

using NHibernate.Linq;

using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.Infrastructure;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;
using SimpleBlog.Infrastructure.Extensions;
using SimpleBlog.Infrastructure.Authentication;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTab("Posts")]
    public class PostsController : Controller
    {
        private const int _postsPerPage = 5;

        // GET: Admin/Posts
        public ActionResult Index(int currentPage = 1)
        {
            var totalPostsCount = Database.Session.Query<Post>().Count();

            var baseQuery = Database.Session.Query<Post>().OrderByDescending(x => x.Id);

            var postIds = baseQuery
                .Skip((currentPage - 1) * _postsPerPage)
                .Take(_postsPerPage)
                .Select(p => p.Id)
                .ToArray();

            var currentPostsForPage = baseQuery
                .Where(p => postIds.Contains(p.Id))
                .FetchMany(f => f.Tags)
                .Fetch(f => f.PostingUser)
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
                IsNew = true,
                Tags = Database.Session.Query<Tag>().Select(tag => new TagCheckbox
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    IsChecked = false
                }).ToArray()
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
                Title = post.Title,
                Tags = Database.Session.Query<Tag>().Select(tag => new TagCheckbox
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    IsChecked = post.Tags.Contains(tag)
                }).ToArray()
            });
        }
        
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Form(PostsForm form)
        {
            form.IsNew = form.PostId == null;

            if (!ModelState.IsValid)
                return View(form);

            var selectedTags = ParseTagsFromForm(form.Tags).ToArray();

            Post post;
            if (form.IsNew)
            {
                post = new Post
                {
                    CreatedAt = DateTime.UtcNow,
                    PostingUser = SiteAuthManager.CurrentUser
                };

                foreach (var tag in selectedTags)
                {
                    post.Tags.Add((tag));
                }
            }
            else
            {
                post = Database.Session.Load<Post>(form.PostId);

                if(post == null)
                    return HttpNotFound();

                post.UpdatedAt = DateTime.UtcNow;

                foreach (var tagToAdd in selectedTags.Where(t => !post.Tags.Contains(t)))
                {
                    post.Tags.Add(tagToAdd);
                }

                foreach (var tagToRemove in post.Tags.Where(t => !selectedTags.Contains(t)).ToArray())
                {
                    post.Tags.Remove(tagToRemove);
                }
            }

            post.Title = form.Title;
            post.Slug = form.Slug;
            post.Content = form.Content;

            Database.Session.SaveOrUpdate(post);

            return RedirectToAction("Index");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Trash(int id, int currentPage)
        {
            var post = Database.Session.Load<Post>(id);

            if(post == null)
                return new HttpNotFoundResult();

            post.DeletedAt = DateTime.UtcNow;
            
            Database.Session.Update(post);

            return RedirectToAction("Index", new {CurrentPage = currentPage});
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Delete(int id, int currentPage)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                return new HttpNotFoundResult();

            Database.Session.Delete(post);

            return RedirectToAction("Index", new { CurrentPage = currentPage });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Restore(int id, int currentPage)
        {
            var post = Database.Session.Load<Post>(id);

            if (post == null)
                return new HttpNotFoundResult();

            post.DeletedAt = null;

            Database.Session.Update(post);

            return RedirectToAction("Index", new { CurrentPage = currentPage });
        }

        private IEnumerable<Tag> ParseTagsFromForm(IEnumerable<TagCheckbox> tags)
        {
            foreach (var tagInfo in tags.Where(t => t.IsChecked))
            {
                if (tagInfo.Id != null)
                {
                    yield return Database.Session.Load<Tag>(tagInfo.Id);
                    continue;
                }

                var existingTag = Database.Session.Query<Tag>().FirstOrDefault(t => t.Name == tagInfo.Name);

                if (existingTag != null)
                {
                    yield return existingTag;
                    continue;
                }

                var newTag = new Tag()
                {
                    Name = tagInfo.Name,
                    Slug = tagInfo.Name.Slugify()
                };

                Database.Session.Save(newTag);

                yield return newTag;
            }
        }
    }
}