﻿using System.Linq;
using System.Web;
using NHibernate.Linq;
using SimpleBlog.Infrastructure.Constants;
using SimpleBlog.NHibernate;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Infrastructure.Authentication
{
    public static class SiteAuthManager
    {
        // Properties
        public static User CurrentUser
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;

                var user = HttpContext.Current.Items[SiteConstants.UserKey] as User;

                if (user == null)
                {
                    user =
                        Database.Session.Query<User>()
                            .FirstOrDefault(u => u.Username == HttpContext.Current.User.Identity.Name);

                    if (user == null)
                        return null;

                    HttpContext.Current.Items[SiteConstants.UserKey] = user;
                }

                return user;
            }
        }
    }
}