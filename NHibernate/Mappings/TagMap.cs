using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.NHibernate.Mappings
{
    public class TagMap : ClassMapping<Tag>
    {
        public TagMap()
        {
            Table("tags");

            Id(x => x.Id, x => x.Generator(Generators.Identity));

            Property(x => x.Slug, x => x.NotNullable(true));
            Property(x => x.Name, x => x.NotNullable(true));

            Bag(x => x.Posts,
                x =>
                {
                    x.Key(y => y.Column("tag_id"));
                    x.Table("post_tags");
                },
                x => x.ManyToMany(y => y.Column("post_id")));
        }
    }
}