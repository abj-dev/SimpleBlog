using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Infrastructure.Authentication;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
    public class LayoutController : Controller
    {
        [ChildActionOnly]
        public ActionResult Sidebar()
        {
            return View(new LayoutSidebar
            {
                IsLoggedIn =  SiteAuthManager.CurrentUser != null,
                Username = SiteAuthManager.CurrentUser != null ? SiteAuthManager.CurrentUser.Username : "",
                IsAdminRole = User.IsInRole("Admin"),
                Tags = Database.Session.Query<Tag>()
                .Select(tag => new
                {
                    tag.Id,
                    tag.Name,
                    tag.Slug,
                    PostCount = tag.Posts.Count
                })
                .Where(t => t.PostCount > 0)
                .OrderByDescending(p => p.PostCount)
                .Select(tag => new SidebarTag (tag.Id, tag.Name, tag.Slug, tag.PostCount))
                .ToList()
            });
        }
    }
}