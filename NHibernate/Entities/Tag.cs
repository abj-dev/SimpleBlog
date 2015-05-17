using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SimpleBlog.NHibernate.Entities
{
    public class Tag
    {
        public virtual int Id { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Post> Posts { get; set; }

        public Tag()
        {
            Posts = new Collection<Post>();
        }
    }
}