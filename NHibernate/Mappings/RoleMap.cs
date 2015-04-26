using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using SimpleBlog.NHibernate.Entities;

namespace SimpleBlog.NHibernate.Mappings
{
    public class RoleMap : ClassMapping<Role>
    {
        public RoleMap()
        {
            Table("roles");

            Id(x => x.Id,
               x => x.Generator(Generators.Identity));

            Property(x => x.Name,
                     x => x.NotNullable(true));
        }
    }
}