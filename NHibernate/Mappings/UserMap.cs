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
        }
    }
}