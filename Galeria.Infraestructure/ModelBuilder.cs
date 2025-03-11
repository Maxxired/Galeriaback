using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;


namespace Galeria.Infraestructure
{
    public static class ModelBuilderExtensions
    {
        public static void RegisterAllEntities<EntityBase>(this ModelBuilder modelBuilder, params Assembly[] assemblies)
        {
            IEnumerable<Type> types = assemblies.SelectMany(a => a.GetExportedTypes()).Where(c => c.IsClass && !c.IsAbstract && c.IsPublic &&
              typeof(EntityBase).IsAssignableFrom(c));
            foreach (Type type in types)
            {
                if (type.GetProperties().Count(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute))) > 1)
                {
                    var orderedKeys = type
        .GetProperties()
        .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(KeyAttribute)))
        .OrderBy(p =>
            p.CustomAttributes.Single(x => x.AttributeType == typeof(ColumnAttribute))?
                .NamedArguments?.Single(y => y.MemberName == nameof(ColumnAttribute.Order))
                .TypedValue.Value ?? 0)
        .Select(x => x.Name)
        .ToArray();

                    modelBuilder.Entity(type).HasKey(orderedKeys);
                }
                else
                {
                    modelBuilder.Entity(type);
                }


            }

        }
    }
}
