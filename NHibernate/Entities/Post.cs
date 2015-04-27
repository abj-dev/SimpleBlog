using System;
using System.Collections.Generic;

namespace SimpleBlog.NHibernate.Entities
{
    public class Post
    {
        public virtual int Id { get; set; }
        public virtual User PostingUser { get; set; }
        public virtual string Title { get; set; }
        public virtual string Slug { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual DateTime? DeletedAt { get; set; }
        public virtual IList<Tag> Tags { get; set; }

        public virtual bool IsDeleted
        {
            get { return DeletedAt != null; }
        }
    }
}