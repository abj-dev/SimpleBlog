using SimpleBlog.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate.Linq;
using SimpleBlog.Areas.Admin.ViewModels;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [SelectedTab("Users")]
    public class UsersController : Controller
    {
        // GET: Administrator/Users
        public ActionResult Index()
        {
            return View(new UsersIndex
            {
                Users = Database.NhSession.Query<User>().ToList()
            });
        }
    }
}