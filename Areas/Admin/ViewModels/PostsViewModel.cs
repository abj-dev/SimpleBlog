
using SimpleBlog.Infrastructure;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.Areas.Admin.ViewModels
{
    public class PostsIndex
    {
        public PagedData<Post> Posts { get; set; }
    }
}