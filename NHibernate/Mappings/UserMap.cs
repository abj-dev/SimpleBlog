using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.NHibernate.Mappings
{
    public class UserMap : ClassMapping<User>
    {
        public UserMap()
        {
            Table("users");

            Id(x => x.Id,
               x => x.Generator(Generators.Identity));

            Property(x => x.Username,
                     x => x.NotNullable(true));

            Property(x => x.Email,
                     x => x.NotNullable(true));

            Property(x => x.PasswordHash,
                     x =>
                     {
                         x.Column("password_hash");
                         x.NotNullable(true);
                     });

            Bag(x => x.Roles,
                x =>
                {
                    x.Table("role_users");
                    x.Key(k => k.Column("user_id"));
                },
                x => x.ManyToMany(k => k.Column("role_id")));
        }
    }
}