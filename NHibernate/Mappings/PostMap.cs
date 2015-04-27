using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.NHibernate.Mappings
{
    public class PostMap : ClassMapping<Post>
    {
        public PostMap()
        {
            Table("posts");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            ManyToOne(x => x.PostingUser,
                x =>
                {
                    x.Column("user_id");
                    x.NotNullable(true);
                });

            Property(x => x.Title, x => x.NotNullable(true));
            Property(x => x.Slug, x => x.NotNullable(true));
            Property(x => x.Content, x => x.NotNullable(true));

            Property(x => x.CreatedAt,
                x =>
                {
                    x.Column("created_at");
                    x.NotNullable(true);
                });

            Property(x => x.UpdatedAt, x => x.Column("updated_at"));
            Property(x => x.DeletedAt, x => x.Column("deleted_at"));

            Bag(x => x.Tags,
                x =>
                {
                    x.Key(y => y.Column("post_id"));
                    x.Table("post_tags");
                },
                x => x.ManyToMany(y => y.Column("tag_id")));
        }
    }
}