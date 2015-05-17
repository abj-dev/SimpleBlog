using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace SimpleBlog.Infrastructure.Extensions
{
    internal static class StringExtensions
    {
        public static string Slugify(this string rawSlug)
        {
            var slugBeenProcessed = rawSlug;

            slugBeenProcessed = Regex.Replace(slugBeenProcessed, @"[^a-zA-Z0-9\s]", "");

            slugBeenProcessed = slugBeenProcessed.ToLower();

            var cleanSlug = Regex.Replace(slugBeenProcessed, @"\s", "-");

            return cleanSlug;
        }
    }
}